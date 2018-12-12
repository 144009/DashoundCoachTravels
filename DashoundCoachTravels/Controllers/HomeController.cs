using DashoundCoachTravels.Helpers;
using DashoundCoachTravels.Models;
using DashoundCoachTravels.Models.DBEntities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using System.Drawing;

namespace DashoundCoachTravels.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var trip_list = dbcontext.Trips.ToList();
            var model_list = new List<ViewEditTripsViewModel>();

            // get 6 trips with closest departure date
            trip_list.OrderBy(item => item.DateDeparture);

            int count = trip_list.Count() - 1; // np 12 because index starts from 0 not 1
            int to = count - 6; // 12 - 6 = 6

            //make sure there are trips in db
            if (count >= 0) // there is at least 1 trip in db
            {
                for (int i = count; i > to; i--) // im ordering from last because .OrderBy/.OrderByDescending returns the same list for some reason
                {
                    // check if there are less than 6 trips in list
                    if (trip_list.Count < i - 1) break;
                    model_list.Add(new ViewEditTripsViewModel { TripInstance = trip_list[i] });
                }
            }


            // model to return
            TripsViewModels model = new TripsViewModels();
            model.List = model_list;

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Destinations()
        {
            LocationsViewModels model = new LocationsViewModels();

            var list = new List<Location>();
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

            foreach (var item in dbcontext.Coaches.ToList())
            {
                list.Add(item);
            }
            model.List = list;

            return View(model);
        }

        public ActionResult Statistics()
        {
            if (!UserRoleHelper.IsAdmin(User.Identity.GetUserId())) // check if current user has admin rights
            {
                return RedirectToAction("AccessDenied", "Manage");
            }

            // get DB objects
            var bookings = dbcontext.Reservations.ToList();
            var trips = dbcontext.Trips.ToList();
            var coaches = dbcontext.Coaches.ToList();
            var locations = dbcontext.Locations.ToList();
            var users = dbcontext.Users.ToList();

            // model holding the data + default values
            StatisticsViewModel model = new StatisticsViewModel();
            //COACH
            model.TotalVehicles = 0;
            model.AvgVehicleSeats = 0;
            //LOCATION
            model.TotalLocations = 0;
            //TRIP
            model.TotalTrips = 0;
            model.TotalTripsSpots = 0;
            model.AvgTripPrice = 0;
            model.AvgTripSpots = 0;
            model.TotalActiveTrips = 0;
            //USERS
            model.TotalUsers = 0;
            model.TotalEmployee = 0;
            //RESERVATIONS
            model.TotalReservationSpotsBooked = 0;
            model.AvgTotalSpotsReserved = 0; ; // in %
            model.TotalTripsBooked = 0;
            model.TotalReservationsLastMonth = 0;

            // COACH Data
            model.TotalVehicles = coaches.Count();
            foreach(var coach in coaches)
            {
                model.AvgVehicleSeats = coach.Seats + model.AvgVehicleSeats;
            }
            if(model.AvgVehicleSeats != 0)
            {
                model.AvgVehicleSeats = model.AvgVehicleSeats / model.TotalVehicles;
            }
            else { model.AvgVehicleSeats = 0; }

            //LOCATION DATA
            model.TotalLocations = locations.Count();

            //TRIP DATA
            model.TotalTrips = trips.Count();
            foreach (var trip in trips)
            {
                model.TotalTripsSpots = trip.NumSpots + model.TotalTripsSpots;
                model.AvgTripPrice = trip.Price + model.AvgTripPrice;
                model.AvgTripSpots = trip.NumSpots + model.AvgTripSpots;
                if(trip.DateBack > DateTime.Now)
                {
                    model.TotalActiveTrips++;
                }
            }
            if(model.TotalTrips != 0)
            {
                model.AvgTripPrice = model.AvgTripPrice / model.TotalTrips;
                model.AvgTripSpots = model.AvgTripSpots / model.TotalTrips;
            }
            else
            {
                model.AvgTripPrice = 0;
                model.AvgTripSpots = 0;
            }

            //USER DATA
            model.TotalUsers = users.Count();
            foreach (var user in users)
            {
                if (UserRoleHelper.IsUserInRole(user.Id, "Employee")) model.TotalEmployee++;
            }

            //RESERVATION DATA
            model.TotalTripsBooked = bookings.Count();
            foreach(var item in bookings)
            {
                model.TotalReservationSpotsBooked = item.NumPeople + model.TotalReservationSpotsBooked;
            }
            model.AvgTotalSpotsReserved = model.TotalReservationSpotsBooked * 100 / model.TotalTripsSpots;

            // per month reservations
            int month = DateTime.Now.Month; 

            int year = DateTime.Now.Year - 1;
            model.ReservationsPerMonth = new List<ReservationsInMonth>();
            for (int i = 0; i < 12; i++)
            {
                model.ReservationsPerMonth.Add(new ReservationsInMonth { Month = month, Year = year, MonthTotalReservations = 0, MonthName = "test"});
                month++;
                if(month > 12)
                {
                    month -= 12;
                    year++;
                }
            } 
            foreach (var item in model.ReservationsPerMonth)
            {
                switch (item.Month)
                {
                    case 1: item.MonthName = "January"; break;
                    case 2: item.MonthName = "February"; break;
                    case 3: item.MonthName = "March"; break;
                    case 4: item.MonthName = "April"; break;
                    case 5: item.MonthName = "May"; break;
                    case 6: item.MonthName = "June"; break;
                    case 7: item.MonthName = "July"; break;
                    case 8: item.MonthName = "August"; break;
                    case 9: item.MonthName = "September"; break;
                    case 10: item.MonthName = "October"; break;
                    case 11: item.MonthName = "November"; break;
                    case 12: item.MonthName = "December"; break;
                }
            }
            // count reservations per month
            foreach (var booking in bookings)
            {
                foreach (var item in model.ReservationsPerMonth)
                {
                    if (booking.DateBooked.Month == item.Month && booking.DateBooked.Year == item.Year) item.MonthTotalReservations++;
                }
                TimeSpan timespan = DateTime.Now - booking.DateBooked;
                if (timespan.Days <= 31)
                {
                    model.TotalReservationsLastMonth++;
                }
            }


            var axis_X = model.ReservationsPerMonth.Select(i => i.MonthName).ToArray();
            var axis_Y = model.ReservationsPerMonth.Select(i => new object[] { i.MonthTotalReservations }).ToArray();
            // *****************************************************************
            // HIGHCHART # 1 FOR RESERVATIONS PER MONTH ************************
            // *****************************************************************
            // https://www.c-sharpcorner.com/article/dotnet-highcharts-with-asp-net-mvc/
            Highcharts columnChart = new Highcharts("columnchart");

            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2

            });

            columnChart.SetTitle(new Title()
            {
                Text = "Reservations"
            });

            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "12 month data"
            });

            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Month", Style = "fontWeight: 'bold', fontSize: '17px'" },
                Categories = axis_X
            });

            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Number of reservations",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });

            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });

            columnChart.SetSeries(new Series[]
            {
                new Series{

                    Name = "Monthly reservations",
                    Data = new Data(axis_Y)
                },
            }
            );

            columnChart.SetPlotOptions(new PlotOptions
            {
                Line = new PlotOptionsLine
                {
                    DataLabels = new PlotOptionsLineDataLabels { Enabled = true },
                    EnableMouseTracking = false
                }
            }
            );

            model.ReservationChart = columnChart;


            return View(model);
        }


    }
}