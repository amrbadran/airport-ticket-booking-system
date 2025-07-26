using System.Diagnostics.CodeAnalysis;
namespace airport_ticket_booking_system.models;

public class Airport
{
    private static int _nextId = 1; 
    public required int Id { get; init; }
    public required string AirportName { get; set; }

    [SetsRequiredMembers]
    public Airport(string airportName)
    {
        this.Id = _nextId++;
        this.AirportName = airportName;
    }
}