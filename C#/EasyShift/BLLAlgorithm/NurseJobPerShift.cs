using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
   public class NurseJobPerShift
    {
        public int NurseJobId { get; set; }//מספור פנימי של התפקידים
        public ShiftPerJob ShiftPerJobBelong { get; set; }
    }
}
