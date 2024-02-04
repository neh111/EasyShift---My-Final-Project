using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace BLLAlgorithm
{
    public class InnerNursesNode 
    {
        public ArrrayCombEmployees InnerNode { get; set; }//צירוף השיבוץ עצמו
        public List<InnerNursesNode> MyChildren { get; set; }
        public InnerNursesNode Parent { get; set; }
        public TreeNurseJobNode  TreeHead { get; set; }
        public NurseJobPerShift NurseJobType { get; set; }

        public static PossiblePlacementNode PoPlNode = new PossiblePlacementNode();
        public static ClassDB ClassDB = new ClassDB();
        public const int NUM_CHILDREN_TO_CONTINUE = 4;
        //מוסיפים ילדים לרשימת ה-MYCHILDREN של העצם שעליו מופעלת הפעולה

        //List<ShiftPerJob> lstNurseJobsPerShift-הרשימה של התפקידים הדרושים עבור המשמרת הזו בחלוקה לתת קטגוריות
        //הרשימה הנ"ל צריכה להיות ממוינת לפי מספר העובדים הדרושים מהקטן לגדול
        //public void BuildInnerNursesTree(List<NurseJobPerShift> lstNurseJobsPerShift, int index, List<EmployeeToPlacement> lstSuitableEmployees)
        //{
        //    if (index >= lstNurseJobsPerShift.Count())
        //        return;
        //    //Build level for the index shift.
        //    List<ArrrayCombEmployees> lstComb = new List<ArrrayCombEmployees>();
        //    ArrrayCombEmployees dataComb = new ArrrayCombEmployees(lstNurseJobsPerShift[index].NumEmployeesRequired);
        //    List<EmployeeToPlacement> suitableLst = SuitableNursesForJob(lstSuitableEmployees, this);
        //    PoPlNode.CombinationUtil(lstComb, dataComb, suitableLst, 0,
        //          suitableLst.Count(), 0, lstNurseJobsPerShift[index].NumEmployeesRequired);
        //    InnerNursesNode innerNursesNode = new InnerNursesNode();
        //    foreach (var item in lstComb)
        //    {
        //        innerNursesNode.InnerNode = item;
        //        innerNursesNode.MyChildren = new List<InnerNursesNode>();
        //        innerNursesNode.Parent = this;
        //        innerNursesNode.TreeHead = this.TreeHead;
        //        innerNursesNode.NurseJobType = lstNurseJobsPerShift[index];
        //        this.MyChildren.Add(innerNursesNode);
        //    }
        //    foreach (var item in this.MyChildren)
        //        item.BuildInnerNursesTree(lstNurseJobsPerShift, index + 1, suitableLst);
        //}

        public List<EmployeeToPlacement> SuitableNursesForJob(List<EmployeeToPlacement> allSuitableEmployees, InnerNursesNode parentNode)
        {
            List<EmployeeToPlacement> lstNew = new List<EmployeeToPlacement>();
            foreach (var item in allSuitableEmployees)
            {
                if (!parentNode.InnerNode.IsExistInArray(item))
                    lstNew.Add(item);
            }
            return lstNew;
        }



        /// <summary>
        /// Find all the paths in nursesTree.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paths"></param>
        public void AllPathsFromNode(TreeNursePath path, List<TreeNursePath> paths)
        {
            path.Path.Add(this);
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


        ///נסיון לפתור בעיות
        ///

        ///


        public void Build(TreeNurseJobNode node,List<ShiftPerJob> relevantJobsForThisShift)
        {

        }
        //public List<PossiblePlacementNode> ContinueMax(Queue<InnerNursesNode> queue, List<NurseJobPerShift> lstAllJobsTypes, int numCategoryJob)
        //{
        //    //מציאת מספר התפקידים ששייכים לקטגוריה הזו
        //    int numJobsTypes = ClassDB.GetNumJobsForSpecificCategory(numCategoryJob);
        //    InnerNursesNode p;
        //    List<InnerNursesNode> nodesThisLevel;
        //    int[] indexesHighestPaths = new int[NUM_CHILDREN_TO_CONTINUE];
        //    List<InnerNursesNode>[] placementOptPerShift = new List<InnerNursesNode>[numJobsTypes];
        //    for (int i = 0; i < placementOptPerShift.Length; i++)
        //        placementOptPerShift[i] = new List<InnerNursesNode>();
        //    while (queue.Count() > 0)
        //    {
        //        p = queue.Dequeue();
        //        if (p == null)
        //        {
        //            p.InitializeNodes(p, lstShiftPerJobAllShifts, 0, placementOptPerShift[0]);
        //            indexesHighestPaths = HighestValues(placementOptPerShift[0]);
        //            DequeueHighestValues(placementOptPerShift[0], indexesHighestPaths, queue);
        //        }
        //        else
        //        {
        //            if (p.NurseJobType.NurseJobId < numJobsTypes)
        //            {
        //                p.InitializeNodes(p, lstShiftPerJobAllShifts, index, placementOptPerShift[index]);
        //                //if (p.ShiftIBelong.ShiftId != queue.ElementAt(0).ShiftIBelong.ShiftId)
        //                if (queue.Count() == 0)
        //                {
        //                    indexesHighestPaths = HighestValues(placementOptPerShift[p.ShiftIBelong.ShiftId]);
        //                    DequeueHighestValues(placementOptPerShift[p.ShiftIBelong.ShiftId], indexesHighestPaths, queue);
        //                }
        //            }
        //        }
        //    }
        //    return BestPlacement(placementOptPerShift[20], indexesHighestPaths);
        //}



    }
}
