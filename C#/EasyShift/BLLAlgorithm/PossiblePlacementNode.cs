using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using BLL;

namespace BLLAlgorithm
{
    /// <summary>
    /// This class is a tree node of the combination options to set the team of the shift.
    /// </summary>
    public class PossiblePlacementNode
    {
        public static ClassDB ClassDB = new ClassDB();
        public const int NUM_CHILDREN_TO_CONTINUE = 5;

        /// <summary>
        /// This current node in this level is combination option for shift & job
        /// </summary>
        public ArrrayCombEmployees Node { get; set; }
        public List<PossiblePlacementNode> MyChildren { get; set; }
        public PossiblePlacementNode Parent { get; set; }
        public ShiftPerJob ShiftIBelong { get; set; }
        public double SumPriorityPerNode { get; set; }//סכום העדיפות של העובדים בצומת הזה
                                                      //לא כ"כ השתמשתי אלא בעץ שמתחלק

        public double AvgSeniorityYearsPerNode { get; set; }

        //הוספות לצורך התכנון דינמי
        public static PossiblePlacementNode[,] MatBestPaths { get; set; }
        public double PathScoreWithMe { get; set; }//ציון המסלול עד הצומת הנוכחי כולל חישוב הצומת הנוכחי

        public int[] ArrNumAppearsForEveryEmp { get; set; }//מספר ההופעות של כל עובד במסלול עד הצומת הנוכחי
        
        public bool IsVisit { get; set; }
        public static List<int> sI = new List<int>();
        //בונה סטטית בשביל אתחול המטריצה
        //static PossiblePlacementNode()
        //{
        //    MatBestPaths=new 
        //}
        public PossiblePlacementNode()
        {
            AvgSeniorityYearsPerNode = 0;
            IsVisit = false;
        }
        public void InitMatBestPaths(int numShifts)
        {
            MatBestPaths = new PossiblePlacementNode[NUM_CHILDREN_TO_CONTINUE, numShifts];
            for (int i = 0; i < NUM_CHILDREN_TO_CONTINUE; i++)
            {
                for (int j = 0; j < numShifts; j++)
                {
                    MatBestPaths[i,j] = new PossiblePlacementNode();
                }
            }
        }
        public List<PossiblePlacementNode> ContinueMax(Queue<PossiblePlacementNode> queue, List<ShiftPerJob> lstShiftPerJobAllShifts)
        {
            PossiblePlacementNode p;
            //List<PossiblePlacementNode> nodesThisLevel;
            int[] indexesHighestPaths = new int[NUM_CHILDREN_TO_CONTINUE];
            List<PossiblePlacementNode>[] placementOptPerShift = new List<PossiblePlacementNode>[21];
            for (int j = 0; j < placementOptPerShift.Length; j++)
                placementOptPerShift[j] = new List<PossiblePlacementNode>();
            int i = 0;
            while (queue.Count() > 0)
            {
                p = queue.Dequeue();

                if (p.ShiftIBelong == null)
                {
                    List<EmployeeToPlacement> suitableEmployees = SuitableEmployeesToSpecificLevel(p, lstShiftPerJobAllShifts[i].AllSuitableEmployees);
                    List<EmployeeToPlacement> orderSuitableEmployees = suitableEmployees.OrderBy(x => x.Employee_Request.priority_id).Reverse().ToList();
                    List<EmployeeToPlacement> partSuitableEmployees = new List<EmployeeToPlacement>();
                    for (int j = 0; j < 6; j++)
                    {
                        partSuitableEmployees.Add(orderSuitableEmployees[j]);
                    }
                    //lstShiftPerJobAllShifts[i].AllSuitableEmployees = partSuitableEmployees;
                    p.InitializeNodes(p, partSuitableEmployees, lstShiftPerJobAllShifts[i], placementOptPerShift[i]);
                    indexesHighestPaths = HighestValues(placementOptPerShift[i]);
                    EnqueueHighestValues(placementOptPerShift[i], indexesHighestPaths, queue);
                    i++;
                }
                else
                {
                    int z;
                    if (p.ShiftIBelong.ShiftId == 2)
                        z = 4;
                    if (i < 21)
                    {
                        List<EmployeeToPlacement> temp = lstShiftPerJobAllShifts[i].AllSuitableEmployees;
                        List<EmployeeToPlacement> suitableEmployees = SuitableEmployeesToSpecificLevel(p, lstShiftPerJobAllShifts[i].AllSuitableEmployees);
                        List<EmployeeToPlacement> orderSuitableEmployees = suitableEmployees.OrderBy(x => x.Employee_Request.priority_id).Reverse().ToList();
                        List<EmployeeToPlacement> partSuitableEmployees = new List<EmployeeToPlacement>();
                        for (int j = 0; j < 6; j++)
                        {
                            partSuitableEmployees.Add(orderSuitableEmployees[j]);
                        }
                        //lstShiftPerJobAllShifts[i].AllSuitableEmployees = partSuitableEmployees;
                        p.InitializeNodes(p, partSuitableEmployees, lstShiftPerJobAllShifts[i], placementOptPerShift[i]);
                        if (queue.Count() == 0)
                        {
                            indexesHighestPaths = HighestValues(placementOptPerShift[i]);
                            EnqueueHighestValues(placementOptPerShift[i], indexesHighestPaths, queue);
                            i++;
                        }
                    }
                }
            }
            return BestPlacement(placementOptPerShift[20], indexesHighestPaths);
        }

