﻿using PDIS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PDIS.Frontend.Controllers
{
    public class RouteController : ApiController
    {
        private RouteManager _routeManager;

        public RouteController()
        {
            _routeManager = new RouteManager();
        }

        //URL: api/Route/GetRouteInfo/{source}?{target}?etc. (I think)
        [Route("GetRouteInfo")]
        public string GetRoute(string source, string target, string cargoType, string weightInKg, string largestSizeInCm, string shipmentDate)
        {
            var routeinfostring = _routeManager.GetRouteInfo(source, target, cargoType, weightInKg, largestSizeInCm, shipmentDate);
            return routeinfostring;
        }




    }
}