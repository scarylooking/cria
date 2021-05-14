using System;
using System.Text.Json.Serialization;

namespace Cria.Models
{
    public class GoogleCaptchaVerificationResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("score")]
        public decimal Score { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("error-codes")]
        public string[] Errors { get; set; }

        public GoogleCaptchaVerificationResponse()
        {
            Success = false;
        }
    }
}