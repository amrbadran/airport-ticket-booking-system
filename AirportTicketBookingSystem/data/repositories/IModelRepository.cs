using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.data.repositories;

public interface IModelRepository<T>
{
    public IEnumerable<T> GetAllItems();

    public Task SaveAll(IEnumerable<IModel> items);
}