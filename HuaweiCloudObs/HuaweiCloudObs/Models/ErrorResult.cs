using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{
    [XmlRoot("Error")]
    public class ErrorResult
    {
        public string Code {  get; set; }

        public string Message { get; set; }

        public string RequestId { get; set; }

        public string HostId { get; set; }

        public string AccessKeyId { get; set; }

        public string SignatureProvided { get; set; }

        public string StringToSign { get; set; }

        public byte[] StringToSignBytes { get; set; }
    }
}
