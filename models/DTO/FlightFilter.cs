using airport_ticket_booking_system.extensions;

namespace airport_ticket_booking_system.models.DTO;

public record FlightFilter(
    double? Price,
    string? DepartCountry,
    string? ArrivalCountry,
    int? DepartAirportId,
    int? ArrivalAirportId,
    DateTime? DepartDate
)
{
    /// <summary>
    /// This function is only for this record to make the filter logic
    /// it returns all predicates and we use them in filter service class.
    /// </summary>
    /// <returns></returns>
    public List<Func<Flight, bool>> GetPredicates()
    {
        var predicates = new List<Func<Flight, bool>>();

        if (Price.HasValue)
            predicates.Add(f => f.Price.IsEqualTo(Price.Value));

        if (!string.IsNullOrEmpty(DepartCountry))
            predicates.Add(f => f.DepartureCountry == DepartCountry);

        if (!string.IsNullOrEmpty(ArrivalCountry))
            predicates.Add(f => f.DestinationCountry == ArrivalCountry);

        if (DepartAirportId.HasValue)
            predicates.Add(f => f.DepartureAirportId == DepartAirportId.Value);

        if (ArrivalAirportId.HasValue)
            predicates.Add(f => f.ArrivalAirportId == ArrivalAirportId.Value);

        if (DepartDate.HasValue)
            predicates.Add(f => f.DepartureDate.Date == DepartDate.Value.Date);

        return predicates;
    }
}