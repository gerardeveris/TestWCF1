using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using TestWCF2.Request;

namespace TestWCF2
{
    [ServiceContract]
    public interface ISTest2
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        [WebInvoke(
               Method = "POST",
               UriTemplate = "Composite",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json
        )]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebInvoke(
               Method = "POST",
               UriTemplate = "Test1",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json
        )]
        Test1 FTest1(Test1 json);
    }
}
