using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLLAlgorithm
{
   public class EmployeeToPlacement
    {
        public Employee_request_Dto Employee_Request { get; set; }
        //public List<PossiblePlacementNode> SuitableSetNodes { get; set; }
       public int NumShiftsToPlacement { get; set; }
       public int NumShiftsLeftToPlacement { get; set; }

        public EmployeeToPlacement()//פעולה בונה ריקה
        {
            this.Employee_Request = new Employee_request_Dto();
        }

        public EmployeeToPlacement(Employee_request_Dto e,int numShiftsToPlacement,int numShiftsLeftToPlacement)
        {
            this.Employee_Request = e;
            this.NumShiftsToPlacement = numShiftsToPlacement;
            this.NumShiftsLeftToPlacement = numShiftsLeftToPlacement;
        }
        
    }
}
