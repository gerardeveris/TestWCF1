namespace TestWCF1.Messages.APlata
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class APlata
    {
        [JsonProperty("id_zona_hogar")]
        public string id_zona_hogar { get; set; }

        [JsonProperty("id_cliente")]
        public string id_cliente { get; set; }

        [JsonProperty("validacion_selfie")]
        public decimal validacion_selfie { get; set; }

        [JsonProperty("nombre")]
        public string nombre { get; set; }

        [JsonProperty("apellidos")]
        public string apellidos { get; set; }

        [JsonProperty("telefono")]
        public string telefono { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("fecha_nacimiento")]
        public string fecha_nacimiento { get; set; }

        [JsonProperty("nacionalidad")]
        public string nacionalidad { get; set; }

        [JsonProperty("dni")]
        public string dni { get; set; }

        [JsonProperty("tipo_documento")]
        public int tipo_documento { get; set; }

        [JsonProperty("direccion")]
        public string direccion { get; set; }

        [JsonProperty("genero")]
        public bool genero { get; set; }

        [JsonProperty("estado_civil")]
        public string estado_civil { get; set; }

        [JsonProperty("responsabilidad_publica")]
        public bool responsabilidad_publica { get; set; }

        [JsonProperty("relacion_responsabilidad")]
        public bool relacion_responsabilidad { get; set; }

        [JsonProperty("profesion")]
        public string profesion { get; set; }

        [JsonProperty("situacion_laboral")]
        public string situacion_laboral { get; set; }

        [JsonProperty("destino_operacion")]
        public int destino_operacion { get; set; }

        [JsonProperty("gastos_mensuales")]
        public decimal gastos_mensuales { get; set; }

        [JsonProperty("ingresos_mensuales")]
        public decimal ingresos_mensuales { get; set; }

        [JsonProperty("num_personas")]
        public int num_personas { get; set; }
        [JsonProperty("pais_nacimiento")]
        public string pais_nacimiento { get; set; }

        [JsonProperty("pais_residencia")]
        public string pais_residencia { get; set; }


    }
    /*
    public partial class APlata
    {
        public static APlata FromJson(string json) => JsonConvert.DeserializeObject<APlata>(json, TestWCF1.Messages.APlata.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this APlata self) => JsonConvert.SerializeObject(self, TestWCF1.Messages.APlata.Converter.Settings);
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
    */
}