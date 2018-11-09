using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Defaults;
using FluentEmail.Mailgun;
using Microsoft.Extensions.Options;

namespace FinanceWeb.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            Execute("", subject, message, email).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            var sender = new MailgunSender(
                "sandbox37d998c92.mailgun.org", // Mailgun Domain
                "key-a3e4c319a474cb01814e0" // Mailgun API Key
            );

            Email.DefaultSender = sender;
            
            var emails = Email
                .From("aaaaaaa")
                .To("@.com")
                .Subject(subject)
                .Body(message,true);

            var response = await emails.SendAsync();
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
