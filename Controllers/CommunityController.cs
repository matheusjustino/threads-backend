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

    [HttpGet]
    public async Task<ActionResult<List<CommunityDTO>>> GetCommunity([FromQuery] ListCommunitiesQueryDTO query)
    {
        var communities = await this._communityService.ListCommunities(query);
        return Ok(communities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommunityDTO>> GetCommunity([FromRoute] string id)
    {
        var community = await this._communityService.GetCommunity(id);
        return Ok(community);
    }

    [HttpPut("{id}/add/member")]
    public async Task<ActionResult<CommunityDTO>> AddMemberToCommunity([FromRoute] string id,
        [FromBody] AddMemberToCommunity body)
    {
        var community = await this._communityService.AddMemberToCommunity(id, body.userId);
        return Ok(community);
    }
}