using System.Reflection;
using airport_ticket_booking_system.validation.attributes;

namespace airport_ticket_booking_system.validation;

public class Validator
{
    // This Takes an Object Class Like Flight, Passenger, Airport....
    public static List<string> Validate(object obj)
    {
        var type = obj.GetType();
        var props = type.GetProperties();

        var errors = new List<string>();

        foreach (var prop in props)
        {
            var valOfProp = prop.GetValue(obj);
            var attrs = prop.GetCustomAttributes<ValidationAttribute>();

            foreach (var attr in attrs)
            {
                if (!attr.isValid(valOfProp))
                {
                    errors.Add(attr.ErrorMessage());
                }
            }
        }

        return errors;
    }
}