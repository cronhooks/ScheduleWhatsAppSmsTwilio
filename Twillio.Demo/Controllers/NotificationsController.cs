using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Twillio.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;
        private readonly IConfiguration _configuration;

        public NotificationsController(ILogger<NotificationsController> logger, 
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("whatsapp")]
        public async Task<IActionResult> SendWhatsappMessage(MessageRequest request)
        {
            try
            {
                // Find your Account SID and Auth Token at twilio.com/console
                // and set the environment variables. See http://twil.io/secure
                string accountSid = _configuration.GetValue<string>("TWILIO_ACCOUNT_SID");
                string authToken = _configuration.GetValue<string>("TWILIO_AUTH_TOKEN");

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: request.Message,
                    from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                    to: new Twilio.Types.PhoneNumber($"whatsapp:{request.Phone}")
                );

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpPost("sms")]
        public async Task<IActionResult> SendSMS(MessageRequest request)
        {
            try
            {
                // Find your Account SID and Auth Token at twilio.com/console
                // and set the environment variables. See http://twil.io/secure
                string accountSid = _configuration.GetValue<string>("TWILIO_ACCOUNT_SID");
                string authToken = _configuration.GetValue<string>("TWILIO_AUTH_TOKEN");

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: request.Message,
                    from: new Twilio.Types.PhoneNumber("+14155238886"),
                    to: new Twilio.Types.PhoneNumber($"{request.Phone}")
                );

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
    }

    public class MessageRequest
    {
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
