using airport_ticket_booking_system.models;
using AutoFixture;

namespace AirportTicketBookingSystem.Tests.servicesTests.filterTests.customizations;

public class AirportCustomization : ICustomization
{
    private int _counter = 1;

    public void Customize(IFixture fixture)
    {
        fixture.Customize<Airport>(c => c.With(
                a => a.Id, () => _counter++
            ));
    }
}