using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineWeb.Models;

public class FlightDetail
{
    public int Id { get; set; }

    public string FlightCode { get; set; } = default!;

    [Column(TypeName = "decimal(6,2)")]
    public decimal Price { get; set; }
}
