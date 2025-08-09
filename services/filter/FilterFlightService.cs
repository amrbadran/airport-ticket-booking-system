using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.services.filter;

public static class FilterFlightService
{
    public static List<Flight> FilterFlights(
        double? price,
        string? departCountry,
        string? arrivalCountry,
        int? departAirportId,
        int? arrivalAirportId,
        DateTime? departDate
    )
    {
        var flightsRepo = new ModelRepository<Flight>(new Flight());
        var listOfFlights = flightsRepo.GetAllItems().ToList();

        var filtered = listOfFlights.Where(flight =>
            (!price.HasValue || flight.Price == price.Value) &&
            (string.IsNullOrEmpty(departCountry) || flight.DepartureCountry == departCountry) &&
            (string.IsNullOrEmpty(arrivalCountry) || flight.DestinationCountry == arrivalCountry) &&
            (!departAirportId.HasValue || flight.DepartureAirportId == departAirportId.Value) &&
            (!arrivalAirportId.HasValue || flight.ArrivalAirportId == arrivalAirportId.Value) &&
            (!departDate.HasValue || flight.DepartureDate.Date == departDate.Value.Date)
        ).ToList();

        return filtered;
    }
    
    
    public static List<Flight> GetAllFlights()
    {
        var flightsRepo = new ModelRepository<Flight>(new Flight());
        var listOfFlights = flightsRepo.GetAllItems().ToList();
        return listOfFlights;
    }

    public static Flight? GetFlightById(int id)
    {
        var flightsRepo = new ModelRepository<Flight>(new Flight());
        var listOfFlights = flightsRepo.GetAllItems().ToList();

        return listOfFlights.FirstOrDefault(flight => flight.Id == id);
    }

    
    
}