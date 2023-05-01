using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Kucoin.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using System.Threading;
using Kucoin.Net.Objects.Internal;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Interfaces.Clients.SpotApi;
using Kucoin.Net.Interfaces.Clients.FuturesApi;
using Kucoin.Net.Clients.SpotApi;
using Kucoin.Net.Clients.FuturesApi;
using Kucoin.Net.Objects.Options;

namespace Kucoin.Net.Clients
{
    /// <inheritdoc cref="IKucoinSocketClient" />
    public class KucoinSocketClient : BaseSocketClient, IKucoinSocketClient
    {
        #region Api clients

        /// <inheritdoc />
        public IKucoinSocketClientSpotStreams SpotStreams { get; }
        /// <inheritdoc />
        public IKucoinSocketClientFuturesStreams FuturesStreams { get; }

        #endregion

        public KucoinSocketClient(ILogger<KucoinSocketClient>? logger = null)  : this ((x) => { }, logger)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient
        /// </summary>
        /// <param name="optionsFunc">Configure the options to use for this client</param>
        public KucoinSocketClient(Action<KucoinSocketOptions> optionsFunc) : this(optionsFunc, null)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient
        /// </summary>
        /// <param name="optionsFunc">Configure the options to use for this client</param>
        public KucoinSocketClient(Action<KucoinSocketOptions> optionsFunc, ILogger<KucoinSocketClient>? logger = null) : base(logger, "Kucoin")
        {
            var options = KucoinSocketOptions.Default.Copy();
            optionsFunc(options);
            Initialize(options);

            SpotStreams = AddApiClient(new KucoinSocketClientSpotStreams(_logger, this, options));
            FuturesStreams = AddApiClient(new KucoinSocketClientFuturesStreams(_logger, this, options));
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsFunc">Configure the options to use as default</param>
        public static void SetDefaultOptions(Action<KucoinSocketOptions> optionsFunc)
        {
            var options = KucoinSocketOptions.Default.Copy();
            optionsFunc(options);
            KucoinSocketOptions.Default = options;
        }

        /// <summary>
        /// Set the API credentials to use in this client
        /// </summary>
        /// <param name="credentials">Credentials to use</param>
        public void SetApiCredentials(KucoinApiCredentials credentials)
        {
            SpotStreams.SetApiCredentials(credentials);
            FuturesStreams.SetApiCredentials(credentials);
        }
    }
}
