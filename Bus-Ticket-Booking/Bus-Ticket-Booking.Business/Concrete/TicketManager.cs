using Bus_Ticket_Booking.Business.Abstract;
using Bus_Ticket_Booking.Data.Abstract;
using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Business.Concrete
{
    public class TicketManager : ITicketService
    {
        private ITicketRepository _ticketRepository;
        public TicketManager(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public void Create(Ticket entity)
        {
            _ticketRepository.Create(entity);
        }

        public void Delete(Ticket entity)
        {
            _ticketRepository.Delete(entity);
        }

        public List<Ticket> GetAll()
        {
            return _ticketRepository.GetAll();
        }

        public Ticket GetById(int id)
        {
            return _ticketRepository.GetById(id);
        }

        public string GetDate(int id)
        {
            return _ticketRepository.GetDate(id);
        }

        public int GetId()
        {
            return _ticketRepository.GetId();
        }

        public List<int> GetSeat(int routeId)
        {
            return _ticketRepository.GetSeat(routeId);
        }

        public int GetSeatNumber(int routeId)
        {
            return _ticketRepository.GetSeatNumber(routeId);
        }

        public Ticket GetLastTicket()
        {
            return _ticketRepository.GetLastTicket();
        }

        public string GetTime(int id)
        {
            return _ticketRepository.GetTime(id);
        }

        public void Update(Ticket entity)
        {
            _ticketRepository.Update(entity);
        }

        public Ticket GetTicket(int PrnNumber)
        {
            return _ticketRepository.GetTicket(PrnNumber);
        }
    }
}
