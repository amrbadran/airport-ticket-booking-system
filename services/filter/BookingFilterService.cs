using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;

namespace airport_ticket_booking_system.services.filter;

public static class BookingFilterService
{
    private static ModelRepository<Booking> BookingRepo = new ModelRepository<Booking>(new Booking());
    private static ModelRepository<Flight> FlightRepo = new ModelRepository<Flight>(new Flight());
    private static ModelRepository<Airport> AirportRepo = new ModelRepository<Airport>(new Airport());
    private static ModelRepository<Passenger> PassengerRepo = new ModelRepository<Passenger>(new Passenger());


    public static List<FilteredResult> FilterJoinedData(
    int? flightId,
    double? price,
    string? departCountry,
    string? arrivalCountry,
    int? departAirportId,
    int? arrivalAirportId,
    DateTime? departDate,
    int? passengerId,
    FlightClassEnum? flightClass
)
{

    var flights = FlightRepo.GetAllItems().ToList();
    var bookings = BookingRepo.GetAllItems().ToList();
    var passengers = PassengerRepo.GetAllItems().ToList();
    var airports = AirportRepo.GetAllItems().ToList();

    var filtered = (from booking in bookings
                    join flight in flights on booking.FlightBooked equals flight.Id
                    join passenger in passengers on booking.PassengerBooked equals passenger.Id
                    join depAirport in airports on flight.DepartureAirportId equals depAirport.Id
                    join arrAirport in airports on flight.ArrivalAirportId equals arrAirport.Id
                    where (!flightId.HasValue || flight.Id == flightId.Value)
                          && (!price.HasValue || flight.Price == price.Value)
                          && (string.IsNullOrEmpty(departCountry) || flight.DepartureCountry == departCountry)
                          && (string.IsNullOrEmpty(arrivalCountry) || flight.DestinationCountry == arrivalCountry)
                          && (!departAirportId.HasValue || flight.DepartureAirportId == departAirportId.Value)
                          && (!arrivalAirportId.HasValue || flight.ArrivalAirportId == arrivalAirportId.Value)
                          && (!departDate.HasValue || flight.DepartureDate.Date == departDate.Value.Date)
                          && (!passengerId.HasValue || passenger.Id == passengerId.Value)
                          && (!flightClass.HasValue || booking.FlightClass == flightClass.Value)
                    select new FilteredResult
                    {
                        Flight = flight,
                        Booking = booking,
                        Passenger = passenger,
                        DepartureAirport = depAirport,
                        ArrivalAirport = arrAirport
                    }).ToList();

    return filtered;
}

    


}