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
        public DbSet<BuyTickets> BuyTickets { get; set; }
        public DbSet<Crewmember> Crewmember { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyTickets>().HasKey(m => new {m.CustID, m.TransactionID, m.TicketNum});
        }
    }
}
