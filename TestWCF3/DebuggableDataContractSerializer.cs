using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml;

namespace TestWCF3
{
    public class DebuggableDataContractSerializer : XmlObjectSerializer
    {
        private XmlObjectSerializer _Serializer;

        public DebuggableDataContractSerializer(XmlObjectSerializer serializerToUse)
        {
            if (serializerToUse == null)
            {
                throw new ArgumentException("Argument cannot be null.", "serializerToUse");
            }
            this._Serializer = serializerToUse;
        }

        public override bool Equals(object obj)
        {
            return this._Serializer.Equals(obj);
        }

        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            return this._Serializer.IsStartObject(reader);
        }

        public override object ReadObject(XmlDictionaryReader reader)
        {
            return this._Serializer.ReadObject(reader);
        }

        public override object ReadObject(XmlReader reader)
        {
            return this._Serializer.ReadObject(reader);
        }

        public override object ReadObject(XmlReader reader, bool verifyObjectName)
        {
            return this._Serializer.ReadObject(reader, verifyObjectName);
        }

        public override string ToString()
        {
            return this._Serializer.ToString();
        }

        public override int GetHashCode()
        {
            return this._Serializer.GetHashCode();
        }

        public override bool IsStartObject(XmlReader reader)
        {
            return this._Serializer.IsStartObject(reader);
        }

        public override object ReadObject(System.IO.Stream stream)
        {
            return this._Serializer.ReadObject(stream);
        }

        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            return this._Serializer.ReadObject(reader, verifyObjectName);
        }

        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            this._Serializer.WriteEndObject(writer);
        }

        public override void WriteEndObject(XmlWriter writer)
        {
            this._Serializer.WriteEndObject(writer);
        }

        public override void WriteObject(System.IO.Stream stream, object graph)
        {
            this._Serializer.WriteObject(stream, graph);
        }

        public override void WriteObject(XmlDictionaryWriter writer, object graph)
        {
            this._Serializer.WriteObject(writer, graph);
        }

        public override void WriteObject(XmlWriter writer, object graph)
        {
            this._Serializer.WriteObject(writer, graph);
        }

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            this._Serializer.WriteObjectContent(writer, graph);
        }

        public override void WriteObjectContent(XmlWriter writer, object graph)
        {
            this._Serializer.WriteObjectContent(writer, graph);
        }

        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            this._Serializer.WriteStartObject(writer, graph);
        }

        public override void WriteStartObject(XmlWriter writer, object graph)
        {
            this._Serializer.WriteStartObject(writer, graph);
        }
    }
}