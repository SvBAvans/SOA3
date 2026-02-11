using Cinema.Domain;

namespace Cinema.PricePolicy;

public interface IFreeTicketPolicy
{
    bool[] GetFreeTickets(IReadOnlyList<MovieTicket> tickets);
}