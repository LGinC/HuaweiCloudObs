using System;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{

    public class UploadObjectResult
    {
        /// <summary>
        /// 对象的版本号。如果桶的多版本状态为开启，则会返回对象的版本号
        /// </summary>
        [XmlName("version-id")]
        public string VersionId { get; set; }

        /// <summary>
        /// 服务端加密方式
        /// </summary>
        [XmlName("x-obs-server-side-encryption")]
        public string ServerSideEncryption { get; set; }

        /// <summary>
        /// SSE-KMS 主密钥
        /// </summary>
        [XmlName("x-obs-server-side-encryption-kms-key-id")]
        public string ServerSideEncryptionKmsKeyId { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的算法
        /// </summary>
        [XmlName("x-obs-server-side-encryption-customer-algorithm")]
        public string ServerSideEncryptionCustomerAlgorithm { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的密钥 该密钥用于加密对象
        /// </summary>
        [XmlName("x-obs-server-side-encryption-customer-key")]
        public string ServerSideEncryptionCustomerKey { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的密钥的MD5值 MD5值用于验证密钥传输过程中没有出错
        /// </summary>
        [XmlName("x-obs-server-side-encryption-customer-key-MD5")]
        public string ServerSideEncryptionCustomerKeyMD5 { get; set; }

        /// <summary>
        /// 对象的存储类型
        /// </summary>
        [XmlName("x-obs-storage-class")]
        public StorageClass StorageClass { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlName("x-obs-id-2")]
        public string ObsId2 { get; set; }
    }
}
