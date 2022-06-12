using CryptoExchange.ACL.CoinMarketCap;
using CryptoExchange.ACL.ExchangeRates;
using Exchange.Common.Authentication;
using Exchange.Common.Authentication.interfaces;
using Exchange.Common.Config;
using Exchange.Common.Currency;
using Exchange.Common.interfaces;
using Exchange.Service;
using ExchangeCurrency.Web.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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

        
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Custom")
                .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>("Custom", null);
            apiConfigs = Configuration.GetSection("APIConfig").Get<APIConfig[]>();
            var client = Configuration["Client:URL"];

            var coinMarketConfig = GetAPIConfiguration("CoinMarket");
            services.AddSingleton(coinMarketConfig);

            var exchangeRateConfig = GetAPIConfiguration("ExchangeRates");
            services.AddSingleton(exchangeRateConfig);

            services.AddControllersWithViews();
            services.AddControllers();

            services.AddSingleton<IAuthentication>(new ClientConfiguration(client, Configuration["ClientApiKey"]));
            services.AddTransient<ICryptoExchangeService,CryptoExchangeService>();
            services.AddSingleton<IFiatCurrency, FiatCurrency>();
            services.AddSingleton<ICryptoCurrency, CryptoCurrency>();
            services.AddSingleton<ICryptoToUSD>(new CoinMarketCapAPI(coinMarketConfig));
            services.AddSingleton<IExchangeBaseOnUSD>(new ExchangeRatesAPI(exchangeRateConfig));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Exchange Service API",
                    Version = "v1",
                    Description = "Crypto Exchange Service, Need ApiKey to use."
                });
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{cryptoSymbol?}");
            });
            
        }
        private IThirdPartyConfiguration GetAPIConfiguration(string name)
        {
            foreach (var config in apiConfigs)
            {
                if (config.Name.ToLower() == name.ToLower())
                {
                    return new ThirdPartyConfiguration(config.URL, Configuration[config.Name + "ApiKey"], config.MaximumRetry);
                }
            }
            return new ThirdPartyConfiguration("", "");
        }
    }
}
