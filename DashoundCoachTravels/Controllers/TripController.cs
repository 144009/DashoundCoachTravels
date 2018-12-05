using DashoundCoachTravels.Helpers;
using DashoundCoachTravels.Models;
using DashoundCoachTravels.Models.DBEntities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DashoundCoachTravels.Controllers
{
    public class TripController : Controller
    {
        //added a local database context for use
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        // GET: Trip
        public ActionResult Index(float? PriceMax, float?PriceMin, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
            message == ManageMessageId.EditDetailsSuccess ? "All changes have been saved."
            : message == ManageMessageId.CreateEntrySuccess ? "Successfully added a new trip."
            : message == ManageMessageId.DeleteEntrySuccess ? "Successfully deleted a trip."
            : message == ManageMessageId.BookEntrySuccess ? "Successfully booked a trip."
            : message == ManageMessageId.Error ? "An error has occurred."
            : message == ManageMessageId.CannotEditEntry ? "There are reservations made for this trip. Cannot edit."
            : "";


            //setting default values of fields for search. Those options will be used to search by if empty
            TripsViewModels model = new TripsViewModels();
            if (PriceMax == null) PriceMax = 999999;
            if (PriceMin == null) PriceMin = 0;

            // variables for creating lists of returned trips
            var trip_list = new List<Trip>();
            var model_list = new List<ViewEditTripsViewModel>();

            //****** takes trip info
            foreach (var trip in dbcontext.Trips.ToList())
            {
                trip_list.Add(trip);
            }
            //now we have a list of all trips now we add subplaces to the returned view

            foreach (var trip in trip_list)
            {
                if (trip.Price < PriceMax && trip.Price > PriceMin)
                {
                    var list = new List<TripLocationsInstanceViewModels>();
                    foreach (var item in dbcontext.Trip_Locations.ToList())
                    {
                        if(item.Id_Trip == trip.Id)
                        {
                            foreach(var location in dbcontext.Locations.ToList())
                            {
                                if(item.Id_Location == location.Id)
                                {
                                    list.Add(new TripLocationsInstanceViewModels
                                    {
                                        Country = location.Country,
                                        Town = location.Town,
                                        Name = location.Name,
                                        Description = location.Description,
                                        LocationImage = location.LocationImage,
                                        Number = item.Number,
                                        RouteInstanceId = item.Id
                                    });
                                }
                            }
                        }
                    }
                    var route = new TripLocationsViewModels();
                    if (list.Count() > 0) route.ListElement = list;
                    route.Id_Trip = trip.Id;
                    route.TripName = trip.Name;

                    model_list.Add(new ViewEditTripsViewModel { TripInstance = trip, Route = route });
                }
            }
            model.List = model_list; 

            foreach(var trip in model.List)
            {
                //return free spots left FOR NOW STATIC VALUES. Bookings controller is not implemented 
                trip.NumSpotsLeft = model.NumSpots;
                //return number of reservations
                trip.NumberOfReservations = 0;
            }

            return View(model);
        }

        // GET: Trip/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Trip/Create
        public ActionResult Create()
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId()))
                if(!UserRoleHelper.IsEmployee(User.Identity.GetUserId()))// check if current user has admin or employee rights
                {
                    return RedirectToAction("AccessDenied", "Manage");
                }

            return View();
        }

        // POST: Trip/Create
        [HttpPost]
        public ActionResult Create(Trip model)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId()))
                if (!UserRoleHelper.IsEmployee(User.Identity.GetUserId()))// check if current user has admin or employee rights
                {
                    return RedirectToAction("AccessDenied", "Manage");
                }
            if (ModelState.IsValid)
            {
                dbcontext.Trips.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("Index", new { Message = ManageMessageId.CreateEntrySuccess });
            }
            return View(model);
        }

        // GET: Trip/Edit/5
        public ActionResult Edit(int? thisTripId)
        {
            if (thisTripId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId()))
                if (!UserRoleHelper.IsEmployee(User.Identity.GetUserId()))// check if current user has admin or employee rights
                {
                    return RedirectToAction("AccessDenied", "Manage");
                }

            //check for reservations. Cannot edit while there are any. Placeholder 
            int NumberOfReservations = 0;
            if (NumberOfReservations > 0)
                return RedirectToAction("Index", new { Message = ManageMessageId.CannotEditEntry });

            Trip trip = dbcontext.Trips.Find(thisTripId); //get current trip
            if (trip == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewEditTripsViewModel model = new ViewEditTripsViewModel();
            model.TripInstance = trip;

            //get a list of all sub-locations that this trip has
            var list = new List<TripLocationsInstanceViewModels>();
            foreach (var item in dbcontext.Trip_Locations.ToList())
            {
                if (item.Id_Trip == thisTripId)
                {
                    foreach (var location in dbcontext.Locations.ToList())
                    {
                        if (item.Id_Location == location.Id)
                        {
                            list.Add(new TripLocationsInstanceViewModels
                            {
                                Country = location.Country,
                                Town = location.Town,
                                Name = location.Name,
                                Description = location.Description,
                                LocationImage = location.LocationImage,
                                Number = item.Number,
                                RouteInstanceId = item.Id
                            });
                        }
                    }
                }
            }
            model.Route = new TripLocationsViewModels();
            if (list.Count() > 0) model.Route.ListElement = list;
            if (thisTripId != null && dbcontext.Trips.Find(thisTripId) != null) model.Route.Id_Trip = (int)thisTripId;

            return View(model);
        }

        // POST: Trip/Edit/5
        [HttpPost]
        public ActionResult Edit(int thisTripId, ViewEditTripsViewModel model)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }
            //check for reservations. Cannot edit while there are any. Placeholder 
            int NumberOfReservations = 0;
            if (NumberOfReservations > 0)
                return RedirectToAction("Index", new { Message = ManageMessageId.CannotEditEntry });

            try
            {
                Trip trip = dbcontext.Trips.Find(thisTripId); //get current trip
                if (trip == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                trip.Name = model.TripInstance.Name;
                trip.DateDeparture = model.TripInstance.DateDeparture;
                trip.DateBack = model.TripInstance.DateBack;
                trip.NumSpots = model.TripInstance.NumSpots;
                trip.Price = model.TripInstance.Price;
                trip.Description = model.TripInstance.Description;
                trip.Banner = model.TripInstance.Banner;
                trip.CoachNumberId = model.TripInstance.CoachNumberId;

                dbcontext.SaveChanges();
                return RedirectToAction("Index", new { Message = ManageMessageId.EditDetailsSuccess });
            }
            catch
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
        }
    

        // GET: Trip/Delete/5
        public ActionResult Delete(int? thisTripId)
        {
            if (thisTripId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId()))
                if (!UserRoleHelper.IsEmployee(User.Identity.GetUserId()))// check if current user has admin or employee rights
                {
                    return RedirectToAction("AccessDenied", "Manage");
                }

            //check for reservations. Cannot edit while there are any. Placeholder 
            int NumberOfReservations = 0;
            if (NumberOfReservations > 0)
                return RedirectToAction("Index", new { Message = ManageMessageId.CannotEditEntry });

            Trip trip = dbcontext.Trips.Find(thisTripId); //get current trip
            if (trip == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(trip);
        }

        // POST: Trip/Delete/5
        [HttpPost]
        public ActionResult Delete(int thisTripId, FormCollection collection)
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId()))
                if (!UserRoleHelper.IsEmployee(User.Identity.GetUserId()))// check if current user has admin or employee rights
                {
                    return RedirectToAction("AccessDenied", "Manage");
                }

            //check for reservations. Cannot edit while there are any. Placeholder 
            int NumberOfReservations = 0;
            if (NumberOfReservations > 0)
                return RedirectToAction("Index", new { Message = ManageMessageId.CannotEditEntry });

            Trip trip = dbcontext.Trips.Find(thisTripId); //get current trip
            dbcontext.Trips.Remove(trip);
            dbcontext.SaveChanges();

            return RedirectToAction("Index", new { Message = ManageMessageId.DeleteEntrySuccess });

        }

        #region Helpers
        public enum ManageMessageId // message pool that can be displayed after an operation
        {
            EditDetailsSuccess,
            CreateEntrySuccess,
            DeleteEntrySuccess,
            BookEntrySuccess,
            CannotEditEntry,
            Error
        }

        #endregion

    }
}
