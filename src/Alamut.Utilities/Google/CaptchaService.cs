using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Alamut.Abstractions.Structure;
using Newtonsoft.Json;

namespace Alamut.Utilities.Google
{
    public class CaptchaService
    {
        private readonly GoogleCaptchaConfig _chaptchaConfig;

        public CaptchaService(GoogleCaptchaConfig chaptchaConfig)
        {
            _chaptchaConfig = chaptchaConfig;
        }

        public async Task<Result> ValidateGoogleChaptcha(string rechaptchaKey)
        {

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"secret", _chaptchaConfig.SecretKey},
                    {"response", rechaptchaKey}
                };

                try
                {
                    var response = await client.PostAsync(_chaptchaConfig.ApiUrl, new FormUrlEncodedContent(values));
                    //var result = await response.Content.ReadAsAsync<GoogelSiteVerifyResponse>();
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<GoogelSiteVerifyResponse>(json);
                        return result.success ? Result.Okay() : Result.Error();
                    }

                    return Result.Error(response.ReasonPhrase);
                }
                catch (Exception ex)
                {
                    return Result.Exception(ex);
                }
            }
        }

        class GoogelSiteVerifyResponse
        {
            public bool success { get; set; }
            public string challenge_ts { get; set; }
            public string hostname { get; set; }
            //public string error-codes { get; set; }
        }
    }

    public class GoogleCaptchaConfig
    {
        public string SecretKey { get; set; }
        public string SiteKey { get; set; }
        public string ApiUrl { get; set; } = "https://www.google.com/recaptcha/api/siteverify";
    }
}
