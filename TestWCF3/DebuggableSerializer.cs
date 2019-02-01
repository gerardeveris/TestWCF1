using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Xml;

namespace TestWCF3
{
    public class DebuggableSerializer : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription description,
            BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription description,
            ClientOperation proxy)
        {
            ReplaceDataContractSerializerOperationBehavior(description);
        }

        public void ApplyDispatchBehavior(OperationDescription description,
            DispatchOperation dispatch)
        {
            ReplaceDataContractSerializerOperationBehavior(description);
        }

        public void Validate(OperationDescription description)
        {
        }

        private static void ReplaceDataContractSerializerOperationBehavior(
            OperationDescription description)
        {
            DataContractSerializerOperationBehavior dcsOperationBehavior =
                description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (dcsOperationBehavior != null)
            {
                description.Behaviors.Remove(dcsOperationBehavior);
                description.Behaviors.Add(
                    new DebuggableDataContractSerializerOperationBehavior(description));
            }
        }
    }

    public class DebuggableDataContractSerializerOperationBehavior
            : DataContractSerializerOperationBehavior
    {

        public DebuggableDataContractSerializerOperationBehavior(
            OperationDescription operationDescription) : base(operationDescription)
        {
        }

        public override XmlObjectSerializer CreateSerializer(Type type, string name,
        string ns, IList<Type> knownTypes)
        {
            DataContractSerializer dcs = new DataContractSerializer(type, name,
                ns, knownTypes);
            DebuggableDataContractSerializer mdcs = new DebuggableDataContractSerializer(dcs);
            return mdcs;
        }

        public override XmlObjectSerializer CreateSerializer(Type type,
            XmlDictionaryString name,
            XmlDictionaryString ns, IList<Type> knownTypes)
        {
            DataContractSerializer dcs = new DataContractSerializer(type, name, ns, knownTypes);
            DebuggableDataContractSerializer mdcs = new DebuggableDataContractSerializer(dcs);
            return mdcs;
        }
    }
}