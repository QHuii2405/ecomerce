using Application.DTOs;

namespace Application.Interfaces;

public interface IChatService
{
    Task<ChatResponse> AskAsync(ChatRequest request);
}
