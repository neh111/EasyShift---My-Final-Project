using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
    public class ArrrayCombEmployees
    {
        public EmployeeToPlacement[] CombArray { get; set; }
        public ArrrayCombEmployees()
        {
                
        }
        public ArrrayCombEmployees(int length)
        {
            CombArray = new EmployeeToPlacement[length];
        }

        public bool IsExistInArray(EmployeeToPlacement e)
        {
            for (int i = 0; i < CombArray.Length; i++)
            { 
                if (CombArray[i].Employee_Request.employee_id==e.Employee_Request.employee_id)
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
