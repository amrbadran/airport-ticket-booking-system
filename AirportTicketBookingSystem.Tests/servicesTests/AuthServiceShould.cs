using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.services.auth;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace AirportTicketBookingSystem.Tests.servicesTests;

public class AuthServiceShould : IClassFixture<Mock<IModelRepository<Passenger>>>
{
    private Mock<IModelRepository<Passenger>> _mockModelRepository;
    private IFixture _fixture;

    public AuthServiceShould()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockModelRepository = new Mock<IModelRepository<Passenger>>();
        
        _mockModelRepository.Setup(p => p.GetAllItems()).Returns(new[]
        {
            new Passenger(1,"amr","12345"),
            new Passenger(2,"ahmad","12345"),
            new Passenger(3,"omar","12345")
        });
    }

    [Fact]
    public void LoginPassengerTest()
    {
        // Arrange
        var sut = new AuthService(_mockModelRepository.Object);

        // Act
    }
}