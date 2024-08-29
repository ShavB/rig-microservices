using Authentication.Service.DTO.Comments;
using Authentication.Service.Models;

namespace Authentication.Service.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Context = commentModel.Context,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId,
            CreateBy = commentModel.AppUser.UserName,
        };
    }

    public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Context = commentDto.Context,
            StockId = stockId
        };
    }

    public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
    {
        return new Comment { Title = commentDto.Title, Context = commentDto.Context, };
    }
}
