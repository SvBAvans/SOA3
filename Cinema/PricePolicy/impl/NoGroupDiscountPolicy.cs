using Cinema.Domain;

namespace Cinema.PricePolicy.impl;

public sealed class NoGroupDiscountPolicy : IGroupDiscountPolicy
{
    public double ApplyDiscount(double subTotal, IReadOnlyList<MovieTicket> tickets)
    {
        return subTotal;
    }
}