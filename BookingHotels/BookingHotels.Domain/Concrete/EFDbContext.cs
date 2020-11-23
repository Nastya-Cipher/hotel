using BookingHotels.Domain.Entities;
using System.Data.Entity;

namespace BookingHotels.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
    }
}
