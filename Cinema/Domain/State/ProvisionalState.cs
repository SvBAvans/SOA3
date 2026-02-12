namespace Cinema.Domain.State;

public class ProvisionalState(Order order) : IOrderState
{
    public void Submit()
    {
        Console.WriteLine("Order is already submitted");
    }

    public void Pay()
    {
        Console.WriteLine("You payed the order");
        order.SetState(order.PayedState);
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
        return "Order state is: Provisional";
    }
}