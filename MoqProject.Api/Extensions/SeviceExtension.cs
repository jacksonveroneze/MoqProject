using System;
using System.Net.Http;
using System.Security.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoqProject.Api.CrossCuting;
using MoqProject.Api.Repositories;
using MoqProject.Api.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Refit;

namespace MoqProject.Api.Extensions
{
    public static class SeviceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(5, (attempt) => TimeSpan.FromSeconds(2), (outcome, timespan, retryCount, context) =>
                    Console.WriteLine($"Tentando pela {retryCount} vez!")
                );

            services.AddRefitClient<ICepRequest>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["UrlViaCep"]))
                .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
                    SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12
                })
                .AddPolicyHandler(retryPolicy);

            services.AddScoped<ICepService, CepService>();
            services.AddScoped<ICepRepository, CepRepository>();
            
            return services;
        } 
    }
}