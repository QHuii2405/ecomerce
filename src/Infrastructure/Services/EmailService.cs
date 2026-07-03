using Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body, byte[]? attachmentData = null, string? attachmentName = null)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        
        var senderName = emailSettings["SenderName"] ?? "iLuminaty Shop";
        var senderEmail = emailSettings["SenderEmail"] ?? "no-reply@iluminaty.com";
        var host = emailSettings["Host"] ?? "smtp.ethereal.email";
        var port = int.Parse(emailSettings["Port"] ?? "587");
        var username = emailSettings["Username"] ?? "test@ethereal.email";
        var password = emailSettings["Password"] ?? "test-password";

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, senderEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        
        if (attachmentData != null && !string.IsNullOrEmpty(attachmentName))
        {
            bodyBuilder.Attachments.Add(attachmentName, attachmentData, new ContentType("application", "pdf"));
        }

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(message);
        }
        catch
        {
            // Fallback: Nếu không gửi được email thật (ví dụ do cấu hình sai), 
            // chúng ta vẫn catch lỗi để không làm gián đoạn luồng chạy chính của ứng dụng
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
