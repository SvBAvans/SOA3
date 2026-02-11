using Cinema.Domain;
using Cinema;
using Cinema.Exporter;

var movie = new Movie("Dune 2");
var screening = new MovieScreening(movie, new DateTime(2026, 2, 7, 20, 0, 0, DateTimeKind.Local), 12.50); // donderdag
movie.AddScreening(screening);

var order = new Order(1, isStudentOrder: true);

order.AddSeatReservation(new MovieTicket(screening, false, 5, 10));
order.AddSeatReservation(new MovieTicket(screening, true, 5, 11));  // premium
order.AddSeatReservation(new MovieTicket(screening, false, 5, 12));
order.AddSeatReservation(new MovieTicket(screening, true, 5, 13));  // premium

var (free, premium, group) = order.CreatePricePolicies();
Console.WriteLine($"Price: {order.CalculatePrice(free, premium, group):0.00}");

order.Export(new TextExporter());
order.Export(new JsonExporter());

Console.WriteLine("Exported order_1.txt and order_1.json");
