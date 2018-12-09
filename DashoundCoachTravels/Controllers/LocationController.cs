using DashoundCoachTravels.Helpers;
using DashoundCoachTravels.Models;
using DashoundCoachTravels.Models.DBEntities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashoundCoachTravels.Controllers
{
    public class LocationController : Controller
    {
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        // GET: Location
        public ActionResult Index(ManageMessageId? message)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ViewBag.StatusMessage =
                message == ManageMessageId.EditDetailsSuccess ? "All changes have been saved."
                : message == ManageMessageId.CreateEntrySuccess ? "Successfully added a new location."
                : message == ManageMessageId.DeleteEntrySuccess ? "Successfully deleted a location."
                : message == ManageMessageId.Error ? "An error has occured."
                : "";

            LocationsViewModels model = new LocationsViewModels();

            var list = new List<Location>();
            foreach (var item in dbcontext.Locations.ToList())
            {
                list.Add(item);
            }
            model.List = list;
            return View(model);
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            var model = dbcontext.Locations.Find(id);
            if (model == null) return HttpNotFound();

            return View(model);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }

            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location model)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            if (ModelState.IsValid)
            {

                /*Coach coach = new Coach()
                {
                    Brand = model.Brand,
                    VehModel = model.VehModel,
                    Seats = model.Seats,
                    DateAdded = model.DateAdded,
                    VehicleNumber = model.VehicleNumber,
                    VehScreenshot = model.VehScreenshot
                };*/

                //model.Id_Trip = 0; -> commented TripID FK line In DBentities\Coach.cs

                dbcontext.Locations.Add(model);
                dbcontext.SaveChanges();

                return RedirectToAction("Index", new { Message = ManageMessageId.CreateEntrySuccess });
            }
            return View(model);
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            Location CurrLoc = dbcontext.Locations.Find(id);
            if (CurrLoc == null) return HttpNotFound();

            return View(CurrLoc);
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Location model)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = dbcontext.Locations.Find(id);
                    if (modelItem == null) return HttpNotFound();

                    modelItem.Country = model.Country;
                    modelItem.Town = model.Town;
                    modelItem.Name = model.Name;
                    modelItem.Description = model.Description;
                    modelItem.LocationImage = model.LocationImage;

                    dbcontext.SaveChanges();

                    return RedirectToAction("Index", new { Message = ManageMessageId.EditDetailsSuccess });
                }
                catch
                {
                    return RedirectToAction("Index", new { Message = ManageMessageId.Error });
                }

            }

            return View(model);
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            Location CurrLoc = dbcontext.Locations.Find(id);
            if (CurrLoc == null) return HttpNotFound();

            return View(CurrLoc);
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            try
            {
                // TODO: Add delete logic here
                Location CurrLoc = dbcontext.Locations.Find(id);
                if (CurrLoc == null) return HttpNotFound();
                dbcontext.Locations.Remove(CurrLoc);
                dbcontext.SaveChanges();

                return RedirectToAction("Index", new { Message = ManageMessageId.DeleteEntrySuccess });
            }
            catch
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
        }

        #region Helpers

        public enum ManageMessageId // message pool that can be displayed after an operation
        {
            EditDetailsSuccess,
            CreateEntrySuccess,
            DeleteEntrySuccess,
            Error
        }

        #endregion

    }
}
