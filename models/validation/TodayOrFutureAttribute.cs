namespace airport_ticket_booking_system.models.validation;
using System.ComponentModel.DataAnnotations;

public class TodayOrFutureAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime date)
        {
            return date.Date >= DateTime.Today;
        }
        return false;
    }
}
