using CryptoExchange.ACL.CoinMarketCap;
using CryptoExchange.ACL.ExchangeRates;
using Exchange.Common.Currency;
using Exchange.Common.interfaces;
using Exchange.Service;
using ExchangeCurrency.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace ExchangeCurrency.Specs.Hooks
{
    public class TestServerFixture : IDisposable
    {
        public TestServer server { get; }

        public HttpClient client { get; }

        public TestServerFixture()
        {
            // Arrange
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TestStartup>();
            // anything else you might need?....

            server = new TestServer(builder);

            client = server.CreateClient();
        }

        public void Dispose()
        {
            server.Dispose();
            client.Dispose();
        }
    }
    public sealed class SelfHostedApi : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder =>
            builder.UseStartup<Startup>());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });
        }
        
    }
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to addservices to the container.
        public void ConfigureService(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            services.AddControllers();
            services.AddScoped<CryptoExchangeService>();
            services.AddScoped<ITraditionalCurrency, TraditionalCurrency>();
            services.AddScoped<ICryptoCurrency, CryptoCurrency>();
            services.AddScoped<ICryptoToUSD, CoinMarketCapAPI>();
            services.AddScoped<IExchangeBaseOnUSD, ExchangeRatesAPI>();
            // *** Do not add custom services here
        }
        // This method gets called by the runtime. Use this method to configurethe HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
