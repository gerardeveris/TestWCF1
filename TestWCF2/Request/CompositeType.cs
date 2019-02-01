using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TestWCF2.Request
{
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember(Name ="bool_value")]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember(Name = "string_value")]
        [Required(ErrorMessage = "Requerido")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "TEST LONG GERARD")]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}