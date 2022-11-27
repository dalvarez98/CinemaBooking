using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class Manager
    {
        [Key]
        public int EmpID { get; set; }
        [Required(ErrorMessage = "Please provide a First Name", AllowEmptyStrings = false)]
        public string FirstN { get; set; }

        [Required(ErrorMessage = "Please provide a Last Name", AllowEmptyStrings = false)]
        public string LastN { get; set; }

        [BindProperty, DataType(DataType.Date)]
        [Required(ErrorMessage = "Please provide a DOB", AllowEmptyStrings = false)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please provide a SSN", AllowEmptyStrings = false)]
        public string SSN { get; set; }

        [Required(ErrorMessage = "Please provide a Address", AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please provide a City", AllowEmptyStrings = false)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please provide a State", AllowEmptyStrings = false)]
        public string State { get; set; }

        [Required(ErrorMessage = "Please provide a Zip Code", AllowEmptyStrings = false)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please provide an Email Address", AllowEmptyStrings = false)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please provide a valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide Phone Number", AllowEmptyStrings = false)]
        public string PhoneNum { get; set; }

        [Required(ErrorMessage = "Please provide a Salary", AllowEmptyStrings = false)]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Please provide a Username", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [RegularExpression(@"^.{8,}$", ErrorMessage = "Minimum 8 characters required")]
        [Required(ErrorMessage = "Please provide a Password", AllowEmptyStrings = false)]
        public string Password { get; set; }

        public int CinemaID { get; set; } //Foreign Key

    }
}
