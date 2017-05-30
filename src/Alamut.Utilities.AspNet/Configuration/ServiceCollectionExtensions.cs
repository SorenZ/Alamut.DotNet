using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// https://www.strathweb.com/2016/09/strongly-typed-configuration-in-asp-net-core-without-ioptionst/
namespace Alamut.Utilities.AspNet.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static TConfig ConfigurePoco<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
