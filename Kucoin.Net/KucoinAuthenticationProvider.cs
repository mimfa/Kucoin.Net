﻿using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Kucoin.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Kucoin.Net
{
    internal class KucoinAuthenticationProvider : AuthenticationProvider<KucoinApiCredentials>
    {
        public KucoinAuthenticationProvider(KucoinApiCredentials credentials): base(credentials)
        {
            if (credentials.CredentialType != ApiCredentialsType.Hmac)
                throw new Exception("Only Hmac authentication is supported");
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>();

            if (!auth)
                return;

            uri = uri.SetParameters(uriParameters, arraySerialization);
            headers.Add("KC-API-KEY", _credentials.Key!.GetString());
            headers.Add("KC-API-TIMESTAMP", GetMillisecondTimestamp(apiClient).ToString());
            headers.Add("KC-API-PASSPHRASE", SignHMACSHA256(_credentials.PassPhrase.GetString(), SignOutputType.Base64));
            headers.Add("KC-API-KEY-VERSION", "2");

            var jsonContent = parameterPosition == HttpMethodParameterPosition.InBody ? JsonConvert.SerializeObject(bodyParameters) : string.Empty;
            var signData = headers["KC-API-TIMESTAMP"] + method + Uri.UnescapeDataString(uri.PathAndQuery) + jsonContent;
            headers.Add("KC-API-SIGN", SignHMACSHA256(signData, SignOutputType.Base64));
        }
    }
}
