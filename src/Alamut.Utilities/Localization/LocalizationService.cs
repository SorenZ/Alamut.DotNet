using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alamut.Helpers.Localization;
using Alamut.Utilities.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Alamut.Utilities.Localization
{

    /// <summary>
    /// abstact class for localization service
    /// provide shared features for localization
    /// </summary>
    public abstract class AbstactLocalizationService : ILocalizationService
    {
        private readonly IOptions<LocalizationConfig> _configuration;

        protected AbstactLocalizationService(IOptions<LocalizationConfig> options)
        {
            _configuration = options;
        }

        public Dictionary<string, string> GetSupportedLanguges()
        {
            return _configuration.Value.SupportedLanguges;
        }

        public IList<CultureInfo> GetSupportedCulture()
        {
            return _configuration.Value.SupportedLanguges
                .Select(s => new CultureInfo(s.Key))
                .ToList();
        }

        public string CurrenttLanguage => Language.Current;

        public abstract void SetCurrentLanguage(string isoLanguage);

        public string CurrenttLanguageTitle => this._configuration.Value.SupportedLanguges[Language.Current];

        public string DefaultLanguage => _configuration.Value.DefaultLanguage;
        
        public bool IsMulitLanguage => _configuration.Value.IsMultiLanguage;
    }

    /// <summary>
    /// provide information about localization 
    /// use cookie for storage
    /// </summary>
    /// <remarks>
    /// used in control panel or admin
    /// you have to use CookieRequestCultureProvider to work with this class
    /// https://docs.asp.net/projects/api/en/latest/autoapi/Microsoft/AspNetCore/Localization/CookieRequestCultureProvider/index.html
    /// </remarks>
    public class CookieBasedLocalizationService : AbstactLocalizationService
    {
        private readonly HttpContext _httpContext;

        public CookieBasedLocalizationService(IHttpContextAccessor contextAccessor, 
            IOptions<LocalizationConfig> options) : base(options)
        {
            _httpContext = contextAccessor.HttpContext;
        }

        //public override string CurrenttLanguage {
        //    set
        //    {
        //        _httpContext.Response.Cookies.Append("Sam.Language", value,
        //            new CookieOptions {Expires = DateTimeOffset.UtcNow.AddYears(1)});
        //    }

        //    get
        //    {
        //        string value;
        //        return _httpContext.Request.Cookies.TryGetValue("Sam.Language", out value)
        //            ? value
        //            : base.DefaultLanguage;
        //    }
        //}

        public override void SetCurrentLanguage(string isoLanguage)
        {
            _httpContext.Response.Cookies.Append(
                //CookieRequestCultureProvider.DefaultCookieName,
                "Sam.Culture",
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(isoLanguage)),
                new CookieOptions {Expires = DateTimeOffset.UtcNow.AddYears(1)});

        }
    }

    /// <summary>
    /// provide information about localization 
    /// use thread to store information 
    /// </summary>
    /// <remarks>
    /// use in web site that relize on ASP.NET Standard Localization system
    /// you have to use SubDomainRequestCultureProvider for this class to work well 
    /// </remarks>
    public class ThreadBasedLocalizationService : AbstactLocalizationService
    {
        public ThreadBasedLocalizationService(IOptions<LocalizationConfig> options) : base(options)
        {
        }

        //[Obsolete("use SetCurrentLanguage()")]
        //public override string CurrenttLanguage
        //{
        //    set
        //    {
        //        throw new NotImplementedException("Thread-Based could not change the language");
        //    }
        //    get
        //    {
        //        return Language.Current;
        //    }
        //}

        public override void SetCurrentLanguage(string isoLanguage)
        {
            throw new NotImplementedException("Thread-Based could not change the language, redirect to current url and change subdomain.");
        }
    }
}
