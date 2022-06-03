using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.WebUI.Models
{
    public class RouteTicket
    {
        public List<Ticket> Tickets { get; set; }
        public List<Route> Routes { get; set; }
        public List<City> Cities { get; set; }
        public Route NewRoute { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public Ticket Ticket { get; set; }
    }
}
