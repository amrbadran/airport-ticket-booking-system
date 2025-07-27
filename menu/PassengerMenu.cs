namespace airport_ticket_booking_system.menu;

public class PassengerMenu
{
    public void Welcome()
    {
        Console.WriteLine("""
                            1.Filter Flights
                            2.Show All Flights
                            3.Show Your Booking
                            4.Book Flight
                            5.Exit
                          """);
    }
}