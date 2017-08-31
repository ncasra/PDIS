using CESEIT;
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
        private readonly Pathfinder _pathfinder;
        private readonly TLService _tlService;
        private readonly OAService _oaService;
        private Graph _africaGraph;

        public RouteManager()
        {
            _distanceProvider = new DistanceProvider();
            _pathfinder = new Pathfinder(_distanceProvider);
            _tlService = new TLService();
            _oaService = new OAService();
            //ConstructGraph();
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
            return new List<(RouteTypes, RouteInfo)>()
            {
                (RouteTypes.Cheapest, cheapestRoute),
                (RouteTypes.Fastest, fastestRoute),
                (RouteTypes.GoldenMiddleWay, aristotelesRoute),
                (RouteTypes.Greedy, greedyRoute),
            };
        }

        public string GetTelstar(string source, string target)
        {
            return JsonConvert.SerializeObject(_tlService.GetRoute(source, target, "2017-01-01", 1.0, 1.0, "WEAPONS", false).Result);
        }
        public string GetOceanic(string source, string target)
        {
            return JsonConvert.SerializeObject(_oaService.GetRoute(source, target, "2017-01-01", 1.0, 1.0, "WEAPONS", false).Result);
        }


    }
}

