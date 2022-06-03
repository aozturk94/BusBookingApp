using Bus_Ticket_Booking.Business.Abstract;
using Bus_Ticket_Booking.Entity;
using Bus_Ticket_Booking.WebUI.EmailServices;
using Bus_Ticket_Booking.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.WebUI.Controllers
{
    public class TicketController : Controller
    {
        private IRouteService _routeService;
        private ITicketService _ticketService;
        private IBusService _busService;
        private IEmailSender _emailSender;

        public TicketController(IRouteService routeService, ITicketService ticketService, IBusService busService, IEmailSender emailSender)
        {
            _routeService = routeService;
            _ticketService = ticketService;
            _busService = busService;
            _emailSender = emailSender;
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            Route route = _routeService.GetRouteDetails(id);
            int seatNumber = _ticketService.GetSeatNumber(id);
            List<int> takenSeat = _ticketService.GetSeat(id);
            List<int> seatNumbers = new List<int>();
            int seats = _busService.GetSeatCapacity(id/id);

            for (int i = 1; i <= seats; i++)
            {
                seatNumbers.Add(i);
            }

            foreach (var item in takenSeat)
            {
                seatNumbers.Remove(item);
            }

            ViewBag.Seat = seats;
            ViewBag.SeatNumber = seatNumber;
            ViewBag.SeatNumbers = new SelectList(seatNumbers);

            return View(route);

        }

        [HttpPost]
        public IActionResult Details(int routeId, double price, string startLocation, string endLocation, int seatNumber, string phoneNumber, string firstName, string lastName)
        {
            Random rnd = new Random();
            int PrnNumber = rnd.Next();
            var entity = new Ticket()
            {
                CostumerName = firstName,
                CosturmerSurname = lastName,
                PhoneNumber = phoneNumber,
                TravelFrom = startLocation,
                TravelTo = endLocation,
                SeatNumber = seatNumber,
                Price = price,
                RouteId = routeId,
                PeronNumber = PrnNumber
            };

            _ticketService.Create(entity);
            return RedirectToAction("TicketDetails");
        }

        public IActionResult TicketDetails()
        {
            Ticket lastTicket = _ticketService.GetLastTicket();
            int routeId = _ticketService.GetId();
            string Time = _ticketService.GetTime(routeId);
            string Date = _ticketService.GetDate(routeId);

            var routeTicket = new RouteTicket()
            {
                Date = Date,
                Time = Time,
                Ticket = lastTicket
            };
            return View(routeTicket);
        }

        [HttpPost]
        public async Task<IActionResult> TicketDetails(string email)
        {
            Ticket lastTicket = _ticketService.GetLastTicket();
            int routeId = _ticketService.GetId();
            string Time = _ticketService.GetTime(routeId);
            string Date = _ticketService.GetDate(routeId);

            var routeTicket = new RouteTicket()
            {
                Date = Date,
                Time = Time,
                Ticket = lastTicket
            };

            await _emailSender.SendEmailAsync(email, "OZTUR Ticket Info", @$"<table>
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Ticket No</th>
                                                                            <th>Name</th>
                                                                            <th>Travel From</th>
                                                                            <th>Travel To</th>
                                                                            <th>Seat Number</th>
                                                                            <th>Price</th>
                                                                            <th>Date - Time</th>
                                                                            <th>Sefer No</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>{routeTicket.Ticket.PeronNumber}</td>
                                                                            <td>{routeTicket.Ticket.CostumerName} {routeTicket.Ticket.CosturmerSurname}</td>
                                                                            <td>{routeTicket.Ticket.TravelFrom}</td>
                                                                            <td>{routeTicket.Ticket.TravelTo}</td>
                                                                            <td>{routeTicket.Ticket.SeatNumber}</td>
                                                                            <td>{routeTicket.Ticket.Price} ₺</td>
                                                                            <td>{routeTicket.Date} - {routeTicket.Time}</td>
                                                                            <td>{routeTicket.Ticket.RouteId}</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>");

            CreateMessage("Bilet bilgileriniz mail adresine gönderilmiştir!", "success");
            return View(routeTicket);
        }
        private void CreateMessage(string message, string alertType)
        {
            var msg = new AlertMessage()
            {
                Message = message,
                AlertType = alertType
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
    }
}
