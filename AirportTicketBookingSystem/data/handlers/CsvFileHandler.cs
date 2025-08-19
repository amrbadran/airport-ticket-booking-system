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
    
    /// <summary>
    /// This Function For Getting all lines from a file
    /// May Throws An NotFoundFileException
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IModel> GetAll()
    {
        return File.ReadAllLines(Filepath)
            .Skip(1)
            .Select((item) => Model.FromString(item));
    }

    /// <summary>
    /// This Function For writing all lines to a file
    /// May Throws An NotFoundFileException
    /// </summary>
    /// <returns></returns>
    public async Task WriteAllAsync(IEnumerable<IModel> items)
    {
        var csvLines = items.Select(entity => entity.ToString());
        await File.WriteAllLinesAsync(Filepath, Enumerable.Repeat(Header, 1).Concat(csvLines));
    }
}