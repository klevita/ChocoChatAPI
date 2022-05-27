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
    [HttpGet("{UserId:length(24)}/Creator")]
    public async Task<List<Message>> Get(string UserId)
    {
        var messages = await _messageService.GetAsync2(UserId);
        return messages;
    }
}