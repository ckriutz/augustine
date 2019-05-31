using Augustine.Senators.WebApi.Contexts;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Augustine.Senators.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            ConfigureAuthService(services, Configuration.GetValue<string>("IdentityUrl"));

            services.AddDbContext<SenatorsContext>(options => options.UseInMemoryDatabase("SenatorsDatabase"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Senators API", Version = "v1" });
                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{Configuration.GetValue<string>("IdentityUrl")}connect/authorize",
                    TokenUrl = $"{Configuration.GetValue<string>("IdentityUrl")}connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "senatorsapi", "Senators API" }
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();

                //c.AddSecurityRequirement(
                //    new Dictionary<string,
                //    IEnumerable<string>>
                //    {
                //        { "Bearer", Enumerable.Empty<string>() },
                //    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            // NOTE: The following Cors statement opens everything up fairly wide.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Senators API V1");
                c.RoutePrefix = string.Empty;
                c.OAuthClientId("augustinesenatorwebapiswaggerui");
                c.OAuthAppName("Senators Swagger UI");
            });

            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "CatchAll", action = "Index"});
            });

        }

        private void ConfigureAuthService(IServiceCollection services, string identityurl)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddIdentityServerAuthentication(options =>
            {
                options.Authority = identityurl;
                options.RequireHttpsMetadata = true;
                options.ApiName = "senatorsapi";
            });                
        }
    }
}
