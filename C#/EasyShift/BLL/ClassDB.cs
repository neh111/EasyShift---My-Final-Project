using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DAL;
using DTO;


namespace BLL
{
    //מחלקה המיועדת לשליפות שונות ממסד הנתונים
    public class ClassDB
    {
        public static EasyShiftEntities db { get; set; }
        public static List<Employee_Dto> Employees { get; set; }
        public static List<Employee_request_Dto> EmployeesRequests { get; set; }
        public static List<Jobs_Dto> Jobs { get; set; }
        public static List<Jobs_to_shift_Dto> JobsToShift { get; set; }
        public static List<Priority_Dto> Priorities { get; set; }
        public static List<Shift_Dto> Shifts { get; set; }
        public static List<ShiftType_Dto> ShiftTypes { get; set; }
        public static List<PlacementResults_Dto> PlacementResults { get; set; }
        public static Settings_Dto Settings { get; set; }
        static ClassDB()
        {
            db = new EasyShiftEntities();
            Employees = GetAllEmployeesDto();
            EmployeesRequests = GetAllEmployeesRequest();
            Jobs = GetAllJobs();
            JobsToShift = GetAllJobsToShift();
            Priorities = GetAllPrioritiesReact();
            ShiftTypes = GetAllShiftTypesByList();
            EditSequenceInShiftsTypes(ShiftTypes);
            Shifts = GetAllShifts();
            EditSequenceInShifts(Shifts);
            PlacementResults = GetAllPlacement_results();
            Settings = GetSettings();
        }

        /////////////////////////////////Settings//////////////////////////
        public static Settings_Dto GetSettings()
        {
            return Settings_Dto.DalToDto(db.Settings_tbl.FirstOrDefault());
        }



        ///////////////////////////Employee///////////////////////////////////
        //פונקציה לבדיקת תקינות עובד ע"י קבלת סיסמא ותז
        public bool GetemployeePassAndId(int pass, string id)
        {
            return Employees.Exists(e => e.employee_id == pass && e.employee_id_str == id);
        }
        public int GetEmployeeByIdStr(int empIdStr)
        {
            return Employees.Where(e => e.employee_id == empIdStr).FirstOrDefault().job_id;
        }
        public int GetEmployeeByIdStr2(string empIdStr)
        {
            return Employees.Where(e => e.employee_id_str == empIdStr).FirstOrDefault().job_id;
        }
        public int AddEmployee(Employee_Dto e)
        {
            db.Employee_tbl.Add(e.DtoTODal());
            db.SaveChanges();
            Employees = GetAllEmployeesDto();
            return GetEmployeeByIdStr2(e.employee_id_str);
        }
        public int GetEmployeeJobIdByDescription(string description)
        {
            return Jobs.Where(x => x.description == description).FirstOrDefault().job_id;
        }
        //public RequestResult GetAllEmployees()
        //{
        //    List<Employee_Dto> list = new List<Employee_Dto>();
        //    int i = 5;
        //    //foreach (var item in db.Employee_tbl.ToList())
        //    //{
        //    //    list.Add(Employee_Dto.DalToDto(item));
        //    //}
        //    return new RequestResult() { Data = i, Message = "success", Status = true };
        //}
        public static List<Priority_Dto> GetAllPrioritiesReact()
        {
            List<Priority_Dto> list = new List<Priority_Dto>();
            foreach (var item in db.Priority_tbl.ToList())
                list.Add(Priority_Dto.DalToDto(item));
            return list;
        }
        public static List<Employee_Dto> GetAllEmployeesDto()
        {
            List<Employee_Dto> list = new List<Employee_Dto>();
            foreach (var item in db.Employee_tbl.ToList())
                list.Add(Employee_Dto.DalToDto(item));
            return list;
        }
        public Employee_Dto GetEmployeeById(int empId)
        {
            return Employees.Where(x => x.employee_id == empId).FirstOrDefault();
        }
        //public void AddEmployee(Employee_Dto e)
        //{
        //    //if (e is object)
        //    // {
        //    db.Employee_tbl.Add(e.DtoTODal());
        //    int i = db.SaveChanges();
        //    Console.WriteLine(i);
        //    // }
        //}

