﻿using System.Net.Mail;
using System.Net;
using System.Text;

namespace MSTestProject.Mocking
{
    public interface IEmailSender
    {
        void EmailFile(string address, string body, string filename, string subject);
    }

    public class EmailSender : IEmailSender
    {
        public void EmailFile(string address, string body, string filename, string subject)
        {
            var client = new SmtpClient(SystemSettingsHelper.EmailSmtpHost)
            {
                Port = SystemSettingsHelper.EmailPort,
                Credentials = new NetworkCredential(SystemSettingsHelper.EmailUsername, SystemSettingsHelper.EmailPassword)
            };

            var from = new MailAddress(SystemSettingsHelper.EmailFromEmail, SystemSettingsHelper.EmailFromName, Encoding.UTF8);
            var to = new MailAddress(address);

            var message = new MailMessage(from, to)
            {
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = body,
                BodyEncoding = Encoding.UTF8
            };

            message.Attachments.Add(new Attachment(filename));
            client.Send(message);
            message.Dispose();

            File.Delete(filename);
        }
    }
}
