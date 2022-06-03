using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Data.Abstract
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        int GetSeatNumber(int routeId);
        List<int> GetSeat(int routeId);
        Ticket GetLastTicket();
        int GetId();
        string GetDate(int routeid);
        string GetTime(int routeid);
        Ticket GetTicket(int PrnNumber);
    }
}
