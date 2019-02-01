using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestWCF1.Messages.old
{
    [DataContract]
    public class SolicitudVisita
    {
        [DataMember]
        public string id_inmueble { get; set; }
    }
}