using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class Transaction
    {
        [Key]
        public int TransactionID{ get; set; }
        [Required]
        public string CardType { get; set; }
        [Required]
        public string CreditCNum { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public float total { get; set; }
        [Required]
        public int CustID { get; set; }//Foreign Key
    }
}
