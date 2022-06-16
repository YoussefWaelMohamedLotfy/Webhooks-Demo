using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.MessageBus;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly AirlineDbContext _context;
    private readonly IMapper _mapper;

    public FlightsController(AirlineDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{flightCode}", Name = "GetFlightDetailsByCode")]
    public ActionResult<FlightDetailReadDto> GetFlightDetailsByCode(string flightCode)
    {
        var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightCode);

        if (flight is null)
            return NotFound();

        return Ok(_mapper.Map<FlightDetailReadDto>(flight));
    }

    [HttpPost]
    public async Task<ActionResult<FlightDetailReadDto>> CreateFlight(FlightDetailCreateDto request)
    {
        var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == request.FlightCode);

        if (flight is not null)
            return NoContent();

        var flightDetailinDb = _mapper.Map<FlightDetail>(request);

        try
        {
            await _context.FlightDetails.AddAsync(flightDetailinDb);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var result = _mapper.Map<FlightDetailReadDto>(flightDetailinDb);
        return CreatedAtRoute(nameof(GetFlightDetailsByCode), new { flightCode = result.FlightCode }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFlightDetail([FromServices] IMessageBusClient client, int id, FlightDetailUpdateDto request)
    {
        var flight = await _context.FlightDetails.FindAsync(id);

        if (flight is  null)
            return NotFound();

        decimal oldPrice = flight.Price;

        _mapper.Map(request, flight);

        var result = _mapper.Map<FlightDetailReadDto>(flight);

        try
        {
            await _context.SaveChangesAsync();

            if (oldPrice != flight.Price)
            {
                Console.WriteLine("Price Changed - Place message on bus");

                var message = new NotificationMessageDto
                {
                    WebhookType = "pricechange",
                    OldPrice = oldPrice,
                    NewPrice = flight.Price,
                    FlightCode = flight.FlightCode
                };
                client.SendMessage(message);
            }
            else
                Console.WriteLine("No Price change");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
