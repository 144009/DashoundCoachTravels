using DashoundCoachTravels.Models;
using DashoundCoachTravels.Models.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashoundCoachTravels.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Destinations()
        {
            LocationsViewModels model = new LocationsViewModels();

            var list = new List<Location>();

            if (model.Country == null) model.Country = "";
            if (model.Town == null) model.Town = "";
            if (model.Name == null) model.Name = "";

            foreach (var item in dbcontext.Locations.ToList())
            {
                list.Add(item);
            }
            model.List = list;
            return View(model);
        }

        public ActionResult Fleet()
        {
            FleetViewModel model = new FleetViewModel();

            var list = new List<Coach>();

            if (model.Brand == null) model.Brand = "";
            if (model.VehModel == null) model.VehModel = "";

            foreach (var item in dbcontext.Coaches.ToList())
            {
                list.Add(item);
            }
            model.List = list;

            return View(model);
        }

        public ActionResult Statistics()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}