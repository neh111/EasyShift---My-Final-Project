using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL;
using DTO;
namespace WebApiEasyShift.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        ClassDB classDB = new ClassDB();
        
        [HttpGet]
        public List<Priority_Dto> GetAllPriority()
        {
            List<Priority_Dto> result = ClassDB.GetAllPrioritiesReact();
            return result;
        }
        [HttpGet]
        public List<Jobs_Dto> GetAllJob()
        {
            List<Jobs_Dto> r = ClassDB.GetAllJobs();
            return r;
        }
        [HttpGet]
        public List<string> GoToGetAllShiftsType()
        {
            List<string> r = classDB.GetAllShiftsType();
            return r;
        }
        [HttpGet]
        public int[,] GoToPlacementResultPerEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            int[,] r = classDB.PlacementResultPerEmployee(employeeId, startDate, endDate);
            return r;
        }
        [HttpGet]
        public List<List<string>>[,] GoToFinalPlacement(DateTime startDate, DateTime endDate)
        {
            List<List<string>>[,] r = classDB.FinalPlacement(startDate, endDate);
            return r;
        }
        
        [HttpGet]
        public bool GetemployeeByPassAndId(string id, int pass)
        {
            RequestResult request = new RequestResult();
            request.Status = classDB.GetemployeePassAndId(pass, id);
            return request.Status;
        }
        [HttpPost]
        public int AddEmp(Employee_Dto e)
        {
            return classDB.AddEmployee(e);
        }
        [HttpGet]
        public int GoToGetEmployeeJobIdByDescription(string desc)
        {
            return classDB.GetEmployeeJobIdByDescription(desc);
        }
        [HttpGet]
        public int GoToGetEmployeeJobByIdStr(string pass)
        {
            return classDB.GetEmployeeJobByIdStr(pass);
        }
        [HttpGet]
        public List<int> GoToGetJobsToShiftByJob(int jobId, DateTime start, DateTime end)
        {
            List<Jobs_to_shift_Dto> result = classDB.GetJobsToShiftByJobAndDate(jobId, start, end);
            List<int> numEmployeeRequird = new List<int>();
            foreach (var item in result)
                numEmployeeRequird.Add(item.num_employees_requierd);
            return numEmployeeRequird;
        }
        [HttpGet]
        public List<string> GoToDaysNames()
        {
            List<string> te = classDB.DaysNames();
            return te;
        }
        //פונקציה שמחזירה את כותרות הימים לפי מספר הימים של המנהל
        [HttpPost]
        public void AddRequst(int jobId, string[,] matrix)
        {
            classDB.AddManagerRequest(jobId, matrix);
        }
        //פונקציה המקבלת קוד עובד ואת טבלה של בחירת עדיפות של כל משמרת
        [HttpPost]
        public void GetPriorityTable(int empid, string[,] matrix)
        {
            classDB.AddRequestsForEmployee(empid, matrix);
        }
        [HttpPost]
        public void GoToSaveShiftStructure([FromBody] List<ShiftType_Dto> shiftType_Dtos, [FromUri] int[] days, int numShiftsInDays)
        {
            classDB.SaveShiftStructure(shiftType_Dtos, days, numShiftsInDays);
        }
        // PUT: api/Employee/5
        public void Put(int id, [FromBody] string value)
        {
        }

        
        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }

    }
}