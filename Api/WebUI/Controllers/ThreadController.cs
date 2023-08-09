namespace ThreadsBackend.Api.Application.Controllers;

using Microsoft.AspNetCore.Mvc;
using ThreadsBackend.Api.Domain.DTOs.Thread;
using ThreadsBackend.Api.Application.Services;

[Route("api/threads")]
[ApiController]
public class ThreadController : ControllerBase
{
    private readonly IThreadService _threadService;

    public ThreadController(IThreadService threadService)
    {
        this._threadService = threadService;
    }

    [HttpPost]
    public async Task<ActionResult<ThreadDTO>> CreateThread([FromBody] CreateThreadDTO body)
    {
        var thread = await this._threadService.CreateThread(body);
        return Ok(thread);
    }

    [HttpGet]
    public async Task<ActionResult<List<ThreadDTO>>> ListThreads([FromQuery] ListThreadsQueryDTO query)
    {
        var threads = await this._threadService.ListThreads(query);
        return Ok(threads);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ThreadDTO>> GetThread([FromRoute] Guid id)
    {
        var thread = await this._threadService.GetThread(id);
        return Ok(thread);
    }

    [HttpPost("add/comment")]
    public async Task<ActionResult<ThreadDTO>> AddCommentToThread([FromBody] AddCommentDTO body)
    {
        var thread = await this._threadService.AddCommentToThread(body);
        return Ok(thread);
    }
}