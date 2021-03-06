﻿// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using TestWCF1.Messages.Test1;
//
//    var test1 = Test1.FromJson(jsonString);
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace TestWCF1.Messages.Test1
{
    [DataContract]
    public class Test1
    {
        public Test1(Exception ex)
        {
            String message = ex.Message;
            //InnerException = new ExceptionDetail(ex);
        }

        //private int poId_value;

        //[DataMember]
        //public int PurchaseOrderId
        //{
        //    get { return poId_value; }
        //    set { poId_value = value; }
        //}

        private string cError;
        [DataMember(Name = "error", EmitDefaultValue = true)]
        public string Error
        {
            get { return cError; }
            set { cError = value; }
        }

        [DataMember(Name = "cTest1", Order = 2, EmitDefaultValue = true)]
        public int? CTest1 = 1;

        private int cTest2 = 0;
        [DataMember(Name = "cTest2", Order = 1)]   
        public int CTest2
        {
            get
            {
                return this.cTest2;
            }
            set
            {
                //this.cTest2 = value;
                if (value.ToString().Length < 3) this.cTest2 = value;
                else
                {
                    this.cTest2 = 999;
                    cError = "Error de lenght";
                }
            }
        }
        
        [DataMember(Name = "cTest3", Order = 0, EmitDefaultValue = false)]
        public dynamic CTest3 { get; set; }

        private dynamic cGuid;
        [DataMember(Name = "cGuid", Order = 3)]
        [StringLength(36, ErrorMessage = "Maximum length is 20")]
        public dynamic CGuid
        {
            get { return cGuid; }
            set
            {
                try
                {
                    Guid test = new Guid(value);
                    cGuid = value;
                }
                catch
                {
                    cError = "El campo no es de tipo Guid";
                }
            }
        }
    }
}
