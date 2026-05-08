using Azure;
using Azure.Communication.Email;
using System.Threading.Tasks;

namespace CoreApp
{
    // Call EmailManager.Configure(connectionString) at application startup before first use.
    public class EmailManager
    {
        private static string _connectionString = string.Empty;

        public static void Configure(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Azure Communication connection string cannot be empty.", nameof(connectionString));
            _connectionString = connectionString;
        }

        public async Task<string> SendEmail(string emailAddress, string otp)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException(
                    "EmailManager has not been configured. Call EmailManager.Configure(connectionString) at startup.");

            var emailClient = new EmailClient(_connectionString);
            var emailContent = new EmailContent("OTP Verification")
            {
                PlainText = $"\nHere is your verification code: {otp}"
            };

            var emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "SIMECID User") };
            var emailRecipients = new EmailRecipients(emailAddresses);
            var emailMessage = new EmailMessage(
                "DoNotReply@66e6180a-61c2-48bc-a646-7eb37607e7d8.azurecomm.net",
                emailRecipients,
                emailContent);

            var emailSendOperation = await emailClient.SendAsync(
                WaitUntil.Completed, emailMessage, CancellationToken.None);

            return emailSendOperation.Value.Status.ToString();
        }
    }
}
