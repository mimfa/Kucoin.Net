using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Options;
using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin Rest options
    /// </summary>
    public class KucoinRestOptions : ExchangeOptions
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static KucoinRestOptions Default { get; set; } = new KucoinRestOptions();

        /// <inheritdoc />
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        /// <summary>
        /// Spot API options
        /// </summary>
        public KucoinHttpApiOptions SpotOptions { get; private set; } = new KucoinHttpApiOptions()
        {
            TradeEnvironment = KucoinEnvironments.Live,
            RateLimiters = new List<IRateLimiter>
            {
                     new RateLimiter()
                        .AddPartialEndpointLimit("/api/v1/orders", 180, TimeSpan.FromSeconds(3), null, true, true)
                        .AddApiKeyLimit(200, TimeSpan.FromSeconds(10), true, true)
                        .AddTotalRateLimit(100, TimeSpan.FromSeconds(10))
            }
        };

        /// <summary>
        /// Futures API options
        /// </summary>
        public KucoinHttpApiOptions FuturesOptions { get; private set; } = new KucoinHttpApiOptions()
        {
            TradeEnvironment = KucoinEnvironments.Live
        };

        internal KucoinRestOptions Copy()
        {
            var options = Copy<KucoinRestOptions>();
            options.SpotOptions = SpotOptions.Copy<KucoinHttpApiOptions>();
            options.FuturesOptions = FuturesOptions.Copy<KucoinHttpApiOptions>();
            return options;
        }
    }
}
