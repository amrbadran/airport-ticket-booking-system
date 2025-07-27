using airport_ticket_booking_system.data.interfaces;
using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.data.handlers;

public class CsvFileService : IFileHandler
{
    private string Filepath { get; }
    private IModel Model { get; }
    private string Header { get; }

    public CsvFileService(string filepath,
        IModel model)
    {
        Filepath = filepath;
        Model = model;
        Header = File.ReadLines(Filepath).Take(1).ToList()[0];
    }

    public IEnumerable<IModel> GetAll()
    {
        return File.ReadAllLines(Filepath)
            .Skip(1)
            .Select((item) => Model.FromString(item));
    }

    public async Task WriteAllAsync(IEnumerable<IModel> items)
    {
        var csvLines = items.Select(entity => entity.ToString());
        await File.WriteAllLinesAsync(Filepath, Enumerable.Repeat(Header, 1).Concat(csvLines));
    }
}