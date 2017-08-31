using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDIS.Model
{
    public class RouteInfo
    {
        public List<string> RouteStops;
        public double TotalCost;
        public double TotalTime;
        public int RouteId;
        public RouteInfo()
        {
            RouteStops = new List<string>();
            TotalCost = 0;
            TotalTime = 0;
        }
    }
}
