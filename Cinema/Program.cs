// See https://aka.ms/new-console-template for more information

using Cinema.Domain;

var movie = new Movie("Dune 2");
var screening = new MovieScreening(movie, new DateTime(2026, 2, 7, 20, 0, 0, DateTimeKind.Local), 12.50); // donderdag
movie.AddScreening(screening);

List<MovieTicket> tickets = [
    new(screening, false, 5, 10),
    new(screening, true, 5, 11),
    new(screening, false, 5, 12),
    new(screening, true, 5, 13)
];

var order = new Order(1, false, tickets);
Console.WriteLine(order.State);

order.Submit();
Console.WriteLine(order.State);

order.AddSeatReservation(new MovieTicket(screening, false, 10, 5));

order.Pay();
Console.WriteLine(order.State);

order.AddSeatReservation(new MovieTicket(screening, false, 10, 5));
order.SetState(order.ProcessedState);

order.Cancel();
Console.WriteLine(order.State);
