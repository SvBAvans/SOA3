using Cinema.Domain;
using Cinema.Util;

namespace Cinema.PricePolicy.impl;

public sealed class NonStudentFreeTicketPolicy : IFreeTicketPolicy
{
    public bool[] GetFreeTickets(IReadOnlyList<MovieTicket> tickets)
    {
        var isFree = new bool[tickets.Count];
        int weekdayCounter = 0;

        for (int i = 0; i < tickets.Count; i++)
        {
            bool isWeekday = WeekdayUtil.IsWeekday(tickets[i].MovieScreening.DateAndTime);
            if (!isWeekday) continue;

            weekdayCounter++;
            if (weekdayCounter % 2 == 0)
            {
                isFree[i] = true;
            }
            
        }
        
        return isFree;
    }
}