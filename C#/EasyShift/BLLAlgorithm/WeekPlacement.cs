using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
    public class WeekPlacement
    {
        public List<TreesPerJob> AllTrees { get; set; }
        public List<List<TreePath>> AllPathsEachTree { get; set; }
        public List<TreePath[]> AllCombinationPaths { get; set; }
        public Shift[,] MatSuitableEmployeesToTheWeek { get; set; }

        public void SetAllPathsEachTree()
        {
            AllPathsEachTree = new List<List<TreePath>>();
            foreach (var tree in AllTrees)
            {
                List<TreePath> treePaths = new List<TreePath>();
                TreePath path = new TreePath();
                tree.TreeHead.AllPathsFromNode(path, treePaths);
                AllPathsEachTree.Add(treePaths);
            }
        }




        /// <summary>
        /// The function does all the combination of placement to the week.
        /// </summary>
        /// <param name="lstBig">The length of each array is the num of jobs.</param>
        /// <param name="lst">List of the all possible paths per job.</param>
        /// <param name="arrayToAddList">Temporary array to store current combination</param>
        /// <param name="start">Where to take from lst.</param>
        /// <param name="end">lst.Count()</param>
        /// <param name="index">The current index to put in the []arrayToAddList.</param>
        /// <param name="numComb">Equal to the numbers of the jobs.</param>
        /// 
        //לבצע על זה מעקבבבבבב!!!!! 
        public void CombinationUtil(List<TreePath[]> lstBig, List<List<TreePath>> lst,
            TreePath[] arrayToAddList, int start, int end, int index, int numComb, List<TreePath> l)
        {
            if (index == numComb)
            {
                lstBig.Add(arrayToAddList);
                return;
            }

            for (int i = start; i <= end &&
                    end - i + 1 >= numComb - index; i++)
            {
                for (int j = 0; j < lst[i].Count(); j++)
                {
                    l = lst[i];
                    arrayToAddList[index].PathInJobTree = l[j].PathInJobTree;
                    CombinationUtil(lstBig, lst, arrayToAddList, i + 1, end, index + 1, numComb,l);
                }
            }
        }

    }
}
