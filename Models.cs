using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SYN001
{
    public static class Namespaces
    {
        public const string Sbdh = "http://eessi.dgempl.ec.europa.eu/namespaces/sbdh";
        public const string Xsi = "http://www.w3.org/2001/XMLSchema-instance";
        public const string Sed = "http://ec.europa.eu/eessi/ns/4_2/SYN001";
    }
    
    public static class Constraints
    {
        public static string[] Countries = "AT|BE|BG|HR|CY|CZ|DK|EE|FI|FR|DE|EL|HU|IS|IE|IT|LV|LI|LT|LU|MT|NL|NO|PL|PT|RO|SK|SI|ES|SE|CH|UK".Split('|');
        public static string[] Roles = "CaseOwner|CounterParty|IntelligentRA".Split('|');
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
        public Identifier Identifier { get; set; }
        public string ContactTypeIdentifier { get; set; }
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
        [XmlElement(Namespace = "")]
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
        public string officialID { get; set; }
        public List<EbMSSignatureCertificate> EbMSSignatureCertificates { get; set; }
        public MessageEndpoint SystemMessageEndpoint { get; set; }
        public List<TLSCertificate> TLSCertificates { get; set; }
    }

    public class EbMSSignatureCertificate
    {
        public CertificateIdentification CertificateIdentification { get; set; }
    }

    public class BusinessSignatureCertificate
    {
        public CertificateIdentification CertificateIdentification { get; set; }
    }

    public class InternalTLSCertificate
    {
        public CertificateIdentification CertificateIdentification { get; set; }
    }

    public class ExternalTLSCertificate
    {
        public CertificateIdentification CertificateIdentification { get; set; }
    }

    public class CertificateIdentification
    {
        public string thumbprint { get; set; }
    }

    public class MessageEndpoint
    {
        public string channel { get; set; }
        public string messageExchangePattern { get; set; }
    }

    public class TLSCertificate
    {
        public CertificateIdentification CertificateIdentification { get; set; }
    }

    public class Institution
    {
        public string officialID { get; set; }
        public string cLDId { get; set; }
        public Value countryCode { get; set; }
        public Value isPublicIndicator { get; set; }
        public Value isLiaisonBodyIndicator { get; set; }
        public List<Relation> Relations { get; set; }
        public List<Translation> Name { get; set; }
        public ValidityPeriod ValidityPeriod { get; set; }
        public List<PostalAddress> PostalAddresses { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public List<Competence> Competences { get; set; }
        public List<EbMSSignatureCertificate> EbMSSignatureCertificates { get; set; }
        public List<BusinessSignatureCertificate> BusinessSignatureCertificates { get; set; }
        public MessageEndpoint BusinessMessageEndpoint { get; set; }
        public MessageEndpoint SystemMessageEndpoint { get; set; }
    }

    public class Value
    {
        public string value { get; set; }
    }

    public class Relation
    {
        public InstitutionIdentification InstitutionIdentification { get; set; }
        public Value relation { get; set; }
    }

    public class InstitutionIdentification
    {
        public string officialID { get; set; }
        public Value countryCode { get; set; }
    }

    public class Translation
    {
        public string languageCode { get; set; }
        public string text { get; set; }
    }

    public class ValidityPeriod
    {
        public DateTime start { get; set; }
    }

    public class PostalAddress
    {
        public string zIPCode { get; set; }
        public bool isVisitorAddress { get; set; }
        public Street Street { get; set; }
        public Town Town { get; set; }
    }

    public class Street
    {
        public Translation Translation { get; set; }
    }

    public class Town
    {
        public Translation Translation { get; set; }
    }

    public class ContactInfo
    {
        public List<URL> URLs { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
        public List<FaxNumber> FaxNumbers { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }

    public class URL
    {
        public string uRL { get; set; }
    }

    public class EmailAddress
    {
        public string emailAddress { get; set; }
    }

    public class FaxNumber
    {
        public string faxNumber { get; set; }
    }

    public class PhoneNumber
    {
        public string phoneNumber { get; set; }
    }

    public class Competence
    {
        public string id { get; set; }
        public string applicationRole { get; set; }
        public ValidityPeriod ValidityPeriod { get; set; }
        public BUCType BUCType { get; set; }
        public bool isEESSIReady { get; set; }
    }

    public class BUCType
    {
        public string name { get; set; }
    }

    public class AccessPoint
    {
        public string officialID { get; set; }
        public Value countryCode { get; set; }
        public List<Translation> Name { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public List<PostalAddress> PostalAddresses { get; set; }
        public ValidityPeriod ValidityPeriod { get; set; }
        public List<LinkedInstitution> LinkedInstitutions { get; set; }
        public List<EbMSSignatureCertificate> EbMSSignatureCertificates { get; set; }
        public List<ExternalTLSCertificate> ExternalTLSCertificates { get; set; }
        public List<InternalTLSCertificate> InternalTLSCertificates { get; set; }
        public MessageEndpoint SystemMessageEndpoint { get; set; }
        public MessageEndpoint BusinessMessageEndpoint { get; set; }
    }

    public class ContactDetails 
    {
        public ContactInfo ContactInfo { get; set; }
    }

    public class LinkedInstitution
    {
        public InstitutionIdentification Institution { get; set; }
        public ValidityPeriod ValidityPeriod { get; set; }
    }

    public class Certificate
    {
        public string serialNumber { get; set; }
        public string subject { get; set; }
        public string issuer { get; set; }
        public DateTime validFrom { get; set; }
        public DateTime validTo { get; set; }
        public string thumbprint { get; set; }
        public byte[] publicKey { get; set; }
    }
}
