using MailKit.Net.Smtp;
using MimeKit;
using ReminderSchedulerApp.Models;

namespace ReminderSchedulerApp.Models
{
    public class EmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IConfiguration config)
        {
            _settings = config.GetSection("EmailSettings").Get<EmailSettings>();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
{
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("Reminder App", "youremail@gmail.com"));
    message.To.Add(new MailboxAddress("", toEmail));
    message.Subject = subject;
    message.Body = new TextPart("plain") { Text = body };

    using var client = new SmtpClient();
    await client.ConnectAsync("smtp.gmail.com", 587, false);
    await client.AuthenticateAsync("youremail@gmail.com", "your_app_password");
    await client.SendAsync(message);
    await client.DisconnectAsync(true);

    Console.WriteLine($"[Hangfire] Sent email to {toEmail} ✅");
}

    }
}
