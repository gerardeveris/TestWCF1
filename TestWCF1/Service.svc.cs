using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TestWCF1.Messages;
using TestWCF1.Messages.SolicitudVisita;
using TestWCF1.Messages.APlata;
using TestWCF1.Messages.Return_APlata;
using System.Web.Script.Serialization;
using System.Web;
using Newtonsoft.Json.Linq;
using System.ServiceModel.Web;
using System.Runtime.Serialization.Json;
using TestWCF1.Messages.Test1;
using TestWCF1.Messages.Test2;
using DevTrends.WCFDataAnnotations;

namespace TestWCF1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service.svc o Service.svc.cs en el Explorador de soluciones e inicie la depuración.
    [ValidateDataAnnotationsBehavior]
    public class Service : IService
    {
        CrmDataController CrmDataController { get; set; }

        public Service()
        {
            try
            {
                var serverUrl = ConfigurationManager.AppSettings["organization"].ToString();
                var username = ConfigurationManager.AppSettings["username"].ToString();
                var password = ConfigurationManager.AppSettings["password"].ToString();
                this.CrmDataController = new CrmDataController(serverUrl, username, password);
            }
            catch
            {

            }
        }

        public String SolicitarDisponibilidadVisita(SolicitudVisita solicitud)
        {
            Return_SolicitarDisponibilidadVisita respuesta = new Return_SolicitarDisponibilidadVisita();
            try
            {
                respuesta = CrmDataController.SolicitarDisponibilidadVisita(solicitud);
                string str = JsonConvert.SerializeObject(respuesta.FechasOcupadas.ToList());
                //respuesta = str;
            }
            catch (Exception ex)
            {
                respuesta.Error = errorOnJsonOrService(ex);
                return respuesta.ToJson();
            }
            return respuesta.ToJson();
        }

        public String SolicitarDisponibilidadVisitaStream(Stream solicitud)
        {
            Return_SolicitarDisponibilidadVisita respuesta = new Return_SolicitarDisponibilidadVisita();
            try
            {
                var reader = new StreamReader(solicitud);
                var jsonString = reader.ReadToEnd();
                var toJ13 = JsonConvert.DeserializeObject(jsonString);
                var toJ14 = JsonConvert.DeserializeObject(toJ13.ToString());
                var toJ3 = JsonConvert.SerializeObject(reader.ToString());
                var toJ4 = JsonConvert.SerializeObject(solicitud);
                //respuesta = CrmDataController.SolicitarDisponibilidadVisita.s.ser;
                string str = JsonConvert.SerializeObject(respuesta.FechasOcupadas.ToList());
                //respuesta = str;
            }
            catch (Exception ex)
            {
                respuesta.Error = errorOnJsonOrService(ex);
                return respuesta.ToJson();
            }
            return respuesta.ToJson();
        }

        public String SolicitarDisponibilidadVisitaString(String solicitud)
        {
            Return_SolicitarDisponibilidadVisita respuesta = new Return_SolicitarDisponibilidadVisita();
            try
            {

                var toJ13 = JsonConvert.DeserializeObject(solicitud);
                var toJ14 = JsonConvert.DeserializeObject(toJ13.ToString());
                //respuesta = CrmDataController.SolicitarDisponibilidadVisita.s.ser;
                string str = JsonConvert.SerializeObject(respuesta.FechasOcupadas.ToList());
                //respuesta = str;
            }
            catch (Exception ex)
            {
                respuesta.Error = errorOnJsonOrService(ex);
                return respuesta.ToJson();
            }
            return respuesta.ToJson();
        }
       
        public String APlata(APlata solicitud)
        {
            ReturnAPlata respuesta = new ReturnAPlata();
            String r1 = "";
            try
            {
                //Object jObj = new Object();
                //string json = JsonConvert.SerializeObject(respuesta);
                //string toJ14 = JsonConvert.DeserializeObject(respuesta.ToJson());

                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //jsonO = jss.Serialize(respuesta);

                respuesta = CrmDataController.APlata(solicitud);
                
                respuesta.PreautorizadoCliente = 873.43M;
                respuesta.Test = "ñññ`qweqéérw´´@'";
                respuesta.ToString();
                respuesta.ToJson();
                r1 = respuesta.ToJson();
                r1 = r1.ToString();

                string output = JsonConvert.SerializeObject(respuesta);


                dynamic obj = JsonConvert.DeserializeObject(respuesta.ToJson());
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var d = jss.Deserialize<dynamic>(obj);
                
            }
            catch (Exception ex)
            {
                /*
                respuesta.error = errorOnJsonOrService(ex);
                return respuesta;
                */
            }

            return respuesta.ToJson();
        }

        public Stream APlataUnescape(APlata solicitud)
        {
            ReturnAPlata respuesta = new ReturnAPlata();
            String r1 = "";
            MemoryStream stream = new MemoryStream();
            Stream tS1 = new MemoryStream();
            try
            {
                respuesta.PreautorizadoCliente = 873.43M;
                respuesta.Test = "ñññ`qweqéérw´´@'";

                //MemoryStream stream2 = new MemoryStream(Encoding.ASCII.GetBytes(respuesta.ToJson()));
                //public stream checkSymbolExistJson(string pSymbol) { Person p = new Person(); p.name = pSymbol; p.age = 15; string json = JsonConvert.SerializeObject(p); return new MemoryStream(Encoding.UTF8.GetBytes(json)); }

                WebOperationContext.Current.OutgoingResponse.ContentType = " application/json; charset=utf-8";
                tS1 =  new MemoryStream(Encoding.UTF8.GetBytes(respuesta.ToJson()));

                byte[] byteArray = Encoding.ASCII.GetBytes(respuesta.ToJson());
                stream = new MemoryStream(byteArray);

                //MemoryStream stream1 = new MemoryStream();
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ReturnAPlata));
                //ser.WriteObject(stream1, respuesta);
                //ser.ToString();
                //StreamReader sr = new StreamReader(stream1);
                //StreamReader sr = new StreamReader(stream1);

                //Object jObj = new Object();
                //string json = JsonConvert.SerializeObject(respuesta);
                //toJ14 = JsonConvert.DeserializeObject(respuesta.ToJson());
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //jsonO = jss.Serialize(respuesta);
                //respuesta = CrmDataController.APlata(solicitud);
                //respuesta.ToString();
                //respuesta.ToJson();
                //r1 = respuesta.ToJson();
                //r1 = r1.ToString();
                //string json = JsonConvert.SerializeObject(respuesta.ToJson());
                //string json1 = JsonConvert.SerializeObject(r1);
                //var toJ13 = JsonConvert.DeserializeObject(respuesta.ToJson());
                //var toJ14 = JsonConvert.DeserializeObject(toJ13.ToString());
                //String a = toJ13.ToString();
                //string json1 = JsonConvert.SerializeObject(a);
                //var json2 = new JavaScriptSerializer().Serialize(respuesta);
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //String jsonO = jss.Serialize(json);
                
            }
            catch (Exception ex)
            {
                /*
                respuesta.error = errorOnJsonOrService(ex);
                return respuesta;
                */
            }
            return tS1;
            //return respuesta.ToJson();
        }

        public ReturnAPlata APlataObj(APlata solicitud)
        {
            ReturnAPlata respuesta = new ReturnAPlata();
            Object obj = new Object();
            try
            { 
                respuesta = CrmDataController.APlata(solicitud);
                respuesta.PreautorizadoCliente = 873.43M;
                respuesta.Test = "ñññ`qweqéérw´´@'";
                TestWCF1.Messages.Return_APlata.Serialize.ToJson(respuesta);
                var json = new JavaScriptSerializer().Serialize(respuesta);
                string sj = TestWCF1.Messages.Return_APlata.Serialize.ToJson(respuesta).ToString();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject(sj);
            }
            catch (Exception ex)
            {
                /*
                respuesta.error = errorOnJsonOrService(ex);
                return respuesta;
                */
            }

            return respuesta;
        }

        public Test1 FTest1(Test1 order)
        {
            //Test1 test = new Test1();
            //StreamReader reader = new StreamReader(order);
            //string JSONdata = reader.ReadToEnd();

            CustomException ex = new CustomException();
            ex.Title = "Error Funtion:Add()";
            ex.ExceptionMessage = "Error occur while doing add function.";
            ex.InnerException = "Inner exception message from serice";
            ex.StackTrace = "Stack Trace message from service.";
           // throw new FaultException(ex.ToString());

            return order;
        }

        public Test2 FTest2(Test2 order)
        {
            return order;
        }

        #region Utils
        //######################## UTILS ###########################//
        private string SanitizeReceivedJson(string jsonString)
        {
            jsonString = jsonString.Replace(@"\", " ");
            string myString = jsonString.Replace(@"\""", @"");
            return jsonString;
        }
        private Return_Error errorOnJsonOrService(Exception ex)
        {
            Return_Error respuesta = null;
            if (CrmDataController == null)
            {
                return respuesta = Error("903", "ERROR_SERVICE", "CRM Acces failed: " + errorMaxLength(ex.Message));
            }
            else
            {
                return respuesta = Error("904", "ERROR_INVALID_JSON", errorMaxLength(ex.Message));
            }
        }
        public string toJson(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }
        public string errorMaxLength(string e)
        {
            if (e.Length > 500)
            {
                e = e.Substring(0, 500);
            }
            return e;
        }
        public Return_Error Error(string codigo, string titulo, string descripcion)
        {
            Return_Error respError = new Return_Error();
            respError.response_code = codigo;
            respError.response_title = titulo;
            respError.response_description = descripcion;
            return respError;
        }
        #endregion
    }
}
