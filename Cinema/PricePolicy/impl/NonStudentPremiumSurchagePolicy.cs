namespace Cinema.PricePolicy.impl;

public sealed class NonStudentPremiumSurchagePolicy : IPremiumSurchargePolicy
{
    public double GetSurchargePerPremiumTicket()
    {
        return 3.0;
    }
}