using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
    public class ShiftPerJob
    {
        public int ShiftId { get; set; }
        public int JobId { get; set; }
        public List<EmployeeToPlacement> AllSuitableEmployees { get; set; }//Without filtering,
                                                                           //the employess that didn't
                                                                           //choose I can't to this shift.
        public int NumEmployeesRequired { get; set; }

        public int SumPriorities { get; set; }

        public ShiftPerJob() { }

        public ShiftPerJob(int shiftId, List<EmployeeToPlacement> allSuitableEmployees, int jobId, int numEmployeesRequired,int sumPriorities)
        {
            this.ShiftId = shiftId;
            this.JobId = jobId;
            //this.ShiftDay = shiftDay;
            this.AllSuitableEmployees = allSuitableEmployees;
            this.NumEmployeesRequired = numEmployeesRequired;
            this.SumPriorities = sumPriorities;
        }

    }
}
