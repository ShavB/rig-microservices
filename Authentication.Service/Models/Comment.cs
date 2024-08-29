using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Service.Models;

[Table("Comments")]
public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int? StockId { get; set; } // Navigation property
    public Stock? Stock { get; set; }
    public string AppuserId { get; set; }
    public AppUser AppUser { get; set; }
}
