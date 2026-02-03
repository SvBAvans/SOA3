using Cinema.Domain;
using Newtonsoft.Json;
using System.Text;

namespace Cinema
{
    public class Order
    {
        public int OrderNr { get; set; }
        public bool IsStudentOrder { get; set; }
        public List<MovieTicket> Tickets = new List<MovieTicket>();

        public Order(int orderNr, bool isStudentOrder)
        {
            OrderNr = orderNr;
            IsStudentOrder = isStudentOrder;
        }

        public int GetOrderNr()
        {
            return OrderNr;
        }

        public void AddSeatReservation(MovieTicket ticket)
        {
            Tickets.Add(ticket);
        }

        public double CalculatePrice()
        {
            if (Tickets.Count == 0) return 0.0;

            // Determine which tickets are free.
            bool[] isFree = new bool[Tickets.Count];

            if (IsStudentOrder)
            {
                for (int i = 1; i < Tickets.Count; i += 2)
                {
                    isFree[i] = true;
                }
            }
            else
            {
                int weekdayCounter = 0;
                for (int i = 0; i < Tickets.Count; i++)
                {
                    DayOfWeek day = Tickets[i].MovieScreening.DateAndTime.DayOfWeek;
                    bool isWeekday = day is DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday or DayOfWeek.Thursday;

                    if (!isWeekday) continue;

                    weekdayCounter++;
                    if (weekdayCounter % 2 == 0)
                    {
                        isFree[i] = true;
                    }
                }
            }

            // Calculate total without group discount
            double premiumExtra = IsStudentOrder ? 2.0 : 3.0;

            double subTotal = 0.0;
            for (int i = 0; i < Tickets.Count; i++)
            {
                if (isFree[i]) continue;

                MovieTicket ticket = Tickets[i];
                double price = ticket.MovieScreening.PricePerSeat;

                if (ticket.IsPremiumTicket())
                {
                    price += premiumExtra;
                }

                subTotal += price;
            }

            // Calculate groupdiscount for non-students
            if (!IsStudentOrder && Tickets.Count >= 6)
            {
                bool isWeekend = Tickets.All(ticket =>
                    ticket.MovieScreening.DateAndTime.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday or DayOfWeek.Sunday);
            
            
                if (isWeekend)
                {
                    subTotal *= 0.90;
                }
            }

            return Math.Round(subTotal, 2, MidpointRounding.AwayFromZero);
        }

        public void Export(TicketExportFormat exportFormat)
        {
            if (exportFormat == TicketExportFormat.JSON)
            {
                string json = ExportToJson(true);
                File.WriteAllText("output.json", json);
            }     
            else if (exportFormat == TicketExportFormat.PLAINTEXT)
            {
                string text = ExportToPlainText();
                File.WriteAllText("output.txt", text);
            }
        }

        private string ExportToJson(bool pretty = false)
        {
            var json = JsonConvert.SerializeObject(this,  pretty ? Formatting.Indented : Formatting.None);
            return json;
        }
        
        private string ExportToPlainText()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Order number: {OrderNr}");
            sb.AppendLine($"Student order: {(IsStudentOrder ? "Yes" : "No")}");
            sb.AppendLine();
            sb.AppendLine("Tickets:");

            foreach (var ticket in Tickets)
            {
                sb.AppendLine(
                    $"- Movie: {ticket.MovieScreening.Movie.Title}, " +
                    $"Date: {ticket.MovieScreening.DateAndTime:dd-MM-yyyy HH:mm}, " +
                    $"Row: {ticket.RowNr}, " +
                    $"Seat: {ticket.SeatNr}, " +
                    $"Premium: {(ticket.IsPremiumTicket() ? "Yes" : "No")}"
                );
            }

            sb.AppendLine();
            sb.AppendLine($"Total price: €{CalculatePrice():0.00}");

            return sb.ToString();
        }

    }

    public enum TicketExportFormat
    {
        PLAINTEXT,
        JSON
    }
}
