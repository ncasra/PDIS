﻿using CESEIT;
using Newtonsoft.Json;
using PDIS.Model;
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
        private Graph _africaGraph;

        public RouteManager()
        {
            _distanceProvider = new DistanceProvider();
            _pathfinder = new Pathfinder(_distanceProvider);
            ConstructGraph();
        }

        private void ConstructGraph()
        {
            _africaGraph = new Graph();
            HandleRoutes(EdgeType.Ship, "AfricaBoatGraph", _africaGraph);
            HandleRoutes(EdgeType.Car, "AfricaCarGraph", _africaGraph);
            HandleRoutes(EdgeType.Airplane, "AfricaAirplaneGraph", _africaGraph);            
        }

        private void HandleRoutes(EdgeType routeType, string sectionName, Graph africa)
        {
            NameValueCollection edges = (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            Dictionary<string, Node> nameNodes = new Dictionary<string, Node>();
            var keys = edges.AllKeys;
            foreach (var city in keys)
            {
                var node = new Node() { Name = city };
                nameNodes.Add(city, node);
                africa.Nodes.Add(node);
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


    }
}

