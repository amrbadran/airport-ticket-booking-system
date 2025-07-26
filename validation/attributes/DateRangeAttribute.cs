namespace airport_ticket_booking_system.validation.attributes;

public class DateRangeAttribute : ValidationAttribute
{
    public override bool isValid(object obj)
    {
        return obj is DateTime dateTime && dateTime.Date >= DateTime.Today;
    }

    public override string ErrorMessage()
    {
        return "Allowed Range (today → future)";
    }
}