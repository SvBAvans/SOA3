using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public class MovieTicket
    {
        public MovieScreening MovieScreening {  get; set; }
        public int RowNr { get; set; }
        public int SeatNr { get; set; }
        public bool IsPremium { get; set; }

        public MovieTicket(MovieScreening movieScreening, bool isPremiumReservation, int seatRow, int seatNr)
        {
            MovieScreening = movieScreening;
            RowNr = seatRow;
            SeatNr = seatNr;
            IsPremium = isPremiumReservation;
        }

        public bool IsPremiumTicket()
        {
            return IsPremium;
        }

        public double GetPrice()
        {
            return MovieScreening.PricePerSeat;
        }

        public override string ToString()
        {
            var premiumTxt = IsPremium ? "Premium" : "Standard";
            return $"{MovieScreening.Movie.Title} | {MovieScreening.DateAndTime:yyyy-MM-dd HH:mm} | Row {RowNr}, Seat {SeatNr} | {premiumTxt}";
        }
    }
}
