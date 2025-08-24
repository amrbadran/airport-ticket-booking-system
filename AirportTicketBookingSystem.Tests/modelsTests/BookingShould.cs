using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;
using AutoFixture;
using FluentAssertions;

namespace AirportTicketBookingSystem.Tests.modelsTests;

public class BookingShould
{
    private Fixture _fixture;

    public BookingShould()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void BookingCreateSuccess()
    {
        // Arrange & Act 
        var sut = _fixture.Build<Booking>()
            .With(b => b.FlightBooked, new Random().Next(1, int.MaxValue))
            .With(b => b.PassengerBooked, new Random().Next(1, int.MaxValue))
            .Create();

        // Assert
        sut.FlightBooked.Should().BeGreaterThan(0);
        sut.PassengerBooked.Should().BeGreaterThan(0);
        sut.FlightClass.Should().BeOneOf(
            FlightClassEnum.Business,
            FlightClassEnum.Economy,
            FlightClassEnum.FirstClass);
    }

    [Fact]
    public void BookingToStringSuccess()
    {
        // Arrange
        var sut = _fixture.Build<Booking>()
            .With(b => b.FlightBooked, new Random().Next(1, int.MaxValue))
            .With(b => b.PassengerBooked, new Random().Next(1, int.MaxValue))
            .Create();

        // Act
        var expected = $"{sut.FlightBooked},{sut.PassengerBooked},{sut.FlightClass}";

        // Assert
        sut.ToString().Should().Be(expected);
    }

    [Fact]
    public void BookingFromStringSuccess()
    {
        // Arrange
        var sut = _fixture.Build<Booking>()
            .With(b => b.FlightBooked, new Random().Next(1, int.MaxValue))
            .With(b => b.PassengerBooked, new Random().Next(1, int.MaxValue))
            .Create();

        var line = sut.ToString();

        // Act
        var expected = sut.FromString(line);

        // Assert
        sut.Should().Be(expected);
    }
}