using Bus_Ticket_Booking.Business.Abstract;
using Bus_Ticket_Booking.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Business.Concrete
{
    public class BusManager : IBusService
    {
        private IBusRepository _busRepository;
        public BusManager(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }
        public int GetSeatCapacity(int id)
        {
            return _busRepository.GetSeatCapacity(id);
        }
    }
}
