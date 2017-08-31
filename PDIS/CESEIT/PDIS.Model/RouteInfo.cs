using System;
using System.Collections.Generic;

namespace PDIS.Model
{
    public class RouteInfo
    {
        public List<string> RouteStops;
        public double TotalTime;
        public double TotalCost;

        public RouteInfo()
        {
            RouteStops = new List<string>();
            TotalTime = 0;
            TotalCost = 0;
        }
    }
}
