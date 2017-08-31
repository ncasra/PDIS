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
        public double Distance(string source, string target, EdgeType edgetype, double weight, DateTime shipmentDate, (double, double) metric)
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
