using airport_ticket_booking_system.data.interfaces;
using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.services.uploading;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace AirportTicketBookingSystem.Tests.servicesTests;

public class UploadFlightsServiceShould
{
    private Mock<IFileHandler> _mockFileHandler;
    private Mock<IModelRepository<Flight>> _mockModelRepository;
    private Fixture _fixture;
    private IEnumerable<Flight> _flights;

    public UploadFlightsServiceShould()
    {
        _fixture = new Fixture();
        _mockFileHandler = new Mock<IFileHandler>();
        _mockModelRepository = new Mock<IModelRepository<Flight>>();
        _flights = _fixture.CreateMany<Flight>();

        _mockFileHandler
            .Setup(m => m.GetAll())
            .Returns(_flights);
        _mockFileHandler
            .Setup(m => m.FileExists(It.IsAny<string>()))
            .Returns(true);
        _mockModelRepository
            .Setup(m => m.SaveAll(It.IsAny<IEnumerable<IModel>>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task UploadFlightsTestUpdated()
    {
        // Arrange
        var sut = new UploadFlightsService(_mockModelRepository.Object, _mockFileHandler.Object);
        _mockModelRepository
            .Setup(m => m.GetAllItems())
            .Returns(_fixture.CreateMany<Flight>());

        // Act
        var messages = await sut.Upload("flights.csv");

        // Assert
        messages.Count(m => m.StartsWith('A')).Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task UploadFlightsTestNotUpdated()
    {
        // Arrange
        var sut = new UploadFlightsService(_mockModelRepository.Object, _mockFileHandler.Object);
        _mockModelRepository
            .Setup(m => m.GetAllItems())
            .Returns(_flights);

        // Act
        var messages = await sut.Upload("flights.csv");

        // Assert
        messages.Count(m => m.StartsWith('A')).Should().Be(0);
    }
}