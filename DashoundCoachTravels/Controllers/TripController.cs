using DashoundCoachTravels.Helpers;
using DashoundCoachTravels.Models;
using DashoundCoachTravels.Models.DBEntities;
using Microsoft.AspNet.Identity;
using PagedList;
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
        public ActionResult Index(float? PriceMax, float?PriceMin, ManageMessageId? message, int? page)
        {
            ViewBag.StatusMessage =
            message == ManageMessageId.EditDetailsSuccess ? "All changes have been saved."
            : message == ManageMessageId.CreateEntrySuccess ? "Successfully added a new trip."
            : message == ManageMessageId.DeleteEntrySuccess ? "Successfully deleted a trip."
            : message == ManageMessageId.BookEntrySuccess ? "Successfully booked a trip."
            : message == ManageMessageId.Error ? "An error has occurred."
            : message == ManageMessageId.CannotEditEntry ? "There are reservations made for this trip. Cannot edit."
            : "";
            // THIS ACTION WAS ANALYZED IN DETAIL in provided documentation (chapter 4)

            // tutorial used: https://github.com/TroyGoode/PagedList
            var pageIndex = (page ?? 1); // first page index. Must start at least at 1
            var pageSize = 8;  // objects per page


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
                        if(item.Id_Trip == trip.Id) // first check if [Trip] table and [TripLocation] table are connected via same ID. Below same for location
                        {
                            foreach(var location in dbcontext.Locations.ToList())
                            {
                                if(item.Id_Location == location.Id) // check the table created after removing many-many table connection
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
                trip.NumSpotsLeft = countFreeSpots(trip.TripInstance.Id);
                //return number of reservations
                trip.NumberOfReservations = countReservavtionsMade(trip.TripInstance.Id);
            }

            // add coach info to List
            foreach(var trip in model.List)
            {
                foreach (var item in dbcontext.Coaches.ToList())
                {
                    if (trip.TripInstance.CoachNumberId == item.Id)
                    {
                        trip.CoachBanner = item.VehScreenshot;
                        trip.CoachModel = item.Brand + " " + item.VehModel + " - no." + item.VehicleNumber;
                    }
                }
            }


            // convert normal list to a pagedlist with given index and size
            model.List = model.List.ToPagedList(pageIndex, pageSize);

            return View(model);
        }

        // GET: Trip/Details/5
        public ActionResult Details(int? thisTripId)
        {
            if (thisTripId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // ViewBag value for holding part of google maps url data string
            ViewBag.Markers = "";
            string[] colours = { "red", "green", "blue"};


            Trip trip = dbcontext.Trips.Find(thisTripId);
            if(trip == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            ViewEditTripsViewModel model = new ViewEditTripsViewModel();
            model.TripInstance = trip;

            var list = new List<TripLocationsInstanceViewModels>();
            int i = 0;
            foreach (var item in dbcontext.Trip_Locations.ToList())
            {
                if (item.Id_Trip == thisTripId)
                {
                    foreach (var location in dbcontext.Locations.ToList())
                    {
                        if (item.Id_Location == location.Id)
                        {
                            // Create a marker string using current location data; can be modified for any url string if needed depending on map provider
                            ++i;
                            // cycles the string array in a loop
                            ViewBag.Markers += "&markers=color:" + colours[(i - 1) % 3] + "|label:" + i.ToString() + "|" + location.Name + "," + location.Town + "," + location.Country;
                            // ********************
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


            //return free spots left FOR NOW STATIC VALUES. Bookings controller is not implemented 
            model.NumSpotsLeft = countFreeSpots((int)thisTripId);
            //return number of reservations
            model.NumberOfReservations = countReservavtionsMade((int)thisTripId);

            // add coach info to List
            foreach (var item in dbcontext.Coaches.ToList())
            {
                if (trip.CoachNumberId == item.Id)
                {
                    model.CoachBanner = item.VehScreenshot;
                    model.CoachModel = item.Brand + " " + item.VehModel + " - no." + item.VehicleNumber;
                }
            }

            return View(model);
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
                // CoachNumberId is a required field in Trip Table BUT because when assigning a coach to a trip we need to be sure its not already 
                // assigned to a different trip going on in  the time of creating this trip (checked by looking for trips in progress)
                // there is no way to know at the time of this action what coaches can be used since we dont know when THIS trip will start-end. 
                // we know that only after its created. so we assign a coachID which will never be created by DB and later assign a proprt value in edit action
                model.CoachNumberId = -1;

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

            //check for reservations. Cannot edit while there are any. 
            int NumberOfReservations = countReservavtionsMade((int)thisTripId);

            Trip trip = dbcontext.Trips.Find(thisTripId); //get current trip
            if (trip == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewEditTripsViewModel model = new ViewEditTripsViewModel();
            model.TripInstance = trip;


            //check for reservations. Cannot edit while there are any and trip is yet to end
            var currTime = DateTime.Now;
            if (NumberOfReservations > 0 && model.TripInstance.DateBack > currTime)
                return RedirectToAction("Index", new { Message = ManageMessageId.CannotEditEntry });


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

            // list that has every coach in database
            var listOfCoaches = dbcontext.Coaches.ToList();

            var currDate = DateTime.Now;
            // go through every trip in db that is in progress atm. A coach assigned to that trip will be removed from our list, so it cant be assigned 
            //to this currently edited trip
            foreach (var coach in dbcontext.Coaches.ToList())
            {
                foreach (var tripInstance in dbcontext.Trips.ToList())
                {
                    if (tripInstance.DateDeparture < model.TripInstance.DateDeparture && model.TripInstance.DateDeparture < tripInstance.DateBack)
                        if(coach.Id == tripInstance.CoachNumberId)
                            {
                                listOfCoaches.Remove(coach);
                            }
                }
            }
            ViewBag.DateDeparture = model.TripInstance.DateDeparture;
            ViewBag.DateBack = model.TripInstance.DateBack;

            model.CoachVehicleIdList = new SelectList(listOfCoaches, "Id", "VehicleNumber");

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
            int NumberOfReservations = countReservavtionsMade((int)thisTripId);

            //check for reservations. Cannot edit while there are any and trip is yet to end
            var currTime = DateTime.Now;
            if (NumberOfReservations > 0 && model.TripInstance.DateBack > currTime)
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
            int NumberOfReservations = countReservavtionsMade((int)thisTripId);

            Trip trip = dbcontext.Trips.Find(thisTripId); //get current trip
            if (trip == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            
            //check for reservations. Cannot edit while there are any and trip is yet to end
            var currTime = DateTime.Now;
            if (NumberOfReservations > 0 && trip.DateBack > currTime)
                return RedirectToAction("Index", new { Message = ManageMessageId.CannotEditEntry });


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

            
            int NumberOfReservations = countReservavtionsMade((int)thisTripId);

            Trip trip = dbcontext.Trips.Find(thisTripId); //get current trip

            //check for reservations. Cannot edit while there are any and trip is yet to end
            var currTime = DateTime.Now;
            if (NumberOfReservations > 0 && trip.DateBack > currTime)
                return RedirectToAction("Index", new { Message = ManageMessageId.CannotEditEntry });

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

        // functions counting number of reservatons and number of reserved spots in a given trip
        private int countFreeSpots(int thisTripId)
        {
            Trip trip = dbcontext.Trips.Find(thisTripId);
            int spotsReservedTotal = 0;
            foreach (var item in dbcontext.Reservations.ToList())
            {
                if (item.Id_Trip == trip.Id) spotsReservedTotal += item.NumPeople; // eg 22
            }
            return spotsReservedTotal; 
        }

        private int countReservavtionsMade(int thisTripId)
        {
            Trip trip = dbcontext.Trips.Find(thisTripId);
            int reservationsMade = 0;
            foreach (var item in dbcontext.Reservations.ToList())
            {
                if (item.Id_Trip == trip.Id) reservationsMade++;
            }
            return reservationsMade;
        }

        #endregion

    }
}
