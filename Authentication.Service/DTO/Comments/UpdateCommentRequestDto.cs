using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.DTO.Comments;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be 5 characters")]
    [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Context must be 5 characters")]
    [MaxLength(280, ErrorMessage = "Context cannot be over 280 characters")]
    public string Context { get; set; } = string.Empty;
}