namespace ThreadsBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using ThreadsBackend.Services;
using ThreadsBackend.DTOs.Community;

[Route("api/communities")]
[ApiController]
public class CommunityController : ControllerBase
{
    private readonly ICommunityService _communityService;

    public CommunityController(ICommunityService communityService)
    {
        this._communityService = communityService;
    }

    [HttpPost]
    public async Task<ActionResult<CommunityDTO>> CreateCommunity([FromBody] CreateCommunityDTO body)
    {
        var community = await this._communityService.CreateCommunity(body);
        return Ok(community);
    }
}