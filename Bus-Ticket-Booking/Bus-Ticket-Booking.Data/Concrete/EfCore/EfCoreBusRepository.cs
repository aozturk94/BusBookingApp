using Bus_Ticket_Booking.Data.Abstract;
using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Data.Concrete.EfCore
{
    public class EfCoreBusRepository : EfCoreGenericRepository<Bus, BookingContext>, IBusRepository
    {
        public int GetSeatCapacity(int id)
        {
            using (var context = new BookingContext())
            {
                var busCapacity = context.Buses
                    .Where(i => i.BusId == id)
                    .Select(i => i.BusSeatCapacity)
                    .FirstOrDefault();

                return busCapacity;
            }
        }
    }
}
