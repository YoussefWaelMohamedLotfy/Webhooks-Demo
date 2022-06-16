using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebhookSubscriptionController : ControllerBase
{
    private readonly AirlineDbContext _context;
    private readonly IMapper _mapper;

    public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{secret}", Name = "GetSubscriptionBySecret"),]
    public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionBySecret(Guid secret)
    {
        var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.Secret == secret);

        if (subscription is null)
            return NotFound();

        return Ok(_mapper.Map<WebhookSubscriptionReadDto>(subscription));
    }

    [HttpPost]
    public async Task<ActionResult<WebhookSubscriptionReadDto>> CreateSubscription(WebhookSubscriptionCreateDto request)
    {
        var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.WebhookURI == request.WebhookURI);

        if (subscription is not null)
            return NoContent();

        subscription = _mapper.Map<WebhookSubscription>(request);
        subscription.Secret = Guid.NewGuid();
        subscription.WebhookPublisher = "Pan Austalian Airlines";

        try
        {
            await _context.WebhookSubscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        var result = _mapper.Map<WebhookSubscriptionReadDto>(subscription);
        return CreatedAtRoute(nameof(GetSubscriptionBySecret), new { secret = result.Secret }, result);
    }
}
