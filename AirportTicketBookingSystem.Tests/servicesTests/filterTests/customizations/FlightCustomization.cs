using airport_ticket_booking_system.models;
using AutoFixture;

namespace AirportTicketBookingSystem.Tests.servicesTests.filterTests.customizations;

public class FlightCustomization : ICustomization
{
    
    
    private int _counter = 1;
    private int _counter2 = 1;
    private int _counter3 = 1;
    private readonly int _maxNumberOfFlights;
    public FlightCustomization(int maxNumberOfFlights)
    {
        _maxNumberOfFlights = maxNumberOfFlights;
    }
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Flight>(c => 
            c.With(f => f.Id, () => _counter++)
                .With(f => f.ArrivalAirportId, () => _counter2++)
                .With(f => f.DepartureAirportId, () => (_counter3++ % _maxNumberOfFlights) + 1)
            );
    }
}