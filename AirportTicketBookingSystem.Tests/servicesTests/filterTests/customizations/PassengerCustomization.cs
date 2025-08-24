using airport_ticket_booking_system.models;

namespace AirportTicketBookingSystem.Tests.servicesTests.filterTests.customizations;
using AutoFixture;

public class PassengerCustomization : ICustomization
{
    private int _counter = 1;
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Passenger>(
                c => c.With(
                    p => p.Id,() => _counter++));
    }
}