using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using TestWCF1.Messages;
using TestWCF1.Messages.SolicitudVisita;
using TestWCF1.Messages.APlata;
using TestWCF1.Messages.Return_APlata;
using System.IO;
using System.Net;
using System.ServiceModel.Activation;
using TestWCF1.Messages.Test1;
using TestWCF1.Messages.Test2;
using Newtonsoft.Json;
using System.ComponentModel;

namespace TestWCF1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService" en el código y en el archivo de configuración a la vez.


    [ServiceContract]
    public interface IService
    {
        #region Aplata
        [OperationContract]
        [WebInvoke(UriTemplate = "SolicitarDisponibilidadVisita",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        String SolicitarDisponibilidadVisita(SolicitudVisita order);

        [OperationContract]
        [WebInvoke(UriTemplate = "SolicitarDisponibilidadVisitaStream",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        String SolicitarDisponibilidadVisitaStream(Stream order);

        [OperationContract]
        [WebInvoke(UriTemplate = "SolicitarDisponibilidadVisitaString",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        String SolicitarDisponibilidadVisitaString(String order);


        [OperationContract]
        [WebInvoke(UriTemplate = "APlata",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        String APlata(APlata json);

        [OperationContract]
        [WebInvoke(UriTemplate = "APlataUnescape",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        Stream APlataUnescape(APlata json);

        [OperationContract]
        [WebInvoke(UriTemplate = "APlataObj",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        ReturnAPlata APlataObj(APlata json);
        #endregion

        [OperationContract]
        [FaultContract(typeof(CustomException), Action = Constants.FaultAction)]
        [WebInvoke(
               Method = "POST",
               UriTemplate = "Test1",
               BodyStyle = WebMessageBodyStyle.Wrapped,
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json
               )
        ]
        Test1 FTest1(Test1 json);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        [WebInvoke(
              Method = "POST",
              UriTemplate = "Test2",
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json
              )
       ]
        Test2 FTest2(Test2 json);

      
    }
}
