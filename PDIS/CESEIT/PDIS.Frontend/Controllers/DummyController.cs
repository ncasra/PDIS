using PDIS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PDIS.Frontend.Controllers
{
    [System.Web.Http.RoutePrefix("api/Dummy")]
    public class DummyController : ApiController
    {
        private RouteManager _routeManager = new RouteManager();
        public DummyController()
        {

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetTelstar/{source}/{target}")]
        public string GetTelstar(string source, string target)
        {
            return _routeManager.GetTelstar(source, target);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOceanic/{source}/{target}")]
        public string GetOceanic(string source, string target)
        {
            return _routeManager.GetOceanic(source, target);
        }
    }
}
