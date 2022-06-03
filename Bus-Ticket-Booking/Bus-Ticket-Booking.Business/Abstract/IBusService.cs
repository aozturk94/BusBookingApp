using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Business.Abstract
{
    public interface IBusService
    {
        int GetSeatCapacity(int id);
    }
}
