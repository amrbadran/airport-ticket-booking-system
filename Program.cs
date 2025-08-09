using System.ComponentModel.DataAnnotations;
using airport_ticket_booking_system.menu;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.services.auth;

PrintWelcome();
do
{
    await Run();
} while (true);

async Task Run()
{
    var (loginManager, loginPassenger) = Login();
    if (loginPassenger != null)
    {
        await Passenger(new PassengerMenu(), loginPassenger);
    }
    else if (loginManager)
    {
        await Manager(new ManagerMenu());
    }
    else
    {
        Console.WriteLine("Retry");
    }
}

void PrintWelcome()
{
    Console.WriteLine("""

                      ** Welcome To Airport Ticket Booking System **
                      ===================================================
                      """);
}

(bool, Passenger?) Login()
{
    // Enter The Username & Password
    Console.Write("Username : ");
    string? username = Console.ReadLine();
    Console.Write("Password : ");
    string? password = Console.ReadLine();

    // Apply Username & Password to AuthService
    var loginManager = new AuthService().LoginManager(username, password);
    var loginPassenger = new AuthService().LoginPassenger(username, password);

    return (loginManager, loginPassenger);
}

// This Functions Handles Passenger Menu
async Task Passenger(PassengerMenu p, Passenger loginPassenger)
{
    try
    {
        p.Welcome();
        EnumPassengerChoice choice = (EnumPassengerChoice)int.Parse(Console.ReadLine()!);
        switch (choice)
        {
            case EnumPassengerChoice.FilterFlights:
                p.FilterFlights();
                break;
            case EnumPassengerChoice.ShowAllFlights:
                p.ShowAllFlights();
                break;
            case EnumPassengerChoice.ShowBooking:
                p.ShowYourBookings(loginPassenger.Id);
                break;
            case EnumPassengerChoice.BookFlight:
                await p.BookFlight(loginPassenger.Id);
                break;
            case EnumPassengerChoice.CancelBooking:
                await p.CancelBooking(loginPassenger.Id);
                break;
            case EnumPassengerChoice.ModifyBooking:
                await p.ModifyBooking(loginPassenger.Id);
                break;
        }
    }
    catch (FormatException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (ValidationException e)
    {
        Console.WriteLine(e.ValidationResult);
    }
}

// This Functions Handles Manager Menu
async Task Manager(ManagerMenu m)
{
    try
    {
        m.Welcome();
        EnumManagerChoice choice = (EnumManagerChoice)int.Parse(Console.ReadLine()!);
        switch (choice)
        {
            case EnumManagerChoice.ImportFlights:
                await m.UploadFlights();
                break;
            case EnumManagerChoice.FilterBooking:
                m.FilterBooking();
                break;
        }
    }
    catch (FormatException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (ValidationException e)
    {
        Console.WriteLine(e.ValidationResult);
    }
}