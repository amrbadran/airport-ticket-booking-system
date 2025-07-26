using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.validation.attributes;
namespace airport_ticket_booking_system.models;

public class Flight
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;
    
    public double Price { get; set; }
    
    [Required]
    public string DepartureCountry { get; set; }
    
    [Required]
    public string DestinationCountry { get; set; }
    
    [DateRangeAttribute]
    public DateTime DepartureDate { get; set; }
    
    [Required]
    public Airport DepartureAirport { get; set; }
    
    [Required]
    public Airport ArrivalAirport { get; set; }
    
    [Required]
    public FlightClassEnum FlighClass { get; set; }
}