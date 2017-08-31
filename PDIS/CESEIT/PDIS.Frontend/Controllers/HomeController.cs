using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CESEIT;

namespace PDIS.Frontend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //TODO: locations
            List<string> locations = new List<string>();
            locations.Add("Dummy Sierra Leone");
            locations.Add("Dummy Wadai");
            ViewBag.Locations = locations;
            return View();
        }

        public ActionResult Kvittering()
        {
            return View("Kvittering");
        }
    }
}
