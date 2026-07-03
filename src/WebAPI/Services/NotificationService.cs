using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WebAPI.Hubs;

namespace WebAPI.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IServiceScopeFactory _scopeFactory;

    public NotificationService(
        IHubContext<NotificationHub> hubContext,
        IServiceScopeFactory scopeFactory)
    {
        _hubContext = hubContext;
        _scopeFactory = scopeFactory;
    }

    public async Task SendNotificationToAdminsAsync(string title, string message)
    {
        await _hubContext.Clients.Group("Admins").SendAsync("ReceiveNotification", new { title, message, date = System.DateTime.UtcNow });

        // Gửi email cho tất cả Admin
        using var scope = _scopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var admins = await unitOfWork.Users.FindAsync(u => u.Role == "Admin");
        foreach (var admin in admins)
        {
            if (!string.IsNullOrEmpty(admin.Email))
            {
                await emailService.SendEmailAsync(admin.Email, title, message);
            }
        }
    }

    public async Task SendNotificationToUserAsync(string userId, string title, string message)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new { title, message, date = System.DateTime.UtcNow });

        // Gửi email cho user
        using var scope = _scopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        if (Guid.TryParse(userId, out var userGuid))
        {
            var user = await unitOfWork.Users.GetByIdAsync(userGuid);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                await emailService.SendEmailAsync(user.Email, title, message);
            }
        }
    }
}
