using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DTO;

namespace BLLAlgorithm
{
    public class Algorithm
    {
        public static ClassDB ClassDB { get; set; }
        public List<Employee_Dto> Employees { get; set; }
        public List<Employee_request_Dto> EmployeesRequests { get; set; }
        public List<Jobs_Dto> Jobs { get; set; }
        public List<Jobs_to_shift_Dto> JobsToShift { get; set; }
        public List<Priority_Dto> Priorities { get; set; }
        public Algorithm()
        {
            ClassDB = new ClassDB();
            //הבאת כל הרשימות הנחוצות
            Employees = ClassDB.GetAllEmployeesDto();
            EmployeesRequests = ClassDB.GetAllEmployeesRequest();
            Jobs = ClassDB.GetAllJobs();
            JobsToShift = ClassDB.GetAllJobsToShift();
            Priorities = ClassDB.GetAllPrioritiesByList();
        }


        public List<Jobs_to_shift_Dto> GetJobsToShiftByJobAndDate(int jobId, DateTime startDate, DateTime endDate)
        {
            return JobsToShift.Where(x => x.job_id == jobId &&
            x.request_date.Year >= startDate.Year &&
            x.request_date.Month >= startDate.Month &&
            x.request_date.Day >= startDate.Day &&
            x.request_date.Year <= endDate.Year &&
            x.request_date.Month <= endDate.Month &&
            x.request_date.Day <= endDate.Day)
                .ToList();
        }

        public List<Jobs_to_shift_Dto> GetJobsToShiftByJob(int jobId)
        {
            return JobsToShift.Where(x => x.job_id == jobId).ToList();
        }

        public List<Employee_request_Dto> GetEmployeeReqByShiftAndJobId(int shiftId, int jobId)
        {
            return EmployeesRequests.Where(x => x.shift_id == shiftId && x.job_id == jobId).ToList();
        }


        public int GetNumShiftsInWeekByEmpId(int employeeId)
        {
            return Employees.Where(x => x.employee_id == employeeId).FirstOrDefault().num_shifts_in_week;
        }

        public List<Employee_Dto> GetEmployeesByJobId(int jobId)
        {
            return Employees.Where(x => x.job_id == jobId).ToList();
        }

        public int GetPriorityIdByDescription(string description)
        {
            return Priorities.Where(x => x.priority_description == description).FirstOrDefault().priority_id;
        }

        public int GetNumEmpRequiredByShiftAndJobId(int shiftId, int jobId)
        {
            return JobsToShift.Where(x => x.shift_id == shiftId && x.job_id == jobId).FirstOrDefault().num_employees_requierd;
        }

        //על סמך רשימת Employee_request_Dto נבנה רשימה של EmployeeToPlacement
        //בניית רשימת EmployeeToPlacement
        public List<EmployeeToPlacement> ConvertFromListOfEmployee_request_DtoToEmployeeToPlacement(List<Employee_request_Dto> lst)
        {
            List<EmployeeToPlacement> convertedList = new List<EmployeeToPlacement>();
            EmployeeToPlacement e;
            int numShiftsToPlacement;
            foreach (var item in lst)
            {
                numShiftsToPlacement = GetNumShiftsInWeekByEmpId(item.employee_id);
                e = new EmployeeToPlacement(item, numShiftsToPlacement, numShiftsToPlacement);
                convertedList.Add(e);
            }
            return convertedList;
        }






        /// <summary>
        /// פונקציה שלוקחת את כל העובדים שלא הגישו בקשה למשמרת הספציפית הזו אך באופן כללי עובדים
        ///בתפקיד הספציפי הזה ולעשות שתהיה להם בקשה עבור המשמרת הזו ברמת עדיפות ניטרלית
        /// </summary>
        /// <param name="listEmployeesSubmittedReq"></param>
        /// <returns></returns>
        public void BuildEmployeeReqToEmployeesNotSubmitReq(List<Employee_request_Dto> listEmployeesSubmitedReq, int jobId, int shiftId)
        {
            int tempJobId;
            if (jobId != 2 && jobId != 3 && jobId != 4)
                tempJobId = 1;
            else
                tempJobId = jobId;
            List<Employee_Dto> employee_Dtos = GetEmployeesByJobId(tempJobId);
            Employee_request_Dto e;
            bool flag;
            foreach (var emp in employee_Dtos)
            {
                flag = true;
                foreach (var empReq in listEmployeesSubmitedReq)
                {
                    if (emp.employee_id == empReq.employee_id)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    e = ClassDB.BuildInstanceOfEmployeeRequestAndSaveChangesInDb(emp.employee_id, shiftId, jobId, GetPriorityIdByDescription("ניטרלי"), 0);
                    listEmployeesSubmitedReq.Add(e);
                }
            }
        }

