namespace airport_ticket_booking_system.extensions;

public static class DoubleExtensions
{
    public static bool IsEqualTo(this double a, double b, double epsilon = 0.01)
    {
        return Math.Abs(a - b) < epsilon;
    }
}
