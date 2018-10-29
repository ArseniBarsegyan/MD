using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MD.CoreApi.Helpers;
using MD.Data;
using MD.Identity;
using Microsoft.AspNetCore.Http;

namespace MD.CoreApi
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

            services.AddTransient<IRepository<Note>, NoteRepository>((ctx) =>
            {
                var context = ctx.GetService<AppIdentityDbContext>();
                return new NoteRepository(context);
            });

            services.AddMvcCore().AddAuthorization().AddJsonFormatters();

            services.AddAuthentication(ConstantsHelper.AuthenticationType) // it is a Bearer token
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = ConstantsHelper.IdentityServerUrl; //Identity Server URL
                    options.RequireHttpsMetadata = false; // make it false since we are not using https
                    options.ApiName = ConstantsHelper.ApiName; //api name which should be registered in IdentityServer
                });

            services.AddMvc();

            //services.AddIdentity<AppUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AppIdentityDbContext>()
            //    .AddDefaultTokenProviders();

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            //services
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(cfg =>
            //    {
            //        cfg.RequireHttpsMetadata = false;
            //        cfg.SaveToken = true;
            //        cfg.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidIssuer = Configuration[ConstantsHelper.JwtIssuer],
            //            ValidAudience = Configuration[ConstantsHelper.JwtIssuer],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[ConstantsHelper.JwtKey])),
            //            ClockSkew = TimeSpan.Zero // remove delay of token when expire
            //        };
            //    });
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseCors(ConstantsHelper.CorsPolicy);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            dbContext.Database.EnsureCreated();
        }
    }
}
