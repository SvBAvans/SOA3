using Cinema.Domain.State;

namespace Tests;

public class OrderCancelledStateTests
{
    [Fact]
    public void AddSeatReservation_IsNotAllowed_AfterCancelled()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.Cancel();

        var ticketCount = order.Tickets.Count;

        // Act
        order.AddSeatReservation(order.Tickets.First());

        // Assert
        Assert.Equal(ticketCount, order.Tickets.Count);
        Assert.IsType<CancelledState>(order.State);
    }

    [Fact]
    public void Pay_IsNotAllowed_AfterCancelled()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.Cancel();

        // Act
        order.Pay();

        // Assert
        Assert.IsType<CancelledState>(order.State);
    }
    
    [Fact]
    public void Submit_IsNotAllowed_AfterCancelled()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.Cancel();

        // Act
        order.Submit();

        // Assert
        Assert.IsNotType<SubmittedState>(order.State);
    }

    [Fact]
    public void Cancel_IsNotAllowed_AfterCancelled()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.Cancel();

        // Act
        order.Cancel();

        // Assert
        Assert.IsType<CancelledState>(order.State);
    }
}