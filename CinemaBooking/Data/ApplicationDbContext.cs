using CinemaBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Crewmember> Crewmember { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<TheaterRooms> TheaterRoom { get; set; }
        public DbSet<Screening> Screening { get; set; }
    }
}
