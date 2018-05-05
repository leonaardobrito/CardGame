﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vue.Domain.Multiplayer;

namespace Vue
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
            services.AddMvc();
            services.AddSignalR();

            services.AddSingleton<IGameLobbyHub, GameLobbyHub>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Setup WebpackDevMidleware for "Hot module replacement" while debugging
                var options = new WebpackDevMiddlewareOptions() { HotModuleReplacement = true };
                app.UseWebpackDevMiddleware(options);
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSignalR(routes => 
            {
                routes.MapHub<GameLobbyHub>("/gamelobby");
            });

            app.UseMvc(routes =>
            {
                /* 
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");*/

                // Setup additional routing for SPA
                routes.MapSpaFallbackRoute(
                name: "spa-fallback",
                defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
