namespace Cinema.Domain.State;

public class PayedState : IOrderState
{
    public void Submit()
    {
        Console.WriteLine("Order is being processed.");
    }

    public void Pay()
    {
        Console.WriteLine("Order is already paid and being processed.");
    }

    public void AddSeatReservation(MovieTicket ticket)
    {
        Console.WriteLine("Order is being processed.");
    }

    public void Cancel()
    {
        Console.WriteLine("Cannot cancel order. Order is being processed.");
    }
    
    public override string ToString()
    {
        return "Order state is: Payed";
    }
}