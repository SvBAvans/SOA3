namespace Cinema.Domain.State;

public class ProcessedState : IOrderState
{
    public void Submit()
    {
        Console.WriteLine("Order is already processed");
    }

    public void Pay()
    {
        Console.WriteLine("Order is already processed");
    }

    public void AddSeatReservation(MovieTicket ticket)
    {
        Console.WriteLine("Order is already processed");
    }

    public void Cancel()
    {
        Console.WriteLine("Order is already processed");
    }
    
    public override string ToString()
    {
        return "Order state is: Processed";
    }
}