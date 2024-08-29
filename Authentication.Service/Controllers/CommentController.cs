using Authentication.Service.DTO.Comments;
using Authentication.Service.Extensions;
using Authentication.Service.Interfaces;
using Authentication.Service.Mappers;
using Authentication.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController(
    ICommentRepository commentRepository,
    IStockRepository stockRepository,
    UserManager<AppUser> userManager
) : ControllerBase
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly UserManager<AppUser> _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comments = await _commentRepository.GetAllAsync();
        var commentDto = comments.Select(x => x.ToCommentDto());
        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await _commentRepository.GetById(id);
        return Ok(comment?.ToCommentDto());
    }

    [HttpPost("{StockId:int}")]
    public async Task<IActionResult> CreateComment(
        [FromRoute] int StockId,
        CreateCommentDto createCommentDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _stockRepository.StockExists(StockId))
        {
            return BadRequest("stock does not exists");
        }
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var commentModel = createCommentDto.ToCommentFromCreate(StockId);
        commentModel.AppuserId = appUser.Id;
        await _commentRepository.CreateComment(commentModel);
        return CreatedAtAction(
            nameof(GetCommentById),
            new { id = commentModel.Id },
            commentModel.ToCommentDto()
        );
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateComment(
        [FromRoute] int id,
        [FromBody] UpdateCommentRequestDto updateCommentRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await _commentRepository.UpdateComment(
            id,
            updateCommentRequestDto.ToCommentFromUpdate()
        );
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleComment([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await _commentRepository.DeleteComment(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }
}
