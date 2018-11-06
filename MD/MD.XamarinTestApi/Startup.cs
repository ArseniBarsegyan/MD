﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MD.Data;
using MD.Helpers;
using MD.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MD.XamarinTestApi
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
            string connectionString;
#if DEBUG
            connectionString = Configuration.GetConnectionString(ConstantsHelper.DefaultConnection);
#else
            connectionString = Configuration.GetConnectionString(ConstantsHelper.ReleaseVersionConnection);
#endif
            services.AddCors(options =>
            {
                options.AddPolicy(ConstantsHelper.CorsPolicy,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            var opt = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            services.AddTransient(s => new AppIdentityDbContext(opt.Options, ConstantsHelper.ContextShemaName));

            services.AddTransient<IRepository<Note>, NoteRepository>(ctx =>
            {
                var context = ctx.GetService<AppIdentityDbContext>();
                return new NoteRepository(context);
            });

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppIdentityDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(ConstantsHelper.CorsPolicy);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            dbContext.Database.EnsureCreated();
        }
    }
}