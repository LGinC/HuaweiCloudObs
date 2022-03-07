namespace HuaweiCloudObs.Models.Buckets
{
    /// <summary>
    /// 列举桶内对象V2请求
    /// </summary>
    public class GetObjectsV2Request
    {
        /// <summary>
        /// 只能取值为2，表明采用ListObjectV2接口
        /// </summary>
        [XmlName("list-type")]
        public string ListType { get; set; } = "2";

        /// <summary>
        /// 列举以指定的字符串prefix开头的对象
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 列举桶内对象列表时，指定一个标识符，从该标识符以后按字典顺序返回对象列表
        /// </summary>
        [XmlName("start-after")]
        public string StartAfter { get; set; }

        /// <summary>
        /// 指定返回的最大对象数，返回的对象列表将是按照字典顺序的最多前max-keys个对象，范围是[1，1000]，超出范围时，按照默认的1000进行处理
        /// </summary>
        [XmlName("max-keys")]
        public int MaxKeys { get; set; }

        /// <summary>
        /// 用来指定将对象名按照特定字符分割的分割符。如果指定了prefix参数，按delimiter对所有对象命名进行分割，多个对象分割后prefix到一个delimiter间都相同对象会形成一条CommonPrefixes
        /// <para>如果没有携带prefix参数，按delimiter对所有对象命名进行分割，多个对象分割后从对象名开始到第一个delimiter之间相同的部分形成一条CommonPrefixes</para>
        /// </summary>
        public string Delimiter { get; set; }

        /// <summary>
        /// 列举桶内对象列表时，从该token以后按字典顺序返回对象列表
        /// </summary>
        [XmlName("continuation-token")]
        public string ContinuationToken { get; set; }

        /// <summary>
        /// 对响应中的部分元素进行指定类型的编码
        /// <para>如果Delimiter、Marker（或KeyMarker）、Prefix、NextMarker（或NextKeyMarker）和Key包含xml 1.0标准不支持的控制字符</para>
        /// <para>可通过设置encoding-type对响应中的Delimiter、Marker（或KeyMarker）、Prefix（包括CommonPrefixes中的Prefix）、NextMarker（或NextKeyMarker）和Key进行编码</para>
        /// </summary>
        [XmlName("encoding-type")]
        public string EncodingType { get; set; }

        /// <summary>
        /// 结果是否返回对象拥有者信息
        /// </summary>
        [XmlName("fetch-owner")]
        public bool FetchOwner { get; set; }
    }
}
