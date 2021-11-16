﻿using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public class KucoinTransfer
    {
        /// <summary>
        /// Apply id
        /// </summary>
        public string ApplyId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Status of the transfer
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// Quantity of the transfer
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Reason if failed
        /// </summary>
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// Offset
        /// </summary>
        public long Offset { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("createdAt")]
        public DateTime CreateTime { get; set; }
    }
}