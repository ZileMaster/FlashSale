using Domain.Entities;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTests;

public class EventTests
{
    [Fact]
    public void ReduceStock_Should_DecreaseQuantity_When_StockIsAvailable()
    {
        //Arange
        var concert = new Event("Taylor Swift", 100);
        int quantityToBuy = 5;
        
        //Act
        concert.ReduceStock(quantityToBuy); 
        
        //Assert
        Assert.Equal(95, concert.AvailableTickets);
    }

    [Fact]
    public void ReduceStock_Should_Throw_When_NotEnoughStock()
    {
        //Arrange
        var concert = new Event("Taylor Swift", 5);
        int quantityToBuy = 10;
        
        //Act & Assert
        Assert.Throws<InvalidOperationException>(() => concert.ReduceStock(quantityToBuy));
    }
}