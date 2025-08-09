using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.services.filter;

public class FilteredResult
{
    public Flight Flight { get; set; }
    public Booking Booking { get; set; }
    public Passenger Passenger { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
}
