using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public List<ShiftPerJob> AllJobs { get; set; }
    }
}
