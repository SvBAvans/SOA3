using Cinema.Domain.State;

namespace Tests;

public class OrderProvisionalStateTests
{
    
    [Fact]
    public void AddSeatReservation_IsAllowed_InProvisional()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        var ticketCount = order.Tickets.Count;
        order.SetState(order.ProvisionalState);

        // Act
        order.AddSeatReservation(order.Tickets.First());

        // Assert
        Assert.Equal(ticketCount + 1, order.Tickets.Count);
    }
    
    [Fact]
    public void Pay_FromProvisional_MovesToPayed()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.SetState(order.ProvisionalState);

        // Act
        order.Pay();

        // Assert
        Assert.IsType<PayedState>(order.State);
    }
    
    [Fact]
    public void Submit_IsNotAllowed_AfterProvisional()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.Submit();
        order.SetState(order.ProvisionalState);

        // Act
        order.Submit();

        // Assert
        Assert.IsNotType<SubmittedState>(order.State);
    }

    [Fact]
    public void Cancel_FromProvisional_MovesToCancelled()
    {
        // Arrange
        var order = OrderTestHelper.CreateDefaultOrder(DateTime.Now.AddDays(2));
        order.SetState(order.ProvisionalState);

        // Act
        order.Cancel();

        // Assert
        Assert.IsType<CancelledState>(order.State);
    }
}