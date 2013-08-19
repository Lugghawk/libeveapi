using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace libeveapi
{
    /// <summary>
    /// Shamelessly "borrowed" from the EveMon project.
    /// http://evemon.battleclinic.com/
    /// </summary>
    [XmlRoot]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSer = new XmlSerializer(typeof(TValue));

            reader.Read();
            reader.ReadStartElement("dictionary");
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                TKey key = (TKey)keySer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            if (this.Count != 0)
            {
                reader.ReadEndElement();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSer = new XmlSerializer(typeof(TValue));

            writer.WriteStartElement("dictionary");
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                valueSer.Serialize(writer, this[key]);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
