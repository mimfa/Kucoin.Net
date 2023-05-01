using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Objects;
using Kucoin.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinRestClient" />
    public class KucoinRestClient : BaseRestClient, IKucoinRestClient
    {
        /// <inheritdoc />
        public IKucoinClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IKucoinClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Create a new instance of KucoinClient using provided options
        /// </summary>
        /// <param name="optionsFunc">The options to use for this client</param>
        public KucoinRestClient(ILogger<KucoinRestClient> logger = null, HttpClient httpClient = null) : this((x) => { }, httpClient, logger)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinClient using provided options
        /// </summary>
        /// <param name="optionsFunc">The options to use for this client</param>
        public KucoinRestClient(Action<KucoinRestOptions> optionsFunc) : this(optionsFunc, null)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinClient using provided options
        /// </summary>
        /// <param name="optionsFunc">The options to use for this client</param>
        public KucoinRestClient(Action<KucoinRestOptions> optionsFunc, HttpClient? httpClient = null, ILogger<KucoinRestClient> logger = null) : base(logger, "Kucoin")
        {
            var options = KucoinRestOptions.Default.Copy();
            optionsFunc(options);
            Initialize(options);

            SpotApi = AddApiClient(new KucoinClientSpotApi(_logger, httpClient, this, options));
            FuturesApi = AddApiClient(new KucoinClientFuturesApi(_logger, httpClient, this, options));
        }

        /// <inheritdoc />
        public void SetApiCredentials(KucoinApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            FuturesApi.SetApiCredentials(credentials);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsFunc">Options to use as default</param>
        public static void SetDefaultOptions(Action<KucoinRestOptions> optionsFunc)
        {
            var options = KucoinRestOptions.Default.Copy();
            optionsFunc(options);
            KucoinRestOptions.Default = options;
        }
    }
}
