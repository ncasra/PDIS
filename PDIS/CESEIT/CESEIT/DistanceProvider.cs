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
        

        public DistanceProvider()
        {
            edgeInfo = new Dictionary<(string start, string finish), (double time, double price)>();
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

        public double Distance(string source, string target, EdgeType edgetype, double weight, DateTime shipmentDate, (double time, double price) metric, bool preferShip = false)
        {
            double dist = 0;
            switch (edgetype)
            {
                case EdgeType.Ship:
                    //Lookup in own table
                    break;
                case EdgeType.Car:
                    //Call Telstar Logistics service
                    break;
                case EdgeType.Airplane:
                    //Call Oceanic Airlines service
                    break;

            }
            return dist;
        }
    }
}
