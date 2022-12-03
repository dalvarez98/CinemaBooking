using CinemaBooking.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<BuysTicket> BuysTicket { get; set; }
        public DbSet<Crewmember> Crewmember { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Seats> Seats { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TheaterRooms> TheaterRoom { get; set; }
        public DbSet<Screening> Screening { get; set; }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuysTicket>().HasKey(m => new {m.CustID, m.TransactionID, m.TicketNum});
            modelBuilder.Entity<Seats>().HasKey(m => new { m.SeatNum, m.TheaterID });
        }
    }
}
