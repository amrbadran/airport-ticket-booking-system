using airport_ticket_booking_system.models.enums;

namespace airport_ticket_booking_system.models;

public class Flight
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;

    public double Price { get; set; }

    public string DepartureCountry { get; set; }

    public string DestinationCountry { get; set; }

    public DateTime DepartureDate { get; set; }

    public Airport DepartureAirport { get; set; }

    public Airport ArrivalAirport { get; set; }

    public FlightClassEnum FlighClass { get; set; }
}