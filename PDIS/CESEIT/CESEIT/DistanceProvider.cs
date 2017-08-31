using PDIS.DataAccess;
using ServiceGateway.Models;
using ServiceGateway.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESEIT
{
    public class DistanceProvider
    {
        //Cache already requested edges
        private Dictionary<(string start, string finish), (double time, double price)> edgeInfo;
        private TLService _tlService;
        private OAService _oaService;
        private PriceRepository _priceRepo;

        public DistanceProvider()
        {
            edgeInfo = new Dictionary<(string start, string finish), (double time, double price)>();
            _tlService = new TLService();
            _oaService = new OAService();
            _priceRepo = new PriceRepository();
        }


        public (double time, double price) GetEdgeInfo(string start, string finish)
        {
            (double time, double price) info;
            var success = edgeInfo.TryGetValue((start, finish), out info);
            if (!success)
            {
                var success2 = edgeInfo.TryGetValue((finish, start), out info);
                if (!success2)
                {
                    return (double.MaxValue, double.MaxValue);
                }
            }
            return info;
        }

        public double Distance(string source, string target, EdgeType edgetype, double weight, double largestSizeInCm, CargoType cargoType, DateTime shipmentDate, (double time, double price) metric, bool preferShip = false)
        {
            double dist = 0;
            RouteResponse result;
            (double time, double price) outPair;
            switch (edgetype)
            {
                case EdgeType.Ship:
                    //Lookup in own table

                    bool trygetbool = edgeInfo.TryGetValue((source, target), out outPair);
                    if (!trygetbool)
                    {
                        trygetbool = edgeInfo.TryGetValue((target, source), out outPair);
                    }
                    if (!trygetbool)
                    {
                        outPair = _priceRepo.Get(source, target, shipmentDate, cargoType.ToString(), weight, largestSizeInCm);
                        edgeInfo.Add((source, target), outPair);
                    }
                    dist = outPair.time * metric.time + outPair.price * metric.price;
                    if (preferShip)
                    {
                        dist *= 0.9;
                    }
                    break;
                case EdgeType.Car:
                    //Call Telstar Logistics service
                    bool trygetter = edgeInfo.TryGetValue((source, target), out outPair);
                    if (!trygetter)
                    {
                        trygetter = edgeInfo.TryGetValue((target, source), out outPair);
                    }
                    if (!trygetter)
                    {
                        result = _tlService.GetRoute(source, target, shipmentDate.ToShortDateString(), weight, largestSizeInCm, cargoType.ToString(), false).Result;
                        outPair = (result.TimeInHours, result.CostInDollars);
                        edgeInfo.Add((source, target), outPair);
                    }
                    
                    dist = outPair.time * metric.time + outPair.price * metric.price;
                    break;
                case EdgeType.Airplane:
                    //Call Oceanic Airlines service
                    bool trygetme = edgeInfo.TryGetValue((source, target), out outPair);
                    if (!trygetme)
                    {
                        trygetme = edgeInfo.TryGetValue((target, source), out outPair);
                    }
                    if (!trygetme)
                    {
                        result = _oaService.GetRoute(source, target, shipmentDate.ToShortDateString(), weight, largestSizeInCm, cargoType.ToString(), false).Result;
                        outPair = (result.TimeInHours, result.CostInDollars);
                        edgeInfo.Add((source, target), outPair);
                    }                    
                    dist = outPair.time * metric.time + outPair.price * metric.price;
                    break;

            }
            return dist;
        }
    }
}
