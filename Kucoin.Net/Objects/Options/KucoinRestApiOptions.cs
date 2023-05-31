using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    /// <summary>
    /// Options for the Kucoin rest API
    /// </summary>
    public class KucoinRestApiOptions : RestApiOptions
    {
        /// <inheritdoc />
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }
    }
}
