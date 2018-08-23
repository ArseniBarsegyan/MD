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
using MyDiary.CoreApi.Helpers;
using MyDiary.Data;

namespace MyDiary.CoreApi
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

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, ApplicationContext>();
            services.AddTransient<IRepository<Note>, NoteRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration[ConstantsHelper.JwtIssuer],
                        ValidAudience = Configuration[ConstantsHelper.JwtIssuer],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[ConstantsHelper.JwtKey])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(ConstantsHelper.CorsPolicy);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            dbContext.Database.EnsureCreated();
        }
    }
}
