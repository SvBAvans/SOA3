namespace Cinema.Domain.State;

public class CancelledState : IOrderState
{
    public void Submit()
    {
        Console.WriteLine("Order is cancelled");
    }

    public void Pay()
    {
        Console.WriteLine("Order is cancelled");
    }

    public void AddSeatReservation(MovieTicket ticket)
    {
        Console.WriteLine("Order is cancelled");
    }

    public void Cancel()
    {
        Console.WriteLine("Order is cancelled");
    }
    
    public override string ToString()
    {
        return "Order state is: Cancelled";
    }
}