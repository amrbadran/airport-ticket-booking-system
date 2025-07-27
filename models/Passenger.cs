using System.Diagnostics.CodeAnalysis;

namespace airport_ticket_booking_system.models;

public class Passenger
{
    public int Id { get; }
    public string Username { get; set; }

    public string Password { get; set; }

    [SetsRequiredMembers]
    public Passenger(int id, string username, string password)
    {
        this.Id = id;
        this.Username = username;
        this.Password = password;
    }

    public override bool Equals(object? obj)
    {
        return obj is Passenger p && p.Id == this.Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{this.Id},{this.Username},{this.Password}";
    }
}