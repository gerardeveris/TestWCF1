//// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
////
////    using TestWCF1.Messages.Test1;
////
////    var test1 = Test1.FromJson(jsonString);

//namespace TestWCF1.Messages.Test1
//{
//    using System;
//    using System.Collections.Generic;

//    using System.Globalization;
//    using Newtonsoft.Json;
//    using Newtonsoft.Json.Converters;

//    public partial class Test1
//    {
//        [JsonProperty("email", Required = Required.Always)]
//        public String Email { get; set; }

//        [JsonProperty("id_zona_hogar", Required = Required.Always)]
//        public String IdZonaHogar { get; set; }
//    }

//    public partial class Test1
//    {
//        public static Test1 FromJson(string json) => JsonConvert.DeserializeObject<Test1>(json, TestWCF1.Messages.Test1.Converter.Settings);
//    }

//    public static class Serialize
//    {
//        public static string ToJson(this Test1 self) => JsonConvert.SerializeObject(self, TestWCF1.Messages.Test1.Converter.Settings);
//    }

//    internal static class Converter
//    {
//        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//        {
//            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//            DateParseHandling = DateParseHandling.None,
//            Converters = {
//                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//            },
//        };
//    }
//}
