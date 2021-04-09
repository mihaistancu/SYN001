using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SYN001
{
    public static class Namespaces
    {
        public const string Sbdh = "http://eessi.dgempl.ec.europa.eu/namespaces/sbdh";
        public const string Xsi = "http://www.w3.org/2001/XMLSchema-instance";
    }
    
    [XmlRoot(Namespace = Namespaces.Sbdh)]
    public class StandardBusinessDocument
    {
        [XmlAttribute(Namespace = Namespaces.Xsi)]
        public string schemaLocation { get; set; }
        public StandardBusinessDocumentHeader StandardBusinessDocumentHeader { get; set; }
    }

    [XmlType(Namespace = Namespaces.Sbdh)]
    public class StandardBusinessDocumentHeader
    {
        public string HeaderVersion { get; set; }
        public Participant Sender { get; set; }
        public Participant Receiver { get; set; }
        public DocumentIdentification DocumentIdentification { get; set; }
    }

    [XmlType(Namespace = Namespaces.Sbdh)]
    public class Participant
    {
        public string ContactTypeIdentifier { get; set; }
        public Identifier Identifier { get; set; }
    }

    [XmlType(Namespace = Namespaces.Sbdh)]
    public class Identifier
    {
        [XmlAttribute]
        public string Authority { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    public class DocumentIdentification
    {
        public DateTime CreationDateAndTime { get; set; }
        public string CaseAction { get; set; }
        public string Type { get; set; }
        public string InstanceIdentifier { get; set; }
        public string TypeVersion { get; set; }
    }
}
