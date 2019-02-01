using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TestWCF3
{
    [DataContract]
    public class Divide
    { 
        int numero;
        int divisor;

        [DataMember(Name = "numero")]
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [DataMember(Name = "divisor")]
        public int Divisor
        {
            get { return divisor; }
            set { divisor = value; }
        }
    }
}