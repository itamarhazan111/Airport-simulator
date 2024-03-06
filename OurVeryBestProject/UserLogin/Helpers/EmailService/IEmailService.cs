namespace UserLogin.Helpers.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(string email,string num);
    }
}
