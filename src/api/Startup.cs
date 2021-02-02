using blog_api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace blog_api
{
    /// <summary>
    /// Startup class of the ASP.NET Core application.
    /// </summary>
    public class Startup
    {
        private const string _roleClaimType = "role";

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
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtOptions = Configuration.GetSection("JwtBearer").Get<JwtBearerOptions>();

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments("blog-api.xml");
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "blog_api", Version = "v1" });
            });

            services.AddDbContext<BlogContext>(options =>
            {
                options.UseSqlite(
                    Configuration.GetConnectionString("Data"),
                    builder => builder.MigrationsAssembly(this.GetType().Assembly.FullName));
            });

            services.AddCors(options => options.AddDefaultPolicy(b =>
            {
                b.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = jwtOptions.Authority;
                    options.Audience = jwtOptions.Audience;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.NameClaimType = "preferred_username";
                    options.TokenValidationParameters.RoleClaimType = _roleClaimType;
                });

            services.AddTransient<IClaimsTransformation>(_ => new KeycloakRolesClaimsTransformation(_roleClaimType, jwtOptions.Audience));

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.CanUnpublish, policy => policy.RequiresKeycloakEntitlement("Posts", "Unpublish"));
            }).AddKeycloakAuthorization(options =>
            {
                options.Audience = jwtOptions.Audience;
                options.TokenEndpoint = $"{jwtOptions.Authority}/protocol/openid-connect/token";
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "blog_api v1"));
        }
    }
}
