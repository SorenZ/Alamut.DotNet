using System.IO;
using Alamut.Data.MongoDb.Mapper;
using Alamut.Sample.DataDriven.Contracts;
using Alamut.Sample.DataDriven.Mapper;
using Alamut.Sample.DataDriven.Models;
using Alamut.Sample.DataDriven.Repositories;
using Alamut.Sample.DataDriven.Services;
using Alamut.Utilities.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Alamut.Sample.DataDriven
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        private readonly IConfigurationRoot _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();


            // -----------------------------<( Dependency Injection )>-----------------------------
            // ASP.NET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<UserResolverService>(); // to find and resolve user information service throughout alamut and other framework

            // database
            //var settings = new MongoClientSettings
            //{
            //    ClusterConfigurator = cb =>
            //    {
            //        var textWriter = TextWriter.Synchronized(new StreamWriter("mylogfile.txt"));
            //        cb.AddListener(new LogListener(textWriter));
            //    }
            //};

            services.AddSingleton<IMongoDatabase>(_ =>
                new MongoClient(this._configuration["data:mongoConnection"])
                    .GetDatabase(this._configuration["data:mongoDatabase"]));
             
            // repositories 
            services.AddScoped<IArticleRepo, ArticleRepo>();

            // services
            services.AddScoped<IArticleService, ArticleService>();

            // object mapping
            AutoMapper.Mapper.Initialize(config => config.AddProfile<MapStrapper>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // configure database
            MongoMapper.MapId<Article>();
        }
    }
}
