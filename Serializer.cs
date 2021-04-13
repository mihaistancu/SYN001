using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SYN001
{
    class Serializer
    {
        public static void Serialize<T>(T data, Stream output)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("sbdh", Namespaces.Sbdh);
            namespaces.Add("xsi", Namespaces.Xsi);

            var settings = new XmlWriterSettings
            {
                Indent = true
            };

            using (var writer = XmlWriter.Create(output, settings))
            {
                serializer.Serialize(writer, data, namespaces);
            }
        }
    }
}
