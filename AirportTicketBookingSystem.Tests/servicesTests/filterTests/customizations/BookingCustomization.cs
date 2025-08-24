using airport_ticket_booking_system.models;
using AutoFixture;

namespace AirportTicketBookingSystem.Tests.servicesTests.filterTests.customizations;

public class BookingCustomization : ICustomization
{
    private int _counter = 1;
    private int _counter2 = 1;
    
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Booking>(c =>
            c.With(a => a.FlightBooked, () => _counter++)
                .With(a => a.PassengerBooked, () => _counter2++)
        );
    }
}