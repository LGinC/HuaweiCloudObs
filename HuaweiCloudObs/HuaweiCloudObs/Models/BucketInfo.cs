using System;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{
    public class BucketInfo
    {
        /// <summary>
        /// 桶 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreationDate { get; set; }

        /// <summary>
        /// 所在区域
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 桶类型
        /// <para>OBJECT：对象存储桶</para>
        /// <para>POSIX：并行文件系统</para>
        /// </summary>
        public string BucketType { get; set; }
    }
}
