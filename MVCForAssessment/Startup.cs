using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MVCForAssessment.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCForAssessment.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http;
using MVCForAssessment.Middlewares;

namespace MVCForAssessment
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
                
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSingleton<IDependencyDemo, DependencyDemo>();
            //services.AddHttpContextAccessor();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "localhost";
            //    options.InstanceName = "SampleInstance";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("this is First Middle ware....!");
            //    await next();
            //});

            //app.Use(async (conetxt, next) =>
            //{
            //    conetxt.Response.Redirect("https://www.w3schools.com/");
            //    await next();
            //});

            //app.Use(async (conetxt, next) =>
            //{
            //    conetxt.Response.Redirect("https://www.tutorialsteacher.com/mvc/asp.net-mvc-tutorials");
            //    await next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Iam From Run Middle Ware");
            //});

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();
            //app.UseMyCustomMiddleware();

           
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
              
                endpoints.MapGet("/Hello",async context =>

                 {
                     context.Response.Redirect("https://www.w3schools.com/");

                 });

                endpoints.MapControllerRoute(
                    name: "ConstraintTest",
                    pattern: "/{string :minlength(4)}/{id:int}/{username:minlength(4)}",
                    defaults: new { controller = "Employees", action = "Index" }
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                
                
                endpoints.MapControllerRoute(
                   name: "Privacy",
                   pattern: "/Privacy",
                   defaults: new { controller = "Home", action = "Privacy" }
                   );
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
