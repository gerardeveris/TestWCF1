// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using TestWCF1.Messages.Test2;
//
//    var test2 = Test2.FromJson(jsonString);

namespace TestWCF1.Messages.Test2
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Test2
    {
        [JsonProperty("cTest1", Required = Required.Always)]
        public string CTest1 { get; set; }

        [JsonProperty("cTest2", Required = Required.Always)]
        public Guid CTest2 { get; set; }

        [JsonProperty("cTest3", Required = Required.Always)]
        public long CTest3 { get; set; }
    }

    public partial class Test2
    {
        public static Test2 FromJson(string json) => JsonConvert.DeserializeObject<Test2>(json, TestWCF1.Messages.Test2.Converter.Settings);
    }
    

    public static class Serialize
    {
        public static string ToJson(this Test2 self) => JsonConvert.SerializeObject(self, TestWCF1.Messages.Test2.Converter.Settings);
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
