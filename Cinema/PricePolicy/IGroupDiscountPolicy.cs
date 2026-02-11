using Cinema.Domain;

namespace Cinema.PricePolicy;

public interface IGroupDiscountPolicy
{
    double ApplyDiscount(double subTotal, IReadOnlyList<MovieTicket> tickets);
}