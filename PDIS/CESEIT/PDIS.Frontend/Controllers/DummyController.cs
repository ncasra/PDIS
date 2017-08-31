using PDIS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PDIS.Frontend.Controllers
{
    public class DummyController : ApiController
    {
        private RouteManager _routeManager = new RouteManager();

        [Route("GetTelstar/{source}/{target}")]
        public string GetTelstar(string source, string target)
        {
            return _routeManager.GetTelstar(source, target);
        }

        [Route("GetOceanic/{source}/{target}")]
        public string GetOceanic(string source, string target)
        {
            return _routeManager.GetOceanic(source, target);
        }
    }
}