        /// <summary>
        /// Pushes the leafs of the highest paths to the queue.
        /// </summary>
        /// <param name="children"></param>
        /// <param name="indexesHighestPaths"></param>
        /// <param name="queue"></param>           
        public void EnqueueHighestValues(List<PossiblePlacementNode> children, int[] indexesHighestPaths, Queue<PossiblePlacementNode> queue)
        {
            for (int i = 0; i < indexesHighestPaths.Length; i++)
                queue.Enqueue(children[indexesHighestPaths[i]]);
        }
        //הפונקציה מקבלת רשימה של ילדים ואת האינדקסים של המסלולים הכי טובים ומחזירה את המסלול הכי טוב בצורה של רשימה שמהווה שיבוץ
        public List<PossiblePlacementNode> BestPlacement(List<PossiblePlacementNode> children, int[] indexesHighestPaths)
        {
            List<PossiblePlacementNode> bestPlacement = new List<PossiblePlacementNode>();
            for (int i = 0; i < indexesHighestPaths.Length; i++)
                bestPlacement.Add(children[indexesHighestPaths[i]]);
            PossiblePlacementNode best = HighestPath(bestPlacement);
            List<PossiblePlacementNode> bestPath = new List<PossiblePlacementNode>();
            GetPath(best, bestPath);
            return bestPath;
        }

        public PossiblePlacementNode HighestPath(List<PossiblePlacementNode> optPlacement)
        {
            List<double> scores = ScoresPaths(optPlacement);//all the scores of paths
            return optPlacement[RetAndRemoveMaxIndex(scores)];
        }

        ///מאתחל צומת בעץ ובונה לו ילדים
        public void InitializeNodes(PossiblePlacementNode node, List<EmployeeToPlacement> suitableEmployees,ShiftPerJob shift, List<PossiblePlacementNode> nodesThisLevel)
        {
            //sI.Add(i);
            //if (i >= lstShiftPerJobAllShifts.Count())
            //    return;
            //Build level for the i shift.
            //List<EmployeeToPlacement> lstOptionsEmployees = SuitableEmployeesToSpecificLevel(node, lstShiftPerJobAllShifts[i].AllSuitableEmployees);
            List<ArrrayCombEmployees> lstComb = new List<ArrrayCombEmployees>();
            ArrrayCombEmployees dataComb = new ArrrayCombEmployees(shift.NumEmployeesRequired);
            CombinationUtil(lstComb, dataComb, suitableEmployees, 0,
                suitableEmployees.Count(), 0, shift.NumEmployeesRequired);
            BuildChildrenToParent(node, lstComb, shift, nodesThisLevel);
        }

