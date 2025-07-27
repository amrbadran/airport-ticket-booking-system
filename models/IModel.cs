namespace airport_ticket_booking_system.models;

public interface IModel
{
    IModel FromString(string line);
}