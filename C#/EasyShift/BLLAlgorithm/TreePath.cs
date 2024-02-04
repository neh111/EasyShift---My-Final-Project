using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLLAlgorithm
{
    public class TreePath
    {
        public List<PossiblePlacementNode> PathInJobTree { get; set; }
        public double SumPriority { get; set; }
        public double AvgSumSeniorityYears { get; set; }
        public double AvgSeniorityYears { get; set; }
        public double StandardDeviation { get; set; }//סטיית תקן
        public double PathScore { get; set; }
        public TreePath()
        {
            PathInJobTree = new List<PossiblePlacementNode>();
        }

        
        public void CalcAvgSeniorityYears()
        {
            AvgSumSeniorityYears = 0;
            double sum = 0;
            foreach (var item in PathInJobTree)
            {
                sum += item.AvgSeniorityYearsPerNode;
            }
            AvgSeniorityYears = sum / PathInJobTree.Count();
        }
        /// <summary>
        /// Calculate standard deviation of seniority years in path.
        /// </summary>
        public void CalcStandardDeviation()
        {
            CalcAvgSeniorityYears();
            double sumDistFromAvg = 0,k, i = 0;
            foreach (var item in PathInJobTree)
            {
                i++;
                k = item.AvgSeniorityYearsPerNode;
                sumDistFromAvg += Math.Pow(k - AvgSeniorityYears, 2);
            }
            StandardDeviation = Math.Sqrt(sumDistFromAvg / i);
        }

        public double CalcSumPriority()
        {
            SumPriority = 0;
            for (int i = 0; i < PathInJobTree.Count(); i++)
                SumPriority += PathInJobTree[i].SumPriorityPerNode;
            return SumPriority;
        }
        /// <summary>
        /// Calculate the final score of the path.
        /// </summary>
        public void CalcPathScore()
        {
            CalcSumPriority();//חישוב ואתחול סך העדיפות של כל המסלול
            if(PathInJobTree.Count()>1)
            { 
            CalcStandardDeviation();//חישוב ואתחול סטיית תקן של המסלול עליו מופעלת הפעולה
            if (StandardDeviation != 0)
                PathScore = SumPriority + 1 / StandardDeviation;
                else
                    PathScore = SumPriority;//עושים אחד חלקי הסטיית תקן כי אני רוצה את הסטיית תקן
            }
            else
                PathScore = SumPriority;
        }

    }
}
