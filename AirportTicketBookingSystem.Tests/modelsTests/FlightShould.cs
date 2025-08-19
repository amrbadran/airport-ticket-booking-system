using System.Runtime.InteropServices.JavaScript;
using airport_ticket_booking_system.models;
using AutoFixture;
using FluentAssertions;

namespace AirportTicketBookingSystem.Tests.modelsTests;

public class FlightShould
{
    private Fixture _fixture;

    public FlightShould()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void FlightCreateSuccess()
    {
        // Arrange & Act 
        var sut = _fixture.Build<Flight>()
            .With(f => f.Id, new Random().Next(1, int.MaxValue))
            .With(f => f.ArrivalAirportId, new Random().Next(1, int.MaxValue))
            .With(f => f.DepartureAirportId, new Random().Next(1, int.MaxValue))
            .With(f => f.DepartureDate, DateTime.Today.AddDays(5))
            .Create();

        // Assert
        sut.Id.Should().BeGreaterThan(0);
        sut.Price.Should().BeGreaterThan(0.0);
        sut.DestinationCountry.Should().NotBeNull();
        sut.DepartureCountry.Should().NotBeNull();
        sut.DepartureDate.Should().BeOnOrAfter(DateTime.Today);
        sut.ArrivalAirportId.Should().BeGreaterThan(0);
        sut.DepartureAirportId.Should().BeGreaterThan(0);
    }

    [Fact]
    public void FlightToStringSuccess()
    {
        // Arrange 
        var sut = _fixture.Build<Flight>()
            .With(f => f.Id, new Random().Next(1, int.MaxValue))
            .With(f => f.ArrivalAirportId, new Random().Next(1, int.MaxValue))
            .With(f => f.DepartureAirportId, new Random().Next(1, int.MaxValue))
            .With(f => f.DepartureDate, DateTime.Today.AddDays(5))
            .Create();

        // Act
        var expected = $"{sut.Id},{sut.Price},{sut.DepartureCountry},{sut.DestinationCountry},{sut.DepartureDate.ToString("yyyy-MM-dd")},{sut.DepartureAirportId},{sut.ArrivalAirportId}";

        // Assert
        sut.ToString().Should().Be(expected);
    }

    [Fact]
    public void FlightFromStringSuccess()
    {
        // Arrange
        var sut = _fixture.Build<Flight>()
            .With(f => f.Id, new Random().Next(1, int.MaxValue))
            .With(f => f.ArrivalAirportId, new Random().Next(1, int.MaxValue))
            .With(f => f.DepartureAirportId, new Random().Next(1, int.MaxValue))
            .With(f => f.DepartureDate, DateTime.Today.AddDays(5))
            .Create();

        var line = sut.ToString();

        // Act
        var expected = sut.FromString(line);

        // Assert
        sut.Should().Be(expected);
    }
}