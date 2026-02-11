namespace Cinema.PricePolicy.impl;

public sealed class NonStudentPremiumSurchargePolicy : IPremiumSurchargePolicy
{
    public double GetSurchargePerPremiumTicket()
    {
        return 3.0;
    }
}