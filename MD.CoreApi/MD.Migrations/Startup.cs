using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MD.Migrations.Context;

namespace MD.Migrations
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            string connectionString;
#if DEBUG
            connectionString = Configuration.GetConnectionString("DefaultConnection");
#else
            connectionString = Configuration.GetConnectionString("AzureDatabaseConnection");
#endif

            services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        public void Configure()
        {
        }
    }
}