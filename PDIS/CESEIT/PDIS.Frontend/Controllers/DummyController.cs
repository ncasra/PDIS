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

        [Route("GetTelstar")]
        public string GetTelstar()
        {
            return _routeManager.GetTelstar();
        }

        [Route("GetOceanic")]
        public string GetOceanic()
        {
            return _routeManager.GetOceanic();
        }
    }
}
