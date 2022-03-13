using EvrenDev.Application.Interfaces.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EvrenDev.Application.SharedPreferences;

namespace EvrenDev.Infrastructure.Persistence.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;

        public EmailSender(
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _loggerFactory = loggerFactory;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(MailParameter parameter)
        {

            var smtp = _configuration.GetSection("SmtpInformation").Get<SmtpInformation>();
            var sender = _configuration.GetSection("SenderInformation").Get<SenderInformation>();
            
            var from = new MailAddress(smtp.Username, smtp.Name);
            var to = new MailAddress(parameter.ToEmail, parameter.ToName);

            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = smtp.Server,
                    Port = smtp.Port,
                    EnableSsl = smtp.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtp.Username, smtp.Password)
                };

                var email = new MailMessage();
                email.From = from;
                email.To.Add(to);
                email.Subject = parameter.Subject;
                email.Body = parameter.HtmlContent;
                email.IsBodyHtml = true;
                
                await smtpClient.SendMailAsync(email);

                var logger = _loggerFactory.CreateLogger<EmailSender>();
                logger.LogInformation("Email başarılı bir şekilde gönderildi.");
            }
            catch (Exception e)
            {
                var logger = _loggerFactory.CreateLogger<EmailSender>();
                logger.LogError(e, "Eposta gönderilirken bir hata oluştu.");
                throw;
            }
        }
    }
}
