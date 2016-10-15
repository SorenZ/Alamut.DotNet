using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Alamut.Utilities.AutoMapper
{
    public static class MapperExtensions
    {
        private static void AddAutoMapperClasses(IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
        {
            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();

            var allTypes = assembliesToScan.SelectMany(a => a.ExportedTypes).ToArray();

            var profiles =
                allTypes
                    .Where(t => typeof (Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                    .Where(t => !t.GetTypeInfo().IsAbstract);

            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
        }
    }
}