        public int GetNumShiftsInWeekByEmpId(int employeeId)
        {
            return Employees.Where(x => x.employee_id == employeeId).FirstOrDefault().num_shifts_in_week;
        }
        public int GetPriorityIdByDescription(string description)
        {
            return Priorities.Where(x => x.priority_description == description).FirstOrDefault().priority_id;
        }
        public List<Employee_Dto> GetEmployeesByJobId(int jobId)
        {
            return Employees.Where(x => x.job_id == jobId).ToList();
        }

        public int? GetSeniorityYearsById(int employeeId)
        {
            return Employees.Where(x => x.employee_id == employeeId).FirstOrDefault().seniority_years;
        }
        /////////////////////////Employee_request///////////////////////////////
        public static List<Employee_request_Dto> GetAllEmployeesRequest()
        {
            List<Employee_request_Dto> list = new List<Employee_request_Dto>();
            foreach (var item in db.Employee_request_tbl.ToList())
                list.Add(Employee_request_Dto.DalToDto(item));
            return list;
        }

        //public Employee_request_Dto BuildInstanceOfEmployeeRequestAndSaveChangesInDb(int empId, int shiftId, int jobId, int priorityId, int status)
        //{
        //    Employee_request_Dto e = new Employee_request_Dto();
        //    e.employee_id = empId;
        //    e.shift_id = shiftId;
        //    e.job_id = jobId;
        //    e.priority_id = priorityId;
        //    e.status = status;
        //    e.request_date = DateTime.Now;
        //    db.Employee_request_tbl.Add(e.DtoTODal());
        //    db.SaveChanges();
        //    Employees = GetAllEmployeesDto();
        //    return e;
        //}

        public void AddEmployeesRequest(List<Employee_request_Dto> lstRequests)
        {
            foreach (var item in lstRequests)
                db.Employee_request_tbl.Add(item.DtoTODal());
            db.SaveChanges();
            Employees = GetAllEmployeesDto();
        }

        //הפעולה מחזירה את כל בקשות העובדים שהגישו בקשה למשמרת ספציפית עבור תפקיד ספציפי
        public List<Employee_request_Dto> GetEmployeeReqByShiftAndJobId(int shiftId, int jobId, DateTime start, DateTime end)
        {
            return EmployeesRequests.Where(x => x.shift_id == shiftId && x.job_id == jobId && x.request_date >= start && x.request_date <= end).ToList();
        }
        ///////////////////////Placement_results/////////////////////////
        public static List<PlacementResults_Dto> GetAllPlacement_results()
        {
            List<PlacementResults_Dto> list = new List<PlacementResults_Dto>();
            foreach (var item in db.PlacementResults_tbl.ToList())
                list.Add(PlacementResults_Dto.DalToDto(item));
            return list;
        }

        public void SavePlacementsResultsPerJob(List<PlacementResults_Dto> placement, List<int> empReqIdies)
        {
            foreach (var item in placement)
                db.PlacementResults_tbl.Add(item.DtoTODal());
            foreach (var id in empReqIdies)
                db.Employee_request_tbl.Where(x => x.employee_request_id == id).FirstOrDefault().status = 1;
            db.SaveChanges();
            PlacementResults = GetAllPlacement_results();
            EmployeesRequests = GetAllEmployeesRequest();
        }
        ///////////////////////Shift////////////////////////////
        public static List<Shift_Dto> GetAllShifts()
        {
            List<Shift_Dto> list = new List<Shift_Dto>();
            foreach (var item in db.Shift_tbl.ToList())
                list.Add(Shift_Dto.DalToDto(item));
            return list;
        }

        public static List<ShiftType_Dto> GetAllShiftTypesByList()
        {
            List<ShiftType_Dto> list = new List<ShiftType_Dto>();
            foreach (var item in db.ShiftType_tbl.ToList())
                list.Add(ShiftType_Dto.DalToDto(item));
            return list;
        }
        public List<string> GetAllShiftsType()
        {
            List<string> timesShift = new List<string>();
            foreach (var item in ShiftTypes)
                timesShift.Add(item.beginning_time.ToString() + "-" + item.end_time.ToString());
            return timesShift;
        }
        public static void EditSequenceInShiftsTypes(List<ShiftType_Dto> Shifts)
        {
            for (int i = 1; i < Shifts.Count(); i++)
                if (CheckTimesOfTwoShifts(Shifts[i - 1], Shifts[i]) == 1)
                    Shifts[i].IsPermittedSequence = 1;
        }

