using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;

namespace airport_ticket_booking_system.services.booking;

public static class BookingService
{
    private static ModelRepository<Booking> bookingRepo = new ModelRepository<Booking>(new Booking());

    public static async Task BookFlight(int passengerId, int flightId, FlightClassEnum flightClass)
    {
        var bookingList = bookingRepo.GetAllItems().ToList();

        bookingList.Add(new Booking()
        {
            FlightBooked = flightId,
            FlightClass = flightClass,
            PassengerBooked = passengerId
        });

        await bookingRepo.SaveAll(bookingList);
    }

    public static List<Booking> GetAllBooking(int passengerId)
    {
        var bookingList = bookingRepo.GetAllItems()
            .Where(b => b.PassengerBooked == passengerId).ToList();
        return bookingList;
    }

    public static async Task CancelBooking(int passengerId, int flightId)
    {
        var bookingList = bookingRepo.GetAllItems().ToList();

        bookingList.RemoveAll(b => b.PassengerBooked == passengerId && b.FlightBooked == flightId);

        await bookingRepo.SaveAll(bookingList);
    }
}