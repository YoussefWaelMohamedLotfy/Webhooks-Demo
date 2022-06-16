using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace AirlineSendAgent.App;

internal class AppHost : IAppHost
{
    public void Run()
    {
        Console.WriteLine("Hello World!");
    }

}
