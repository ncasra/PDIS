using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIS.Pathfinder
{
    public class Graph
    {
        public List<Node> Nodes;

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public Graph(List<Node> nodes)
        {
            Nodes = nodes;
        }
    }
}
