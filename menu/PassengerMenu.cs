using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.services.booking;
using airport_ticket_booking_system.services.filter;

namespace airport_ticket_booking_system.menu;

public class PassengerMenu
{
    public void Welcome()
    {
        Console.WriteLine("""
                            1.Filter Flights
                            2.Show All Flights
                            3.Show Your Booking
                            4.Book Flight
                            5. Cancel Booking
                            6. Modify Booking
                            7.Exit
                          """);
    }

    public void FilterFlights()
    {
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
        int? arrivalAirportId =
            string.IsNullOrWhiteSpace(arrivalAirportIdInput) ? null : int.Parse(arrivalAirportIdInput);

        Console.Write("Departure Date (yyyy-MM-dd) (leave empty if not important): ");
        string departDateInput = Console.ReadLine();
        DateTime? departDate = string.IsNullOrWhiteSpace(departDateInput)
            ? null
            : DateTime.ParseExact(departDateInput, "yyyy-MM-dd", null);

        var filteredFlights = FilterFlightService.FilterFlights(
            price,
            departCountry,
            arrivalCountry,
            departAirportId,
            arrivalAirportId,
            departDate
        );
        if (filteredFlights.Count() > 0)
        {
            Console.WriteLine("Filtered Flights:");
            foreach (var flight in filteredFlights)
            {
                Console.WriteLine(
                    $"Flight #{flight.Id} - {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate:yyyy-MM-dd}");
            }
        }
        else
        {
            Console.WriteLine("No flights matched your filters.");
        }
    }

    public void ShowAllFlights()
    {
        var flights = FilterFlightService.GetAllFlights();
        foreach (var flight in flights)
        {
            Console.WriteLine(
                $"Flight #{flight.Id} - {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate:yyyy-MM-dd}");
        }
    }

    public void ShowYourBookings(int passengerId)
    {
        var bookings = BookingService.GetAllBooking(passengerId);
        var flights = FilterFlightService.GetAllFlights();
        var bookingAndFlights = bookings.Join(flights,
            b => b.FlightBooked
            , f => f.Id,
            (booking, flight) =>
                $"Flight #{flight.Id} - {flight.DepartureCountry} to {flight.DestinationCountry} " +
                $"on {flight.DepartureDate:yyyy-MM-dd} with class {booking.FlightClass} " +
                $"Total Price:{(double)(booking.FlightClass + 1) * flight.Price}");

        foreach (var bookingAndFlight in bookingAndFlights)
        {
            Console.WriteLine(bookingAndFlight);
        }
    }

    public async Task BookFlight(int passengerId)
    {
        Console.Write("Flight Id: ");
        int flightId = int.Parse(Console.ReadLine());
        Flight? f = FilterFlightService.GetFlightById(flightId);

        if (f != null)
        {
            Console.WriteLine($"Price For Economy {f.Price}, Business {f.Price * 2}, First Class {f.Price * 3}");
            Console.Write("Flight Class: (Economy 0, Business 1, First Class 2) ");
            FlightClassEnum flightClass = (FlightClassEnum)int.Parse(Console.ReadLine());

            await BookingService.BookFlight(passengerId, flightId, flightClass);
        }
        else
        {
            Console.WriteLine("No Flight With this id");
        }
    }

    public async Task CancelBooking(int passengerId)
    {
        Console.Write("Flight Id: ");
        int flightId = int.Parse(Console.ReadLine());
        Flight? f = FilterFlightService.GetFlightById(flightId);

        if (f != null)
        {
            await BookingService.CancelBooking(passengerId, flightId);
        }
        else
        {
            Console.WriteLine("No Flight With this id");
        }
    }

    public async Task ModifyBooking(int passengerId)
    {
        Console.Write("Flight Id (To Update): ");
        int flightId = int.Parse(Console.ReadLine());
        Flight? f = FilterFlightService.GetFlightById(flightId);

        if (f != null)
        {
            Console.WriteLine($"Price For Economy {f.Price}, Business {f.Price * 2}, First Class {f.Price * 3}");
            Console.Write("Flight Class: (Economy 0, Business 1, First Class 2) ");
            FlightClassEnum flightClass = (FlightClassEnum)int.Parse(Console.ReadLine());

            await BookingService.ModifyBooking(passengerId, flightId, flightClass);
        }
        else
        {
            Console.WriteLine("No Flight With this id");
        }
    }
}