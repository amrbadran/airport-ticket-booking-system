using airport_ticket_booking_system.models;

namespace AirportTicketBookingSystem.Tests.modelsTests;
using AutoFixture;
using FluentAssertions;
public class PassengerShould
{
    private Fixture _fixture;

    public PassengerShould()
    {
        _fixture = new Fixture();
    }
    
    [Fact]
    public void PassengerCreateSuccess()
    {
        // Arrange & Act 
        var sut = _fixture.Build<Passenger>()
            .With(a => a.Id, new Random().Next(1, int.MaxValue))
            .Create();

        // Assert
        sut.Username.Should().NotBeNullOrEmpty();
        sut.Username.Length.Should().BeInRange(1, 20);
        sut.Password.Should().NotBeNullOrEmpty();
        sut.Username.Length.Should().BeInRange(1, 20);

        sut.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void PassengerToStringSuccess()
    {
        // Arrange
        var sut = _fixture.Build<Passenger>()
            .With(a => a.Id, new Random().Next(1, int.MaxValue))
            .Create();

        // Act
        var expected = $"{sut.Id},{sut.Username},{sut.Password}";

        // Assert
        sut.ToString().Should().Be(expected);
    }

    [Fact]
    public void PassengerFromStringSuccess()
    {
        // Arrange
        var sut = _fixture.Build<Passenger>()
            .With(a => a.Id, new Random().Next(1, int.MaxValue))
            .Create();
        var line = sut.ToString();

        // Act
        var expected = sut.FromString(line);

        // Assert
        sut.Should().Be(expected);
    }

}