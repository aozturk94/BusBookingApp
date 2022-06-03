using Bus_Ticket_Booking.Data.Abstract;
using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Data.Concrete.EfCore
{
    public class EfCoreCityRepository : EfCoreGenericRepository<City, BookingContext>, ICityRepository
    {
    }
}
 