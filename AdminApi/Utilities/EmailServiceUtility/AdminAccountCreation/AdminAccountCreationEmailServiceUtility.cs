using AdminApi.DTOs.Requests.EmailRequests;
using MimeKit;

namespace AdminApi.Utilities.EmailServiceUtility.AdminAccountCreation
{
    public class AdminAccountCreationEmailServiceUtility : MimeMessage
    {
        public AdminAccountCreationEmailServiceUtility(AdminAccountEmailRequestDTO request)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();

            string emailTemplate = Path.Combine(Directory.GetCurrentDirectory(), "Views", "SendEmailView", "AdminAccountEmail.html");
            string emailBody = File.ReadAllText(emailTemplate);
            emailBody = emailBody.Replace("{{AdminName}}", request.Firstname + " " + request.Lastname)
                .Replace("{{AdminEmail}}", request.Email)
                .Replace("{{AdminPassword}}", "" + request.Password);
            bodyBuilder.HtmlBody = emailBody;

            this.To.Add(new MailboxAddress(request.Firstname + " " + request.Lastname, request.Email));
            this.Subject = "Admin Account Created";
            this.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = bodyBuilder.HtmlBody };
        }
    }
}
