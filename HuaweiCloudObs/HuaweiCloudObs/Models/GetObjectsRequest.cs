namespace HuaweiCloudObs.Models
{
    public class GetObjectsRequest
    {
        /// <summary>
        /// 列举以指定的字符串prefix开头的对象
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 列举桶内对象列表时，指定一个标识符，从该标识符以后按字典顺序返回对象列表
        /// </summary>
        public string Marker { get; set; }

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
        /// 列举对象时的起始位置
        /// <para>有效值：上次请求返回体的NextKeyMarker值</para>
        /// </summary>
        [XmlName("key-marker")]
        public string KeyMarker { get; set; }

        /// <summary>
        /// 返回的对象列表将是按照字典顺序排序后在该标识符以后的对象(单次返回最大为1000个)。如果version-id-marker不是key-marker对应的一个版本号，则该参数无效
        /// <para>本参数只适用于多版本例举场景 与请求中的key-marker配合使用  有效值：对象的版本号，即上次请求返回体的NextVersionIdMarker值</para>
        /// </summary>
        [XmlName("version-id-marker")]
        public string VersionIdMarker { get; set; }

        /// <summary>
        /// 对响应中的部分元素进行指定类型的编码
        /// <para>如果Delimiter、Marker（或KeyMarker）、Prefix、NextMarker（或NextKeyMarker）和Key包含xml 1.0标准不支持的控制字符</para>
        /// <para>可通过设置encoding-type对响应中的Delimiter、Marker（或KeyMarker）、Prefix（包括CommonPrefixes中的Prefix）、NextMarker（或NextKeyMarker）和Key进行编码</para>
        /// </summary>
        [XmlName("encoding-type")]
        public string EncodingType { get; set; }
    }
}
