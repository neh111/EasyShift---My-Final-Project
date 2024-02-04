using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DTO;

namespace BLL
{
    public class Algorithm
    {
        public static ClassDB ClassDB { get; set; }
        public int NumShifts { get; set; }
        public Algorithm()
        {
            ClassDB = new ClassDB();
            NumShifts = ClassDB.GetNumShifts();
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
                numShiftsToPlacement = ClassDB.GetNumShiftsInWeekByEmpId(item.employee_id);
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
        public void BuildEmployeeReqToEmployeesNotSubmitReq(List<Employee_request_Dto> employeesSubmitedReqThisShift, int jobId, int shiftId)
        {
            List<Employee_Dto> employee_Dtos = ClassDB.GetEmployeesByJobId(jobId);
            foreach (var emp in employee_Dtos)
            {
                CheckRequests(employeesSubmitedReqThisShift, emp, jobId, shiftId);
            }
        }

        public void CheckRequests(List<Employee_request_Dto> employeesSubmitedReqThisShift,Employee_Dto employee, int jobId, int shiftId)
        {
            bool flag=true;
            Employee_request_Dto e;
            List<Employee_request_Dto> requestsToAdd = new List<Employee_request_Dto>();
            foreach (var empReq in employeesSubmitedReqThisShift)
            {
                if (employee.employee_id == empReq.employee_id)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                e = BuildInstanceOfEmployeeRequestAndSaveChangesInDb(employee.employee_id, shiftId, jobId,ClassDB.GetPriorityIdByDescription("ניטרלי"), 0);
                requestsToAdd.Add(e);
                employeesSubmitedReqThisShift.Add(e);
            }
            if(requestsToAdd.Count()>0)
            { 
            lock(ClassDB)
            {
                ClassDB.AddEmployeesRequest(requestsToAdd);
            }
            }
        }

        public Employee_request_Dto BuildInstanceOfEmployeeRequestAndSaveChangesInDb(int empId, int shiftId, int jobId, int priorityId, int status)
        {
            Employee_request_Dto e = new Employee_request_Dto();
            e.employee_id = empId;
            e.shift_id = shiftId;
            e.job_id = jobId;
            e.priority_id = priorityId;
            e.status = status;
            e.request_date = DateTime.Now;
            return e;
        }
        public void RunAlgorithm()
        {
            List<Task> tasks = new List<Task>();
            TreesPerJob treesPerJob = new TreesPerJob();
            ////מעבר על כל התפקידים
            foreach (var job in ClassDB.GetAllJobs())
            {
                Task t = Task.Run(() => BuildJobTree(job));
                tasks.Add(t);
            }
            tasks.ForEach(n => n.Wait());
        }

        /// <summary>
        /// builds a tree options for one job
        /// </summary>
        /// <param name="treesPerJob">an root tree object</param>
        /// <param name="lstJobToShift">list of required employess</param>
        /// <param name="job">the job which the tree is builds for</param>
        private void BuildJobTree(Jobs_Dto job)
        {
            //מביא רשימה של תפקידים למשמרות עבור התפקיד הספציפי הזה
            DateTime start = new DateTime(2021, 12, 26);
            DateTime end = new DateTime(2021, 12, 26);
            List<Jobs_to_shift_Dto> lstJobToShift = new List<Jobs_to_shift_Dto>();
            lstJobToShift = ClassDB.GetJobsToShiftByJobAndDate(job.job_id, start, end);
            List<ShiftPerJob> lstShiftPerJob = new List<ShiftPerJob>();
            //בונה רשימה של כל המשמרות שעבורם דרוש תפקיד זה
            lstJobToShift.ForEach(x => BuildJobsToShift(lstShiftPerJob,x));
            List<ShiftPerJob> orderLstShiftPerJob = lstShiftPerJob.OrderBy(X => X.SumPriorities).Reverse().ToList();
            new TreesPerJob().BuildTree(job.job_id, lstShiftPerJob, NumShifts);
        }

        private void BuildJobsToShift(List<ShiftPerJob> lstShiftPerJob,Jobs_to_shift_Dto jobToShift)
        {
            DateTime start = new DateTime(2022, 06, 09);
            DateTime end = new DateTime(2022, 06, 10);
            //רשימה של כל העובדים שיש להם בקשה עבור התפקיד הנוכחי והמשמרת הנוכחית
            List<Employee_request_Dto> allSuitableEmployees = ClassDB.GetEmployeeReqByShiftAndJobId(jobToShift.shift_id, jobToShift.job_id,start,end);
             //יש לקחת את כל העובדים שלא הגישו בקשה למשמרת הספציפית הזו אך באופן כללי עובדים
            //בתפקיד הספציפי הזה ולעשות שתהיה להם בקשה עבור המשמרת הזו ברמת עדיפות ניטרלית
            BuildEmployeeReqToEmployeesNotSubmitReq(allSuitableEmployees, jobToShift.job_id, jobToShift.shift_id);
            //על סמך הרשימה לעיל נבנה רשימה של EmployeeToPlacement
            //בניית רשימת EmployeeToPlacement
            List<EmployeeToPlacement> e = ConvertFromListOfEmployee_request_DtoToEmployeeToPlacement(allSuitableEmployees);
            ShiftPerJob tempShiftPerJob = new ShiftPerJob(jobToShift.shift_id, e, jobToShift.job_id, ClassDB.GetNumEmpRequiredByShiftAndJobId(jobToShift.shift_id, jobToShift.job_id), SumPriorities(allSuitableEmployees));
            lstShiftPerJob.Add(tempShiftPerJob);
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
