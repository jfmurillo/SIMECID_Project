using Azure;
using Azure.Communication.Email;
using System.Threading.Tasks;

namespace CoreApp
{
    // Call ApptAlertManager.Configure(connectionString) at application startup before first use.
    public class ApptAlertManager
    {
        private static string _connectionString = string.Empty;

        public static void Configure(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Azure Communication connection string cannot be empty.", nameof(connectionString));
            _connectionString = connectionString;
        }

        public async Task<string> SendAppointmentAlert(string emailAddress)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException(
                    "ApptAlertManager has not been configured. Call ApptAlertManager.Configure(connectionString) at startup.");

            var emailClient = new EmailClient(_connectionString);
            var emailContent = new EmailContent("SIMECID: Please Review Your Pending Appointments")
            {
                PlainText = "Dear user,\n\n" +
                    "This is a friendly reminder to review your pending appointments and ensure everything is in order.\n" +
                    "Please log in to your account to confirm the details of your upcoming appointments.\n\n" +
                    "Best regards,\nThe SIMECID Team"
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
