namespace airport_ticket_booking_system.validation.attributes;

[AttributeUsage(AttributeTargets.Property,AllowMultiple = true)]
public abstract class ValidationAttribute : Attribute
{
    public abstract bool isValid(Object obj);
    public abstract string ErrorMessage();
}