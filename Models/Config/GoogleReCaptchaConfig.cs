namespace Cria.Models.Config
{
    public class GoogleReCaptchaConfig
    {
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }

        public double SuccessThreshold { get; set; }

        public GoogleReCaptchaConfig()
        {
            SuccessThreshold = 0.6;
        }
    }
}
