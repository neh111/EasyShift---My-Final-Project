using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
   public class SplitJobPerShift
    {
        public int SplitJobId { get; set; }//מספור פנימי של התפקידים
        public ShiftPerJob ShiftPerJobBelong { get; set; }
    }
}
