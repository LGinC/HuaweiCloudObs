using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{
    public class OverrideResponseHeaders
    {
        /// <summary>
        /// 重写响应中的Content-Type头
        /// </summary>
        [XmlElement(ElementName = "response-content-type")]
        public string ResponseContentType { get; set; }

        /// <summary>
        /// 重写响应中的Content-Language头
        /// </summary>
        [XmlElement(ElementName = "response-content-language")]
        public string ResponseContentLanguage {  get; set; }

        /// <summary>
        /// 重写响应中的Expires头
        /// </summary>
        [XmlElement(ElementName = "response-expires")]
        public string ResponseExpires {  get; set;}

        /// <summary>
        /// 重写响应中的Cache-Control头
        /// </summary>
        [XmlElement(ElementName = "response-cache-control")]
        public string ResponseCacheControl {  get; set; }

        /// <summary>
        /// 重写响应中的Content-Disposition头
        /// <para>response-content-disposition=attachment; filename*=utf-8''name1  下载对象重命名为“name1”</para>
        /// </summary>
        [XmlElement(ElementName = "response-content-disposition")]
        public string ResponseContentDisposition { get; set; }

        /// <summary>
        /// 重写响应中的Content-Encoding头
        /// </summary>
        [XmlElement(ElementName = "response-content-encoding")]
        public string ResponseContentEncoding { get; set; }
    }
}
