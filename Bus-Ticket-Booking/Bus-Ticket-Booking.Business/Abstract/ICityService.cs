using Bus_Ticket_Booking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Business.Abstract
{
    public interface ICityService
    {
        City GetById(int id);
        void Create(City entity);
        void Update(City entity);
        void Delete(City entity);
        List<City> GetAll();
    }
}
