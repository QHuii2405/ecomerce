namespace WebAPI.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> Ask(ChatRequest request)
    {
        var response = await _chatService.AskAsync(request);
        return Ok(response);
    }
}
