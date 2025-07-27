using airport_ticket_booking_system.data.handlers;
using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;

namespace airport_ticket_booking_system.services.uploading;

public static class UploadFlightsService
{
    private static readonly string _projectRoot =
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

    private static readonly ModelRepository<Flight> FlightRepo = new ModelRepository<Flight>(new Flight());

    public static async Task<List<string>> Upload(string filename)
    {
        string path = Path.Combine(_projectRoot, "files", filename);

        if (!File.Exists(path))
            throw new FileNotFoundException($"File not found at path: {path}");
        
        var newFlights = new ModelRepository<Flight>(new Flight(), filename).GetAllItems().ToList();
        var existingFlights = FlightRepo.GetAllItems().ToList();

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


        await FlightRepo.SaveAll(existingFlights);

        return resultMessages;
    }
}