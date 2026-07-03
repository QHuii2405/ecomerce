using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body, byte[]? attachmentData = null, string? attachmentName = null);
}
