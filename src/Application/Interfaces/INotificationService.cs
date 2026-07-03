using System.Threading.Tasks;

namespace Application.Interfaces;

public interface INotificationService
{
    Task SendNotificationToAdminsAsync(string title, string message);
    Task SendNotificationToUserAsync(string userId, string title, string message);
}
