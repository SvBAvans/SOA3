namespace Cinema.Domain.State;

public interface IOrderState
{
    void Submit();
    void Pay();
    void AddSeatReservation(MovieTicket ticket);
    void Cancel();
    string ToString();
}