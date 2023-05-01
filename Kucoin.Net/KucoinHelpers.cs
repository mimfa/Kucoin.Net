using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace Kucoin.Net
{
    /// <summary>
    /// Helpers for Kucoin
    /// </summary>
    public static class KucoinHelpers
    {
        /// <summary>
        /// Add the IKucoinClient and IKucoinSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsCallback">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsCallback">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IKucoinSocketClient for the service collection. Defaults to Scoped.</param>
        /// <returns></returns>
        public static IServiceCollection AddKucoin(
            this IServiceCollection services, 
            Action<KucoinRestOptions>? defaultRestOptionsCallback = null,
            Action<KucoinSocketOptions>? defaultSocketOptionsCallback = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            if (defaultRestOptionsCallback != null)
                KucoinRestClient.SetDefaultOptions(defaultRestOptionsCallback);

            if (defaultSocketOptionsCallback != null)
                KucoinSocketClient.SetDefaultOptions(defaultSocketOptionsCallback);
            
            services.AddHttpClient()
            services.AddTransient<IKucoinRestClient, KucoinRestClient>();
            if (socketClientLifeTime == null)
                services.AddSingleton<IKucoinSocketClient, KucoinSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IKucoinSocketClient), typeof(KucoinSocketClient), socketClientLifeTime.Value));
            return services;
        }

        /// <summary>
        /// Validate the string is a valid Kucoin symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static void ValidateKucoinSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");

            if (!Regex.IsMatch(symbolString, "^((([A-Z]|[0-9]){1,})[-](([A-Z]|[0-9]){1,}))$"))
                throw new ArgumentException($"{symbolString} is not a valid Kucoin symbol. Should be [BaseAsset]-[QuoteAsset] e.g. ETH-BTC");
        }
    }
}