        //מאתחל צומת ללא בניית ילדים
        public void InitializeNodeNoChilren(PossiblePlacementNode node, ShiftPerJob shiftPerJob)
        {
            List<EmployeeToPlacement> lstOptionsEmployees = SuitableEmployeesToSpecificLevel(node, shiftPerJob.AllSuitableEmployees);
            List<ArrrayCombEmployees> lstComb = new List<ArrrayCombEmployees>();
            ArrrayCombEmployees dataComb = new ArrrayCombEmployees(shiftPerJob.NumEmployeesRequired);
            CombinationUtil(lstComb, dataComb, lstOptionsEmployees, 0,
                lstOptionsEmployees.Count(), 0, shiftPerJob.NumEmployeesRequired);
            node.Node = lstComb[0];
        }
        /// <summary>
        /// Return the list of the employees that suitable to this specific shift.
        /// According to number of shifts that left to placement and no shifts continuously.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="AllSuitableEmployees"></param>
        /// <returns></returns>
        public List<EmployeeToPlacement> SuitableEmployeesToSpecificLevel(PossiblePlacementNode parent, List<EmployeeToPlacement> allSuitableEmployees)
        {
            List<EmployeeToPlacement> lstNew = new List<EmployeeToPlacement>();
            EmployeeToPlacement emp_i;
            if (parent.Node == null)//If it is the first shift
                return allSuitableEmployees;
            ArrrayCombEmployees combParent = parent.Node;
            int countAppears;
            for (int i = 0; i < allSuitableEmployees.Count(); i++)
            {
                emp_i = allSuitableEmployees[i];
                countAppears = NumSetsInPath(parent, emp_i, 0);
                if (!(combParent.IsExistInArray(emp_i)) && countAppears < emp_i.NumShiftsLeftToPlacement)
                {
                    lstNew.Add(allSuitableEmployees[i]);
                }
            }
            return lstNew;
        }




        /// <summary>
        /// Build children to parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="lstCombThisLevel"></param>
        /// <param name="shiftIBelong"></param>
        public void BuildChildrenToParent(PossiblePlacementNode parent, List<ArrrayCombEmployees> lstCombThisLevel, ShiftPerJob shiftIBelong, List<PossiblePlacementNode> nodesThisLevel)
        {
            parent.MyChildren = new List<PossiblePlacementNode>();
            PossiblePlacementNode p;
            for (int i = 0; i < lstCombThisLevel.Count(); i++)
            {
                p = new PossiblePlacementNode();
                p.Node = lstCombThisLevel[i];
                p.MyChildren = null;
                p.Parent = parent;
                p.ShiftIBelong = shiftIBelong;
                p.CalcSumPriorityNode();
                //לחשב ציון לכל אלד מהצמתים
                p.AvgSumSeniorityYearsPerNode();
                parent.MyChildren.Add(p);
                nodesThisLevel.Add(p);
            }
        }



        public void CombinationUtil(List<ArrrayCombEmployees> lstComb, ArrrayCombEmployees dataComb,
          List<EmployeeToPlacement> employees, int start, int end, int index, int numEmployeesRequired)
        {
            if (index == numEmployeesRequired)
            {
                lstComb.Add(dataComb);
                return;
            }

            for (int i = start; i < end &&
                    end - i >= numEmployeesRequired - index; i++)
            {
                ArrrayCombEmployees newToAdd = new ArrrayCombEmployees();
                newToAdd.CombArray = new EmployeeToPlacement[numEmployeesRequired];
                for (int j = 0; j < dataComb.CombArray.Length; j++)
                    newToAdd.CombArray[j] = dataComb.CombArray[j];
                newToAdd.CombArray[index] = employees[i];
                CombinationUtil(lstComb, newToAdd, employees, i + 1,
                                end, index + 1, numEmployeesRequired);
            }
        }



