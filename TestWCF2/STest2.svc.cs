using DevTrends.WCFDataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TestWCF2.Request;

namespace TestWCF2
{
    [ValidateDataAnnotationsBehavior]
    public class STest2 : ISTest2
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Test1 FTest1(Test1 request)
        {

            
            return request;
        }
    }
}
