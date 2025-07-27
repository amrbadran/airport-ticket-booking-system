using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.services.filter;

namespace airport_ticket_booking_system.menu;

public class ManagerMenu
{
    public void Welcome()
    {
        Console.WriteLine("""
                          1. Import Flights
                          2. Filter Booking
                          3. Exit
                          """);
    }

    public void FilterBooking()
{
    Console.Write("Flight ID (leave empty if not important): ");
    string flightIdInput = Console.ReadLine();
    int? flightId = string.IsNullOrWhiteSpace(flightIdInput) ? null : int.Parse(flightIdInput);

    Console.Write("Price (leave empty if not important): ");
    string priceInput = Console.ReadLine();
    double? price = string.IsNullOrWhiteSpace(priceInput) ? null : double.Parse(priceInput);

    Console.Write("Departure Country (leave empty if not important): ");
    string departCountryInput = Console.ReadLine();
    string? departCountry = string.IsNullOrWhiteSpace(departCountryInput) ? null : departCountryInput;

    Console.Write("Arrival Country (leave empty if not important): ");
    string arrivalCountryInput = Console.ReadLine();
    string? arrivalCountry = string.IsNullOrWhiteSpace(arrivalCountryInput) ? null : arrivalCountryInput;

    Console.Write("Departure Airport ID (leave empty if not important): ");
    string departAirportIdInput = Console.ReadLine();
    int? departAirportId = string.IsNullOrWhiteSpace(departAirportIdInput) ? null : int.Parse(departAirportIdInput);

    Console.Write("Arrival Airport ID (leave empty if not important): ");
    string arrivalAirportIdInput = Console.ReadLine();
    int? arrivalAirportId = string.IsNullOrWhiteSpace(arrivalAirportIdInput) ? null : int.Parse(arrivalAirportIdInput);

    Console.Write("Departure Date (yyyy-MM-dd) (leave empty if not important): ");
    string departDateInput = Console.ReadLine();
    DateTime? departDate = string.IsNullOrWhiteSpace(departDateInput)
        ? null
        : DateTime.ParseExact(departDateInput, "yyyy-MM-dd", null);

    Console.Write("Passenger ID (leave empty if not important): ");
    string passengerIdInput = Console.ReadLine();
    int? passengerId = string.IsNullOrWhiteSpace(passengerIdInput) ? null : int.Parse(passengerIdInput);

    Console.Write("Flight Class (Economy (0), Business (1), First (2)) (leave empty if not important): ");
    string flightClassInput = Console.ReadLine();
    FlightClassEnum? flightClass = string.IsNullOrWhiteSpace(flightClassInput)
        ? null
        : (FlightClassEnum)int.Parse(flightClassInput);

    var filteredResults = BookingFilterService.FilterJoinedData(
        flightId,
        price,
        departCountry,
        arrivalCountry,
        departAirportId,
        arrivalAirportId,
        departDate,
        passengerId,
        flightClass
    );

    if (filteredResults.Count() > 0)
    {
        Console.WriteLine("Filtered Bookings:");
        foreach (var result in filteredResults)
        {
            Console.WriteLine($"Booking: Passenger '{result.Passenger.Username}' booked Flight #{result.Flight.Id} " +
                              $"from {result.Flight.DepartureCountry} to {result.Flight.DestinationCountry} " +
                              $"on {result.Flight.DepartureDate:yyyy-MM-dd} - Class: {result.Booking.FlightClass}");
        }
    }
    else
    {
        Console.WriteLine("No bookings matched your filters.");
    }
}

}