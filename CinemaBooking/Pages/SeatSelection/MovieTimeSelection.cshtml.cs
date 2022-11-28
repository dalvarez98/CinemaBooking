using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CinemaBooking.Pages.SeatSelection
{
    public class MovieTimeSelectionModel : PageModel
    {
        [BindProperty]
        public string selectedLocation { get; set; }
        [BindProperty]
        public string selectedDate { get; set; }
        [BindProperty]
        public string selectedMovie { get; set; }
        [BindProperty]
        public string selectedTheaterOptions { get; set; }
        private const int dateRange = 30;
        private ApplicationDbContext db { get; set; }
        public List<SelectListItem> locationList = new List<SelectListItem>();
        public List<String> dateList = new List<String>();
        public SelectList Cinema { get; set; }
        public SelectList Dates { get; set; }
        public SelectList Movie { get; set; }
        public class CinemaTime
        {
            public CinemaTime()
            {
                Cinema = "null";
                Date = "null";
                Movie = "null";
                Theater = 0;
                Seat = 0;
            }
            public CinemaTime(string cinema, string date, string movie, int theater = 0, int seat = 0)
            {
                Cinema = cinema;
                Date = date;
                Movie = movie;
                Theater = theater;
                Seat = seat;
            }
            public void Copy(CinemaTime c)
            {
                Cinema = c.Cinema;
                Date = c.Date;
                Movie = c.Movie;
                Theater = c.Theater;
                Seat = c.Seat;
            }

            public string Cinema { get; set; }
            public string Date { get; set; }
            public string Movie { get; set; }
            public int Theater { get; set; }
            public int Seat { get; set; }
        }
        public MovieTimeSelectionModel(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        //Grabs all available locations of theaters from database
        public void locations()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                {
                    String myCommand = "SELECT CinemaID, Name FROM Cinema WHERE State = 'FL' ";
                    SqlCommand cmd = new SqlCommand(myCommand, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SelectListItem listItem = new SelectListItem
                        {
                            Text = reader["CinemaID"].ToString(),
                            Value = reader["Name"].ToString()
                        };
                        locationList.Add(listItem);
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error");
            }
        }
        //Stores all dates 30 days from today
        public void dates()
        {
            int i;
            string now = DateTime.Now.ToShortDateString();
            DateTime temp = DateTime.Now;

            dateList.Add(now);

            for (i = 1; i <= dateRange; i++)
            {
                var day = temp.AddDays(i).ToShortDateString();
                dateList.Add(day);
            }
        }
        public void OnGet()
        {
            locations();
            dates();
            selectedDate = DateTime.Now.ToShortDateString();
            selectedTheaterOptions = "Reserved Seating";
            this.Cinema = new SelectList(locationList, "Value", "Value");
            this.Dates = new SelectList(dateList);
            this.Movie = new SelectList(this.db.Movie, "MovieID", "MovieTitle");
        }
        public IActionResult OnPost()
        {
            locations();
            this.Cinema = new SelectList(locationList, "Value", "Value");

            dates();
            this.Dates = new SelectList(dateList);
            var dataMovie = db.Movie.Where(m => m.MovieID.ToString() == selectedMovie).FirstOrDefault();
            selectedMovie = dataMovie.MovieTitle;
            this.Movie = new SelectList(this.db.Movie, "MovieID", "MovieTitle");
            CinemaTime cinemaTime = new CinemaTime(selectedLocation, selectedDate, selectedMovie);

            return RedirectToPage("/SeatSelection/Seats",  cinemaTime);
        }
    }
}
