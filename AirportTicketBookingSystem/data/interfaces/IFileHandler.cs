using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.data.interfaces;

public interface IFileHandler
{
    IEnumerable<IModel> GetAll();

    Task WriteAllAsync(IEnumerable<IModel> items);
}