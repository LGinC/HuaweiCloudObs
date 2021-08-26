using System.Collections.Generic;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{
    [XmlRoot("ListAllMyBucketsResult", Namespace = "http://obs.myhwclouds.com/doc/2015-06-30/")]
    public class ListAllMyBucketsResult : BaseResult
    {
        /// <summary>
        /// 桶所有者
        /// </summary>
        public Owner Owner { get; set; }

        /// <summary>
        /// 桶列表
        /// </summary>
        [XmlArray("Buckets")]
        [XmlArrayItem("Bucket")]
        public List<BucketInfo> Buckets {  get; set; }
    }

    public class Owner
    {
        /// <summary>
        /// 拥有者id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public string DomainId { get; set; }
    }
}
