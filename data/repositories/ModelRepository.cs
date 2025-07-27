using airport_ticket_booking_system.data.handlers;
using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.data.repositories;

public class ModelRepository<T>
{
    private IEnumerable<T> _items = new List<T>();

    private string _projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    private string _path;

    private IModel Item;
    public ModelRepository(IModel item)
    {
        Item = item;
        _path = Path.Combine(_projectRoot, "files", DeterminePath());
    }
    
    public IEnumerable<T> GetAllItems()
    {
        _items = new CsvFileService(_path, Item).GetAll().Select(model => (T)model);
        return _items;
    }

    public async Task SaveAll(IEnumerable<Flight> flights)
    {
        await new CsvFileService(_path, new Flight()).WriteAllAsync(flights);
    }

    private string DeterminePath()
    {
        if (typeof(T) == typeof(Flight))
        {
            return "Flights.csv";
        }
        else if (typeof(T) == typeof(Booking))
        {
            return "Bookings.csv";
        }
        else if (typeof(T) == typeof(Passenger))
        {
            return "Passengers.csv";
        }
        else if (typeof(T) == typeof(Airport))
        {
            return "Airports.csv";
        }

        return "";
    }
    
}