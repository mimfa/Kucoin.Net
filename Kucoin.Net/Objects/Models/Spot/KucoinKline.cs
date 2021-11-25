﻿using System;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Kline info
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class KucoinKline: ICommonKline
    {
        /// <summary>
        /// The start time of the kline
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The open price
        /// </summary>
        [ArrayProperty(1)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// The close price
        /// </summary>
        [ArrayProperty(2)]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// The highest price during this kline
        /// </summary>
        [ArrayProperty(3)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price during this kline
        /// </summary>
        [ArrayProperty(4)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The volume of the kline
        /// </summary>
        [ArrayProperty(5)]
        public decimal Volume { get; set; }
        /// <summary>
        /// The volume of the kline
        /// </summary>
        [ArrayProperty(6)]
        public decimal QuoteVolume { get; set; }

        decimal ICommonKline.CommonHighPrice => HighPrice;
        decimal ICommonKline.CommonLowPrice => LowPrice;
        decimal ICommonKline.CommonOpenPrice => OpenPrice;
        decimal ICommonKline.CommonClosePrice => ClosePrice;
        decimal ICommonKline.CommonVolume => Volume;
        DateTime ICommonKline.CommonOpenTime => OpenTime;
    }
}
