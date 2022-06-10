using CryptoExchange.ACL.CoinMarketCap;
using CryptoExchange.ACL.ExchangeRates;
using Exchange.Common.Config;
using Exchange.Common.Currency;
using Exchange.Common.interfaces;
using Exchange.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ExchangeCurrency.Web
{
    public class Root
    {
        public List<APIConfig> APIConfig { get; set; }
    }
    public class APIConfig
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int MaximumRetry { get; set; }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();
            if (env.IsEnvironment("Development"))
            {
                builder.AddUserSecrets(Assembly.GetExecutingAssembly());
            }
            var NN = env.EnvironmentName;
            Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private APIConfig[] apiConfigs; 

        private APIConfiguration GetAPIConfiguration(string name)
        {
            foreach (var config in apiConfigs)
            {
                if (config.Name.ToLower() == name.ToLower())
                {
                    return new APIConfiguration(Configuration[config.Name+"ApiKey"], config.URL, config.MaximumRetry);
                }
            }
            return new APIConfiguration("","");
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            apiConfigs = Configuration.GetSection("APIConfig").Get<APIConfig[]>();
            var coinMarketConfig = GetAPIConfiguration("CoinMarket");
            services.AddSingleton(coinMarketConfig);

            var exchnageRateConfig = GetAPIConfiguration("ExchangeRates");
            services.AddSingleton(exchnageRateConfig);

            services.AddControllersWithViews();
            services.AddControllers();
            services.AddTransient<CryptoExchangeService>();
            services.AddSingleton<ITraditionalCurrency>(new TraditionalCurrency());
            services.AddSingleton<ICryptoCurrency>(new CryptoCurrency());
            services.AddSingleton<ICryptoToUSD>(new CoinMarketCapAPI(coinMarketConfig));
            services.AddSingleton<IExchangeBaseOnUSD>(new ExchangeRatesAPI(exchnageRateConfig));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{cryptoSymbol?}");
            });
        }
    }
}
