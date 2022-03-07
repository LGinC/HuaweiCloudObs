using JetBrains.Annotations;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Models.Buckets
{

    /// <summary>
    /// 创建桶请求
    /// </summary>
    public class CreateBucketRequest
    {
        /// <summary>
        /// 桶名称
        /// </summary>
        [NotNull]
        public string BucketName { get; set; }

        /// <summary>
        /// 桶所属区域
        /// </summary>
        [NotNull]
        public string Location { get; set; }
    }

    /// <summary>
    /// 创建桶配置
    /// </summary>
    [XmlRoot("CreateBucketConfiguration", Namespace = "http://obs.myhwclouds.com/doc/2015-06-30/")]
    public class CreateBucketConfiguration
    {
        /// <summary>
        /// 区域
        /// </summary>
        public string Location { get; set; }
    }

    /// <summary>
    /// 创建桶附加消息头
    /// </summary>
    public class CreateBucketHeaders : BaseRequestHeaders
    {
        /// <summary>
        /// 授权给指定domain下的所有用户有FULL_CONTROL权限，并且在默认情况下，该FULL_CONTROL权限将传递给桶内所有对象 <para>示例:id=租户id</para>
        /// </summary>
        [XmlName("x-obs-grant-full-control-delivered")]
        public IEnumerable<string> GrantFullControlDelivered { get; set; }

        /// <summary>
        /// 创建桶时带上此消息头设置桶的存储类型为多AZ。不携带时默认为单AZ。用户携带该头域指定新创的桶的存储类型为多AZ，存在一种情况是当该区域如果不支持多AZ存储，则该桶的存储类型仍为单AZ<para>示例: 3az</para>
        /// </summary>
        [XmlName("x-obs-az-redundancy")]
        public IEnumerable<string> AzRedundancy { get; set; }

        /// <summary>
        /// 创建桶时可以带上此消息头以创建并行文件系统<para>示例:Enable</para>
        /// </summary>
        [XmlName("x-obs-fs-file-interface")]
        public string FileInterface { get; set; }

        /// <summary>
        /// 企业项目id，开通企业项目的用户可以从企业项目服务获取，格式为uuid，默认项目传“0”或者不带该头域，未开通企业项目的用户可以不带该头域
        /// </summary>
        [XmlName("x-obs-epid")]
        public string Epid { get; set; }
    }
}
