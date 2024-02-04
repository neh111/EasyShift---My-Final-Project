using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLAlgorithm
{
    public class TreeNursePath
    {
        public List<InnerNursesNode> Path { get; set; }
        public TreeNursePath()
        {
            Path = new List<InnerNursesNode>();
        }
    }
}
