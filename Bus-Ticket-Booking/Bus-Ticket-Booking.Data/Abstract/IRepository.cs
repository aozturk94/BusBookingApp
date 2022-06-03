using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Data.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
