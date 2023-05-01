using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net
{
    /// <summary>
    /// Rest environ
    /// </summary>
    public static class KucoinEnvironments
    {
        /// <summary>
        /// Live environment
        /// </summary>
        public static TradeEnvironment Live { get; } 
            = new TradeEnvironment(
                TradeEnvironmentNames.Live, new Dictionary<string, string>() {
                    { "Rest", KucoinApiAddresses.Default.SpotAddress },
                    { "Socket", KucoinApiAddresses.Default.SpotAddress },
                });

        /// <summary>
        /// Testnet/sandbox environment
        /// </summary>
        public static TradeEnvironment Testnet { get; }
            = new TradeEnvironment(
                    TradeEnvironmentNames.Live, new Dictionary<string, string>() {
                        { "Rest", KucoinApiAddresses.TestNet.SpotAddress },
                        { "Socket", KucoinApiAddresses.TestNet.SpotAddress },
                    });
    }
}
