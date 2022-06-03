using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Entity
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string CostumerName { get; set; }
        public string CosturmerSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string TravelFrom { get; set; }
        public string TravelTo { get; set; }
        public int SeatNumber { get; set; }
        public double Price { get; set; }
        public int PeronNumber { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}
