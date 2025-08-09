using airport_ticket_booking_system.data.handlers;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.utils;

namespace airport_ticket_booking_system.data.repositories;

public class ModelRepository<T>
{
    private string _path;

    private IModel _item;

    public ModelRepository(IModel item, string? filename = null)
    {
        _item = item;
        _path = Path.Combine(Constants.ProjectRoot, "files", filename ?? DeterminePath());
    }

    public IEnumerable<T> GetAllItems()
    {
        return new CsvFileService(_path, _item).GetAll().Select(model => (T)model);
    }

    public async Task SaveAll(IEnumerable<IModel> items)
    {
        await new CsvFileService(_path, _item).WriteAllAsync(items);
    }

    private string DeterminePath()
    {
        if (Constants.TypePathMap.TryGetValue(typeof(T), out var path))
            return path;

        return "";
    }
}