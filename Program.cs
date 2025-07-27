// See https://aka.ms/new-console-template for more information

using airport_ticket_booking_system.data.handlers;
using airport_ticket_booking_system.models;

Console.WriteLine("Hello, World!");

Flight f = (Flight)new Flight().FromString("1,85,Italy,France,2025-11-11,10,12");


string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
string path = Path.Combine(projectRoot, "files", "Flights.csv");

CsvFileService cc = new CsvFileService(path, new Flight());

foreach (var model in cc.GetAll())
{
    Console.WriteLine(model);
}

List<Flight> flights = cc.GetAll().Select(model => (Flight)model).ToList();

flights.Add(f);
await cc.WriteAllAsync(flights);

