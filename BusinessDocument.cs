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
        public const string Sed = "http://ec.europa.eu/eessi/ns/4_2/SYN001";
    }
    
    [XmlRoot(Namespace = Namespaces.Sbdh)]
    public class StandardBusinessDocument
    {
        [XmlAttribute(Namespace = Namespaces.Xsi)]
        public string schemaLocation { get; set; }
        public StandardBusinessDocumentHeader StandardBusinessDocumentHeader { get; set; }
        
        [XmlElement(Namespace = Namespaces.Sed)]
        public SYN001 SYN001 { get; set; }
    }
        
    public class StandardBusinessDocumentHeader
    {
        public string HeaderVersion { get; set; }
        public Participant Sender { get; set; }
        public Participant Receiver { get; set; }
        public DocumentIdentification DocumentIdentification { get; set; }
        public BusinessScope BusinessScope { get; set; }
    }
        
    public class Participant
    {
        public string ContactTypeIdentifier { get; set; }
        public Identifier Identifier { get; set; }
    }
        
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

    public class BusinessScope
    {
        public string DocumentVersion { get; set; }
        public string SetId { get; set; }
        public CaseId CaseId { get; set; }
    }

    public class CaseId
    {
        public string InstanceIdentifier { get; set; }
        public string BusinessServiceName { get; set; }
    }

    public class SYN001
    {
        public IRSync IRSync { get; set; }
    }

    public class IRSync
    {
        public string version { get; set; }
        public InstitutionRepository InstitutionRepository { get; set; }
    }

    public class InstitutionRepository
    {
        public CentralServicesNode CentralServicesNode { get; set; }
        public List<Institution> Institutions { get; set; }
        public List<AccessPoint> AccessPoints { get; set; }
        public List<Certificate> Certificates { get; set; }
    }

    public class CentralServicesNode
    {
        
    }

    public class Institution
    {

    }

    public class AccessPoint
    {

    }

    public class Certificate
    {

    }
}
