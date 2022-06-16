namespace AirlineWeb.Dtos;

public class FlightDetailReadDto
{
    public int Id { get; set; }

    public string FlightCode { get; set; } = default!;

    public decimal Price { get; set; }
}
