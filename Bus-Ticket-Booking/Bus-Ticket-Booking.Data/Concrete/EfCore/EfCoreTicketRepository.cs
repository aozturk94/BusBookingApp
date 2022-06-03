using Bus_Ticket_Booking.Data.Abstract;
using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Data.Concrete.EfCore
{
    public class EfCoreTicketRepository : EfCoreGenericRepository<Ticket, BookingContext>, ITicketRepository
    {
        public string GetDate(int routeid)
        {
            using (var context = new BookingContext())
            {
                var ticketDate = context
                    .Routes
                    .Where(i => i.RouteId == routeid)
                    .Select(i => i.Date)
                    .FirstOrDefault();

                return ticketDate;
            }
        }

        public string GetTime(int routeid)
        {
            using (var context = new BookingContext())
            {
                var ticketTime = context.Routes
                    .Where(i => i.RouteId == routeid)
                    .Select(i => i.Time)
                    .FirstOrDefault();

                return ticketTime;
            }
        }

        public int GetId()
        {
            using (var context = new BookingContext())
            {
                var id = context.Tickets
                    .OrderByDescending(i => i.TicketId)
                    .Select(i => i.RouteId)
                    .FirstOrDefault();
                return id;
            }
        }

        public Ticket GetLastTicket()
        {
            using (var context = new BookingContext())
            {
                var lastTicket = context.Tickets
                    .OrderByDescending(i => i.TicketId)
                    .FirstOrDefault();
                return lastTicket;
            }
        }

        public List<int> GetSeat(int routeId)
        {
            using (var context = new BookingContext())
            {
                var seat = context.Tickets
                    .Where(i => i.RouteId == routeId)
                    .Select(i => i.SeatNumber)
                    .ToList();

                return seat;
            }
        }

        public int GetSeatNumber(int routeId)
        {
            using (var context = new BookingContext())
            {
                return context.Tickets
                    .Where(i => i.RouteId == routeId)
                    .Select(i => i.SeatNumber)
                    .Count();
            }
        }

        public Ticket GetTicket(int PrnNumber)
        {
            using (var context = new BookingContext())
            {
                var ticket = context
                    .Tickets
                    .Where(i => i.PeronNumber == PrnNumber)
                    .FirstOrDefault();

                return ticket;
            }
        }
    }
}
