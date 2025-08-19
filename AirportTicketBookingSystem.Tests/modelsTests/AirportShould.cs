using airport_ticket_booking_system.models;
using AutoFixture;
using FluentAssertion;
using FluentAssertions;

namespace AirportTicketBookingSystem.Tests.modelsTests;

public class AirportShould
{
    private Fixture _fixture;

    public AirportShould()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void AirportCreateSuccess()
    {
        // Arrange & Act 
        var sut = _fixture.Build<Airport>()
            .With(a => a.Id, new Random().Next(1, int.MaxValue))
            .Create();

        // Assert
        sut.AirportName.Should().NotBeNull();
        sut.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void AirportToStringSuccess()
    {
        // Arrange
        var sut = _fixture.Build<Airport>()
            .With(a => a.Id, new Random().Next(1, int.MaxValue))
            .Create();

        // Act
        var expected = $"{sut.Id},{sut.AirportName}";

        // Assert
        sut.ToString().Should().Be(expected);
    }

    [Fact]
    public void AirportFromStringSuccess()
    {
        // Arrange
        var sut = _fixture.Build<Airport>()
            .With(a => a.Id, new Random().Next(1, int.MaxValue))
            .Create();

        var line = sut.ToString();

        // Act
        var expected = sut.FromString(line);

        // Assert
        sut.Should().Be(expected);
    }
}