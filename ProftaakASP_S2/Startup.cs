﻿using Data.Contexts;
using Data.Interfaces;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProftaakASP_S2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddSingleton<IAppointmentContext, AppointmentContextSql>();
            services.AddSingleton<ICategoryContext, CategoryContextSql>();
            services.AddSingleton<IChatContext, ChatContextSql>();
            services.AddSingleton<IQuestionContext, QuestionContextSql>();
            services.AddSingleton<IReactionContext, ReactionContextSql>();
            services.AddSingleton<IUserContext, UserContextSql>();
            services.AddSingleton<ILogContext, LogContextSql>();


            services.AddSingleton<UserLogic>();
            services.AddSingleton<QuestionLogic>();
            services.AddSingleton<ReactionLogic>();
            services.AddSingleton<CategoryLogic>();
            services.AddSingleton<ChatLogic>();
            services.AddSingleton<AppointmentLogic>();
            services.AddSingleton<LogLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=Login}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Volunteer",
                    template: "{controller=Volunteer}/{action=Index}/{id?}");
            });
        }
    }
}
