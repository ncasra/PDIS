using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CESEIT;

namespace ServiceGateway.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            List<string> cargoTypes = new List<string>();
            cargoTypes.Add("Standard");
            cargoTypes.Add("Våben");
            ViewBag.CargoTypes = cargoTypes;

            return View();
        }
    }
}
