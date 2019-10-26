using System;
using AutoMapper;
using Blog.Models;
using Blog.Repository;
using Blog.Repository.Interface;
using Blog.Repository.Interface.Repositories;
using Blog.Repository.Repositories;
using Blog.Repository.Seeding;
using Blog.Services.Interfaces.Services.Article;
using Blog.Services.Services.Article;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.API
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
            services.AddDbContext<ServiceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ServiceContext>();
            services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ServiceContext>();

            services.AddAuthentication().AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddSignalR();

            services.AddAutoMapper(typeof(Startup));
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IServiceContext, ServiceContext>();
            services.AddScoped<ServiceContext, ServiceContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddCors(o => o.AddPolicy("corsPolicy",
                builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            //#region Seeding Data
            try
            {
                var serviceProvider = services.BuildServiceProvider();
                var dbContext = serviceProvider.GetRequiredService<ServiceContext>();
                if (!dbContext.Articles.Any())
                {
                    var serviceContextSeeding = new ServiceContextSeeding(dbContext);
                    serviceContextSeeding.Seed();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //#endregion
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer("start");
                }
            });
            // global cors policy
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}