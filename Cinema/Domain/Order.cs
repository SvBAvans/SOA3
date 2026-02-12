using Cinema.Domain.State;

namespace Cinema.Domain;

public class Order
{

    public IOrderState CreatedState { get; }
    public IOrderState CancelledState { get; }
    public IOrderState SubmittedState { get; }
    public IOrderState PayedState { get; }
    public IOrderState ProcessedState { get; }
    public IOrderState ProvisionalState { get; }

    public IOrderState State;
    public int OrderNr { get; set; }
    public bool IsStudentOrder { get; set; }
    public List<MovieTicket> Tickets { get; }

    public Order(int orderNr, bool isStudentOrder, List<MovieTicket> tickets)
    {
        OrderNr = orderNr;
        IsStudentOrder = isStudentOrder;
        Tickets = tickets;

        CreatedState = new CreatedState(this);
        CancelledState = new CancelledState();
        SubmittedState = new SubmittedState(this);
        PayedState = new PayedState();
        ProcessedState = new ProcessedState();
        ProvisionalState = new ProvisionalState(this);
        State = CreatedState;
    }

    public void SetState(IOrderState state)
    {
        State = state;
    }


    public void Submit()
    {
        State.Submit();
    }

    public void Pay()
    {
        State.Pay();
    }

    public void AddSeatReservation(MovieTicket ticket)
    {
        State.AddSeatReservation(ticket);
    }

    public void Cancel()
    {
        State.Cancel();
    }
}