using AnonChatAPI.Models;
using AnonChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnonChatAPI.Control;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly MessageService _messageService;

    public MessageController(MessageService messageService) =>
        _messageService = messageService;

    [HttpGet]
    public async Task<List<Message>> Get() =>
        await _messageService.GetAsync();
    [HttpGet("{UserId:length(24)}/CreatorId")]
    public async Task<List<Message>> GetByUserId(string UserId)
    {
        var messages = await _messageService.GetAsync2(UserId);
        return messages;
    }
    [HttpGet("{ForumId:length(24)}/ForumId")]
    public async Task<List<Message>> GetByForumId(string ForumId)
    {
        var messages = await _messageService.GetAsync3(ForumId);
        return messages;
    }
	[HttpGet("{ForumId:length(24)}/MessagesQuantity")]
	public string GetMQ(string ForumId)
	{
		var messages = _messageService.GetAsync4(ForumId);
		return messages.ToString(); 
	}
	[HttpPost]
	public async Task<IActionResult> Post(Message newM)
	{
		string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
		newM.Date = date;
		await _messageService.CreateAsync(newM);
		return CreatedAtAction(nameof(Get), new { id = newM.Id }, newM);
	}
	[HttpPut("{id:length(24)}")]
	public async Task<IActionResult> Update(string id, Message updatedM)
	{
		var message = await _messageService.GetAsync1(id);

		if (message is null)
		{
			return NotFound();
		}
		updatedM.Id = message.Id;

		await _messageService.UpdateAsync(id, updatedM);

		return NoContent();
	}

	[HttpDelete("{id:length(24)}")]
	public async Task<IActionResult> Delete(string id)
	{
		var message = await _messageService.GetAsync2(id);

		if (message is null)
		{
			return NotFound();
		}

		await _messageService.RemoveAsync(id);

		return NoContent();
	}
}