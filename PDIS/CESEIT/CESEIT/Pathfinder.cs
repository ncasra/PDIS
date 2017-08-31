using PDIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PDIS.Pathfinder
{
    public class PathFinder
    {
        private DistanceProvider _distanceProvider;

        public PathFinder(DistanceProvider distanceProvider)
        {
            _distanceProvider = distanceProvider;
        }

        public RouteInfo GetRoute(Graph graph, string source, string target, CargoType type, double weight, double largestSize, DateTime date, (double, double) metric, bool preferShip = false)
        {
            RouteInfo info = new RouteInfo();
            var routeDict = Dijsktra(graph, source, target, type, weight, largestSize, date, metric, preferShip);
            info.RouteStops.Add(target);
            string currentnode = target;
            while (true)
            {
                Node prevNode;
                bool getSuccess = routeDict.TryGetValue(graph.Nodes.First(n => n.Name == currentnode), out prevNode);
                if (!getSuccess)
                    break;
                info.RouteStops.Insert(0, prevNode.Name);
                currentnode = prevNode.Name;
                if (currentnode == source)
                    break;
            }
            for (int i = 0; i<info.RouteStops.Count -1; i++)
            {
                var timeAndMoney = _distanceProvider.GetEdgeInfo(info.RouteStops[i], info.RouteStops[i + 1]);
                info.TotalCost += timeAndMoney.price;
                info.TotalTime += timeAndMoney.time;
            }
            return info;
        }


        private Dictionary<Node,Node> Dijsktra(Graph graph, string source, string target, CargoType type, double weight, double largestSize, DateTime date, (double time, double price) metric, bool preferShip = false)
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
                    var newDist = dist[u] + _distanceProvider.Distance(u.Name, v.Name, etype, weight, largestSize, type, date, metric, preferShip);
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
