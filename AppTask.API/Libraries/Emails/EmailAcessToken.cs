using AppTask.Models;
using System.Net.Mail;

namespace AppTask.API.Libraries.Emails
{
    public class EmailAcessToken
    {
        private SmtpClient _smtp;

        public EmailAcessToken(SmtpClient smtp)
        {
            _smtp = smtp;
        }

        public void Send(UserModel usermodel)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress("jvictorsousa2015@gmail.com"),
                Subject = "Token de Acesso - AppTask",
                Body = $"Esse é o seu token de acesso: {usermodel.AcessToken}",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(usermodel.Email);
            _smtp.Send(mailMessage);
        }
    }
}
