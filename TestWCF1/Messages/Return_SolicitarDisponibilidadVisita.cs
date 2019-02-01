// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var testVisita = TestVisita.FromJson(jsonString);

namespace TestWCF1.Messages
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Return_SolicitarDisponibilidadVisita
    {
        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("fechas_Ocupadas")]
        public FechasOcupadas[] FechasOcupadas { get; set; }
    }

    public partial class FechasOcupadas
    {
        [JsonProperty("hora_inicio")]
        public String HoraInicio { get; set; }

        [JsonProperty("hora_fin")]
        public String HoraFin { get; set; }

        public FechasOcupadas(DateTime fechaInicio)
        {
            HoraInicio = fechaInicio.ToLocalTime().ToString("dd/MM/yyyy HH:mm"); ;
            HoraFin = fechaInicio.ToLocalTime().AddMinutes(30).ToString("dd/MM/yyyy HH:mm"); ;
        }
    }

    public partial class Return_SolicitarDisponibilidadVisita
    {
        public static Return_SolicitarDisponibilidadVisita FromJson(string json) => JsonConvert.DeserializeObject<Return_SolicitarDisponibilidadVisita>(json, TestWCF1.Messages.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Return_SolicitarDisponibilidadVisita self) => JsonConvert.SerializeObject(self, TestWCF1.Messages.Converter.Settings);
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
