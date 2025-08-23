using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.DTO;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.services.booking;
using airport_ticket_booking_system.services.filter;

namespace airport_ticket_booking_system.menu;

public class PassengerMenu
{
    public void Welcome()
    {
        Console.WriteLine("""
                            1. Filter Flights
                            2. Show All Flights
                            3. Show Your Booking
                            4. Book Flight
                            5. Cancel Booking
                            6. Modify Booking
                            7. Exit
                          """);
    }

    /// <summary>
    /// A Menu Function for Filtering The Flights (Searching Feature)
    /// </summary>
    public void FilterFlights()
    {
        var filteredFlights = FilterFlightService.FilterFlights(
            new FlightFilter(
                Price: Reader.ReadNullableDouble("Price (leave empty if not important): "),
                DepartCountry: Reader.ReadNullableString("Departure Country (leave empty if not important): "),
                ArrivalCountry: Reader.ReadNullableString("Arrival Country (leave empty if not important): "),
                DepartAirportId: Reader.ReadNullableInt("Departure Airport ID (leave empty if not important): "),
                ArrivalAirportId: Reader.ReadNullableInt("Arrival Airport ID (leave empty if not important): "),
                DepartDate: Reader.ReadNullableDate("Departure Date (yyyy-MM-dd) (leave empty if not important): ")
            )
        );

        if (filteredFlights.Any())
        {
            Console.WriteLine("Filtered Flights:");
            filteredFlights.ForEach(flight => Console.WriteLine(
                $"Flight #{flight.Id} - {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate:yyyy-MM-dd}")
            );
        }
        else
        {
            Console.WriteLine("No flights matched your filters.");
        }
    }

    /// <summary>
    /// A Menu function for showing all flights in the system
    /// </summary>
    public void ShowAllFlights()
    {
        FilterFlightService
            .GetAllFlights()
            .ForEach(flight => Console.WriteLine(
                $"Flight #{flight.Id} - {flight.DepartureCountry} to {flight.DestinationCountry} on {flight.DepartureDate:yyyy-MM-dd}"));
    }

    /// <summary>
    /// Menu Function for showing all bookings related to passenger
    /// </summary>
    /// <param name="passengerId"></param>
    public void ShowYourBookings(int passengerId)
    {
        BookingPassengerService
            .GetBookingPassenger(passengerId)
            .ToList()
            .ForEach(m => Console.WriteLine(m));
    }

    /// <summary>
    /// This is for Booking Flight based on passenger
    /// No need to try..catch because we handle this in Program.Cs
    /// </summary>
    /// <param name="passengerId"></param>
    public async Task BookFlight(int passengerId)
    {
        int flightId = Reader.ReadIntInput("Flight Id: ");
        Flight? flight = FilterFlightService.GetFlightById(flightId);

        if (flight == null)
        {
            Console.WriteLine("No Flight With this id");
            return;
        }

        DisplayFlightPrices(flight);
        FlightClassEnum flightClass =
            Reader.ReadFlightClass("Flight Class: (Economy 0, Business 1, First Class 2) ");
        
        var service = new BookingService(new ModelRepository<Booking>(new Booking()));
        
        await service.BookFlight(passengerId, flightId, flightClass);
    }

    /// <summary>
    /// This is Menu Function that for cancelling a booking for a passenger
    /// </summary>
    /// <param name="passengerId"></param>
    public async Task CancelBooking(int passengerId)
    {
        int flightId = Reader.ReadIntInput("Flight Id: ");
        Flight? flight = FilterFlightService.GetFlightById(flightId);

        if (flight == null)
        {
            Console.WriteLine("No Flight With this id");
            return;
        }

        var service = new BookingService(new ModelRepository<Booking>(new Booking()));

        await service.CancelBooking(passengerId, flightId);
    }

    /// <summary>
    /// This is for modifing the booking for a passenger
    /// </summary>
    /// <param name="passengerId"></param>
    public async Task ModifyBooking(int passengerId)
    {
        int flightId = Reader.ReadIntInput("Flight Id: ");
        Flight? flight = FilterFlightService.GetFlightById(flightId);

        if (flight == null)
        {
            Console.WriteLine("No Flight With this id");
            return;
        }

        DisplayFlightPrices(flight);
        FlightClassEnum flightClass =
            Reader.ReadFlightClass("Flight Class: (Economy 0, Business 1, First Class 2) ");
        
        var service = new BookingService(new ModelRepository<Booking>(new Booking()));

        await service.ModifyBooking(passengerId, flightId, flightClass);
    }

    // Helper Methods
    private void DisplayFlightPrices(Flight flight)
    {
        Console.WriteLine(
            $"Price For Economy {flight.Price}, Business {flight.Price * 2}, First Class {flight.Price * 3}");
    }
}