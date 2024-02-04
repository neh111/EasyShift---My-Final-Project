using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BLLAlgorithm
{
   public class CombPerJob
    {
        public int JobId { get; set; }
        public int NumEmpReq { get; set; }
        public  List<Employee_request_Dto[]> LstComb{ get; set; }
        public CombPerJob()
        {
            LstComb = new List<Employee_request_Dto[]>();
        }
        public CombPerJob(int jobId,int numEmpReq)
        {
            this.JobId = jobId;
            this.NumEmpReq = numEmpReq;
            this.LstComb = new List<Employee_request_Dto[]>();
        }
    }
}
