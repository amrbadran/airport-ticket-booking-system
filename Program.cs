using airport_ticket_booking_system.menu;
using airport_ticket_booking_system.models;
using airport_ticket_booking_system.services.auth;

Console.Write("Username : ");
string username = Console.ReadLine();
Console.Write("Password : ");
string password = Console.ReadLine();

var loginManager = new AuthService().LoginManager(username, password);
var loginPassenger = new AuthService().LoginPassenger(username, password);
do
{
    if (loginPassenger != null)
    {
        var p = new PassengerMenu();
        p.Welcome();
        EnumPassengerChoice choice = (EnumPassengerChoice)int.Parse(Console.ReadLine());
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
    else if (loginManager)
    {
        var m = new ManagerMenu();
        m.Welcome();
        EnumManagerChoice choice = (EnumManagerChoice)int.Parse(Console.ReadLine());
        switch (choice)
        {
            case EnumManagerChoice.ImportFlights:
                break;
            case EnumManagerChoice.FilterBooking:
                m.FilterBooking();      
                break;
        }
    }
    else
    {
        Console.WriteLine("Retry");
    }
} while (true);

void PrintWelcome()
{
    Console.WriteLine("""

                      ** Welcome To Airport Ticket Booking System **
                      ===================================================
                      """);
}