        public static void EditSequenceInShifts(List<Shift_Dto> Shifts)
        {
            for (int i = 1; i < Shifts.Count(); i++)
            {
                //אם רצף המשמרות לא באותו יום ולא יום אחרי יום
                if (Shifts[i].day - Shifts[i - 1].day > 1)//למשמרת זו מותר לשבץ עובדים ישר לאחר המשמרת שקודמת לה
                    Shifts[i].IsPermittedSequence = 1;
                //אם זה באותו יום
                if (Shifts[i].day - Shifts[i - 1].day == 0)
                    if (GetIsPermittedShiftType(Shifts[i].shift_type_id) == 1)
                        Shifts[i].IsPermittedSequence = 1;
                //אם זה יום אחרי
                if (Shifts[i].day - Shifts[i - 1].day == 1)
                    CheckIsPermittedTwoShiftsDayAfter(Shifts[i - 1], Shifts[i]);
            }
        }

        public static void CheckIsPermittedTwoShiftsDayAfter(Shift_Dto shift1, Shift_Dto shift2)
        {
            if (CheckTimesOfTwoShifts(GetShiftType(shift1.shift_type_id), GetShiftType(shift2.shift_type_id)) == 1)
                shift2.IsPermittedSequence = 1;
        }

        public static ShiftType_Dto GetShiftType(int shiftTypeId)
        {
            return ShiftTypes.Where(x => x.shift_type_id == shiftTypeId).FirstOrDefault();
        }

        public static int CheckTimesOfTwoShifts(ShiftType_Dto shift1, ShiftType_Dto shift2)
        {
            TimeSpan endTimePrev = shift1.end_time;
            TimeSpan beginningTime = shift2.beginning_time;
            if (endTimePrev.Subtract(beginningTime).TotalMinutes > 60)//לבדוק איך עושים השוואת זמנים
                return 1;
            return 0;
        }

        public static int GetIsPermittedShiftType(int shiftTypeId)
        {
            return ShiftTypes.Where(x => x.shift_type_id == shiftTypeId).FirstOrDefault().IsPermittedSequence;
        }
        //public List<string> GetAllShiftsType()
        //{
        //    List<ShiftType_Dto> list = new List<ShiftType_Dto>();
        //    List<string> timesShift = new List<string>();
        //    foreach (var item in db.ShiftType_tbl.ToList())
        //        list.Add(ShiftType_Dto.DalToDto(item));
        //    foreach (var item in list)
        //        timesShift.Add(item.beginning_time.ToString() + "-" + item.end_time.ToString());
        //    return timesShift;
        //}
        public int GetShiftDay(int shiftId)
        {
            return Shifts.Where(x => x.shift_id == shiftId).FirstOrDefault().day;
        }
        public Shift_Dto GetShiftById(int shiftId)
        {
            return Shifts.Where(x => x.shift_id == shiftId).FirstOrDefault();
        }

        public int GetNumShifts()
        {
            return Shifts.Count();
        }
        //public List<string> GetAllShiftsType()
        //{
        //    List<ShiftType_Dto> list = new List<ShiftType_Dto>();
        //    List<string> timesShift = new List<string>();
        //    foreach (var item in db.ShiftType_tbl.ToList())
        //        list.Add(ShiftType_Dto.DalToDto(item));
        //    foreach (var item in list)
        //        timesShift.Add(item.beginning_time.ToString() + "-" + item.end_time.ToString());
        //    return timesShift;
        //}


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
        ///////////////////////Priority////////////////////////////
        public RequestResult GetAllPriorities()
        {
            List<Priority_Dto> list = new List<Priority_Dto>();
            foreach (var item in db.Priority_tbl.ToList())
                list.Add(Priority_Dto.DalToDto(item));
            return new RequestResult() { Data = list, Message = "success", Status = true };
        }
        public List<Priority_Dto> GetAllPrioritiesByList()
        {
            List<Priority_Dto> list = new List<Priority_Dto>();
            foreach (var item in db.Priority_tbl.ToList())
                list.Add(Priority_Dto.DalToDto(item));
            return list;
        }

        public int GetPriiorityIdByDescription(string description)
        {
            return Priorities.Where(x => x.priority_description == description).FirstOrDefault().priority_id;
        }

