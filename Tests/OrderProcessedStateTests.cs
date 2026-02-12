using Cinema.Domain.State;

namespace Tests;

public class OrderProcessedStateTests
{
    [Fact]
    public void AddSeatReservation_IsNotAllowed_AfterProcessed()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.SetState(order.ProcessedState);

        var ticketCount = order.Tickets.Count;

        // Act
        order.AddSeatReservation(order.Tickets.First());

        // Assert
        Assert.Equal(ticketCount, order.Tickets.Count);
        Assert.IsType<ProcessedState>(order.State);
    }

    [Fact]
    public void Pay_IsNotAllowed_AfterProcessed()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.SetState(order.ProcessedState);

        // Act
        order.Pay();

        // Assert
        Assert.IsType<ProcessedState>(order.State);
    }
    
    [Fact]
    public void Submit_IsNotAllowed_AfterProcessed()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.SetState(order.ProcessedState);

        // Act
        order.Submit();

        // Assert
        Assert.IsNotType<SubmittedState>(order.State);
    }

    [Fact]
    public void Cancel_IsNotAllowed_AfterProcessed()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.SetState(order.ProcessedState);

        // Act
        order.Cancel();

        // Assert
        Assert.IsNotType<CancelledState>(order.State);
    }
}