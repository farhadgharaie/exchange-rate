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
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets(Assembly.GetExecutingAssembly());
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        private APIConfig[] apiConfigs; 

        private ThirdPartAPIConfig GetAPIConfiguration(string name)
        {
            foreach (var config in apiConfigs)
            {
                if (config.Name.ToLower() == name.ToLower())
                {
                    return new ThirdPartAPIConfig(Configuration[config.Name+"ApiKey"], config.URL, config.MaximumRetry);
                }
            }
            return new ThirdPartAPIConfig("","");
        }
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
