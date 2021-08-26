using System;

namespace HuaweiCloudObs.Models
{
    /// <summary>
    /// 对象元数据  <see href="https://support.huaweicloud.com/ugobs-obs/obs_41_0025.html">官方文档</see>
    /// </summary>
    public class ObjectMetaData
    {
        /// <summary>
        /// 为请求的对象提供一个默认的文件名赋值给该对象，当下载对象或者访问对象时，以默认文件名命名的文件将直接在浏览器上显示或在访问时弹出文件下载对话框
        /// <para>例如：元数据名称选择为“ContentDisposition”，元数据值填写为“attachment;filename="testfile.xls"”，当通过链接访问设置了该元数据的对象时，会直接弹出一个对象下载的对话框，且对象名称会被修改为“testfile.xls”</para>
        /// </summary>
        [XmlName("x-obs-meta-ContentDisposition")]
        public string ContentDisposition { get; set; }

        /// <summary>
        /// 说明访问者希望采用的语言或语言组合，以根据自己偏好的语言来定制。详情请参见HTTP协议中关于ContentLanguage的定义。
        /// </summary>
        [XmlName("x-obs-meta-ContentLanguage")]
        public string ContentLanguage {  get; set; }

        /// <summary>
        /// 为对象提供重定向功能，重定向到其他对象或者外部的URL。重定向功能通过静态网站托管实现。
        /// </summary>
        [XmlName("x-obs-meta-WebsiteRedirectLocation")]
        public string WebsiteRedirectLocation { get; set; }

        /// <summary>
        /// 指定对象被下载时的内容编码格式，可以设置如下类型：
        /// <para>标准定义：compress、deflate、exi、identity、gzip、pack200-gzip</para>
        /// <para>其他：br、bzip2、lzma 、peerdist、sdch、xpress、xz</para>
        /// </summary>
        [XmlName("x-obs-meta-ContentEncoding")]
        public string ContentEncoding { get; set; }

        /// <summary>
        /// 指定对象被下载时的网页的缓存行为
        /// <para>可缓冲性：public、private、no-cache、only-if-cached</para>
        /// <para>到期时间：max-age=&lt;seconds&gt;、s-maxage=&lt;seconds&gt;、max-stale[=&lt;seconds&gt;]、min-fresh=&lt;seconds&gt;、stale-while-revalidate=&lt;seconds&gt;、stale-if-error=&lt;seconds&gt;</para>
        /// <para>重新验证和重新加载：must-revalidate、proxy-revalidate、immutable</para>
        /// <para>其他：no-store、no-transform</para>
        /// </summary>
        [XmlName("x-obs-meta-CacheControl")]
        public string CacheControl { get; set; }

        /// <summary>
        /// 设置缓存过期时间（GMT）
        /// </summary>
        [XmlName("x-obs-meta-Expires")]
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// 设置对象的文件类型 详情请参见<see href="https://support.huaweicloud.com/ugobs-obs/obs_41_0025.html#obs_41_0025__section1973224795419">对象元数据Content-Type介绍</see>
        /// </summary>
        [XmlName("x-obs-meta-ContentType")]
        public string ContentType { get; set; }
    }
}
