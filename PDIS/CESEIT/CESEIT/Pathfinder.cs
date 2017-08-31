using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CESEIT
{
    public class Pathfinder
    {
        private DistanceProvider _distanceProvider;
        public Dictionary<Node,Node> Dijsktra(Graph graph, string source, string target, CargoType type, double weight)
        {
            Dictionary<Node, double> dist = new Dictionary<Node, double>();
            Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
            List<Node> unvisited = new List<Node>();
            foreach(Node node in graph.Nodes)
            {
                prev.Add(node, null);
                if (node.Name == source)
                {
                    dist.Add(node, 0);
                    unvisited.Add(node);
                    continue;
                }
                dist.Add(node, double.MaxValue);
                unvisited.Add(node);
            }

            while (unvisited.Count != 0)
            {
                Node u = unvisited.OrderBy(node => dist[node]).First();
                unvisited.Remove(u);
                if (u.Name == target)
                {
                    return prev;
                }

                foreach (var neighTup in u.Neighbors)
                {
                    Node v = neighTup.node;
                    EdgeType etype = neighTup.edgetype;
                    var newDist = dist[u] + _distanceProvider.Distance(u.Name, v.Name, etype, weight);
                    if (newDist < dist[v])
                    {
                        dist[v] = newDist;
                        prev[v] = u;
                    }
                }

            }
            throw new RouteNotFoundException();
        }

    }
}
