namespace TravelAgentWeb.Models;

public class WebhookSecret
{
    public int Id { get; set; }

    public Guid Secret { get; set; }

    public string Publisher { get; set; } = default!;
}