        //////////////////////Jobs_to_shift_tbl/////////////////////
        //
        public static List<Jobs_to_shift_Dto> GetAllJobsToShift()
        {
            List<Jobs_to_shift_Dto> list = new List<Jobs_to_shift_Dto>();
            foreach (var item in db.Jobs_to_shift_tbl.ToList())
                list.Add(Jobs_to_shift_Dto.DalToDto(item));
            return list;
        }
        public List<Jobs_to_shift_Dto> GetJobsToShiftByJob(int jobId)
        {
            return JobsToShift.Where(x => x.job_id == jobId).ToList();

        }
        
        public int GetNumEmpRequiredByShiftAndJobId(int shiftId, int jobId)
        {
            return JobsToShift.Where(x => x.shift_id == shiftId && x.job_id == jobId).FirstOrDefault().num_employees_requierd;
        }




        ///////////////////////////Jobs////////////////////////
        public static List<Jobs_Dto> GetAllJobs()
        {
            List<Jobs_Dto> list = new List<Jobs_Dto>();
            foreach (var item in db.Jobs_tbl.ToList())
                list.Add(Jobs_Dto.DalToDto(item));
            return list;
        }

        ////////////////////דברים שצריך להוסיף בשביל הריאקט


        //פונקציה שמקבלת מבנה של משמרות ושומרת בדטה בייס
        public void SaveShiftType(List<ShiftType_Dto> shiftType_Dtos)
        {
            //לבדוק לפי הקישור
            //למחוק את תוכן הטבלה הקודם ואז לשמור
            foreach (var shiftType in shiftType_Dtos)
                db.ShiftType_tbl.Add(shiftType.DtoTODal());
            db.SaveChanges();
            ShiftTypes = GetAllShiftTypesByList();
        }


        //פונקציה שמביאה ימי משמרות בסטרינג
        public List<string> DaysNames()
        {
            List<string> days = new List<string>() { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת" };
            List<string> daysToRet = new List<string>();
            foreach (var shift in Shifts.ToList())
            {
                daysToRet.Add(days[shift.day - 1]);
            }
            days = days.Distinct().ToList();
            return days;
        }


        public void SaveShiftStructure(List<ShiftType_Dto> shiftType_Dtos, int[] days, int numShiftsInDays)
        {
            SaveShiftType(shiftType_Dtos);
            Shift_tbl shift;
            foreach (var day in days)
            {
                for (int i = 0; i < numShiftsInDays; i++)
                {
                    shift = new Shift_tbl();
                    shift.day = day;
                    shift.shift_type_id = i + 1;
                    db.Shift_tbl.Add(shift);
                }
            }
            db.SaveChanges();
        }


        public int GetEmployeeJobByIdStr(string empIdStr)
        {
            return db.Employee_tbl.Where(e => e.employee_id_str == empIdStr).FirstOrDefault().job_id;
        }
        //פונקציה שמקבלת מבנה משמרות וליסט של מספרי ימים ומספר בודד המייצג מספר משמרות בכל יום
        //public void SaveShiftStructure(List<ShiftType_Dto> shiftType_Dtos, List<int> days, int numShiftsInDays)
        //{
        //    int[] countArr = new int[8];
        //    for (int i = 0; i < countArr.Length; i++)
        //        countArr[days[i]]++;
        //    List<int> newDays = new List<int>();
        //    for (int i = 0; i < countArr.Length; i++)
        //        if (countArr[i] % 2 != 0)
        //            newDays.Add(i);
        //    SaveShiftType(shiftType_Dtos);
        //    Shift_tbl shift;
        //    foreach (var day in newDays)
        //    {
        //        for (int i = 0; i < numShiftsInDays; i++)
        //        {
        //            shift = new Shift_tbl();
        //            shift.day = day;
        //            shift.shift_type_id = i + 1;
        //            db.Shift_tbl.Add(shift);
        //        }
        //    }
        //    db.SaveChanges();
        //    Shifts = GetAllShifts();
        //}
        //מטריצה של אחדות ואפסים עבור עובד מתאריך מסוים כדי להראות אם הוא משובץ או לא
        public int[,] PlacementResultPerEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            List<PlacementResults_Dto> results = PlacementResults.Where(x => x.employee_id == employeeId &&
             x.placement_date.Year >= startDate.Year &&
             x.placement_date.Month >= startDate.Month &&
             x.placement_date.Day >= startDate.Day &&
             x.placement_date.Year <= endDate.Year &&
             x.placement_date.Month <= endDate.Month &&
             x.placement_date.Day <= endDate.Day)
                .ToList();
            int days_i = DaysNames().Count(), shiftsInDays_j = ShiftTypes.Count();
            return EditMatPlacement(results, days_i, shiftsInDays_j);
        }

