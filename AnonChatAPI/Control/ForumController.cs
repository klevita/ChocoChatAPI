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

	[HttpGet]
	public async Task<List<Forum>> Get() =>
		await _forumService.GetAsync();
	[HttpGet("{Name}")]
	public async Task<ActionResult<Forum>> GetByName(string Name)
	{
		var forum = await _forumService.GetAsync3(Name);

		if (forum is null)
		{
			return NotFound();
		}

		return forum;
	}
	[HttpGet("{UserId:length(24)}/Creator")]
	public async Task<List<Forum>> Get(string UserId)
	{
		var forum = await _forumService.GetAsync(UserId);
		return forum;
	}
	[HttpGet("{Tag}/Tag")]
	public async Task<List<Forum>> GetTagName(string Tag)
	{
		var forums = await _forumService.GetAsync2(Tag);
		return forums;
	}
    [HttpPost]
    public async Task<IActionResult> Post(Forum newForum)
    {
        string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
        newForum.Date = date;
        await _forumService.CreateAsync(newForum);
        return CreatedAtAction(nameof(Get), new { id = newForum.Id }, newForum);
    }

    [HttpPut("{id:length(24)}")]
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

    [HttpDelete("{id:length(24)}")]
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
