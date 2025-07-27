using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace airport_ticket_booking_system.models;

public class Airport
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; }
    public required string AirportName { get; set; }

    [SetsRequiredMembers]
    public Airport(int id, string airportName)
    {
        this.Id = id;
        this.AirportName = airportName;
    }

    public override bool Equals(object? obj)
    {
        return obj is Airport a && a.Id == Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Id},{AirportName}";
    }
}