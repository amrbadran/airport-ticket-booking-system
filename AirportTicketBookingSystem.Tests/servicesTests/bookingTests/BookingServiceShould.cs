using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.services.booking;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;

namespace AirportTicketBookingSystem.Tests.servicesTests.bookingTests;

public class BookingServiceShould
{
    private Fixture _fixture;
    private Mock<IModelRepository<Booking>> _mockModelRepostiory;
    private IEnumerable<Booking> _bookings;

    public BookingServiceShould()
    {
        _fixture = new Fixture();
        _mockModelRepostiory = new Mock<IModelRepository<Booking>>();
        _bookings = _fixture.CreateMany<Booking>();

        _mockModelRepostiory
            .Setup(m => m.GetAllItems())
            .Returns(_bookings);
        _mockModelRepostiory
            .Setup(m => m.SaveAll(It.IsAny<IEnumerable<Booking>>()))
            .Returns(Task.CompletedTask)
            .Callback<IEnumerable<IModel>>(m => _bookings = (IEnumerable<Booking>)m);
    }

    [Theory]
    [AutoData]
    public async Task BookFlightTest(int passengerId, int flightId, FlightClassEnum flightClass)
    {
        // Arrange
        var sut = new BookingService(_mockModelRepostiory.Object);
        var count = _bookings.Count();
        
        // Act
        await sut.BookFlight(passengerId, flightId, flightClass);

        // Assert
        _bookings.Count().Should().BeGreaterThan(count);
    }

    [Theory]
    [AutoData]
    public async Task ModifyBookingFlightTest(FlightClassEnum flightClass)
    {
        // Arrange
        var sut = new BookingService(_mockModelRepostiory.Object);
        var passengerSut = _bookings.First();

        var (passengerId, flightId) = (passengerSut.PassengerBooked, passengerSut.FlightBooked);

        // Act
        await sut.ModifyBooking(passengerId, flightId, flightClass);
        
        // Assert
        var passenger = _bookings
            .First(p => p.PassengerBooked == passengerId && p.FlightBooked == flightId);

        passenger.FlightBooked.Should().Be(flightId);
        passenger.FlightClass.Should().Be(flightClass);
    }

    [Fact]
    public async Task CancelBookingFlightTest()
    {
        // Arrange
        var sut = new BookingService(_mockModelRepostiory.Object);
        var passengerSut = _bookings.First();
        var (passengerId, flightId) = (passengerSut.PassengerBooked, passengerSut.FlightBooked);

        // Act
        await sut.CancelBooking(passengerId, flightId);
        
        // Assert
        var passenger = _bookings
            .FirstOrDefault(p => p.PassengerBooked == passengerId && p.FlightBooked == flightId);

        passenger.Should().BeNull();
    }

    [Fact]
    public void GetAllBookingTest()
    {
        // Arrange
        var sut = new BookingService(_mockModelRepostiory.Object);
        var passengerSut = _bookings.First();
        var passengerId = passengerSut.PassengerBooked;

        // Act
        List<Booking> bookings = sut.GetAllBooking(passengerId);

        // Assert
        bookings.Should().OnlyContain(b => b.PassengerBooked == passengerId);
    }
}