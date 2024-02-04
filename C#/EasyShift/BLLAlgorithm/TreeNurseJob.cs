using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
    public class TreeSplitJobNode:PossiblePlacementNode
    {
        public List<InnerSplitNode> MyInnerChildren { get; set; }
        public List<SplitJobPerShift> ListSplitJobsForShift { get; set; }
        public List<PossiblePlacementNode> BestInnerPath { get; set; }
        public const int NUM_SHIFTS = 21;

        //כדי לבנות את הילדים נבנה עץ של הילדים אך את הראש שלו נאל 

        // public TreeNursePath MaxValuePathInTree { get; set; }



        public TreesPerJob BuildTree(int jobId, List<ShiftPerJob> listsSplittingJobs)
        {
            TreesPerJob t = new TreesPerJob();
            t.JobId = jobId;
            t.TreeHead = new TreeSplitJobNode ();
            Queue<PossiblePlacementNode> q;
            //q.Enqueue(t.TreeHead);
            //סדור הרשימה
            
            List<ShiftPerJob>[] lstShiftPerJobs= MergeAsShifts(listsSplittingJobs);
            List<ShiftPerJob> orderLst = RetLstForMainNode(lstShiftPerJobs);
            List<ShiftPerJob>[] sortedLst = OrderArray(lstShiftPerJobs);
            t.TreeHead.InitFirstShiftOptions(t.TreeHead, orderLst);
            List<Task<List<PossiblePlacementNode>>> tasks = new List<Task<List<PossiblePlacementNode>>>();
            foreach (var option in t.TreeHead.MyChildren)
            {
                Task<List<PossiblePlacementNode>> task = Task.Run(() =>
                {
                    q = new Queue<PossiblePlacementNode>();
                    q.Enqueue(option);
                    List<PossiblePlacementNode> bestPlacement = option.ContinueMax(q, orderLst);
                    return bestPlacement;
                });
                tasks.Add(task);
            }
            foreach (var task in tasks)
            {
                task.Wait();
            }
            //List<PossiblePlacementNode> bestPlacement = t.TreeHead.ContinueMax(q, orderLst);
            return t;
        }


        /// <summary>
        /// הפונקציה מקבלת אוסף של כל התפקידים הדרושים לכל המשמרות ומסדרת אותם לפי משמרות
        /// </summary>
        /// <param name="jobsToShift"></param>
        /// <returns></returns>
        public List<ShiftPerJob>[] MergeAsShifts(List<ShiftPerJob> jobsToShift)
        {
            //arrShiftPerJob-מערך בגודל 21 כמספר המשמרות כאשר בכל תא יש רשימה של התפקידים הדרושים לאותה משמרת
            List<ShiftPerJob>[] arrShiftPerJob = new List<ShiftPerJob>[NUM_SHIFTS];
            for (int i = 0; i < arrShiftPerJob.Length; i++)
                arrShiftPerJob[i] = new List<ShiftPerJob>();
            foreach (var item in jobsToShift)
                arrShiftPerJob[item.ShiftId - 1].Add(item);
            return arrShiftPerJob;
        }
        /// <summary>
        /// הפעולה מקבלת רשימה של התפקידים לכל המשמרות כמה דרוש לכל תפקיד ספציפי בכל משמרת ומחזירה רשימה המתארת כמה סה"כ תפקידים דרושים לכל משמרת
        /// </summary>
        /// <param name="lstShiftPerJobs"></param>
        /// <returns></returns>
        public List<ShiftPerJob> RetLstForMainNode(List<ShiftPerJob>[] lstShiftPerJobs)
        {
            List<ShiftPerJob> lstToRet = new List<ShiftPerJob>();
            for (int i = 0; i < lstShiftPerJobs.Count(); i++)
            {
                lstToRet.Add(lstShiftPerJobs[i][0]);
                for (int j = 1; j < lstShiftPerJobs[i].Count(); j++)
                {
                    lstToRet[i].NumEmployeesRequired += lstShiftPerJobs[i][j].NumEmployeesRequired;
                }
            }
            return lstToRet;
        }

        /// <summary>
        /// הפונקציה מקבלת מערך כאשר כל תא מייצג משמרת ובו התפקידים מסוג הקטגוריה שדרושים למשמרת הזו.
        /// הפונקציה ממיינת כל תא לפי מספר התפקידים שדרושים לתפקיד.
        /// </summary>
        /// <param name="lstToOrder"></param>
        /// <returns></returns>
        public List<ShiftPerJob>[] OrderArray(List<ShiftPerJob>[] lstToOrder)
        {
            List<ShiftPerJob> sortedList;
            for (int i = 0; i < lstToOrder.Length; i++)
            {
                sortedList= lstToOrder[i].OrderBy(x => x.NumEmployeesRequired).ToList();
                lstToOrder[i] = sortedList;
            }
            return lstToOrder;
        }

       
        //public List<List<TreeNursePath>> AllTreePaths()
        //{
        //    TreeNursePath maxValuePath = new TreeNursePath(), path;
        //    List<TreeNursePath> allPathsFromInnerNode;
        //    List<List<TreeNursePath>> allPathsFromOuterNode = new List<List<TreeNursePath>>();
        //    foreach (var item in MyChildren)
        //    {
        //        path = new TreeNursePath();
        //        allPathsFromInnerNode = new List<TreeNursePath>();
        //        item.AllPathsFromNode(path, allPathsFromInnerNode);
        //        allPathsFromOuterNode.Add(allPathsFromInnerNode);
        //    }
        //    return allPathsFromOuterNode;
        //}

        //public int ValuePath(TreeNursePath path)
        //{
        //    int sum = 0;
        //    for (int i = 0; i < path.Path.Count(); i++)
        //        for (int j = 0; j < path.Path[i].Node.CombArray.Length; j++)
        //            sum += path.Path[i].Node.CombArray[j].Employee_Request.priority_id;
        //    return sum;
        //}

        //public TreeNursePath MaxValuePath(List<List<TreeNursePath>> allPathsInTree)
        //{
        //    int temp = 0, max = 0;
        //    TreeNursePath maxTreeNursePath = new TreeNursePath();
        //    foreach (var outerPath in allPathsInTree)
        //    {
        //        foreach (var innerPath in outerPath)
        //        {
        //            temp = ValuePath(innerPath);
        //            if (temp > max)
        //            {
        //                max = temp;
        //                maxTreeNursePath = innerPath;
        //            }

        //        }
        //    }
        //    return maxTreeNursePath;
        //}

    }
}
