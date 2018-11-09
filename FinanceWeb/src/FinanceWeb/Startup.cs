using System.Threading;
using System.Threading.Tasks;
using FinanceWeb.Controllers;
using FinanceWeb.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FinanceWeb.Models;
using FinanceWeb.Services;
using Microsoft.AspNetCore.Identity.MongoDB;

namespace FinanceWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register identity framework services and also Mongo storage. 
            services.AddIdentityWithMongoStores(Configuration.GetConnectionString("DefaultConnection"))
                .AddDefaultTokenProviders();

            services.AddApplicationInsightsTelemetry(Configuration);

            //services.AddIdentity<IdentityUser, IdentityRole>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //}).AddEntityFrameworkStores<DataContext>()
            //    .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ad", policy => policy.RequireRole("ad"));
            });

            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            // Add application services.
            services.AddTransient<IStockSymbolRepository, StockSymbolRepository>();
            services.AddTransient<IStockSeasonalityDataRepository, StockSeasonalityDataRepository>();
            services.AddTransient<IStockDataRepository, StockDataRepository>();
            services.AddTransient<IStockScoreDataRepository, StockScoreDataRepository>();
            services.AddTransient<IRealDataRepository, RealDataRepository>();
            services.AddTransient<IPastDataRepository, PastDataRepository>();
            services.AddTransient<IFutureDataRepository, FutureDataRepository>();
            services.AddTransient<IAnnualDataRepository, AnnualDataRepository>();
            services.AddTransient<IStockLastDataRepository, StockLastDataRepository>();
            services.AddTransient<IExecutionRepository, ExecutionRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
