// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using TestWCF1.Messages.SolicitudVisita;
//
//    var solicitudVisita = SolicitudVisita.FromJson(jsonString);

namespace TestWCF1.Messages.SolicitudVisita
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SolicitudVisita
    {
        [JsonProperty("id_inmueble")]
        public string IdInmueble { get; set; }
    }

    public partial class SolicitudVisita
    {
        public static SolicitudVisita FromJson(string json) => JsonConvert.DeserializeObject<SolicitudVisita>(json, TestWCF1.Messages.SolicitudVisita.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SolicitudVisita self) => JsonConvert.SerializeObject(self, TestWCF1.Messages.SolicitudVisita.Converter.Settings);
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
