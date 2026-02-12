using Cinema.Domain.State;

namespace Tests;

public class OrderCreatedStateTests
{
    [Fact]
    public void NewOrder_StartsInCreatedState()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));

        // Assert
        Assert.IsType<CreatedState>(order.State);
    }

    [Fact]
    public void Submit_FromCreated_MovesToSubmitted()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));

        // Act
        order.Submit();

        // Assert
        Assert.IsType<SubmittedState>(order.State);
    }

    [Fact]
    public void Cancel_FromCreated_MovesToCancelled()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));

        // Act
        order.Cancel();

        // Assert
        Assert.IsType<CancelledState>(order.State);
    }

    [Fact]
    public void AddSeatReservation_IsAllowed_InCreated()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        var ticketCount = order.Tickets.Count;

        // Act
        order.AddSeatReservation(order.Tickets.First());

        // Assert
        Assert.Equal(ticketCount + 1, order.Tickets.Count);
    }
}