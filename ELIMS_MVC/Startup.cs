using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using ELIMS_MVC.Authorization;
using ELIMS_MVC.Services;
using ELIMS_MVC.Data;

namespace ELIMS_MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration
            //, IHostingEnvironment env
            )
        {
            Configuration = configuration;
            //Environment = env;
        }

        public IConfiguration Configuration { get; }
        //public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Cookies
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Set database connection from appsettings.json
            services.AddDbContext<ELIMS_MVCContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ELIMS_MVCContext")));

            // DO NOT CHANGE BACK TO .AddDefaultIdentity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ELIMS_MVCContext>()
                .AddDefaultTokenProviders();


            //    services.Configure<IdentityOptions>(options =>
            //    {
            //        // Password settings
            //        options.Password.RequireDigit = true;
            //        options.Password.RequireLowercase = true;
            //        options.Password.RequireNonAlphanumeric = false;
            //        options.Password.RequireUppercase = true;
            //        options.Password.RequiredLength = 8;
            //        options.Password.RequiredUniqueChars = 1;

            //        // Lockout settings
            //        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //        options.Lockout.MaxFailedAccessAttempts = 5;

            //        // User settings
            //        options.User.AllowedUserNameCharacters =
            //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //        options.User.RequireUniqueEmail = true;
            //    });


            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);

                options.LoginPath = "/Identity/Account/Login";

                options.AccessDeniedPath = "/Identity/Account/AcessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddMvc(config =>
            {
                // using Microsoft.AspNetCore.Mvc.Authorization and AspNetCore.Authorization
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Authorization handlers
            services.AddScoped<IAuthorizationHandler, UserIsOwnerAuthorizationHandler>();
            services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();

            // User role authorization handlers
            services.AddSingleton<IAuthorizationHandler, ELIMSAdministrationAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ELIMSManagerAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler, ELIMSUserAuthorizationHandler>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ELIMS_MVCContext dbContext)
        {
            //dbContext.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            //app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
