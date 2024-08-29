using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Service.Models;

[Table("Portfolios")]
public class PortFolio
{
    public string? AppuserId { get; set; }
    public int StockId { get; set; }
    public AppUser? AppUser { get; set; }
    public Stock? Stock { get; set; }
}
