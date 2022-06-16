namespace AirlineWeb.Dtos;

public class FlightDetailUpdateDto
{
    public string FlightCode { get; set; } = default!;

    public decimal Price { get; set; }
}
