using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
   public class PlacementToShiftByPriotity:PlacementToShift
    {
        public int SumPointsPriority { get; set; }
        public PlacementToShiftByPriotity():base()
        {
            SumPointsPriority = 0;
        }
    }
}
