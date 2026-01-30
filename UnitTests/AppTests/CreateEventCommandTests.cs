using Application.Common.Interfaces;
using Application.Features.Events.Commands.CreateEvent;
using Domain.Entities;
using NSubstitute;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTests.AppTests;

public class CreateEventCommandTests
{

    [Fact]
    public async Task Handle_Should_CreateEvent_And_PersistToRepository()
    {
        //Arrange
        var mockRepo = Substitute.For<IEventRepository>();
        var command = new CreateEventCommand("SuperConcert", 500);
        var handler = new CreateEventCommandHandler(mockRepo);
        
        //Act
        var resultId = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        await mockRepo.Received(1).AddAsync(Arg.Any<Event>(),  Arg.Any<CancellationToken>());
        Assert.NotEqual(Guid.Empty, resultId);
    }
    
}