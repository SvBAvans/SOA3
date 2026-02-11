using Cinema.Exporter;
using Cinema.PricePolicy;
using Cinema.PricePolicy.impl;

namespace Cinema.Domain
{
    public class Order
    {
        public int OrderNr { get; set; }
        public bool IsStudentOrder { get; set; }
        public List<MovieTicket> Tickets { get; } = new List<MovieTicket>();

        public Order(int orderNr, bool isStudentOrder)
        {
            OrderNr = orderNr;
            IsStudentOrder = isStudentOrder;
        }

        public int GetOrderNr()
        {
            return OrderNr;
        }

        public void AddSeatReservation(MovieTicket ticket)
        {
            Tickets.Add(ticket);
        }

        public double CalculatePrice(
            IFreeTicketPolicy freeTicketPolicy,
            IPremiumSurchargePolicy premiumSurchargePolicy,
            IGroupDiscountPolicy groupDiscountPolicy)
        {
            if (Tickets.Count == 0) return 0;

            bool[] isFree = freeTicketPolicy.GetFreeTickets(Tickets);
            double premiumExtra = premiumSurchargePolicy.GetSurchargePerPremiumTicket();

            double subTotal = 0.0;
            for (int i = 0; i < Tickets.Count; i++)
            {
                if (isFree[i]) continue;
                
                var ticket = Tickets[i];
                double price = ticket.MovieScreening.PricePerSeat;

                if (ticket.IsPremiumTicket())
                {
                    price += premiumExtra;
                }

                subTotal += price;
            }

            subTotal = groupDiscountPolicy.ApplyDiscount(subTotal, Tickets);
            
            return Math.Round(subTotal, 2, MidpointRounding.AwayFromZero);
        }

        public void Export(IExporter exporter)
        {
            exporter.Export(this);
        }
        
        public (IFreeTicketPolicy free, IPremiumSurchargePolicy premium, IGroupDiscountPolicy group) CreatePricePolicies()
        {
            if (IsStudentOrder)
            {
                return (
                    new StudentFreeTicketPolicy(),
                    new StudentPremiumSurchargePolicy(),
                    new NoGroupDiscountPolicy()
                );
            }

            return (new NonStudentFreeTicketPolicy(),
                new NonStudentPremiumSurchargePolicy(),
                new NonStudentWeekendGroupDiscountPolicy());
        }
    }

    public enum TicketExportFormat
    {
        PLAINTEXT,
        JSON
    }
}
