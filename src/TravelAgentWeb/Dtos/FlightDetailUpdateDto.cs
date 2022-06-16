namespace TravelAgentWeb.Dtos;

public class FlightDetailUpdateDto
{
    public string Publisher { get; set; } = default!;

    public Guid Secret { get; set; }

    public string FlightCode { get; set; } = default!;

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public string WebhookType { get; set; } = default!;
}
