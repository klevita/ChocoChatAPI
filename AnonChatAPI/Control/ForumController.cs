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
	public async Task<ActionResult<Forum>> Get(string Name)
	{
		var forum = await _forumService.GetAsync(Name);

		if (forum is null)
		{
			return NotFound();
		}

		return forum;
	}
}
