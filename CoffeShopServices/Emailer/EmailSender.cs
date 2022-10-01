using CoffeShopServices.Emailer.Models;
using MailKit.Net.Smtp;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace CoffeShopServices.Emailer
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailModel email)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("CoffeShop",email.Sender));
            message.To.Add(new MailboxAddress("CoffeShopClient",email.Receiver));
            
            message.Body = new BodyBuilder() { HtmlBody= $"<div><p>{email.Message}</p></div>"}.ToMessageBody();
            message.Subject = email.Theme;
            using (SmtpClient client = new SmtpClient())
            {
                client.Connect(_configuration.GetConnectionString("MailConnection"), int.Parse(_configuration.GetSection("MailProperties:Port").Value));
                client.Authenticate(_configuration.GetSection("MailProperties:Authenticate:username").Value, _configuration.GetSection("MailProperties:Authenticate:password").Value);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
