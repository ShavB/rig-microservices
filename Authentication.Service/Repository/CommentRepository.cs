using Authentication.Service.Data;
using Authentication.Service.Interfaces;
using Authentication.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Service.Repository;

public class CommentRepository(ApplicationDBContext applicationDBContext) : ICommentRepository
{
    private readonly ApplicationDBContext _applicationDBContext = applicationDBContext;

    public async Task<Comment> CreateComment(Comment comment)
    {
        await _applicationDBContext.Comments.AddAsync(comment);
        _applicationDBContext.SaveChanges();
        return comment;
    }

    public async Task<Comment?> DeleteComment(int id)
    {
        var existingComment = await _applicationDBContext.Comments.FirstOrDefaultAsync(i =>
            i.Id == id
        );
        if (existingComment == null)
        {
            return null;
        }
        _applicationDBContext.Comments.Remove(existingComment);
        _applicationDBContext.SaveChanges();
        return existingComment;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _applicationDBContext.Comments.Include(x => x.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetById(int id)
    {
        return await _applicationDBContext
            .Comments.Include(x => x.AppUser)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Comment> UpdateComment(int id, Comment commentModel)
    {
        var comment = await _applicationDBContext.Comments.FindAsync(id);
        if (comment == null)
        {
            return null;
        }

        comment.Title = commentModel.Title;
        comment.Context = comment.Context;
        _applicationDBContext.SaveChanges();
        return comment;
    }
}
