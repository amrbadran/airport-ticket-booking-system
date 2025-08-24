using airport_ticket_booking_system.models.enums;
using System.ComponentModel.DataAnnotations;
using airport_ticket_booking_system.models.validation;

namespace airport_ticket_booking_system.models;

public class Booking : IModel
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

    public IModel FromString(string line)
    {
        string[] items = line.Split(',');
        Booking b = new Booking()
        {
            FlightBooked = int.Parse(items[0]),
            PassengerBooked = int.Parse(items[1]),
            FlightClass = Enum.Parse<FlightClassEnum>(items[2])
        };
        ValidationHelper.ValidateObjectOrThrow(b);
        return b;
    }
}