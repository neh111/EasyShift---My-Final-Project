using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BLL;
using DTO;
namespace BLL
{
    public class TreesPerJob
    {
        public int JobId { get; set; }
        public PossiblePlacementNode TreeHead { get; set; }//It's Node have to be null because it isn't really shift.
        public List<PossiblePlacementNode> BestPlacementPath { get; set; }
        public static ClassDB ClassDB = new ClassDB();


        //פונקציה האחראית על בניית העץ ושמירת השיבוץ
        public void BuildTree(int jobId, List<ShiftPerJob> lstShiftPerJobAllShifts,int numShifts)
        {
            TreesPerJob t = new TreesPerJob();
            t.JobId = jobId;
            t.TreeHead = new PossiblePlacementNode();
            //בניית התור שמשמש לבניית העץ
            Queue<PossiblePlacementNode> q = new Queue<PossiblePlacementNode>();
            //דחיפת שורש העץ לתור
            q.Enqueue(t.TreeHead);
            //קריאה לפונקציה הבונה את העץ ומחזירה את השיבוץ היעיל
            List<PossiblePlacementNode> bestPlacement = t.TreeHead.ContinueMax(q, lstShiftPerJobAllShifts, numShifts);
            t.BestPlacementPath = bestPlacement;
            //שמירת השיבוץ
            SavePlacementForJob(bestPlacement);
        }


        //פונקציה ששומרת את תוצאות השיבוץ
        public void SavePlacementForJob(List<PossiblePlacementNode> bestPlacement)
        {
            List<int> empReqIdies = new List<int>();
            List<PlacementResults_Dto> placement = new List<PlacementResults_Dto>();
            PlacementResults_Dto empPlacement;
            foreach (var shift in bestPlacement)
            {
                foreach (var employee in shift.Node.CombArray)
                {
                    empReqIdies.Add(employee.Employee_Request.employee_request_id);
                    empPlacement = new PlacementResults_Dto();
                    empPlacement.shift_id = shift.ShiftIBelong.ShiftId;
                    empPlacement.employee_id = employee.Employee_Request.employee_id;
                    empPlacement.job_id = employee.Employee_Request.job_id;
                    empPlacement.placement_date = DateTime.Today;
                    placement.Add(empPlacement);
                }
            }
            lock(ClassDB)
            { 
            ClassDB.SavePlacementsResultsPerJob(placement,empReqIdies);
            }
        }
    }
}
