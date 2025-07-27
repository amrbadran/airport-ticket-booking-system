using airport_ticket_booking_system.services.auth;

do
{
    Console.Write("Username : ");
    string username = Console.ReadLine();
    Console.Write("Password : ");
    string password = Console.ReadLine();

    bool LoginManager = new AuthService().LoginManager(username, password);
    bool LoginPassenger = new AuthService().LoginPassenger(username, password);
    if (LoginManager)
    {
        Console.WriteLine("Welcome To Manager Page");
    }
    else if (LoginPassenger)
    {
        Console.WriteLine("Weclome To passenger page");
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