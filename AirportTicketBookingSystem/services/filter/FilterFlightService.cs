using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.extensions;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.DTO;

namespace airport_ticket_booking_system.services.filter;

public class FilterFlightService
{
    private IModelRepository<Flight> _flightsRepo;

    public FilterFlightService(IModelRepository<Flight> flightsRepo)
    {
        _flightsRepo = flightsRepo;
    }
    /// <summary>
    /// This Filter Flights using list of predicates if the filter property isn't null, so we want its predicate
    /// i wroted this before using query syntax and it was not readable, this is much better.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public List<Flight> FilterFlights(FlightFilter filter)
    {
        var query = _flightsRepo.GetAllItems();

        var predicates = filter.GetPredicates();

        foreach (var predicate in predicates)
            query = query.Where(predicate);

        return query.ToList();
    }

    public List<Flight> GetAllFlights()
    {
        return _flightsRepo
            .GetAllItems()
            .ToList();
    }

    public Flight? GetFlightById(int id)
    {
        return _flightsRepo
            .GetAllItems()
            .ToList()
            .FirstOrDefault(flight => flight.Id == id);
    }
}