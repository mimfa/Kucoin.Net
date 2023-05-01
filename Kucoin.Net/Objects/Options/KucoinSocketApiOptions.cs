using CryptoExchange.Net.Objects.Options;

namespace Kucoin.Net.Objects.Options
{
    public class KucoinSocketApiOptions : SocketApiOptions
    {
        public new KucoinApiCredentials? ApiCredentials
        {
            get => (KucoinApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }
    }
}
