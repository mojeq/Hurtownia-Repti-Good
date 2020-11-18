using AutoMapper;
using EFCoreSecondLevelCacheInterceptor;
using Hangfire;
using FluentValidation.AspNetCore;
using HurtowniaReptiGood.Models;
using HurtowniaReptiGood.Models.Interfaces.Repositories;
using HurtowniaReptiGood.Models.Repositories;
using HurtowniaReptiGood.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using HurtowniaReptiGood.Models.Interfaces;

namespace HurtowniaReptiGood
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });

            services.AddEFSecondLevelCache(options =>
            {
                options.UseMemoryCacheProvider(EFCoreSecondLevelCacheInterceptor.CacheExpirationMode.Absolute, TimeSpan.FromMinutes(30))
                    .DisableLogging(false)
                    .CacheAllQueries(EFCoreSecondLevelCacheInterceptor.CacheExpirationMode.Absolute, TimeSpan.FromMinutes(30));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MyContext>()
                .AddDefaultTokenProviders()

                .AddRoleManager<RoleManager<IdentityRole>>();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Login";
            });

            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<ICustomerAccountService, CustomerAccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDpdService, DpdService>();
            services.AddScoped<ISubiektAPIService, SubiektAPIService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMailRepository, MailRepository>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped(typeof(IAppService), typeof(AppService));
            services.AddScoped<ICartService, CartService>();
            services.AddScoped(typeof(ICartService), typeof(CartService));

            services.AddAutoMapper((typeof(Startup)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");

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

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=CustomerAccount}/{action=OrdersHistory}/{id?}");
            });

            RecurringJob.AddOrUpdate<SubiektAPIService>((x => x.DownloadAndUpdateProductsStockFromSubiektGT()), Cron.Hourly);
        }
    }
}
