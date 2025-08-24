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

    private IModelRepository<Passenger> _modelRepository;
    public AuthService(IModelRepository<Passenger> modelRepository)
    {
        _modelRepository = modelRepository;
    }
    
    /// <summary>
    /// login function for managers
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>if the manager exists return true othwerwise false</returns>
    public bool LoginManager(string? username, string? password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return false;
        return managers
            .Count(m => m.Key == username && m.Value == password) > 0;
    }

    /// <summary>
    /// login function for passengers
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>if the passenger exists in the system return the object otherwise return null</returns>
    public Passenger? LoginPassenger(string? username, string? password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;
        var listOfPassengers =  _modelRepository
            .GetAllItems().ToList(); 
        var matches = listOfPassengers
            .Where(p => p.Username == username && p.Password == password)
            .Take(1)
            .ToList();

        return matches.Count == 1 ? matches[0] : null;

    }
}