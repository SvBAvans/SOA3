using Cinema.Domain.State;

namespace Tests;

public class OrderSubmittedStateTests
{
    [Fact]
    public void Pay_FromSubmitted_MovesToPayed()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();

        // Act
        order.Pay();

        // Assert
        Assert.IsType<PayedState>(order.State);
    }

    [Fact]
    public void Submit_IsNotAllowed_AfterSubmitted()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();

        // Act
        order.Submit();

        // Assert
        Assert.IsType<SubmittedState>(order.State);
    }

    [Fact]
    public void Cancel_IsAllowed_BeforePayment()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();

        // Act
        order.Cancel();

        // Assert
        Assert.IsType<CancelledState>(order.State);
    }
    
    [Fact]
    public void AddSeatReservation_IsAllowed_InSubmitted()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        var ticketCount = order.Tickets.Count;
        order.Submit();

        // Act
        order.AddSeatReservation(order.Tickets.First());

        // Assert
        Assert.Equal(ticketCount + 1, order.Tickets.Count);
    }
}