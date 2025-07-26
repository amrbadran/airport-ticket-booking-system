using System.Diagnostics.CodeAnalysis;

namespace airport_ticket_booking_system.models;

public class Passenger
{
    private static int _nextId = 1; 
    public required int Id { get; init; }
    public required string Username { get; set; }

    [SetsRequiredMembers]
    public Passenger(string username)
    {
        this.Id = _nextId++;
        this.Username = username;
    }
    
}