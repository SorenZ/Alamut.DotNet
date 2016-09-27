using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Alamut.Helpers.Localization;
using Microsoft.Extensions.Options;

namespace Alamut.Utilities.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IOptions<LocalizationConfig> _configuration;

        public LocalizationService(IOptions<LocalizationConfig> options)
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

        public string CurrentLanguage => this.IsMulitLanguage ? Language.Current : string.Empty;

        public string CurrentLanguageTitle => this._configuration.Value.SupportedLanguges[Language.Current];

        public string DefaultLanguage => _configuration.Value.DefaultLanguage;

        public bool IsMulitLanguage => _configuration.Value.IsMultiLanguage;
    }
}
