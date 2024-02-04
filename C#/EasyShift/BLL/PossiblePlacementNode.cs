using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    /// <summary>
    /// This class is a tree node of the combination options to set the team of the shift.
    /// </summary>
    public class PossiblePlacementNode
    {
        public static ClassDB ClassDB = new ClassDB();
        public static int NumPaths { get; set; }
        public static int NumEmployees { get; set; }

        /// <summary>
        /// This current node in this level is combination option for shift & job
        /// </summary>
        public ArrrayCombEmployees Node { get; set; }
        public List<PossiblePlacementNode> MyChildren { get; set; }
        public PossiblePlacementNode Parent { get; set; }
        public ShiftPerJob ShiftIBelong { get; set; }
        public double SumPriorityPerNode { get; set; }
        public double AvgSeniorityYearsPerNode { get; set; }
        public double PathScoreWithMe { get; set; }//ציון המסלול עד הצומת הנוכחי כולל חישוב הצומת הנוכחי

        public Dictionary<int, int> NumAppearsForEveryEmp { get; set; }//מספר ההופעות של כל עובד במסלול עד הצומת הנוכחי

        static PossiblePlacementNode()
        {
            Settings_Dto settings;
            settings = ClassDB.Settings;
            NumPaths = settings.num_paths;
            NumEmployees = settings.num_employees;
        }
        public PossiblePlacementNode()
        {
            AvgSeniorityYearsPerNode = 0;
            NumAppearsForEveryEmp = new Dictionary<int, int>();
        }

        public List<PossiblePlacementNode>[] BuildArrList(int numShifts)
        {
            List<PossiblePlacementNode>[] placementOptPerShift = new List<PossiblePlacementNode>[numShifts];
            for (int i = 0; i < placementOptPerShift.Length; i++)
                placementOptPerShift[i] = new List<PossiblePlacementNode>();
            return placementOptPerShift;
        }
        public List<PossiblePlacementNode> ContinueMax(Queue<PossiblePlacementNode> queue, List<ShiftPerJob> lstShiftPerJobAllShifts, int numShifts)
        {
            PossiblePlacementNode p;
            int[] indexesHighestPaths = new int[NumPaths];
            List<PossiblePlacementNode>[] placementOptPerShift = BuildArrList(numShifts);
            int i = 0;
            while (queue.Count() > 0)
            {
                p = queue.Dequeue();
                if (i < numShifts)
                    i = BuildNode(i, numShifts, p, lstShiftPerJobAllShifts, queue, indexesHighestPaths, placementOptPerShift);
            }
            return BestPlacement(placementOptPerShift[numShifts - 1], indexesHighestPaths);
        }

        public int BuildNode(int i, int numShifts, PossiblePlacementNode p, List<ShiftPerJob> lstShiftPerJobAllShifts, Queue<PossiblePlacementNode> queue, int[] indexesHighestPaths, List<PossiblePlacementNode>[] placementOptPerShift)
        {
            List<EmployeeToPlacement> suitableEmployees;
            if (i < numShifts - 1)
            {
                suitableEmployees = SuitableEmployeesToSpecificLevel(p, lstShiftPerJobAllShifts[i].AllSuitableEmployees, ClassDB.GetShiftById(lstShiftPerJobAllShifts[i + 1].ShiftId), true);
            }
            else
                suitableEmployees = SuitableEmployeesToSpecificLevel(p, lstShiftPerJobAllShifts[i].AllSuitableEmployees, new Shift_Dto(), false);
            List<EmployeeToPlacement> orderSuitableEmployees = suitableEmployees.OrderByDescending(x => x.Employee_Request.priority_id).ThenByDescending(x => ClassDB.GetSeniorityYearsById(x.Employee_Request.employee_id)).ThenByDescending(x => x.Employee_Request.request_date.TimeOfDay.TotalMilliseconds).ToList();
            List<EmployeeToPlacement> partSuitableEmployees = RetPartSuitableEmployees(orderSuitableEmployees);
            p.InitializeNodes(p, partSuitableEmployees, lstShiftPerJobAllShifts[i], placementOptPerShift[i]);
            if (queue.Count() == 0)
            {
                indexesHighestPaths = HighestValues(placementOptPerShift[i]);
                EnqueueHighestValues(placementOptPerShift[i], indexesHighestPaths, queue);
                i++;
            }
            return i;
        }
        public List<EmployeeToPlacement> RetPartSuitableEmployees(List<EmployeeToPlacement> orderSuitableEmployees)
        {
            List<EmployeeToPlacement> partSuitableEmployees = new List<EmployeeToPlacement>();
            for (int j = 0; j < NumEmployees; j++)
            {
                if (orderSuitableEmployees.Count() <= NumEmployees)
                {
                    partSuitableEmployees = orderSuitableEmployees;
                    break;
                }
                partSuitableEmployees.Add(orderSuitableEmployees[j]);
            }
            return partSuitableEmployees;
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
            List<PossiblePlacementNode> bestPath = GetPath(best);
            return bestPath;
        }


        public PossiblePlacementNode HighestPath(List<PossiblePlacementNode> optPlacement)
        {
            List<double> scores = ScoresPaths(optPlacement);//all the scores of paths
            return optPlacement[RetAndRemoveMaxIndex(scores)];
        }

        ///מאתחל צומת בעץ ובונה לו ילדים
        public void InitializeNodes(PossiblePlacementNode node, List<EmployeeToPlacement> suitableEmployees, ShiftPerJob shift, List<PossiblePlacementNode> nodesThisLevel)
        {
            List<ArrrayCombEmployees> lstComb = new List<ArrrayCombEmployees>();
            ArrrayCombEmployees dataComb = new ArrrayCombEmployees(shift.NumEmployeesRequired);
            CombinationUtil(lstComb, dataComb, suitableEmployees, 0,
                suitableEmployees.Count(), 0, shift.NumEmployeesRequired);
            BuildChildrenToParent(node, lstComb, shift, nodesThisLevel);
        }

        /// <summary>
        /// Return the list of the employees that suitable to this specific shift.
        /// According to number of shifts that left to placement and no shifts continuously.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="AllSuitableEmployees"></param>
        /// <returns></returns>
        public List<EmployeeToPlacement> SuitableEmployeesToSpecificLevel(PossiblePlacementNode parent, List<EmployeeToPlacement> allSuitableEmployees, Shift_Dto currentNodeShift, bool flag)
        {
            if (parent.Node == null)//If it is the first shift
                return allSuitableEmployees;
            List<EmployeeToPlacement> lstNew = new List<EmployeeToPlacement>();
            Dictionary<int, int> d = parent.NumAppearsForEveryEmp;
            ArrrayCombEmployees combParent = parent.Node;
            if (flag && currentNodeShift.IsPermittedSequence == 1)
            {
                lstNew = CheckEmployees(true, allSuitableEmployees, d, combParent);
            }
            else
            {
                lstNew = CheckEmployees(false, allSuitableEmployees, d, combParent);
            }
            return lstNew;
        }


        public List<EmployeeToPlacement> CheckEmployees(bool flag, List<EmployeeToPlacement> allSuitableEmployees, Dictionary<int, int> d, ArrrayCombEmployees combParent)
        {
            EmployeeToPlacement emp_i;
            List<EmployeeToPlacement> lstNew = new List<EmployeeToPlacement>();
            int countAppears;
            for (int i = 0; i < allSuitableEmployees.Count(); i++)
            {
                emp_i = allSuitableEmployees[i];
                countAppears = d[allSuitableEmployees[i].Employee_Request.employee_id];
                if (flag)
                {
                    if (countAppears < emp_i.NumShiftsLeftToPlacement)
                        lstNew.Add(allSuitableEmployees[i]);
                }
                else
                {
                    if (!combParent.IsExistInArray(emp_i) && countAppears < emp_i.NumShiftsLeftToPlacement)
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
                //לחשב ציון לכל אחד מהצמתים
                p.CalcSumPriorityNode();
                p.AvgSumSeniorityYearsPerNode();
                //הוספת העובדים למילון
                AddEmployeesToDictionary(p);
                parent.MyChildren.Add(p);
                nodesThisLevel.Add(p);
            }
        }


        public void AddEmployeesToDictionary(PossiblePlacementNode p)
        {
            if (p.ShiftIBelong.ShiftId == 1)//אם אתה הראשון ואין לך הורה
            {
                foreach (var employee in p.ShiftIBelong.AllSuitableEmployees)
                    p.NumAppearsForEveryEmp.Add(employee.Employee_Request.employee_id, 0);
            }
            else
            {
                for (int i = 0; i < p.Parent.NumAppearsForEveryEmp.Count(); i++)
                    p.NumAppearsForEveryEmp.Add(p.Parent.NumAppearsForEveryEmp.ElementAt(i).Key, p.Parent.NumAppearsForEveryEmp.ElementAt(i).Value);
            }
            for (int i = 0; i < p.Node.CombArray.Length; i++)
                p.NumAppearsForEveryEmp[p.Node.CombArray[i].Employee_Request.employee_id]++;
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





        public void CalcSumPriorityNode()
        {
            SumPriorityPerNode = 0;
            for (int i = 0; i < this.Node.CombArray.Length; i++)
                SumPriorityPerNode += this.Node.CombArray[i].Employee_Request.priority_id;
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
            if (NumPaths > lstPlacementOptPerShift.Count())
                highestValues = new int[lstPlacementOptPerShift.Count()];
            else
                highestValues = new int[NumPaths];
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
            if (lstPlacementOptPerShift != null)
            {
                foreach (var item in lstPlacementOptPerShift)
                {
                    CalcScore(scores, item);
                }
            }
            return scores;
        }

        public void CalcScore(List<double> scores, PossiblePlacementNode p)
        {
            if (p.Parent.ShiftIBelong == null)//אם אתה תחילת המסלול-אם אתה מייצג משמרת ראשונה
            {
                //חשב ציון לצומת
                p.PathScoreWithMe = p.SumPriorityPerNode;
                scores.Add(p.PathScoreWithMe);
            }
            else
            {
                double standardDeviation = p.CalcStandardDeviation();
                if (standardDeviation != 0)
                    p.PathScoreWithMe = p.Parent.PathScoreWithMe + 1 / standardDeviation + p.SumPriorityPerNode;
                else
                    p.PathScoreWithMe = p.Parent.PathScoreWithMe + p.SumPriorityPerNode;
                scores.Add(p.PathScoreWithMe);
            }
        }
        /// <summary>
        /// return the path from the node till the root
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<PossiblePlacementNode> GetPath(PossiblePlacementNode node)
        {
            List<PossiblePlacementNode> path = new List<PossiblePlacementNode>();
            while (node.Node != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            return path;
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


        public double CalcStandardDeviation()
        {
            PossiblePlacementNode p = this;
            double avgSeniorityYearsForPath = p.CalcAvgSeniorityYearsForPath();
            double sumDistFromAvg = 0, k;
            for (int i = ShiftIBelong.ShiftId; i > 0; i--)
            {
                k = p.AvgSeniorityYearsPerNode;
                sumDistFromAvg += Math.Pow(k - avgSeniorityYearsForPath, 2);
                p = p.Parent;
            }
            return Math.Sqrt(sumDistFromAvg / ShiftIBelong.ShiftId);
        }

        public double CalcAvgSeniorityYearsForPath()
        {
            double sum = 0;
            PossiblePlacementNode p = this;
            for (int i = ShiftIBelong.ShiftId; i > 0; i--)
            {
                sum += p.AvgSeniorityYearsPerNode;
                p = p.Parent;
            }
            return sum / ShiftIBelong.ShiftId;
        }

    }
}
