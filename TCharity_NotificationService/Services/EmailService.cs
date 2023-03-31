using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;
using TCharity_NotificationService.Models.EmailModels;
using Twilio.Base;

namespace TCharity_NotificationService.Services;

public class EmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly SendGridOption _sendGripOption;
    private readonly ILogger<EmailService> _logger;


    public EmailService(ISendGridClient sendGridClient, Microsoft.Extensions.Options.IOptions<SendGridOption> sendGripOption, ILogger<EmailService> logger)
    {
        _sendGridClient = sendGridClient;
        _logger = logger;
        _sendGripOption = sendGripOption.Value;
    }

    public async Task SendPaymentConfirmationAsync(SendPaymentCaptureRequest request)
    {
        var message = CreateMessageFromTemplate(request, "PaymentConfirmationTemplate.html", new
            {
                PostURL = request.PostUrl
            });
        
        var response = await _sendGridClient.SendEmailAsync(message);
        if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Failed to send email: {response.StatusCode} - {await response.Body.ReadAsStringAsync()}");
            throw new Exception($"Failed to send email: {response.StatusCode} - {await response.Body.ReadAsStringAsync()}");
        }
    }

    public async Task SendDisburseNotificationAsync(SendDisburseNotificationRequest request)
    {
        var message = CreateMessageFromTemplate(request, "DonationDisbursementTemplate.html", new
            {
                PostURL = request.PostUrl,
                TotalAmout = request.TotalAmount
            });
        
        var response = await _sendGridClient.SendEmailAsync(message);
        if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Failed to send email: {response.StatusCode} - {await response.Body.ReadAsStringAsync()}");
            throw new Exception($"Failed to send email: {response.StatusCode} - {await response.Body.ReadAsStringAsync()}");
        }
    }
    
    
    private SendGridMessage CreateMessageFromTemplate(SendEmailBaseRequest baseRequest,
        string templateName, object templateData)
    {
        var message = new SendGridMessage();
        message.SetFrom(new EmailAddress(_sendGripOption.FromEmail));
        message.AddTo(new EmailAddress(baseRequest.ToEmail, baseRequest.ToEmailName));
        message.SetSubject(baseRequest.Subject);

        // Read the email template from a file
        var path = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", templateName);
        var htmlContent = File.ReadAllText(path);

        // Replace the placeholders in the template with the actual values
        foreach (var property in templateData.GetType().GetProperties())
        {
            var placeholder = $"{{{property.Name}}}";
            var value = property.GetValue(templateData)?.ToString();
            htmlContent = htmlContent.Replace(placeholder, value ?? "");
        }

        message.AddContent(MimeType.Html, htmlContent);
        return message;
    }
}