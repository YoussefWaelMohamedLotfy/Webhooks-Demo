namespace AirlineWeb.Dtos;

public class WebhookSubscriptionCreateDto
{
    public string WebhookURI { get; set; } = default!;

    public string WebhookType { get; set; } = default!;
}
