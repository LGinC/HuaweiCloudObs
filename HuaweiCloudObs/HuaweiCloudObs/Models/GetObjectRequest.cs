using System;

namespace HuaweiCloudObs.Models
{
    /// <summary>
    /// 获取对象请求
    /// </summary>
    public class GetObjectRequest
    {
        /// <summary>
        /// 重写响应请求头
        /// </summary>
        public OverrideResponseHeaders OverrideHeaders { get; set; }

        /// <summary>
        /// 指定获取对象的版本号
        /// </summary>
        [XmlName("versionId")]
        public string VersionId { get; set; }

        /// <summary>
        /// 图片处理服务
        /// <para>命令方式：x-image-process=image/commands</para>
        /// <para>样式方式：x-image-process=style/stylename</para>
        /// <para>详情请见<see href="https://support.huaweicloud.com/fg-obs/obs_01_0001.html">图片处理特性指南</see></para>
        /// </summary>
        [XmlName("x-image-process")]
        public string XImageProcess { get; set; }

        /// <summary>
        /// 重写响应中的Content-Disposition头
        /// <para>attname=name1   下载对象重命名为“name1”。</para>
        /// </summary>
        [XmlName("attname")]
        public string Attname { get; set; }

        /// <summary>
        /// 如果对象的ETag和请求中指定的ETag相同，则返回对象内容，否则的话返回412（precondition failed）
        /// </summary>
        [XmlName("If-Match")]
        public string IfMatch { get; set; }

        /// <summary>
        /// 如果对象的ETag和请求中指定的ETag不相同，则返回对象内容，否则的话返回304（not modified）
        /// </summary>
        [XmlName("If-None-Match")]
        public string IfNotMatch { get; set; }

        /// <summary>
        /// 如果对象在请求中指定的时间之后没有修改，则返回对象内容；否则的话返回412（precondition failed）
        /// </summary>
        [XmlName("If-Unmodified-Since")]
        public DateTimeOffset? IfUnModifiedSince { get; set; }

        /// <summary>
        /// 如果对象在请求中指定的时间之后有修改，则返回对象内容；否则的话返回412（precondition failed）
        /// </summary>
        [XmlName("If-Modified-Since")]
        public DateTimeOffset? IfModifiedSince { get; set; }
    }
}
