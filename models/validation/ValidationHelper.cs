using System.ComponentModel.DataAnnotations;

namespace airport_ticket_booking_system.models.validation;

public static class ValidationHelper
{
    public static void ValidateObjectOrThrow(object obj)
    {
        var context = new ValidationContext(obj);
        var results = new List<ValidationResult>();
        if (Validator.TryValidateObject(obj, context, results, true)) return;

        var errors = results.SelectMany(vr => vr.MemberNames.Select(mn => $"{mn}: {vr.ErrorMessage}"));
        var message = string.Join("\n", errors);
        throw new ValidationException($"Validation failed: {obj} \n{message}");
    }
}