        public int[,] EditMatPlacement(List<PlacementResults_Dto> results, int days_i, int shiftsInDays_j)
        {
            int[,] placement = new int[days_i, shiftsInDays_j];
            int i, j, shift_id;
            foreach (var res in results)
            {
                shift_id = res.shift_id;
                if (shift_id % shiftsInDays_j == 0)
                {
                    i = shift_id / shiftsInDays_j - 1;
                    j = shiftsInDays_j - 1;
                }
                else
                {
                    i = shift_id / shiftsInDays_j;
                    j = shift_id % shiftsInDays_j - 1;
                }
                placement[i, j] = 1;
            }
            return placement;
        }
        //מטריצה להצגת שיבוץ כללי 
        public List<List<string>>[,] FinalPlacement(DateTime startDate, DateTime endDate)
        {
            int days_i = DaysNames().Count(), shiftsInDays_j = ShiftTypes.Count();
            List<List<string>>[,] placement = new List<List<string>>[days_i, shiftsInDays_j];
            //אתחול המטריצה
            for (int x = 0; x < days_i; x++)
                for (int y = 0; y < shiftsInDays_j; y++)
                    placement[x, y] = new List<List<string>>();

            List<PlacementResults_Dto> allResults = PlacementResults.Where(x =>
             x.placement_date.Year >= startDate.Year &&
             x.placement_date.Month >= startDate.Month &&
             x.placement_date.Day >= startDate.Day &&
             x.placement_date.Year <= endDate.Year &&
             x.placement_date.Month <= endDate.Month &&
             x.placement_date.Day <= endDate.Day)
                .ToList();
            int i, j, shift_id;
            foreach (var res in allResults)
            {
                shift_id = res.shift_id;
                if (shift_id % shiftsInDays_j == 0)
                {
                    i = shift_id / shiftsInDays_j - 1;
                    j = shiftsInDays_j - 1;
                }
                else
                {
                    i = shift_id / shiftsInDays_j;
                    j = shift_id % shiftsInDays_j - 1;
                }
                placement[i, j].Add(EmployeeDetails(res.employee_id));
            }
            return placement;
        }

        public List<string> EmployeeDetails(int empId)
        {
            List<string> details = new List<string>();
            Employee_Dto e = Employees.Where(x => x.employee_id == empId).FirstOrDefault();
            details.Add(e.first_name + " " + e.last_name);
            details.Add(e.employee_id_str);
            details.Add(e.job_id.ToString());
            return details;
        }

        public void AddRequestsForEmployee(int empid, string[,] matrix)
        {
            int shift_id = 0;
            Employee_request_Dto empReq = new Employee_request_Dto();
            empReq.employee_id = empid;
            for (int i = 0; i < Shifts.Count() / ShiftTypes.Count(); i++)
            {
                for (int j = 0; j < ShiftTypes.Count(); j++)
                {
                    shift_id++;
                    empReq.shift_id = shift_id;
                    empReq.job_id = Employees.Where(x => x.employee_id == empid).FirstOrDefault().job_id;
                    if (matrix[i, j] != null)
                        empReq.priority_id = Convert.ToInt32(matrix[i, j]);
                    else
                        empReq.priority_id = GetPriorityIdByDescription("ניטרלי");
                    empReq.request_date = DateTime.Now;
                    db.Employee_request_tbl.Add(empReq.DtoTODal());
                }
            }
            db.SaveChanges();
            EmployeesRequests = GetAllEmployeesRequest();
        }

        public void AddManagerRequest(int jobId, string[,] matrix)
        {
            int shift_id = 0;
            Jobs_to_shift_Dto jobsTo = new Jobs_to_shift_Dto();
            jobsTo.job_id = jobId;
            for (int i = 0; i < Shifts.Count() / ShiftTypes.Count(); i++)
            {
                for (int j = 0; j < ShiftTypes.Count(); j++)
                {
                    if (matrix[i, j] != null)
                    {
                        shift_id++;
                        jobsTo.shift_id = shift_id;
                        jobsTo.num_employees_requierd = Convert.ToInt32(matrix[i, j]);
                        jobsTo.request_date = DateTime.Today;
                        db.Jobs_to_shift_tbl.Add(jobsTo.DtoTODal());
                    }
                }
            }
            db.SaveChanges();
            JobsToShift = GetAllJobsToShift();
        }
    }
}