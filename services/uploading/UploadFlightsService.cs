using airport_ticket_booking_system.data.handlers;
using airport_ticket_booking_system.data.repositories;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.utils;

namespace airport_ticket_booking_system.services.uploading;

public static class UploadFlightsService
{
    private static readonly ModelRepository<Flight> FlightRepo = new ModelRepository<Flight>(new Flight());

    /// <summary>
    /// This Function responsable for uploading the flights from external file to our system
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static async Task<List<string>> Upload(string filename)
    {
        string path = Path.Combine(Constants.ProjectRoot, "files", filename);

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