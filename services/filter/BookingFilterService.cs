using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.DTO;
using airport_ticket_booking_system.models.enums;

namespace airport_ticket_booking_system.services.filter;

public static class BookingFilterService
{
    private static ModelRepository<Booking> BookingRepo = new(new Booking());
    private static ModelRepository<Flight> FlightRepo = new(new Flight());
    private static ModelRepository<Airport> AirportRepo = new(new Airport());
    private static ModelRepository<Passenger> PassengerRepo = new(new Passenger());

    /// <summary>
    /// This Function used for filter all things in system and it used by manager
    /// the idea is filter all things based on parameters then join all the filtered data.
    /// </summary>
    /// <param name="flightId"></param>
    /// <param name="flightFilter"></param>
    /// <param name="passengerId"></param>
    /// <param name="flightClass"></param>
    /// <returns></returns>
    public static List<FilteredResult> FilterJoinedData(
        int? flightId,
        FlightFilter? flightFilter,
        int? passengerId,
        FlightClassEnum? flightClass
    )
    {
        if (flightFilter == null) throw new ArgumentException();

        // Filter All Model Repos then Join Them
        List<Func<Flight, bool>> predicatesFlight = flightFilter.GetPredicates();
        var flights =
            FlightRepo
                .GetAllItems()
                .Where(f => !flightId.HasValue || f.Id == flightId.Value);
        foreach (var predicate in predicatesFlight)
        {
            flights = flights.Where(predicate);
        }

        var bookings =
            BookingRepo
                .GetAllItems()
                .Where(b => !flightClass.HasValue || b.FlightClass == flightClass.Value);

        var passengers =
            PassengerRepo
                .GetAllItems()
                .Where(p => !passengerId.HasValue || p.Id == passengerId.Value);
        var airports = AirportRepo.GetAllItems();

        // Join The Filtered Data
        var filtered = (from booking in bookings
                join flight in flights on booking.FlightBooked equals flight.Id
                join passenger in passengers on booking.PassengerBooked equals passenger.Id
                join depAirport in airports on flight.DepartureAirportId equals depAirport.Id
                join arrAirport in airports on flight.ArrivalAirportId equals arrAirport.Id
                select new FilteredResult
                {
                    Flight = flight,
                    Booking = booking,
                    Passenger = passenger,
                    DepartureAirport = depAirport,
                    ArrivalAirport = arrAirport
                }
            ).ToList();


        return filtered;
    }
}