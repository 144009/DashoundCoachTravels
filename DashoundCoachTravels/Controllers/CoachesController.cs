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
    public class CoachesController : Controller
    {
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        // GET: /Coaches/Index
        public ActionResult Index(ManageMessageId? message)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ViewBag.StatusMessage =
                message == ManageMessageId.EditDetailsSuccess ? "All changes have been saved."
                : message == ManageMessageId.CreateEntrySuccess ? "Successfully added a new vehicle."
                : message == ManageMessageId.DeleteEntrySuccess ? "Successfully deleted a vehicle."
                : message == ManageMessageId.Error ? "An error has occured."
                : "";

            CoachesViewModels model = new CoachesViewModels();

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

        // GET: Coaches/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var model = dbcontext.Coaches.Find(id);
            if (model == null) return HttpNotFound();

            return View(model);
        }

        // GET: Coaches/Create
        public ActionResult Create()
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }

            return View();
        }

        // POST: Coaches/Create
        [HttpPost]
        public ActionResult Create(Coach model) //public ActionResult Create(CoachesViewModel model)
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

                dbcontext.Coaches.Add(model);
                dbcontext.SaveChanges();

                return RedirectToAction("Index", new { Message = ManageMessageId.CreateEntrySuccess });
            }
            return View(model);

        }

        // GET: Coaches/Edit/5
        public ActionResult Edit(int id)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            Coach CurrVeh = dbcontext.Coaches.Find(id);
            if (CurrVeh == null) return HttpNotFound();

            return View(CurrVeh);
        }

        // POST: Coaches/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Coach model)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = dbcontext.Coaches.Find(id);
                    if (modelItem == null) return HttpNotFound();

                    modelItem.Brand = model.Brand;
                    modelItem.VehModel = model.VehModel;
                    modelItem.Seats = model.Seats;
                    modelItem.VehScreenshot = model.VehScreenshot;
                    modelItem.VehicleNumber = model.VehicleNumber;
                    if (model.DateAdded != null) modelItem.DateAdded = model.DateAdded;

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

        // GET: Coaches/Delete/5
        public ActionResult Delete(int id)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            Coach CurrVeh = dbcontext.Coaches.Find(id);
            if (CurrVeh == null) return HttpNotFound();

            return View(CurrVeh);
        }

        // POST: Coaches/Delete/5
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
                Coach CurrVeh = dbcontext.Coaches.Find(id);
                if (CurrVeh == null) return HttpNotFound();
                dbcontext.Coaches.Remove(CurrVeh);
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
