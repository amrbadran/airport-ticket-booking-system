using System.Reflection;
using System.Text;
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

            var str = new StringBuilder();
            str.AppendLine($"{prop.Name}");
            str.AppendLine($"\t Type: {prop.GetType()}");
            str.Append($"\t Constraints: ");

            bool isValidProp = false;
            foreach (var attr in attrs)
            {
                if (!attr.isValid(valOfProp))
                {
                    isValidProp = true;
                    str.Append(attr.ErrorMessage() + ",");
                }
            }

            if (isValidProp)
                errors.Add(str.ToString());
        }

        return errors;
    }
}