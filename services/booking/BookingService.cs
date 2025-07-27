using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;

namespace airport_ticket_booking_system.services.booking;

public static class BookingService
{
    public static async Task BookFlight(int passengerId, int flightId, FlightClassEnum flightClass)
    {
        var bookingRepo = new ModelRepository<Booking>(new Booking());
        var bookingList = bookingRepo.GetAllItems().ToList();
        
        bookingList.Add(new Booking()
        {
            FlightBooked = flightId,
            FlightClass = flightClass,
            PassengerBooked = passengerId
        });

        await bookingRepo.SaveAll(bookingList);
    }
}