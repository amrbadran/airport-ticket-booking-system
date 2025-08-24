using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;

namespace airport_ticket_booking_system.services.booking;

/// <summary>
/// Booking Service Class Handles CRUD Operations for Bookings.
/// </summary>
public class BookingService
{
    private IModelRepository<Booking> _bookingRepo;

    public BookingService(IModelRepository<Booking> bookingRepo)
    {
        _bookingRepo = bookingRepo;
    }

    public async Task BookFlight(int passengerId, int flightId, FlightClassEnum flightClass)
    {
        var bookingList = _bookingRepo.GetAllItems().ToList();

        bookingList.Add(new Booking()
        {
            FlightBooked = flightId,
            FlightClass = flightClass,
            PassengerBooked = passengerId
        });

        await _bookingRepo.SaveAll(bookingList);
    }

    public List<Booking> GetAllBooking(int passengerId)
    {
        return _bookingRepo
            .GetAllItems()
            .Where(b => b.PassengerBooked == passengerId)
            .ToList();
    }

    public async Task CancelBooking(int passengerId, int flightId)
    {
        var bookingList = _bookingRepo.GetAllItems().ToList();

        bookingList.RemoveAll(b => b.PassengerBooked == passengerId && b.FlightBooked == flightId);

        await _bookingRepo.SaveAll(bookingList);
    }

    public async Task ModifyBooking(int passengerId, int flightId, FlightClassEnum flightClass)
    {
        var bookingList = _bookingRepo.GetAllItems().ToList();

        var bookingToModify = bookingList
            .FirstOrDefault(b => b.PassengerBooked == passengerId && b.FlightBooked == flightId);

        if (bookingToModify != null)
        {
            bookingToModify.FlightClass = flightClass;
            await _bookingRepo.SaveAll(bookingList);
        }
    }
}