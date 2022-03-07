namespace HuaweiCloudObs.Models.Buckets
{
    /// <summary>
    /// 桶元数据
    /// </summary>
    public class BucketMetadata
    {
        /// <summary>
        /// 桶的区域位置信息
        /// </summary>
        [XmlName("x-obs-bucket-location")]
        public string Location { get; set; }

        /// <summary>
        /// 桶的默认存储类型
        /// </summary>
        [XmlName("x-obs-storage-class")]
        public StorageClass StorageClass { get; set; }

        /// <summary>
        /// 桶所在的OBS服务版本号
        /// </summary>
        [XmlName("x-obs-version")]
        public string Version { get; set; }

        /// <summary>
        /// 是否为并行文件系统。取值包含Enabled（并行文件系统）
        /// </summary>
        [XmlName("x-obs-fs-file-interface")]
        public string FileInterface { get; set; }

        /// <summary>
        /// 当前桶的企业项目id
        /// </summary>
        [XmlName("x-obs-epid")]
        public string Epid { get; set; }

        /// <summary>
        /// 桶的数据冗余存储策略属性，决定数据是单AZ存储还是多AZ存储。
        /// <para>取值为3az，表示数据冗余存储在同一区域的多个可用区</para>
        /// </summary>
        [XmlName("x-obs-az-redundancy")]
        public string AzRedundancy { get; set; }

        /// <summary>
        /// 当桶设置了CORS配置，如果请求的Origin满足服务端的CORS配置，则在响应中包含这个Origin
        /// </summary>
        [XmlName("Access-Control-Allow-Origin")]
        public string AllowOrigin { get; set; }

        /// <summary>
        /// 当桶设置了CORS配置，如果请求的headers满足服务端的CORS配置，则在响应中包含这个headers
        /// </summary>
        [XmlName("Access-Control-Allow-Headers")]
        public string AllowHeaders { get; set; }

        /// <summary>
        /// 当桶设置了CORS配置，如果请求的Access-Control-Request-Method满足服务端的CORS配置，则在响应中包含这条rule中的Methods
        /// </summary>
        [XmlName("Access-Control-Allow-Methods")]
        public string AllowMethods { get; set; }

        /// <summary>
        /// 当桶设置了CORS配置，服务端CORS配置中的ExposeHeader
        /// </summary>
        [XmlName("Access-Control-Expose-Headers")]
        public string ExposeHeaders { get; set; }
    }
}
