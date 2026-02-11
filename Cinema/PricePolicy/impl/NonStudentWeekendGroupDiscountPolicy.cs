using Cinema.Domain;
using Cinema.Util;

namespace Cinema.PricePolicy.impl;

public sealed class NonStudentWeekendGroupDiscountPolicy : IGroupDiscountPolicy
{
    public double ApplyDiscount(double subTotal, IReadOnlyList<MovieTicket> tickets)
    {
        if (tickets.Count < 6) return subTotal;

        bool allWeekend = tickets.All(ticket => !WeekdayUtil.IsWeekday(ticket.MovieScreening.DateAndTime));
        
        return allWeekend ? subTotal * 0.90 : subTotal;
    }
}