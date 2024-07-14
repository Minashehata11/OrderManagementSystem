using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Services.EmailService
{
    public interface IEmailService
    {
     public void SendEmail(EmailSetting emailSetting);
    }
}
