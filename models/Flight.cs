using airport_ticket_booking_system.models.enums;

namespace airport_ticket_booking_system.models;

public class Flight
{

    public int Id { get;}

    public double Price { get; set; }

    public string DepartureCountry { get; set; }

    public string DestinationCountry { get; set; }

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
            $"{Id},{Price},{DepartureCountry},{DestinationCountry},{DepartureDate},{DepartureAirportId},{DepartureAirportId}";
    }
}