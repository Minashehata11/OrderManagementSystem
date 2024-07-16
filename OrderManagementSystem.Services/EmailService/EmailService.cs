using System.Net;
using System.Net.Mail;

namespace OrderManagementSystem.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public  void SendEmail(EmailSetting emailSetting)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("minashehata495@gmail.com", "vbjrvmhdlpushixb");
            client.Send("minashehata495@gmail.com", emailSetting.TO, emailSetting.Title, emailSetting.Body);
            
        }
    }
}

