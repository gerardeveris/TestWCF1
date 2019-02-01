// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using TestWCF1.Messages.Return_APlata;
//
//    var returnAPlata = ReturnAPlata.FromJson(jsonString);

namespace TestWCF1.Messages.Return_APlata
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ReturnAPlata
    {
        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("preautorizado_cliente")]
        public decimal PreautorizadoCliente { get; set; }

        [JsonProperty("preautorizado_hogar")]
        public decimal PreautorizadoHogar { get; set; }

        [JsonProperty("test")]
        public String Test { get; set; }
    }

    public partial class ReturnAPlata
    {
        public static ReturnAPlata FromJson(string json) => JsonConvert.DeserializeObject<ReturnAPlata>(json, TestWCF1.Messages.Return_APlata.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ReturnAPlata self) => JsonConvert.SerializeObject(self, TestWCF1.Messages.Return_APlata.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
