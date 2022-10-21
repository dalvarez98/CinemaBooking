using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class Cinema
    {
        [Key]
        public int CinemaID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNum { get; set; }
        public int ManagerNum { get; set; } //Foreign Key
    }
}
