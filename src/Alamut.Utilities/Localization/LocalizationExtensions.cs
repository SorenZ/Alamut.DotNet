using System.Linq;
using Alamut.Data.Entity;

namespace Alamut.Utilities.Localization
{
    public static class LocalizationExtensions
    {

        /// <summary>
        /// filter query by language 
        /// </summary>
        /// <remarks>if current system support multi language</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IQueryable<T> FilterByLanguage<T>(this IQueryable<T> source, ILocalizationService service)
            where T : IMultiLanguageEnity
        {
            return service != null && service.IsMulitLanguage
                ? source.Where(q => q.Lang == service.CurrenttLanguage)
                : source;
        }
    }
}