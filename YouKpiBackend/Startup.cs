using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using YouKpiBackend.BusinessLibrary;
using YouKpiBackend.BusinessLibrary.Company;
using YouKpiBackend.BusinessLibrary.Offer;
using YouKpiBackend.DbContexts;

namespace YouKpiBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins().AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                    builder.WithExposedHeaders("content-disposition");
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.Events = new JwtBearerEvents()
                  {
                      OnAuthenticationFailed = async c =>
                      {
                          await Task.Run(() => logger.Debug(c.Exception.Message));
                      }
                  };

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      RoleClaimType = "role",
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });
            //gunzip
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "application/json" });
            });
            // Add automapper
            services.AddAutoMapper(typeof(MappingProfile));
            //DB
            services.AddDbContext<YoukpiContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.CommandTimeout(2700)));


            services.AddScoped<YoukpiContext, YoukpiContext>();
            services.AddScoped<UserLibrary, UserLibrary>();
            services.AddScoped<CompanyLibrary, CompanyLibrary>();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            AddServices(services);
                services.AddControllers();
                services.AddHangfireServer();
                // Add Hangfire services.
                string hangfireConStr = Configuration.GetConnectionString("HangfireConnection");
                services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(hangfireConStr, new SqlServerStorageOptions
                    {
                        SchemaName = "hangfire",
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(30),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));
            
            
        }
        private static void AddServices(IServiceCollection services)
        {
            // dodaje usera do request i pozwala nlogowi go pobrac
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<OfferPdfService, OfferPdfService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            LogManager.Configuration.Variables["conString"] = Configuration.GetConnectionString("DefaultConnection");

            app.UseCors(MyAllowSpecificOrigins);

            // app.UseHttpsRedirection();
            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //this call placement is important
            bool isDashboardReadOnly = Configuration.GetSection("Hangfiredashboard").GetValue<bool>("IsReadOnly");

            if (Debugger.IsAttached)
            {
                isDashboardReadOnly = false;
            }
            var options = new DashboardOptions
            {
                
                IsReadOnlyFunc = (x) => isDashboardReadOnly,
                Authorization = new[] { new CustomAuthorizationFilter() }
            };
            app.UseHangfireDashboard("/hangfiredashboard", options);
            var hangfire = new HangFireJobs();
            BackgroundJob.Enqueue(() => Console.WriteLine("Starting Hangfire!"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
