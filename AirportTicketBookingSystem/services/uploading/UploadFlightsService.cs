using airport_ticket_booking_system.data.handlers;
using airport_ticket_booking_system.data.interfaces;
using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.utils;

namespace airport_ticket_booking_system.services.uploading;

public class UploadFlightsService
{
    private readonly IModelRepository<Flight> _flightRepo;
    private readonly IFileHandler _fileHandler;

    public UploadFlightsService(IModelRepository<Flight> flightRepo, IFileHandler fileHandler)
    {
        _flightRepo = flightRepo;
        _fileHandler = fileHandler;
    }


    /// <summary>
    /// This Function responsable for uploading the flights from external file to our system
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public async Task<List<string>> Upload(string filename)
    {
        string path = Path.Combine(Constants.ProjectRoot, "files", filename);

        if (!_fileHandler.FileExists(path))
            throw new FileNotFoundException($"File not found at path: {path}");

        var newFlights = _fileHandler.GetAll().Select(f => (Flight)f).ToList();
        var existingFlights = _flightRepo.GetAllItems().ToList();

        List<string> resultMessages = new();

        foreach (var flight in newFlights)
        {
            if (existingFlights.Any(f => f.Id == flight.Id))
            {
                resultMessages.Add($"Skipped Flight ID {flight.Id}: already exists.");
                continue;
            }

            existingFlights.Add(flight);
            resultMessages.Add(
                $"Added Flight ID {flight.Id}: {flight.DepartureCountry} to {flight.DestinationCountry}");
        }

        await _flightRepo.SaveAll(existingFlights);

        return resultMessages;
    }
}