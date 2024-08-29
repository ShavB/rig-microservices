namespace Authentication.Service.DTO.Comments;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string CreateBy {get; set;} = string.Empty;
    public int? StockId { get; set; } // Navigation property
}