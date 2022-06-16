using Microsoft.AspNetCore.Mvc;
using TravelAgentWeb.Data;
using TravelAgentWeb.Dtos;

namespace TravelAgentWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly TravelAgentDbContext _context;

    public NotificationsController(TravelAgentDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpPost]
    public ActionResult FlightChanged(FlightDetailUpdateDto request)
    {
        Console.WriteLine($"Webhook Receieved from: {request.Publisher}");

        var secretModel = _context.SubscriptionSecrets.FirstOrDefault(s =>
            s.Publisher == request.Publisher && s.Secret == request.Secret);

        if (secretModel is null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Secret - Ignore Webhook");
            Console.ResetColor();
            return Ok();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Valid Webhook!");
            Console.WriteLine($"Old Price {request.OldPrice}, New Price {request.NewPrice}");
            Console.ResetColor();
            return Ok();
        }
    }


}
