using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAppService.Models;
using UserAppService.Services;
using System.Web;
using UserAppService.Configuration;
using UserAppService.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserAppService.Context;
using System;
using UserAppService.Service;
using Autofac;

namespace UserAppService
{
    public class Startup
    {
        private string _contentRootPath = "";

        public Startup(IHostingEnvironment env)
        {
            _contentRootPath = env.ContentRootPath;

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

        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            string connectionString = Configuration["AppSettings:ConnectionStrings:DefaultConnection"];

            if (connectionString.Contains("%CONTENTROOTPATH%"))
            {
                connectionString = connectionString.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }

            services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            // Add functionality to inject IOptions<T>
            services.AddOptions(); //https://weblog.west-wind.com/posts/2016/may/23/strongly-typed-configuration-settings-in-aspnet-core

            // Add our Config object so it can be injected
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHttpContextAccessor();

            ConfigureJsonFormatter.SetJsonFormatterSerializerSettings(services);
            // Add framework services.
            services.AddMvc();
            // Make authentication compulsory across the board (i.e. shut
            // down EVERYTHING unless explicitly opened up).
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // Register application services.
            services.AddScoped<IAuthService, AuthService>();

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton(Configuration);
            // Add Autofac

            return services.BuildServiceProvider(false);
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

            //app.ApplicationServices.GetRequiredService<ApplicationDbContext>().Seed();

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

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            DbContextSeedData.Seed(app);
        }
    }
}
