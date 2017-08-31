using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CESEIT;
using PDIS.Managers;

namespace PDIS.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly RouteManager _routeManager;

        public HomeController()
        {
            _routeManager = new RouteManager();
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //TODO: locations


            List<string> locations = _routeManager.GetCities();
            //locations.Add("Dummy Sierra Leone");
            //locations.Add("Dummy Wadai");
            ViewBag.Locations = locations;
            return View();
        }

        public ActionResult Kvittering()
        {
            return View("Kvittering");
        }
    }
}
