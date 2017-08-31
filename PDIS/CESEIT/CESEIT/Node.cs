using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESEIT
{
    public class Node
    {
        public List<(Node node, EdgeType edgetype)> Neighbors;
        public string Name;


        public Node(List<(Node, EdgeType)> neighs, string name)
        {
            Neighbors = neighs;
            Name = name;
        }
    }
}
