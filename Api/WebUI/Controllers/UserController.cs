namespace ThreadsBackend.Api.Application.Controllers;

using Microsoft.AspNetCore.Mvc;
using ThreadsBackend.Api.Domain.DTOs.User;
using ThreadsBackend.Api.Domain.DTOs.Thread;
using ThreadsBackend.Api.Application.Services;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        this._userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> ListUsers([FromQuery] ListUsersQueryDTO query)
    {
        var users = await this._userService.ListUsers(query);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser([FromRoute] string id)
    {
        var user = await this._userService.GetUser(id);
        return Ok(user);
    }

    [HttpPatch("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromRoute] string id, [FromForm] UpdateUserDTO body)
    {
        var user = await this._userService.UpdateUser(id, body);
        return Ok(user);
    }

    [HttpGet("{id}/activity")]
    public async Task<ActionResult<List<ThreadDTO>>> GetUserActivity([FromRoute] string id)
    {
        var user = await this._userService.GetUserActivity(id);
        return Ok(user);
    }

    [HttpGet("{id}/profile")]
    public async Task<ActionResult<GetUserProfileResponseDTO>> GetUserProfile([FromRoute] string id, [FromQuery] GetUserProfileQueryDTO query)
    {
        var profile = await this._userService.GetUserProfile(id, query);
        return Ok(profile);
    }

    [HttpGet("suggest")]
    public async Task<ActionResult<List<UserDTO>>> GetSuggestUsers([FromQuery] GetSuggestUsersQueryDTO query)
    {
        var users = await this._userService.GetSuggestUsers(query);
        return Ok(users);
    }
}