using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.DTO;
using airport_ticket_booking_system.services.filter;
using AirportTicketBookingSystem.Tests.servicesTests.filterTests.customizations;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace AirportTicketBookingSystem.Tests.servicesTests.filterTests;


public class FilterFlightServiceShould
{
    private Mock<IModelRepository<Flight>> _mockFlightRepository;
    private Fixture _fixture;
    
    private IEnumerable<Flight> _flights;
    private const int MaxNumberOfRecords = 5;

    public FilterFlightServiceShould()
    {
        _fixture = new Fixture();
        _mockFlightRepository = new Mock<IModelRepository<Flight>>();
        _fixture.Customize(new FlightCustomization(MaxNumberOfRecords));
        
        _flights = _fixture.CreateMany<Flight>(MaxNumberOfRecords);
        _mockFlightRepository.Setup(m => m.GetAllItems()).Returns(_flights);
    }
    
    /// <summary>
    /// for this test you can write multiple tests in the same way
    /// manipulate filter object and provide values for it then assert in the same way
    /// </summary>
    [Fact]
    public void FilterFlightsTest()
    {
        // Arrange
        var sut = new FilterFlightService(_mockFlightRepository.Object);
        var flight = _flights.First();
        var filter = _fixture.Build<FlightFilter>()
            .OmitAutoProperties()
            .With(f => f.ArrivalCountry, flight.DestinationCountry)
            .Create();
        
        // Act
        var flights = sut.FilterFlights(filter);
        
        // Assert
        flights.Should().AllSatisfy(f => f.DestinationCountry.Should().Be(flight.DestinationCountry));

    }

    [Fact]
    public void GetFlightByIdTest()
    {
        // Arrange
        var sut = new FilterFlightService(_mockFlightRepository.Object);
        var flightSut = _flights.First();
        
        // Act
        var flight = sut.GetFlightById(flightSut.Id);
        
        // Assert
        flight.Should().Be(flightSut);
    }

    [Fact]
    public void GetAllFlightsTest()
    {
        // Arrange
        var sut = new FilterFlightService(_mockFlightRepository.Object);
        
        // Act
        var flights = sut.GetAllFlights();
        
        // Assert
        flights.Should().Equal(_flights);
    }
}