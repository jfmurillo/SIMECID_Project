using Org.BouncyCastle.Tls.Crypto.Impl;
using System;
using System.Net;
using System.Net.Mail;
using OtpNet;
using DTO;

namespace CoreApp
{
    public class Mailer
    {
        public void SendEmail()
        {
            /*var keyGenerator = new KeyGenerator();*/
           /* var secretKey = keyGenerator.GenerateRandomKey(20);
            var base32SecretKey = Base32Encoding.ToString(secretKey);*/



            /*var totp = new Totp(secretKey);*/
            /*var totp = new Totp(secretKey, step: 90, totpSize: 4); 
            var totpCode = totp.ComputeTotp(DateTime.UtcNow);*/

            /*bool isValid = totp.VerifyTotp(userEnteredCode, out _);*//*
*/
            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("simecid.services@gmail.com", "SIMECID", System.Text.Encoding.UTF8);
                correo.To.Add(""); // DESTINATARIO
                correo.Subject = "OTP Verification";
                correo.Body = "\r\nHi!\r\n\r\n" +
                    "Your verification code is [code].\r\n\r\n" +
                    "Enter this code in our website to activate your account." +
                    $"\r\n\r\nClick <a\" asp-area=\"\" asp-page=\"/LandingPageSIMECID\">here</a> to open our landing page.\r\n\r\n" +
                    "If you have any questions, send us an email [email to your support team].\r\n\r\n" +
                    "" +
                    "We’re glad you’re here!\r\n" +
                    "The SIMECID team";
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential("simecid.services@gmail.com", "Cenfotec123!");

                smtp.Send(correo);

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

    }
}
