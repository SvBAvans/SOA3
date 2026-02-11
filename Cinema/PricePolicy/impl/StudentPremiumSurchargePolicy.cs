namespace Cinema.PricePolicy.impl;

public sealed class StudentPremiumSurchargePolicy : IPremiumSurchargePolicy
{
    public double GetSurchargePerPremiumTicket()
    {
        return 2.0;
    }
}