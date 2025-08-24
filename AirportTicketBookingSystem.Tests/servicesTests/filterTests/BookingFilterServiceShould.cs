using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.DTO;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.services.filter;
using AirportTicketBookingSystem.Tests.servicesTests.filterTests.customizations;
using AutoFixture;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AirportTicketBookingSystem.Tests.servicesTests.filterTests;

using Moq;

public class BookingFilterServiceShould
{
    private Mock<IModelRepository<Booking>> _mockBookingRepository;
    private Mock<IModelRepository<Flight>> _mockFlightRepository;
    private Mock<IModelRepository<Airport>> _mockAirportRepository;
    private Mock<IModelRepository<Passenger>> _mockPassengerRepository;

    private BookingFilterService _service;
    private Fixture _fixture;

    private const int MaxNumberOfRecords = 5;

    private IEnumerable<Passenger> _passengers;
    private IEnumerable<Airport> _airports;
    private IEnumerable<Flight> _flights;
    private IEnumerable<Booking> _bookings;


    public BookingFilterServiceShould()
    {
        _fixture = new Fixture();

        _mockBookingRepository = new Mock<IModelRepository<Booking>>();
        _mockFlightRepository = new Mock<IModelRepository<Flight>>();
        _mockAirportRepository = new Mock<IModelRepository<Airport>>();
        _mockPassengerRepository = new Mock<IModelRepository<Passenger>>();

        _fixture.Customize(new PassengerCustomization());
        _fixture.Customize(new AirportCustomization());
        _fixture.Customize(new FlightCustomization(MaxNumberOfRecords));
        _fixture.Customize(new BookingCustomization());

        _passengers = _fixture.CreateMany<Passenger>(MaxNumberOfRecords);
        _airports = _fixture.CreateMany<Airport>(MaxNumberOfRecords);
        _flights = _fixture.CreateMany<Flight>(MaxNumberOfRecords);
        _bookings = _fixture.CreateMany<Booking>(MaxNumberOfRecords);

        _mockAirportRepository.Setup(m => m.GetAllItems()).Returns(_airports);
        _mockFlightRepository.Setup(m => m.GetAllItems()).Returns(_flights);
        _mockPassengerRepository.Setup(m => m.GetAllItems()).Returns(_passengers);
        _mockBookingRepository.Setup(m => m.GetAllItems()).Returns(_bookings);

        _service = new BookingFilterService(
            _mockBookingRepository.Object,
            _mockFlightRepository.Object,
            _mockAirportRepository.Object,
            _mockPassengerRepository.Object
        );
    }

    [Fact]
    public void FilterJoinedTestArrivalAirpot()
    {
        // Arrange

        var sut = _service;
        var flight = _flights.First();
        var filterData = _fixture.Build<FlightFilter>()
            .OmitAutoProperties()
            .With(f => f.ArrivalAirportId, flight.ArrivalAirportId)
            .Create();

        // Act
        var result = sut.FilterJoinedData(null, filterData, null, null);

        // Assert
        result.Should().AllSatisfy(r => r.ArrivalAirport.Id.Should().Be(flight.ArrivalAirportId));
    }

    [Fact]
    public void FilterJoinedTestFlightClass()
    {
        // Arrange

        var sut = _service;
        var filterData = _fixture.Build<FlightFilter>()
            .OmitAutoProperties()
            .Create();

        // Act
        var result = sut.FilterJoinedData(
            null,
            filterData,
            null,
            FlightClassEnum.Business);

        // Assert
        result
            .Should()
            .AllSatisfy(r => r.Booking.FlightClass.Should().Be(FlightClassEnum.Business));
    }
}