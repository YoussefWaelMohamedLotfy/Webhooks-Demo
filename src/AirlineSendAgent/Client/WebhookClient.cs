using AirlineSendAgent.Dtos;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AirlineSendAgent.Client;

internal class WebhookClient : IWebhookClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WebhookClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task SendWebhookNotification(FlightDetailChangePayloadDto request)
    {
        Console.WriteLine("Inside send");
        var serializedPayload = JsonSerializer.Serialize(request);

        var httpClient = _httpClientFactory.CreateClient();

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, request.WebhookURI);
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        httpRequest.Content = new StringContent(serializedPayload);

        httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        try
        {
            using var response = await httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Success");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unsuccessful - {ex.Message}");
        }
    }

}
