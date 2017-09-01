using PDIS.Pathfinder;
using Newtonsoft.Json;
using PDIS.Model;
using ServiceGateway.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace PDIS.Managers
{
    public class RouteManager
    {
        private readonly DistanceProvider _distanceProvider;
        private readonly PathFinder _pathfinder;
        private readonly TLService _tlService;
        private readonly OAService _oaService;
        private Graph _africaGraph;
        private Dictionary<int, RouteInfo> _storedRoutes;
        private int _runningKey;
        private OrderManager _orderManager;

        public RouteManager()
        {
            _distanceProvider = new DistanceProvider();
            _pathfinder = new PathFinder(_distanceProvider);
            _tlService = new TLService();
            _oaService = new OAService();
            _storedRoutes = new Dictionary<int, RouteInfo>();
            _runningKey = 0;
            _orderManager = new OrderManager();
            ConstructGraph();
        }

        private void ConstructGraph()
        {
            _africaGraph = new Graph();
            Dictionary<string, Node> nameNodes = new Dictionary<string, Node>();
            HandleRoutes(EdgeType.Ship, "AfricaBoatGraph", _africaGraph, nameNodes);
            HandleRoutes(EdgeType.Car, "AfricaCarGraph", _africaGraph, nameNodes);
            HandleRoutes(EdgeType.Airplane, "AfricaAirplaneGraph", _africaGraph, nameNodes);            
        }

        private void HandleRoutes(EdgeType routeType, string sectionName, Graph africa, Dictionary<string, Node> nameNodes)
        {
            NameValueCollection edges = (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            var keys = edges.AllKeys;
            foreach (var city in keys)
            {
                Node node;
                bool alreadyIndexedCity = nameNodes.TryGetValue(city, out node);
                if (!alreadyIndexedCity)
                {
                    node = new Node() { Name = city };
                    nameNodes.Add(city, node);
                    africa.Nodes.Add(node);
                }
                
            }
            foreach (var city in keys)
            {
                var neighstring = edges.Get(city);
                List<string> neighs = new List<string>();
                neighs.AddRange(neighstring.Split(','));
                neighs = neighs.Select(s => s.Trim()).ToList(); //Pretty dirty
                foreach (var neighName in neighs)
                {
                    nameNodes[city].Neighbors.Add((nameNodes[neighName], routeType));
                }
            }
        }

        public List<string> GetCities()
        {
            return _africaGraph.Nodes.Select(s => s.Name).ToList();
        }

        public string GetRouteInfo(string source, string target, string cargoType, string weightInKg, string largestSizeInCm, string shipmentDate)
        {
            var type = (CargoType)Enum.Parse(typeof(CargoType), cargoType);
            var weight = double.Parse(weightInKg);
            var largest = double.Parse(largestSizeInCm);
            var date = DateTime.Parse(shipmentDate);
            var size = double.Parse(largestSizeInCm);
            var prelim = GetRouteInfo2(source, target, type, weight, size, date);
            var jstring = JsonConvert.SerializeObject(prelim);
            return jstring;
            

        }

        public List<(RouteTypes, RouteInfo)> GetRouteInfo2(string source, string target, CargoType cargoType, double weight, double largestSize, DateTime shipmentDate)
        {
            var cheapestRoute = _pathfinder.GetRoute(_africaGraph, source, target, cargoType, weight, largestSize, shipmentDate, (0, 1));
            var fastestRoute = _pathfinder.GetRoute(_africaGraph, source, target, cargoType, weight, largestSize,shipmentDate, (1, 0));
            var aristotelesRoute = _pathfinder.GetRoute(_africaGraph, source, target, cargoType, weight, largestSize,shipmentDate, (1, 1));
            var greedyRoute = _pathfinder.GetRoute(_africaGraph, source, target, cargoType, weight, largestSize, shipmentDate, (1, 1), preferShip: true);
            cheapestRoute.RouteId = _runningKey;
            _storedRoutes.Add(_runningKey, cheapestRoute);
            _runningKey++;
            fastestRoute.RouteId = _runningKey;
            _storedRoutes.Add(_runningKey, fastestRoute);
            _runningKey++;
            aristotelesRoute.RouteId = _runningKey;
            _storedRoutes.Add(_runningKey, aristotelesRoute);
            _runningKey++;
            greedyRoute.RouteId = _runningKey;
            _storedRoutes.Add(_runningKey, greedyRoute);
            return new List<(RouteTypes, RouteInfo)>()
            {
                (RouteTypes.Cheapest, cheapestRoute),
                (RouteTypes.Fastest, fastestRoute),
                (RouteTypes.GoldenMiddleWay, aristotelesRoute),
                (RouteTypes.Greedy, greedyRoute),
            };
        }


        public bool BuyRoute(int routeId, string type, double weight, string discount)
        {
            RouteInfo routeinf;
            var tryget = _storedRoutes.TryGetValue(routeId, out routeinf);
            if (!tryget)
                return false;
            return _orderManager.CreateInternalOrder(routeinf, type.ToString(), weight, double.Parse(discount));
        }

        public string GetTelstar(string source, string target)
        {
            var theresult = _tlService.GetRoute(source, target, "2017-01-01", 1.0, 1.0, "WEAPONS", false).Result;
            return JsonConvert.SerializeObject(theresult);
        }
        public string GetOceanic(string source, string target)
        {
            return JsonConvert.SerializeObject(_oaService.GetRoute(source, target, "2017-01-01", 1.0, 1.0, "WEAPONS", false).Result);
        }


    }
}

