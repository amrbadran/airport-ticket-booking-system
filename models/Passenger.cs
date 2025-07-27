using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using airport_ticket_booking_system.models.validation;

namespace airport_ticket_booking_system.models;

public class Passenger : IModel
{
    [Key]
    [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
    public int Id { get; }
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Username must be within 1 and 20 chars")]
    public string Username { get; set; }

    [StringLength(20, MinimumLength = 1, ErrorMessage = "password must be within 1 and 20 chars")]
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

    public IModel FromString(string line)
    {
        string[] items = line.Split(',');

        Passenger p = new Passenger(int.Parse(items[0]),items[1],items[2]);
        
        ValidationHelper.ValidateObjectOrThrow(p);

        return p;
    }
}