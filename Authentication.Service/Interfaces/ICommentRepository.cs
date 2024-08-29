using Authentication.Service.Models;

namespace Authentication.Service.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetById(int id);
    Task<Comment> CreateComment(Comment comment);
    Task<Comment> UpdateComment(int id, Comment commentModel);
    Task<Comment> DeleteComment(int id);
}