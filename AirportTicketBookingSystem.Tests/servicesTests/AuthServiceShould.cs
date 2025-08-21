using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.services.auth;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace AirportTicketBookingSystem.Tests.servicesTests;

public class AuthServiceShould
{
    private Mock<IModelRepository<Passenger>> _mockModelRepository;
    private IFixture _fixture;
    private IEnumerable<Passenger> _passengers;
    private const int CountOfMockedPassengers = 3;

    public AuthServiceShould()
    {
        _fixture = new Fixture();
        _mockModelRepository = new Mock<IModelRepository<Passenger>>();
        _passengers = _fixture.CreateMany<Passenger>(CountOfMockedPassengers);
        _mockModelRepository
            .Setup(p => p.GetAllItems())
            .Returns(_passengers);
    }

    [Fact]
    public void LoginPassengerTest()
    {
        // Arrange
        var sut = new AuthService(_mockModelRepository.Object);
        var passengers = _passengers.ToList();
        var randomPassengerIdx = new Random().Next(0, CountOfMockedPassengers - 1);

        // Act
        Passenger? passenger = sut.LoginPassenger(passengers[randomPassengerIdx].Username,
            passengers[randomPassengerIdx].Password);

        // Assert
        passenger.Should().NotBeNull();
    }

    [Fact]
    public void LoginPassengerTestFailure()
    {
        // Arrange
        var sut = new AuthService(_mockModelRepository.Object);
        var p = _fixture.Create<Passenger>();

        // Act
        Passenger? passenger = sut.LoginPassenger(p.Username, p.Password);

        // Assert
        passenger.Should().BeNull();
    }

    [Theory]
    [InlineData("", "12345")]
    [InlineData("Adas", null)]
    [InlineData("Amr", "12345")]
    public void LoginAdminTestFailure(string? username, string? password)
    {
        // Arrange
        var sut = new AuthService(_mockModelRepository.Object);

        // Act
        var result = sut.LoginManager(username, password);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void LoginAdminTest()
    {
        // Arrange
        var sut = new AuthService(_mockModelRepository.Object);

        // Act
        var result = sut.LoginManager("admin", "admin");

        // Assert
        result.Should().BeTrue();
    }
}