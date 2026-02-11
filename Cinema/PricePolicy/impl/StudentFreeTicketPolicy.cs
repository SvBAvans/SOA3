using Cinema.Domain;

namespace Cinema.PricePolicy.impl;

public sealed class StudentFreeTicketPolicy : IFreeTicketPolicy
{
    public bool[] GetFreeTickets(IReadOnlyList<MovieTicket> tickets)
    {
        var isFree = new bool[tickets.Count];
        for (int i = 1; i < tickets.Count; i += 2)
        {
            isFree[i] = true;
        }
        return isFree;
    }
}