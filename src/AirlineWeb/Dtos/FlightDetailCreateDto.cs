namespace AirlineWeb.Dtos;

public class FlightDetailCreateDto
{
    public string FlightCode { get; set; } = default!;

    public decimal Price { get; set; }
}
