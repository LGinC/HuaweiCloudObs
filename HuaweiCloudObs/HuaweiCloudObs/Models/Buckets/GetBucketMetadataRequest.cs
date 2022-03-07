namespace HuaweiCloudObs.Models.Buckets
{
    /// <summary>
    /// 获取桶元数据请求头
    /// </summary>
    public class GetBucketMetadataRequest
    {
        /// <summary>
        /// 预请求指定的跨域请求Origin（通常为域名）
        /// </summary>
        [XmlName("Origin")]
        public string Origin { get; set; }

        /// <summary>
        /// 实际请求可以带的HTTP头域，可以带多个头域
        /// </summary>
        [XmlName("Access-Control-Request-Headers")]
        public string AccessControlRequestHeaders { get; set; }
    }
}
