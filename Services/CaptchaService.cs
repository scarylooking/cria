using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
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
        Task<bool> IsValid(string token);
    }

    public class CaptchaService : ICaptchaService
    {
        private const string VerificationUrl = "https://www.google.com/recaptcha/api/siteverify";

        private readonly GoogleReCaptchaConfig _captchaSettings;
        private readonly ILogger<CaptchaService> _logger;
        private readonly HttpClient _httpClient;

        public CaptchaService(IOptions<GoogleReCaptchaConfig> config, ILogger<CaptchaService> logger, HttpClient httpClient)
        {
            _captchaSettings = config.Value;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<bool> IsValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            return await SendVerificationRequest(token);
        }

        private async Task<bool> SendVerificationRequest(string token)
        {
            if (string.IsNullOrWhiteSpace(_captchaSettings.SecretKey))
            {
                _logger.LogError("_captchaSettings.SecretKey is missing!");
                return false;
            }

            try
            {
                var requestUri = $"{VerificationUrl}?secret={_captchaSettings.SecretKey}&response={token}";

                var rawResponse = await _httpClient.PostAsync(requestUri, null);
                var jsonResponse = await rawResponse.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<GoogleCaptchaVerificationResponse>(jsonResponse);

                if (response == null)
                {
                    _logger.LogError("Response from ReCaptcha Validation service was null");
                    return false;
                }

                if (!response.Success)
                {
                    var errorString = "ReCaptcha Validation service reported unsuccessful validation";

                    if (response.Errors.Any())
                    {
                        errorString += $" and the following errors: {string.Join(',', response.Errors)}";
                    }

                    _logger.LogError(errorString);

                    return false;
                }

                return response.Score >= (decimal)0.6;
            }
            catch (Exception e)
            {
                _logger.LogError($"Encountered an exception while attempting to process captcha validation: {e.Message}", e);
                return false;
            }
        }
    }
}
