using CommandLine;

namespace SYN001
{
    partial class Program
    {
        public class Options
        {
            [Option(Default = 5)]
            public int AccessPointCount { get; set; }

            [Option(Default = 10)]
            public int InstitutionsPerAccessPoint { get; set; }

            [Option(Default = 30)]
            public int CompetencesPerInstitution { get; set; }

            [Option(Default = "5")]
            public string Version { get; set; }

            [Option(Default = "EU:CSN01")]
            public string SenderId { get; set; }

            [Option(Default = "UK:APUK01")]
            public string ReceiverId { get; set; }

            [Option(Default = "EU:CSN01")]
            public string CsnId { get; set; }

            [Option(Default = "UK:APUK01")]
            public string ApId { get; set; }

            [Option(Default = "UK:UK001")]
            public string InstId { get; set; }

            [Option(Default = "UKGATEWAY")]
            public string InstUrl { get; set; }

            [Option(Default = "pull")]
            public string InstPattern { get; set; }

            [Option(Required = false)]
            public string CsnTlsCertPath { get; set; }

            [Option(Required = false)]
            public string CsnEbmsCertPath { get; set; }

            [Option(Required = false)]
            public string ApExtTlsCertPath { get; set; }

            [Option(Required = false)]
            public string ApIntTlsCertPath { get; set; }

            [Option(Required = false)]
            public string ApEbmsCertPath { get; set; }

            [Option(Required = false)]
            public string InstTlsCertPath { get; set; }

            [Option(Required = false)]
            public string InstEbmsCertPath { get; set; }

            [Option(Required = false)]
            public string InstBusinessCertPath { get; set; }
        }
    }
}
