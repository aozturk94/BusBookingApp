using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Entity
{
    public class Route
    {
        public int RouteId { get; set; }
        public string StartLocation { get; set; }
        public string FirstStation { get; set; }
        public string SecondStation { get; set; }
        public string ThirdStation { get; set; }
        public string EndLocation { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double Price { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
