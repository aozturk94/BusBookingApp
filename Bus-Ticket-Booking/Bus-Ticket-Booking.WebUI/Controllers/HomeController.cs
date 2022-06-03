using Bus_Ticket_Booking.Business.Abstract;
using Bus_Ticket_Booking.Entity;
using Bus_Ticket_Booking.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ICityService _cityService;
        private IRouteService _routeService;
        private ITicketService _ticketService;

        public HomeController(ICityService cityService, IRouteService routeService, ITicketService ticketService)
        {
            _cityService = cityService;
            _routeService = routeService;
            _ticketService = ticketService;
        }

        public IActionResult Index(string startLocation, string endLocation, DateTime Date)
        {

            if (startLocation == null || endLocation == null || startLocation == endLocation)
            {
                var cityModel = new RouteTicket()
                {
                    Cities = _cityService.GetAll(),
                    Routes = null
                };

                ViewBag.Cities = new SelectList(cityModel.Cities, "CityId", "CityName");
                return View(cityModel);
            }
            else
            {
                var cityModel = new RouteTicket()
                {
                    Cities = _cityService.GetAll(),
                    Routes = _routeService.GetRoute(startLocation, endLocation, Date)
                };

                TempData["startLocation"] = _routeService.GetStartLocation(startLocation);
                TempData["endLocation"] = _routeService.GetEndLocation(endLocation);
                ViewBag.Cities = new SelectList(cityModel.Cities, "CityId", "CityName");
                return View("Route", cityModel);
            }

        }

        public IActionResult Search(int PeronNumber)
        {
            PeronNumber = Convert.ToInt32(PeronNumber);
            int routeId = _ticketService.GetId();
            string Time = _ticketService.GetTime(routeId);
            string Date = _ticketService.GetDate(routeId);
            Ticket ticket = _ticketService.GetTicket(PeronNumber);
            var routeTicket = new RouteTicket()
            {
                Date = Date,
                Time = Time,
                Ticket = ticket
            };
            return View(routeTicket);
        }
    }
}
