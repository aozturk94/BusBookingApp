using Bus_Ticket_Booking.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.Data.Concrete
{
    public class BookingContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Bus> Buses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = BookingDb");
        }
    }
}
