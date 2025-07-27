using airport_ticket_booking_system.models.enums;
using System.ComponentModel.DataAnnotations;
namespace airport_ticket_booking_system.models;

public class Booking
{

    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int FlightBooked { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int PassengerBooked { get; set; }

    public FlightClassEnum FlightClass { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Booking b && b.PassengerBooked == this.PassengerBooked
                                && b.FlightBooked == this.FlightBooked
                                && b.FlightClass == this.FlightClass;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FlightBooked, PassengerBooked, FlightClass);
    }

    public override string ToString()
    {
        return $"{FlightBooked},{PassengerBooked},{FlightClass}";
    }
}