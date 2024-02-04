using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLLAlgorithm
{
    public class PlacementLevelB
    {
        //Return a list of all the suitable combinations according to number of employees required
        public List<Employee_request_Dto[]> GetAllCombinationsForEmployeesList(List<Employee_request_Dto> lstEmployees, int numEmployeesRequired)
        {
            List<Employee_request_Dto[]> lstComb = new List<Employee_request_Dto[]>();
            Employee_request_Dto[] dataComb = new Employee_request_Dto[numEmployeesRequired];
            CombinationUtil(lstComb, dataComb, lstEmployees, 0, lstEmployees.Count(), 0, numEmployeesRequired);
            return lstComb;
        }

        public void CombinationUtil(List<Employee_request_Dto[]> lstComb, Employee_request_Dto[] dataComb,
          List<Employee_request_Dto> employees, int start, int end, int index, int numEmployeesRequired)
        {
            if (index == numEmployeesRequired)
            {
                lstComb.Add(dataComb);
                return;
            }

            for (int i = start; i <= end &&
                    end - i + 1 >= numEmployeesRequired - index; i++)
            {
                dataComb[index] = employees[i];
                CombinationUtil(lstComb, dataComb, employees, i + 1,
                                end, index + 1, numEmployeesRequired);
            }
        }

    }
}
