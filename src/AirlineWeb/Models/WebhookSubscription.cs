namespace AirlineWeb.Models;

public class WebhookSubscription
{
    public int Id { get; set; }

    public string WebhookURI { get; set; } = default!;

    public Guid Secret { get; set; }

    public string WebhookType { get; set; } = default!;

    public string WebhookPublisher { get; set; } = default!;
}
