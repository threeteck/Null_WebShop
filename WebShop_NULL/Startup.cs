using System;
using DomainModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebShop_NULL.Infrastructure.Filters;
using WebShop_FSharp;
using WebShop_FSharp.Middleware;

namespace WebShop_NULL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFilters();
            //services.AddControllersFromAssembly("WebShop_FSharp"); //Uncomment when/if we will actually have F# controllers
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(option =>
                option.UseNpgsql(connectionString, b => b.MigrationsAssembly("WebShop_NULL")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Register");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddSingleton<CommandService>();
            services.AddAuthorization();
            services.AddScoped<AuthenticationService>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailService>();
            var settings = Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(settings);
            services.AddSingleton(serviceProvider =>
                new EmailConfirmationService(TimeSpan.FromMinutes(5), serviceProvider.GetService<CommandService>())
            );
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddSingleton<IAddressValidator, AddressValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMiddleware<CitiesMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "AdminPanel",
                    pattern: "{controller=AdminPanel}/{action=Products}/{id?}");
            });
        }
    }
}