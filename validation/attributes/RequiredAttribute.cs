namespace airport_ticket_booking_system.validation.attributes;

public class RequiredAttribute : ValidationAttribute
{
    public override bool isValid(object obj)
    {
        return obj is string str ? !string.IsNullOrWhiteSpace(str) : obj != null;
    }

    public override string ErrorMessage()
    {
        return "Required Field";
    }
}