        /// <summary>
        /// Find all the paths in tree.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paths"></param>
        public void AllPathsFromNode(TreePath path, List<TreePath> paths)
        {
            path.PathInJobTree.Add(this);
            if (this.MyChildren == null)
                paths.Add(path);
            else
            {
                foreach (var item in this.MyChildren)
                {
                    item.AllPathsFromNode(path, paths);
                }
            }
        }



        public void CalcSumPriorityNode()
        {
            SumPriorityPerNode = 0;
            for (int i = 0; i < this.Node.CombArray.Length; i++)
                SumPriorityPerNode += this.Node.CombArray[i].Employee_Request.priority_id;
        }



        //אפשר להחליף את הפונקציה הזו ולשמור עבור כל מסלול את מספר השיבוצים עד כה.

        /// <summary>
        /// Return the num of sets in the path till the node for employee.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="employee"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int NumSetsInPath(PossiblePlacementNode node, EmployeeToPlacement employee, int count)
        {
            if (node.Node == null)
                return count;
            if (node.Node.IsExistInArray(employee))
                return NumSetsInPath(node.Parent, employee, count + 1);//לבדוק אם מחזיר טוב!!!!!!!
            return NumSetsInPath(node.Parent, employee, count);
        }


        /// <summary>
        /// Calculate the avg of seniority years for specific node.
        /// </summary>
        /// <returns></returns>
        public double AvgSumSeniorityYearsPerNode()
        {
            double sum = 0, i = 0;
            int empId;
            foreach (var item in this.Node.CombArray)
            {
                i++;
                empId = item.Employee_Request.employee_id;
                sum += ClassDB.GetEmployeeById(empId).seniority_years;
            }
            AvgSeniorityYearsPerNode = sum / i;
            return AvgSeniorityYearsPerNode;
        }




        ///////////////////////ניסיון לפתור את הבעיות  

        public int[] HighestValues(List<PossiblePlacementNode> lstPlacementOptPerShift)
        {
            int[] highestValues;
            if (NUM_CHILDREN_TO_CONTINUE > lstPlacementOptPerShift.Count())
                highestValues = new int[lstPlacementOptPerShift.Count()];
            else
                highestValues = new int[NUM_CHILDREN_TO_CONTINUE];
            List<double> scores = ScoresPaths(lstPlacementOptPerShift);//all the scores of paths
            for (int i = 0; i < highestValues.Length; i++)
            {
                highestValues[i] = RetAndRemoveMaxIndex(scores);
            }
            return highestValues;
        }


        /// <summary>
        /// Return list of scores of paths in tree in middle of building the tree.
        /// </summary>
        /// <param name="lstPathsScores"></param>
        /// <returns>max value index</returns>
        public List<double> ScoresPaths(List<PossiblePlacementNode> lstPlacementOptPerShift)
        {
            List<double> scores = new List<double>();
            TreePath treePath;
            if (lstPlacementOptPerShift != null)
            {
                foreach (var item in lstPlacementOptPerShift)
                {
                    treePath = new TreePath();
                    treePath.PathInJobTree = new List<PossiblePlacementNode>();
                    treePath.PathInJobTree = GetPath(item, treePath.PathInJobTree);
                    treePath.CalcPathScore();
                    scores.Add(treePath.PathScore);
                }
            }
            return scores;
        }
        /// <summary>
        /// return the path from the node till the root
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<PossiblePlacementNode> GetPath(PossiblePlacementNode node, List<PossiblePlacementNode> path)
        {
            if (node.Node == null)
                return path;
            path.Add(node);
            return GetPath(node.Parent, path);
        }


        /// <summary>
        /// find the max value index from the scores of paths in the middle of building the tree.
        /// </summary>
        /// <param name="lstPathsScores"></param>
        /// <returns>max value index</returns>
        public int RetAndRemoveMaxIndex(List<double> lstPathsScores)
        {
            double max = 0;
            int max_i = -1;
            for (int i = 0; i < lstPathsScores.Count(); i++)
            {
                if (lstPathsScores[i] > max)
                {
                    max = lstPathsScores[i];
                    max_i = i;
                }
            }
            lstPathsScores[max_i] = 0;
            return max_i;
        }



    }
}
