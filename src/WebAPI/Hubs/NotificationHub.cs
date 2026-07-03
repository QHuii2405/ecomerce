using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
        var userId = Context.UserIdentifier;

        // Nếu là Admin hoặc Staff, thêm vào nhóm "Admins" để nhận thông báo toàn hệ thống
        if (userRole == "Admin" || userRole == "Staff")
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        // Tự động kết nối tới UserId (SignalR tự động ánh xạ IUserIdProvider tới UserIdentifier)
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole == "Admin" || userRole == "Staff")
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
        }
        await base.OnDisconnectedAsync(exception);
    }
}
