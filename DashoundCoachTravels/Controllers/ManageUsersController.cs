using DashoundCoachTravels.Helpers;
using DashoundCoachTravels.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                : message == ManageMessageId.DeleteUserSuccess ? "Successfully deleted user account."
                : message == ManageMessageId.ChangeOwnRoleErr ? "Cannot change own account permission type!"
                : message == ManageMessageId.Error ? "An error has occured."
                : "";

            return View(dbcontext.Users.ToList());
        }

        // GET: ManageUsers/Details/5
        public ActionResult Details(string Id)
        {
            if (Id == null){return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ApplicationUser CurrUser = dbcontext.Users.Find(Id);
            if (CurrUser == null)
            {
                return HttpNotFound();
            }
            return View(CurrUser);
        }

        // GET: ManageUsers/Edit/5
        public ActionResult Edit(string Id)
        {
            if (Id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ApplicationUser CurrUser = dbcontext.Users.Find(Id);
            if (CurrUser == null)
            {
                return HttpNotFound();
            }
            return View(CurrUser);
        }

        // duplicated action EDIT POST/GET and changed to EditUserRoles
        // GET: ManageUsers/EditUserRoles/5
        public ActionResult EditUserRoles(string Id)
        {
            if (Id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ApplicationUser CurrUser = dbcontext.Users.Find(Id);
            if (CurrUser == null)
            {
                return HttpNotFound();
            }

            EditUserRoleViewModel field = new EditUserRoleViewModel(); // get access to model fields we will be showing
            var userStore = new UserStore<ApplicationUser>(dbcontext); // access to roles using Identity Framework
            var userManager = new UserManager<ApplicationUser>(userStore);

            field.Id = CurrUser.Id;
            field.UserName = CurrUser.UserName;
            field.Email = CurrUser.Email;
            field.Name = CurrUser.Name;
            field.Surname = CurrUser.Surname;
            field.Country = CurrUser.Country;
            field.Town = CurrUser.Town;
            field.Street = CurrUser.Street;
            field.NumHouse = CurrUser.NumHouse;
            field.NumFlat = CurrUser.NumFlat;
            field.ZIPCode = CurrUser.ZIPCode;
            field.PhoneNumber = CurrUser.PhoneNumber;

            if (UserRoleHelper.IsAdmin(field.Id)) { field.RoleType = UserRoleTypes.Administrator; }
            if (UserRoleHelper.IsEmployee(field.Id)) { field.RoleType = UserRoleTypes.Employee; }
            if (UserRoleHelper.IsUser(field.Id)) { field.RoleType = UserRoleTypes.Customer; }

            return View(field);
        }

        // POST: ManageUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // what does Bind do: 2 approches: we can either create a model with all properties we want to edit in this Action OR we can 
        // use Bind(Exclude = "") Bind(Include = "") to tell which properties from a given model to take and edit or exclude
        // usually first approach is better via: https://cpratt.co/stop-using-bind/
        // but here I do not want to create a new model since Im not going to use this too often and have no 
        // plans to change this Action often PLUS UserModel is build in and not something I created from scratch so I dont want to edit it more than needed
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Street,NumHouse,NumFlat,Town,ZIPCode,Country,Email," +
            "                                   EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled," +
            "                                   LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
           if(ModelState.IsValid)
            {
                dbcontext.Entry(applicationUser).State = System.Data.Entity.EntityState.Modified;
                dbcontext.SaveChanges();
                return RedirectToAction("Index", new { Message = ManageMessageId.EditUserSuccess });
            }
            return View(applicationUser);
        }

        // POST: ManageUsers/EditUserRoles/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // all fields will be used so instead of Bind we can use just the ready Model for it
        public ActionResult EditUserRoles(EditUserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
                {
                    return RedirectToAction("AccessDenied", "Manage");
                }

                ApplicationUser CurrUser = dbcontext.Users.Find(model.Id);
                if (CurrUser == null || model.Id == null)
                {
                    return HttpNotFound();
                }

                // user cannot change his own role. Check if user currently editing has same id as the one being edited
                if (model.Id == User.Identity.GetUserId())
                {
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeOwnRoleErr });
                }

                // declaration of needed variables to have the ability to change user roles : Identity Framework. Takes role types and users we will be later using from DB
                var userBeingEdit = new ApplicationUser() { UserName = model.UserName };

                var roleStore = new RoleStore<IdentityRole>(dbcontext);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(dbcontext);
                var userManager = new UserManager<ApplicationUser>(userStore);


                // change user role to Administrator
                if (model.RoleType == UserRoleTypes.Administrator)
                {
                    if (userManager.IsInRole(CurrUser.Id, "User")) userManager.RemoveFromRole(CurrUser.Id, "User");
                    if (userManager.IsInRole(CurrUser.Id, "Employee")) userManager.RemoveFromRole(CurrUser.Id, "Employee");
                    userManager.AddToRole(CurrUser.Id, "Administrator");
                    dbcontext.Entry(CurrUser).State = System.Data.Entity.EntityState.Modified;
                    dbcontext.SaveChanges();

                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeRoleToAdmin });
                }

                // change user role to Employee
                if (model.RoleType == UserRoleTypes.Employee)
                {
                    if (userManager.IsInRole(CurrUser.Id, "User")) userManager.RemoveFromRole(CurrUser.Id, "User");
                    if (userManager.IsInRole(CurrUser.Id, "Administrator")) userManager.RemoveFromRole(CurrUser.Id, "Administrator");
                    userManager.AddToRole(CurrUser.Id, "Employee");
                    dbcontext.Entry(CurrUser).State = System.Data.Entity.EntityState.Modified;
                    dbcontext.SaveChanges();

                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeRoleToEmployee });
                }

                // change user role to Customer\User
                if (model.RoleType == UserRoleTypes.Customer)
                {
                    if (userManager.IsInRole(CurrUser.Id, "Employee")) userManager.RemoveFromRole(CurrUser.Id, "Employee");
                    if (userManager.IsInRole(CurrUser.Id, "Administrator")) userManager.RemoveFromRole(CurrUser.Id, "Administrator");
                    userManager.AddToRole(CurrUser.Id, "User");
                    dbcontext.Entry(CurrUser).State = System.Data.Entity.EntityState.Modified;
                    dbcontext.SaveChanges();

                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeRoleToCustomer });
                }

                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }

            return View(model);
        }


        // GET: ManageUsers/Delete/5
        public ActionResult Delete(string Id)
        {
            if (Id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            ApplicationUser CurrUser = dbcontext.Users.Find(Id);
            if (CurrUser == null) return HttpNotFound();

            return View(CurrUser);
        }

        // POST: ManageUsers/Delete/5
        [HttpPost]
        public ActionResult Delete(string Id, FormCollection collection)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            try
            {
                // TODO: Add delete logic here
                ApplicationUser CurrUser = dbcontext.Users.Find(Id);
                if (CurrUser == null) return HttpNotFound();
                dbcontext.Users.Remove(CurrUser);
                dbcontext.SaveChanges();

                return RedirectToAction("Index", new { Message = ManageMessageId.DeleteUserSuccess });
            }
            catch
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
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
            DeleteUserSuccess,
            ChangeRoleToCustomer,
            ChangeRoleToEmployee,
            ChangeRoleToAdmin,
            ChangeOwnRoleErr,
            Error
        }

#endregion

    }
}
