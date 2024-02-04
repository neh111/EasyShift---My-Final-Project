using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using BLL;
using DAL;

namespace BLLAlgorithm
{
    public class LstByJobLevelA
    {
        public static ClassDB classDB;
        public int JobId { get; set; }
        public List<Employee_request_Dto> Employees { get; set; }
        public LstByJobLevelA()
        {
            Employees = new List<Employee_request_Dto>();
        }
        public LstByJobLevelA(int jobId, List<Employee_request_Dto> l)
        {
            this.JobId = jobId;
            this.Employees = l;
        }

        public void CheckDates()
        {
            bool ifCan = true;
            List<Employee_request_Dto> listSuitableDates = new List<Employee_request_Dto>();
            List<PlacementResults_Dto> allPlacementResults = classDB.GetAllPlacement_results();
            Shift_Dto shift_request, shift_placement;
            int shift_idRequest, shift_request_day, shift_placement_day, shift_request_type, shift_placement_type, difference;
            DateTime date_shift_request, shift_placement_date;
            foreach (var request in this.Employees)
            {
                shift_idRequest = request.shift_id;
                shift_request = classDB.GetAllShifts().Where(x => x.shift_id == shift_idRequest).FirstOrDefault();
                shift_request_day = shift_request.day;
                //date_shift_request = shift_request.shift_date;
                shift_request_type = shift_request.shift_type_id;
                foreach (var placement in allPlacementResults)
                {
                    shift_placement = classDB.GetAllShifts().Where(x => x.shift_id == placement.shift_id).FirstOrDefault();
                    //shift_placement_date = shift_placement.shift_date;
                    shift_placement_day = shift_placement.day;
                    shift_placement_type = shift_placement.shift_type_id;
                    //if (date_shift_request == shift_placement_date)
                    {
                        if (shift_request_day == shift_placement_day)
                        {
                            difference = shift_request_type - shift_placement_type;
                            if (difference == 0 || difference == 1 || difference == -1)
                                ifCan = false;
                        }
                    }
                }
                if (ifCan)
                    listSuitableDates.Add(request);
                ifCan = true;
            }
            this.Employees = listSuitableDates;
        }



        public void FindEmployees(int shift_id, int job_id)
        {
            //מביא את כל העובדים שיש להם בקשה לגבי המשמרת ולתפקיד הזה ושסטטוס השיבוץ הוא פולס כלומר לא התקבלה עדיין בקשת השיבוץ וכן רמת ההעדפה שלהם לבקשה שונה מלא יכול 
            List<Employee_request_Dto> listAllEmployeesRequest = classDB.GetAllEmployeesRequest();
            List<Employee_request_Dto> maybeSuitable = new List<Employee_request_Dto>();
            maybeSuitable = listAllEmployeesRequest.Where(x => x.shift_id == shift_id && x.job_id == job_id && x.status == 0 && x.priority_id != 1).ToList();
            LstByJobLevelA p = new LstByJobLevelA();
            p.JobId = job_id;
            p.Employees = maybeSuitable;
            p.CheckDates();
            PlacementLevelA pA = new PlacementLevelA();
            pA.SaveInMat(shift_id, job_id, p.Employees);
        }
    }
}
