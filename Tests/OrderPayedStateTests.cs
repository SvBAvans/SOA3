using Cinema.Domain.State;

namespace Tests;

public class OrderPayedStateTests
{
    [Fact]
    public void AddSeatReservation_IsNotAllowed_AfterPayment()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.Pay();

        var ticketCount = order.Tickets.Count;

        // Act
        order.AddSeatReservation(order.Tickets.First());

        // Assert
        Assert.Equal(ticketCount, order.Tickets.Count);
    }

    [Fact]
    public void Cancel_IsNotAllowed_AfterPayment()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.Pay();

        // Act
        order.Cancel();

        // Assert
        Assert.IsNotType<CancelledState>(order.State);
    }
}