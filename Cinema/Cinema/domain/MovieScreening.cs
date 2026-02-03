using System.Globalization;

namespace Cinema
{
    public class MovieScreening
    {
        public Movie Movie { get; set; }
        public DateTime DateAndTime { get; set; }
        public double PricePerSeat { get; set; }

        public MovieScreening(Movie movie, DateTime dateAndTime, double pricePerSeat)
        {
            Movie = movie;
            DateAndTime = dateAndTime;
            PricePerSeat = pricePerSeat;
        }

        public double GetPricePerSeat()
        {
            return PricePerSeat;
        }

        public override string ToString()
        {
            return $"{Movie.Title} @ {DateAndTime:yyyy-MM-dd HH:mm} (Seat: {PricePerSeat.ToString("0.00", CultureInfo.InvariantCulture)})";
        }
    }
}
