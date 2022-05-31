using AnonChatAPI.Models;
using AnonChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnonChatAPI.Control;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService) =>
        _userService = userService;

    [HttpGet("GetAllUsers")]
    public async Task<List<User>> Get() =>
        await _userService.GetAsync();

    [HttpGet("GetUserBy/{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }
    [HttpGet("GetUserBy/{NickName}")]
    public async Task<ActionResult<User>> GetByNickName(string NickName)
    {
        var user = await _userService.GetAsync2(NickName);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }
    [HttpGet("GetUserBy/{email}/{password}")]
	public async Task<ActionResult<User>> GetUser(string email, string password)
	{
		var user = await _userService.GetAsync3(email, password);
		if (user is null)
		{
			return NotFound();
		}
		return user;
	}

    [HttpPost("CreateUser")]
    public async Task<IActionResult> Post(UserRegistration newUser)
    {        
        User _newUser = await _userService.CreateAsync(newUser);
		if (_newUser == null)
		{
			return BadRequest("User's name or email is already taken");
		}
		return CreatedAtAction(nameof(Get), new { id = _newUser.Id }, _newUser);
    }

    [HttpPut("ModifyUserBy/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

        await _userService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("DeleteUserBy/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);

        return NoContent();
    }
}