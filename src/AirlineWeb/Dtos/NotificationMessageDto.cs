namespace AirlineWeb.Dtos;

public class NotificationMessageDto
{
    public NotificationMessageDto()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public string WebhookType { get; set; } = default!;
    
    public string FlightCode { get; set; } = default!;

    public decimal OldPrice { get; set; }
    
    public decimal NewPrice { get; set; }
}
