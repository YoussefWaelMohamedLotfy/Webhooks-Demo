using AirlineSendAgent.Dtos;

namespace AirlineSendAgent.Client;

internal interface IWebhookClient
{
    Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayloadDto);
}