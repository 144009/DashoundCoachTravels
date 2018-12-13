using DashoundCoachTravels.Models;
using DashoundCoachTravels.Models.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DashoundCoachTravels.Controllers
{
    public class TripLocationController : Controller
    {
        //added a local database context for use
        private ApplicationDbContext dbcontext = new ApplicationDbContext();


        // GET: TripLocation
        public ActionResult Index(int? thisTripId, ManageMessageId? message)
        {
            if (thisTripId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.StatusMessage =
            message == ManageMessageId.EditDetailsSuccess ? "All changes have been saved."
            : message == ManageMessageId.AddEntrySuccess ? "Successfully added a new location to trip."
            : message == ManageMessageId.DeleteEntrySuccess ? "Successfully deleted a location from trip."
            : message == ManageMessageId.Error ? "An error has occured."
            : "";


            TripLocationsViewModels model = new TripLocationsViewModels();

            model.Id_Trip = (int)thisTripId;
            model.TripName = dbcontext.Trips.Find((int)thisTripId).Name;

            var list = new List<TripLocationsInstanceViewModels>();
            // populate above list with all sublocations the current trip has assigned to it. Like in Trip/Index action we 
            //need to check both ways if trip-location is assigned to each other
            foreach (var item in dbcontext.Trip_Locations.ToList())
            {
                if(item.Id_Trip == thisTripId)
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

            model.ListElement = list;
            return View(model);
        }

        // GET: TripLocation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TripLocation/Add
        public ActionResult Add(int? thisTripId, int? thisLocationId)
        {
            if (thisTripId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if(thisLocationId != null)
            {
                // used to index the number of locations when showing list in Views. Assigned to model.Number
                int i = 1;
                foreach(var item in dbcontext.Trip_Locations.ToList())
                {
                    if (item.Id_Trip == thisTripId) i++;
                }

                dbcontext.Trip_Locations.Add(new Trip_Locations { Id_Location = (int)thisLocationId, Number = i, Id_Trip = (int)thisTripId }); //add a new entry connecting location-trip
                dbcontext.SaveChanges();

                return RedirectToAction("Index", new { thisTripId = thisTripId , Message = ManageMessageId.AddEntrySuccess });
            }

            AddTripLocationsViewModel model = new AddTripLocationsViewModel();
            model.List = dbcontext.Locations.ToList();
            model.TripId = (int)thisTripId;

            return View(model);
        }

        // POST: TripLocation/Add
        [HttpPost]
        public ActionResult Add(int? thisTripId, AddTripLocationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (thisTripId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var list = new List<Location>();
                foreach(var item in dbcontext.Locations.ToList())
                {
                    list.Add(item);
                }
                model.List = list;
                return View(model);

            }
            return View(model);
        }

        // GET: TripLocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TripLocation/Edit/5
        [HttpPost]
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

        // GET: TripLocation/Delete/5
        public ActionResult Delete(int? thisSublocationId)
        {
            if(thisSublocationId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Trip_Locations thisRoute = dbcontext.Trip_Locations.Find(thisSublocationId);
            if (thisRoute == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            DeleteTripLocationsViewModel model = new DeleteTripLocationsViewModel();
            Location location = dbcontext.Locations.Find(thisRoute.Id_Location);

            model.TripId = thisRoute.Id_Trip;
            model.Town = location.Town;
            model.Country = location.Country;
            model.Name = location.Name;
            model.Description = location.Description;
            model.LocationImage = location.LocationImage;


            return View(model);
        }

        // POST: TripLocation/Delete/5
        [HttpPost]
        public ActionResult Delete(int? thisSublocationId, FormCollection collection)
        {
            if (thisSublocationId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                Trip_Locations thisRoute = dbcontext.Trip_Locations.Find(thisSublocationId);
                if (thisRoute == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                bool isDeleted = false; // used to refresh the numbering of sublocations
                Trip_Locations deletedItem = new Trip_Locations();

                foreach (var item in dbcontext.Trip_Locations.ToList())
                {
                    if (isDeleted && item.Id_Trip == thisRoute.Id_Trip) item.Number--;
                    if(item.Id == thisSublocationId)
                    {
                        deletedItem = item;
                        isDeleted = true;
                    }
                }
                dbcontext.Trip_Locations.Remove(deletedItem);
                dbcontext.SaveChanges();

                return RedirectToAction("Index", new { thisTripId = thisRoute.Id_Trip, Message = ManageMessageId.DeleteEntrySuccess });
            }
            catch
            {
                return View("Error");
            }
        }

        #region Helpers
        public enum ManageMessageId // message pool that can be displayed after an operation
        {
            EditDetailsSuccess,
            AddEntrySuccess,
            DeleteEntrySuccess,
            Error
        }

        #endregion
    }
}
