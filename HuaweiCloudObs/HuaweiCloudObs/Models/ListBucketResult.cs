using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{
    [XmlRoot("ListBucketResult", Namespace = "http://obs.myhwclouds.com/doc/2015-06-30/")]
    public class ListBucketResult : BaseResult
    {
        /// <summary>
        /// 桶名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 对象名的前缀，表示本次请求只列举对象名能匹配该前缀的所有对象
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 列举对象时的起始位置标识符
        /// </summary>
        public string Marker { get; set; }

        /// <summary>
        /// 如果本次没有返回全部结果，响应请求中将包含此字段，用于标明本次请求列举到的最后一个对象。后续请求可以指定Marker等于该值来列举剩余的对象
        /// </summary>
        public string NextMarker { get; set; }

        /// <summary>
        /// 列举时最多返回的对象个数
        /// </summary>
        public string MaxKeys { get; set; }

        /// <summary>
        /// “true”表示本次没有返回全部结果；“false”表示本次已经返回了全部结果
        /// </summary>
        public bool IsTruncated { get; set; }

        /// <summary>
        /// 对象列表
        /// </summary>
        [XmlElement("Contents")]
        public List<OjectContent> Contents { get; set; }
    }

    public class OjectContent
    {
        /// <summary>
        /// 对象名
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 对象最近一次被修改的时间（UTC时间）
        /// </summary>
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// 对象的base64编码的128位MD5摘要
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// 对象的字节数
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 对象的存储类型
        /// </summary>
        public StorageClass StorageClass { get; set; }

        /// <summary>
        /// 用户信息，包含用户DomainId和用户名
        /// </summary>
        public Owner Owner {  get; set; }
    }
}
