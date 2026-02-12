namespace Cinema.Domain.State;

public class CreatedState(Order order) : IOrderState
{
    
    public void Submit()
    {
        Console.WriteLine("You submitted the order");
        order.SetState(order.SubmittedState);
    }

    public void Pay()
    {
        Console.WriteLine("Order not submitted");
    }

    public void AddSeatReservation(MovieTicket ticket)
    {
        order.Tickets.Add(ticket);
    }

    public void Cancel()
    {
        Console.WriteLine("You cancelled the order");
        order.SetState(order.CancelledState);
    }

    public override string ToString()
    {
        return "Order state is: Created";
    }
}