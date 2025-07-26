// See https://aka.ms/new-console-template for more information

using airport_ticket_booking_system.models;
using airport_ticket_booking_system.models.enums;
using airport_ticket_booking_system.validation;

Console.WriteLine("Hello, World!");

Flight f = new Flight()
{
    Price = 13.4,
    ArrivalAirport = null,
    DepartureAirport = null,
    DepartureCountry = "Spain",
    DepartureDate = new DateTime(2024, 12, 13),
    DestinationCountry = "Dubai",
    FlighClass = FlightClassEnum.Business
};

var list = Validator.Validate(f);

list.ForEach(m => Console.WriteLine(m));