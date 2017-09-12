using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAppService.Data;
using UserAppService.Models;
using UserAppService.Services;
using System.Web;
using UserAppService.Configuration;
using UserAppService.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace UserAppService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["AppSettings:ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-5DLFS28;AttachDbFilename=|DataDirectory|UserManagement.mdf;Integrated Security=True;User Instance=True"));

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            // Add functionality to inject IOptions<T>
            services.AddOptions(); //https://weblog.west-wind.com/posts/2016/may/23/strongly-typed-configuration-settings-in-aspnet-core

            // Add our Config object so it can be injected
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHttpContextAccessor();

            ConfigureJsonFormatter.SetJsonFormatterSerializerSettings(services);
            IntegrateSimpleInjector(services);
        }

        public void IntegrateSimpleInjector(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseStaticHttpContext();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var FacebookAppId = Configuration[$"{Configuration["AppSetting:FacebookAppIdConfigName"]}"];
            var FacebookAppSecret = Configuration[$"{Configuration["AppSetting:FacebookAppSecretConfigName"]}"];

            if (FacebookAppId != null && FacebookAppSecret != null)
            {
                //app.UseFacebookAuthentication(new FacebookOptions()
                //{
                //    AppId = FacebookAppId,
                //    AppSecret = FacebookAppSecret
                //});
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
