using System.ComponentModel.DataAnnotations;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.models.validation;

namespace airport_ticket_booking_system.models;

public class Flight
{
    
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get;}
    
    
    public double Price { get; set; }

    public string DepartureCountry { get; set; }

    public string DestinationCountry { get; set; }
    
    [TodayOrFuture]
    public DateTime DepartureDate { get; set; }

    public int DepartureAirportId { get; set; }

    public int ArrivalAirportId { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Flight f && f.Id == this.Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public override string ToString()
    {
        return
            $"{Id},{Price},{DepartureCountry},{DestinationCountry},{DepartureDate},{DepartureAirportId},{ArrivalAirportId}";
    }
}