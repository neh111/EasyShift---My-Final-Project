using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLLAlgorithm
{
    public class TreePaths
    {
        public List<PossiblePlacementNode> PathInJobTree { get; set; }
        public TreePaths()
        {
            PathInJobTree = new List<PossiblePlacementNode>();
        }
    }
}
