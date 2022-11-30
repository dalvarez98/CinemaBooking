using CinemaBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CinemaBooking.Pages.ManagerFunctions
{
    public class CreateMovieTimeModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private const int dateRange = 30;
        public SelectList Movie { get; set; }
        public SelectList Dates { get; set; }
        public SelectList TheaterRooms { get; set; }
        public IEnumerable<SelectListItem> Times { get; set; }
        public Dictionary<int, Tuple<int, int>> movieTimeLength;
        public List<String> dateList = new List<String>();
        public List<SelectListItem> theaterRoomList = new List<SelectListItem>();
        [BindProperty]
        public string selectedMovie { get; set; }
        [BindProperty]
        public string selectedDate { get; set; }
        [BindProperty]
        public string selectedTheaterRoom { get; set; }
        [BindProperty]
        public string selectedTime { get; set; }

        public CreateMovieTimeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Obtains list of theaterRooms based off of managers cinemaid
        public void getTheaterRooms()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                {
                    String myCommand = "SELECT TheaterRoom, CinemaID FROM TheaterRoom WHERE CinemaID = " + Convert.ToInt32(User.FindFirst("CinemaId").Value);
                    SqlCommand cmd = new SqlCommand(myCommand, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SelectListItem listItem = new SelectListItem
                        {
                            Text = reader["CinemaID"].ToString(),
                            Value = reader["TheaterRoom"].ToString()
                        };
                        theaterRoomList.Add(listItem);
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error");
            }
        }
        public IEnumerable<SelectListItem> listOfTimes()
        {

            movieTimeLength = new Dictionary<int, Tuple<int, int>>();
            var list = new List<SelectListItem>();
            var tempList = new List<SelectListItem>();
            //This is used to block out times that overlap with length of current movie during screening
            int lenOfMovieHours = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                {
                    String myCommand = "SELECT TheaterRoom, ScreeningDateStart, Screening.MovieID, length FROM Screening JOIN Movie ON Screening.MovieID = Movie.MovieID WHERE TheaterRoom = " + 
                        selectedTheaterRoom + "AND CinemaID = " + Convert.ToInt32(User.FindFirst("CinemaID").Value);
                    SqlCommand cmd = new SqlCommand(myCommand, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tempList.Add(new SelectListItem() { Text = reader["MovieID"].ToString(), Value = reader["ScreeningDateStart"].ToString()});

                        if (movieTimeLength.ContainsKey(Convert.ToInt32(reader["MovieID"])) != true)
                            movieTimeLength.Add(Convert.ToInt32(reader["MovieID"]), new Tuple<int, int>(Convert.ToInt32(reader["TheaterRoom"]), Convert.ToInt32(reader["length"])));
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error");
            }

            //timeRange goes to 11:30 pm for latest showtime
            int timeRange = 24;
            int minuteRange = 30;

            TimeSpan startTime = new TimeSpan(12, 0, 0);
            list.Add(new SelectListItem() { Text = "Choose a time", Value = "0"});
            DateTime startDate = DateTime.Now.Date;

            bool flag = false;

            for (int i = 0; i < timeRange; i++)
            {
                int minutesAdded = minuteRange * i;
                TimeSpan timeAdded = new TimeSpan(0, minutesAdded, 0);
                TimeSpan tm = startTime.Add(timeAdded);
                DateTime result = startDate + tm;

                for (var j = 0; j < tempList.Count; j++)
                {
                    DateTime temp = Convert.ToDateTime(tempList[j].Value);
                    string screeningDate = Convert.ToDateTime(tempList[j].Value).ToShortDateString();
                    bool sameDate = selectedDate.Equals(screeningDate);
                    int resu = DateTime.Compare(result, temp);
                    if (sameDate == true && resu == 0)
                    {
                        flag = true;
                        lenOfMovieHours = movieTimeLength[Convert.ToInt32(tempList[j].Text)].Item2 / 30;
                        break;
                    }
                }

                if (flag)
                {
                    list.Add(new SelectListItem { Text = result.ToShortTimeString(), Value = result.ToShortTimeString(), Disabled = true });
                    flag = false;
                }
                else if (lenOfMovieHours > 0)
                {
                    list.Add(new SelectListItem { Text = result.ToShortTimeString(), Value = result.ToShortTimeString(), Disabled = true });
                    lenOfMovieHours--;
                }
                else
                    list.Add(new SelectListItem { Text = result.ToShortTimeString(), Value = result.ToShortTimeString() });
                }

                return list;
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
            getTheaterRooms();
            selectedTheaterRoom = theaterRoomList.FirstOrDefault().Value;
            TheaterRooms = new SelectList(theaterRoomList, "Value", "Value");
            dates();
            selectedDate = DateTime.Now.ToShortDateString();
            Dates = new SelectList(dateList);
            Times = listOfTimes();
            Movie = new SelectList(_db.Movie, "MovieID", "MovieTitle");
            selectedMovie = Movie.FirstOrDefault().Value;
        }

        public IActionResult OnPost(string createTime)
        {
            if (createTime != null)
            {
                string temp = selectedDate + " " + (Convert.ToDateTime(selectedTime)).ToShortTimeString();
                try
                {
                    using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                    {
                        connection.Open();
                        String myCommand = "INSERT INTO Screening (MovieID,TheaterRoom,ScreeningDateStart,CinemaID) VALUES(@sm,@st,@ti,@id)";
                        SqlCommand command = new SqlCommand(myCommand, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        command.Parameters.Add("@sm", SqlDbType.Int).Value = Convert.ToInt32(selectedMovie);
                        command.Parameters.Add("@st", SqlDbType.Int).Value = Convert.ToInt32(selectedTheaterRoom);
                        command.Parameters.Add("@ti", SqlDbType.DateTime).Value = Convert.ToDateTime(temp);
                        command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(User.FindFirst("CinemaID").Value);  

                        adapter.InsertCommand = command;
                        adapter.InsertCommand.ExecuteNonQuery();

                        command.Dispose();
                        connection.Close();
                    }
                }
                catch (SqlException)
                {
                    Console.WriteLine("Error");
                }

                TempData["success"] = "New Screening Created Successfully";
            }

            getTheaterRooms();
            TheaterRooms = new SelectList(theaterRoomList, "Value", "Value");
            dates();
            Dates = new SelectList(dateList);
            Times = listOfTimes();
            Movie = new SelectList(_db.Movie, "MovieID", "MovieTitle");

            return Page();
        }
    }
}
