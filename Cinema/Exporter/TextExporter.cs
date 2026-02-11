using System.Text;
using Cinema.Domain;

namespace Cinema.Exporter;

public class TextExporter : IExporter
{
    public void Export(Order order)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Order number: {order.OrderNr}");
        sb.AppendLine($"Student order: {(order.IsStudentOrder ? "Yes" : "No")}");
        sb.AppendLine();
        sb.AppendLine("Tickets:");

        foreach (var ticket in order.Tickets)
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
        var (free, premium, group) = order.CreatePricePolicies();
        sb.AppendLine($"Total price: €{order.CalculatePrice(free, premium, group):0.00}");
        
        File.WriteAllText("output.txt", sb.ToString());
    }
}