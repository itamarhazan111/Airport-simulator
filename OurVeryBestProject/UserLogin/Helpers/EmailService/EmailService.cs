using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace UserLogin.Helpers.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string email,string num)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = "change password";
            mail.Body = new TextPart(TextFormat.Html) {Text=num };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            await smtp.SendAsync(mail);
            smtp.Disconnect(true);



        }
    }
}
