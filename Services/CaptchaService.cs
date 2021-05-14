using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cria.Models.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Cria.Models;

namespace Cria.Services
{
    public interface ICaptchaService
    {
        string ClientKey { get; }
        Task<bool> IsValid(string token);
    }

    public class CaptchaService : ICaptchaService
    {
        private readonly GoogleReCaptchaConfig _captchaSettings;
        private readonly ILogger<CaptchaService> _logger;

        public string ClientKey => _captchaSettings.SiteKey;

        public CaptchaService(IOptions<GoogleReCaptchaConfig> config, ILogger<CaptchaService> logger)
        {
            _captchaSettings = config.Value;
            _logger = logger;
        }

        public async Task<bool> IsValid(string token)
        {
            var result = false;

            var googleVerificationUrl = "https://www.google.com/recaptcha/api/siteverify";

            try
            {
                using var client = new HttpClient();

                var rawResponse = await client.PostAsync($"{googleVerificationUrl}?secret={_captchaSettings.SecretKey}&response={token}", null);
                var jsonResponse = await rawResponse.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<GoogleCaptchaVerificationResponse>(jsonResponse);

                result = response != null && response.Success;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to process captcha validation", e);
            }

            return result;
        }
    }
}
