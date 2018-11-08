using DashoundCoachTravels.Helpers;
using DashoundCoachTravels.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

// basic controller layout was generated using: add->controller->asp net mvc 5 controller
// with read\write acctions
// this controller will be used by the admin panel later in view models to edit roles\permissions of users

// some code snippets re-used from ManageController here

namespace DashoundCoachTravels.Controllers
{
    public class ManageUsersController : Controller
    {
        //added a local database context for use
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        // GET: ManageUsers
        public ActionResult Index(ManageMessageId? message) // from ManageController
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ViewBag.StatusMessage =
                message == ManageMessageId.EditUserSuccess ? "All changes have been saved."
                : message == ManageMessageId.ChangeRoleToCustomer ? "Changed account permissions to : Customer/Basic."
                : message == ManageMessageId.ChangeRoleToEmployee ? "Changed account permissions to : Employee."
                : message == ManageMessageId.ChangeRoleToAdmin ? "Changed account permissions to : Administrator."
                : message == ManageMessageId.ChangeOwnRoleErr ? "Cannot change own account permission type!"
                : message == ManageMessageId.Error ? "An error has occured."
                : "";

            return View(dbcontext.Users.ToList());
        }

        // GET: ManageUsers/Details/5
        public ActionResult Details(string UserId)
        {
            if (UserId == null){return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ApplicationUser CurrUser = dbcontext.Users.Find(UserId);
            if (CurrUser == null)
            {
                return HttpNotFound();
            }
            return View(CurrUser);
        }

        // GET: ManageUsers/Edit/5
        public ActionResult Edit(string UserId)
        {
            if (UserId == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ApplicationUser CurrUser = dbcontext.Users.Find(UserId);
            if (CurrUser == null)
            {
                return HttpNotFound();
            }
            return View(CurrUser);
        }

        // POST: ManageUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        protected override void Dispose(bool disposing) //free resources after finishing edit for garbage collector
        {
            if (disposing)
            {
                dbcontext.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Helpers

        public enum ManageMessageId // message pool that can be displayed after an operation
        {
            EditUserSuccess,
            ChangeRoleToCustomer,
            ChangeRoleToEmployee,
            ChangeRoleToAdmin,
            ChangeOwnRoleErr,
            Error
        }

#endregion

    }
}
