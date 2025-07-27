using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.services.auth;

public class AuthService
{
    public Dictionary<string, string> managers = new Dictionary<string, string>()
    {
        { "admin", "admin" },
        { "admin1", "admin1234" }
    };

    public bool LoginManager(string username, string password)
    {
        return managers
            .Count(m => m.Key == username && m.Value == password) > 0;
    }

    public Passenger? LoginPassenger(string username, string password)
    {
        var listOfPassengers = new ModelRepository<Passenger>(new Passenger())
            .GetAllItems().ToList();
        var matches = listOfPassengers
            .Where(p => p.Username == username && p.Password == password)
            .Take(2)
            .ToList();

        return matches.Count == 1 ? matches[0] : null;

    }
}