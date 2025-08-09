using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.extensions;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.DTO;

namespace airport_ticket_booking_system.services.filter;

public static class FilterFlightService
{
    private static ModelRepository<Flight> _flightsRepo = new(new Flight());

    /// <summary>
    /// This Filter Flights using list of predicates if the filter property isn't null, so we want its predicate
    /// i wroted this before using query syntax and it was not readable, this is much better.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static List<Flight> FilterFlights(FlightFilter filter)
    {
        var flightsRepo = new ModelRepository<Flight>(new Flight());
        var query = flightsRepo.GetAllItems();

        var predicates = filter.GetPredicates();

        foreach (var predicate in predicates)
            query = query.Where(predicate);

        return query.ToList();
    }

    public static List<Flight> GetAllFlights()
    {
        return _flightsRepo
            .GetAllItems()
            .ToList();
    }

    public static Flight? GetFlightById(int id)
    {
        return _flightsRepo
            .GetAllItems()
            .ToList()
            .FirstOrDefault(flight => flight.Id == id);
    }
}