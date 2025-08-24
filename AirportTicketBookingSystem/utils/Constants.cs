using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.utils;

public static class Constants
{
    public static readonly string ProjectRoot =
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    public static readonly Dictionary<Type, string> TypePathMap = new()
    {
        { typeof(Flight), "Flights.csv" },
        { typeof(Booking), "Bookings.csv" },
        { typeof(Passenger), "Passengers.csv" },
        { typeof(Airport), "Airports.csv" }
    };
}