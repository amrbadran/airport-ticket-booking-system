using airport_ticket_booking_system.models;
using airport_ticket_booking_system.services.filter;

namespace airport_ticket_booking_system.services.booking;

/// <summary>
/// This class is related to booking & passenger logic
/// </summary>
public static class BookingPassengerService
{
    public static IEnumerable<string> GetBookingPassenger(int passengerId)
    {
        var bookings = BookingService.GetAllBooking(passengerId);
        var flights = FilterFlightService.GetAllFlights();
        return bookings.Join(flights,
            b => b.FlightBooked
            , f => f.Id,
            (booking, flight) => BookingPassengerMessage(flight, booking));
    }

    private static string BookingPassengerMessage(Flight flight, Booking booking)
    {
        return $"Flight #{flight.Id} - {flight.DepartureCountry} to {flight.DestinationCountry} " +
               $"on {flight.DepartureDate:yyyy-MM-dd} with class {booking.FlightClass} " +
               $"Total Price:{(double)(booking.FlightClass + 1) * flight.Price}";
    }
}