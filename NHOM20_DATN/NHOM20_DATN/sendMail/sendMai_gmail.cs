using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MailKit.Security;
using MimeKit;

namespace NHOM20_DATN.sendMail
{
    public class sendMai_gmail
    {
        public int sendMail_gmail(string mail, string subject, string description)
        {
            try
            {
                // Tạo nội dung email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bệnh viện Hospital", "banana1999@gmail.com"));
                message.To.Add(new MailboxAddress("", mail));
                message.Subject = subject;
                var bodyBuilder = new BodyBuilder { TextBody = description };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("bananahospitaldanang@gmail.com", "bvezpjcixxoypqvi"); // Sử dụng mật khẩu ứng dụng
                    client.Send(message);
                    client.Disconnect(true);
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}