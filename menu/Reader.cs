using airport_ticket_booking_system.models.enums;

public static class Reader
{
    public static int ReadIntInput(string prompt)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out var num))
            return num;

        throw new FormatException("Invalid Input Number");
    }


    public static int? ReadNullableInt(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? null : int.Parse(input);
    }

    public static double ReadDoubleInput(string prompt)
    {
        Console.Write(prompt);
        if (double.TryParse(Console.ReadLine(), out var num))
            return num;

        throw new FormatException("Invalid Double Input Number");
    }

    public static double? ReadNullableDouble(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? null : double.Parse(input);
    }

    public static string ReadStringInput(string prompt)
    {
        Console.Write(prompt);
        var result = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(result))
            throw new FormatException("String can't be empty");
        return result;
    }

    public static string? ReadNullableString(string prompt)
    {
        Console.Write(prompt);
        var result = Console.ReadLine();
        return string.IsNullOrWhiteSpace(result) ? null : result;
    }

    public static DateTime? ReadNullableDate(string prompt, string format = "yyyy-MM-dd")
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input)
            ? null
            : DateTime.ParseExact(input, format, null);
    }

    public static FlightClassEnum ReadFlightClass(string prompt)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out var result) &&
            Enum.IsDefined(typeof(FlightClassEnum), result))
        {
            return (FlightClassEnum)result;
        }

        throw new FormatException("Invalid Flight Class Number");
    }
}