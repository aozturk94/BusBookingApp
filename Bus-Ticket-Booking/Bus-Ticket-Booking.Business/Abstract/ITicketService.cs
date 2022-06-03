using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Business.Abstract
{
    public interface ITicketService
    {
        Ticket GetById(int id);
        void Create(Ticket entity);
        void Update(Ticket entity);
        void Delete(Ticket entity);
        List<Ticket> GetAll();

        int GetSeatNumber(int routeId);
        List<int> GetSeat(int routeId);
        Ticket GetLastTicket();
        Ticket GetTicket(int PrnNumber);
        int GetId();
        string GetDate(int id);
        string GetTime(int id);
    }
}
