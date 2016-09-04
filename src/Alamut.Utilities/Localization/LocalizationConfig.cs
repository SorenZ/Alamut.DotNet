using System.Collections.Generic;

namespace Alamut.Utilities.Localization
{
    /// <summary>
    /// provide localization service configuration model
    /// </summary>
    public class LocalizationConfig
    {
        public bool IsMultiLanguage { get; set; } = false;
        public string DefaultLanguage { get; set; }
        public Dictionary<string,string> SupportedLanguges { get; set; }
    }
}