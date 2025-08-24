using System.ComponentModel.DataAnnotations;
using System.Globalization;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.models.validation;

namespace airport_ticket_booking_system.models;

public class Flight : IModel
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; init; }

    [Range(1, Double.MaxValue, ErrorMessage = "Price Should be positive")]
    public double Price { get; set; }

    public string DepartureCountry { get; set; }

    public string DestinationCountry { get; set; }
    
    [TodayOrFuture(ErrorMessage = "Date Must Be in future")]
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
            $"{Id},{Price},{DepartureCountry},{DestinationCountry},{DepartureDate.ToString("yyyy-MM-dd")},{DepartureAirportId},{ArrivalAirportId}";
    }

    public IModel FromString(string line)
    {
        string[] items = line.Split(',');

        Flight f = new Flight()
        {
            Id = int.Parse(items[0]),
            Price = double.Parse(items[1]),
            DepartureCountry = items[2],
            DestinationCountry = items[3],
            DepartureDate = DateTime.ParseExact(items[4], "yyyy-MM-dd", null),
            DepartureAirportId = int.Parse(items[5]),
            ArrivalAirportId = int.Parse(items[6])
        };

        ValidationHelper.ValidateObjectOrThrow(f);
        return f;
    }
}