using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Kucoin socket client options
    /// </summary>
    public class KucoinSocketOptions : SocketExchangeOptions<KucoinEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static KucoinSocketOptions Default { get; set; } = new KucoinSocketOptions()
        {
            Environment = KucoinEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <inheritdoc />
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        /// <summary>
        /// Spot API options
        /// </summary>
        public KucoinSocketApiOptions SpotOptions { get; private set; } = new KucoinSocketApiOptions()
        {
            MaxSocketConnections = 50
        };

        /// <summary>
        /// Futures API options
        /// </summary>
        public KucoinSocketApiOptions FuturesOptions { get; private set; } = new KucoinSocketApiOptions()
        {
            MaxSocketConnections = 50
        };

        internal KucoinSocketOptions Copy()
        {
            var options = Copy<KucoinSocketOptions>();
            options.SpotOptions = SpotOptions.Copy<KucoinSocketApiOptions>();
            options.FuturesOptions = FuturesOptions.Copy<KucoinSocketApiOptions>();
            return options;
        }
    }
}
