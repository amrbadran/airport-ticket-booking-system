using System.ComponentModel.DataAnnotations;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.services.filter;
using airport_ticket_booking_system.services.uploading;

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

    /// <summary>
    /// This Menu Function for manager to filter all bookings in system based on parameters
    /// </summary>
    public void FilterBooking()
    {
        var flightId = Reader.ReadNullableInt("Flight ID (leave empty if not important): ");
        var price = Reader.ReadNullableDouble("Price (leave empty if not important): ");
        var departCountry = Reader.ReadNullableString("Departure Country (leave empty if not important): ");
        var arrivalCountry = Reader.ReadNullableString("Arrival Country (leave empty if not important): ");
        var departAirportId = Reader.ReadNullableInt("Departure Airport ID (leave empty if not important): ");
        var arrivalAirportId = Reader.ReadNullableInt("Arrival Airport ID (leave empty if not important): ");
        var departDate = Reader.ReadNullableDate("Departure Date (yyyy-MM-dd) (leave empty if not important): ");
        var passengerId = Reader.ReadNullableInt("Passenger ID (leave empty if not important): ");
        var flightClass = Reader.ReadNullableFlightClass(
            "Flight Class (Economy (0), Business (1), First (2)) (leave empty if not important): ");

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

        if (filteredResults.Any())
        {
            Console.WriteLine("Filtered Bookings:");
            filteredResults.ForEach(result => Console.WriteLine(
                $"Booking: Passenger '{result.Passenger.Username}' booked Flight #{result.Flight.Id} " +
                $"from {result.Flight.DepartureCountry} to {result.Flight.DestinationCountry} " +
                $"on {result.Flight.DepartureDate:yyyy-MM-dd} - Class: {result.Booking.FlightClass}"));
        }
        else
        {
            Console.WriteLine("No bookings matched your filters.");
        }
    }

    public async Task UploadFlights()
    {
        string fileName = Reader.ReadStringInput("Enter the filename : ");
        try
        {
            List<string> results = await UploadFlightsService.Upload(fileName);
            Console.WriteLine("\nUpload Results:");
            results.ForEach(message => Console.WriteLine("- " + message));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (ValidationException e)
        {
            Console.WriteLine("one or more flights failed validation:");
            Console.WriteLine(e.Message);
        }
    }
}