        /// <summary>
        /// מיזוג שני רשימות
        /// </summary>
        public void MergeTwoLists(List<Employee_request_Dto> first, List<Employee_request_Dto> second)
        {
            foreach (var item in second)
            {
                first.Add(item);
            }
        }


        public void RunAlgorithm()
        {
            TreesPerJob treesPerJob;
            List<Jobs_to_shift_Dto> lstJobToShift;
            //מעבר על כל התפקידים
            foreach (var job in Jobs)
            {
                //מביא רשימה של תפקידים למשמרות עבור התפקיד הספציפי הזה
                DateTime start = new DateTime(2021, 12, 26);
                DateTime end = new DateTime(2021, 12, 26);
                lstJobToShift = GetJobsToShiftByJobAndDate(job.job_id, start, end);
                List<ShiftPerJob> lstShiftPerJob = new List<ShiftPerJob>();
                ShiftPerJob tempShiftPerJob;
                //בונה רשימה של כל המשמרות שעבורם דרוש תפקיד זה
                foreach (var jobToShift in lstJobToShift)
                {
                    //רשימה של כל העובדים שיש להם בקשה עבור התפקיד הנוכחי והמשמרת הנוכחית
                    List<Employee_request_Dto> allSuitableEmployees = new List<Employee_request_Dto>();
                    allSuitableEmployees = GetEmployeeReqByShiftAndJobId(jobToShift.shift_id, jobToShift.job_id);
                    //יש לקחת את כל העובדים שלא הגישו בקשה למשמרת הספציפית הזו אך באופן כללי עובדים
                    //בתפקיד הספציפי הזה ולעשות שתהיה להם בקשה עבור המשמרת הזו ברמת עדיפות ניטרלית
                    BuildEmployeeReqToEmployeesNotSubmitReq(allSuitableEmployees, jobToShift.job_id, jobToShift.shift_id);
                    //MergeTwoLists(allSuitableEmployees, lstToAdd);
                    //על סמך הרשימה לעיל נבנה רשימה של EmployeeToPlacement
                    //בניית רשימת EmployeeToPlacement
                    List<EmployeeToPlacement> e = ConvertFromListOfEmployee_request_DtoToEmployeeToPlacement(allSuitableEmployees);
                    tempShiftPerJob = new ShiftPerJob(jobToShift.shift_id, e, jobToShift.job_id, GetNumEmpRequiredByShiftAndJobId(jobToShift.shift_id, jobToShift.job_id), SumPriorities(allSuitableEmployees));
                    lstShiftPerJob.Add(tempShiftPerJob);

                }
                //בניית עץ המשמרות עבור התפקיד הספציפי
                //אם התפקיד הוא לא תחת הקטגוריה של תפקידי אחיות יש לבנות עץ מהסוג הרגיל
                //if (job.isSplit == 0)
                //{
                //    treesPerJob = new TreesPerJob();
                //    List<ShiftPerJob> l = new List<ShiftPerJob>();
                //    for (int i = 0; i < 13; i++)
                //    {
                //        l.Add(new ShiftPerJob());
                //        l[i].AllSuitableEmployees = new List<EmployeeToPlacement>();
                //        l[i].JobId = lstShiftPerJob[i].JobId;
                //        l[i].NumEmployeesRequired = lstShiftPerJob[i].NumEmployeesRequired;
                //        l[i].ShiftId = lstShiftPerJob[i].ShiftId;
                //        for (int j = 0; j < 5; j++)
                //        {
                //            l[i].AllSuitableEmployees.Add(lstShiftPerJob[i].AllSuitableEmployees[j]);
                //        }
                //    }
                //    treesPerJob.BuildTree(job.job_id, l);
                //}

                if (job.job_id == 1)
                {
                    treesPerJob = new TreesPerJob();
                    List<ShiftPerJob> orderLstShiftPerJob = lstShiftPerJob.OrderBy(X => X.SumPriorities).Reverse().ToList();
                    treesPerJob.BuildTree(job.job_id, lstShiftPerJob);
                }
            }
            int VVV = 2;
        }

        public int SumPriorities(List<Employee_request_Dto> allSuitableEmployees)
        {
            int sum = 0;
            foreach (var employeeRequest in allSuitableEmployees)
                sum += employeeRequest.priority_id;
            return sum;
        }
    }


}
