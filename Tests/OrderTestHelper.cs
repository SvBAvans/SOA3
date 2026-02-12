namespace Tests;

using Cinema.Domain;

public static class OrderTestHelper
{
    public static Order CreateDefaultOrder(DateTime screeningTime)
    {
        var movie = new Movie("Dune 2");
        var screening = new MovieScreening(movie, screeningTime, 12.50);
        movie.AddScreening(screening);

        var tickets = new List<MovieTicket>
        {
            new(screening, false, 5, 10),
            new(screening, true, 5, 11)
        };

        return new Order(1, false, tickets);
    }
}