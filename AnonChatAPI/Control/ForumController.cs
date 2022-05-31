using AnonChatAPI.Models;
using AnonChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnonChatAPI.Control;

[ApiController]
[Route("api/[controller]")]

public class ForumController : ControllerBase
{
	private readonly ForumService _forumService;
	public ForumController(ForumService forumService) =>
		_forumService = forumService;

	[HttpGet("GetAllForums")]
	public async Task<List<Forum>> Get() =>
		await _forumService.GetAsync();
    [HttpGet("GetForumBy/{Id:length(24)}")]
    public async Task<Forum> GetById(string Id)
    {
        var forum = await _forumService.GetAsync4(Id);
        return forum;
    }
    [HttpGet("GetForumBy/{Name}")]
	public async Task<List<Forum>> GetByName(string Name)
	{
		var forum = await _forumService.GetAsync3(Name);
		return forum;
	}
	[HttpGet("GetForumsCreatedBy/{UserId:length(24)}")]
	public async Task<List<Forum>> Get(string UserId)
	{
		var forum = await _forumService.GetAsync(UserId);
		return forum;
	}
	[HttpGet("GetForumsByTag/{Tag}")]
	public async Task<List<Forum>> GetTagName(string Tag)
	{
        var forums = await _forumService.GetAsync2(Tag);
		return forums;
	}
	[HttpPost("CreateForum")]
    public async Task<IActionResult> Post(ForumCreation newForum)
    {              
        Forum forum = await _forumService.CreateAsync(newForum);
        if(forum == null)
		{
            return BadRequest("Forum's name already taken");
        }
        return CreatedAtAction(nameof(Get), new { id = forum.Id }, forum);
    }

    [HttpPut("ModifyForumBy/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Forum updatedForum)
    {
        var forum = await _forumService.GetAsync1(id);

        if (forum is null)
        {
            return NotFound();
        }

        updatedForum.Id = forum.Id;

        await _forumService.UpdateAsync(id, updatedForum);

        return NoContent();
    }

    [HttpDelete("DeleteForumBy/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var forum = await _forumService.GetAsync(id);

        if (forum is null)
        {
            return NotFound();
        }

        await _forumService.RemoveAsync(id);

        return NoContent();
    }
}
