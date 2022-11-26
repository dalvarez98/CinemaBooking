using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class BuyTickets
    {
        public int CustID { get; set; } //foreign Key
        public int TransactionID { get; set; } //Foreign Key
        public int TicketNum { get; set; } //Foreign Key
    }
}
