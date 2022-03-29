using HuaweiCloudObsDefine;
using System.Collections.Generic;

namespace HuaweiCloudObs.Models
{
    public  class BaseRequestHeaders
    {
        /// <summary>
        /// 权限控制策略
        /// <para>各策略详细说明见<see href="https://support.huaweicloud.com/perms-cfg-obs/obs_40_0005.html">ACL</see>章节的“使用头域设置ACL”）。</para>
        /// </summary>
        [XmlName("x-obs-acl")]
        public AcessControlLists? Acl { get; set; }

        /// <summary>
        /// 租户列表 授权该租户下所有用户有读对象和获取对象元数据的权限 <para>示例:id=租户id</para>
        /// </summary>
        [XmlName("x-obs-grant-read")]
        public IEnumerable<string> GrantRead { get; set; }

        /// <summary>
        /// 租户列表  授权该租户下所有用户有获取对象ACL的权限  <para>示例:id=租户id</para>
        /// </summary>
        [XmlName("x-obs-grant-read-acp")]
        public IEnumerable<string> GrantReadAcp { get; set; }

        /// <summary>
        /// 租户列表  授权该租户下所有用户有写对象ACL的权限  <para>示例:id=租户id</para>
        /// </summary>
        [XmlName("x-obs-grant-read-acp")]
        public IEnumerable<string> GrantWriteAcp { get; set; }

        /// <summary>
        /// 租户列表  授权该租户下所有用户有写对象ACL的权限  <para>示例:id=租户id</para>
        /// </summary>
        [XmlName("x-obs-grant-full-control")]
        public IEnumerable<string> GrantFullControl { get; set; }

        /// <summary>
        /// 存储类型
        /// </summary>
        [XmlName("x-obs-storage-class")]
        public StorageClass? StorageClass { get; set; }
    }
}
