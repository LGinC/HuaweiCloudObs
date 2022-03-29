using JetBrains.Annotations;
using System.Collections.Generic;

namespace HuaweiCloudObs.Models
{
    public class PostObjectOptions : BaseRequestHeaders
    {
        public string FileName { get; set; }

        /// <summary>
        /// 对象key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 用来指明请求发起者的Access Key  请求包括安全策略参数policy或signature时，则必须包括此参数
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 该请求的安全策略描述。policy格式请参考<see href="https://support.huaweicloud.com/api-obs/obs_04_0012.html">基于浏览器上传的表单中携带签名</see>章节中policy格式
        /// </summary>
        [NotNull]
        public UploadPolicy Policy { get; set; }

        /// <summary>
        /// 用来统一指明请求发起者的Access Key，请求签名和请求的安全策略。token的优先级高于单独指定的Access Key，请求签名和请求的安全策略
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 指定当此次请求操作成功响应后的重定向的地址
        /// <para>如果此参数值有效且操作成功，响应码为303，Location头域由此参数以及桶名、对象名、对象的ETag组成</para>
        /// <para>如果此参数值无效，则OBS忽略此参数的作用，响应码为204，Location头域为对象地址</para>
        /// </summary>
        [XmlName("success-action-redirect")]
        public string SuccessActionRedirect { get; set; }

        /// <summary>
        /// 自定义元数据 <see href="https://support.huaweicloud.com/ugobs-obs/obs_41_0025.html">官方文档</see>
        /// </summary>
        public Dictionary<string, string> CustomMeta { get; set; }

        /// <summary>
        /// 加入一个或多个自定义的响应头，当用户获取此对象或查询此对象元数据时，加入的自定义响应头将会在返回消息的头域中出现
        /// <para>x-obs-persistent-headers: key1:base64_encode(value1),key2:base64_encode(value2)....</para>
        /// <para>其中key1/key2等为自定义header，若含有非ASCII码或不可识别字符，可以采用URL编码或者Base64编码，服务端只会作为字符串处理，不会做解码。</para>
        /// <para>value1/value2等为对应自定义header的值，base64_encode指做base64编码，即将自定义header和对应值的base64编码作为一个key-value对用“:”连接，然后用“,”将所有的key-value对连接起来，放在x-obs-persistent-headers这个header中即可，服务端会对上传的value做解码处理</para>
        /// <para>约束：</para>
        /// <para>1. 通过该方式指定的自定义响应头不能以“x-obs-”为前缀，比如可以指定“key1”，但是不能指定“x-obs-key1”</para>
        /// <para>2. 不能指定http标准头，例如host/content-md5/origin/range/Content-Disposition等</para>
        /// <para>3. 此头域和自定义元数据总长度不能超过8KB</para>
        /// <para>4. 如果传入相同key，将value以“,”拼接后放入同一个key中返回</para>
        /// </summary>
        public string PersistentHeaders { get; set; }


        /// <summary>
        /// 当桶设置了Website配置，可以将获取这个对象的请求重定向到桶内另一个对象或一个外部的URL，OBS将这个值从头域中取出，保存在对象的元数据中
        /// </summary>
        public string WebsiteRedirectLocation { get; set; }

        /// <summary>
        /// 服务端加密方式
        /// </summary>
        public string ServerSideEncryption { get; set; }

        /// <summary>
        /// SSE-KMS 主密钥，如果用户没有提供 那么默认的主密钥将会被使用
        /// <para>支持两种格式的描述方式： </para>
        /// <para>1. regionID:domainID(租户ID):key/key_id </para>
        /// <para>2.key_id 其中regionID是使用密钥所属region的ID；domainID是使用密钥所属租户的租户ID；key_id是从数据加密服务创建的密钥ID</para>
        /// </summary>
        public string ServerSideEncryptionKmsKeyId { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的算法
        /// <para>需要和ServerSideEncryptionCustomerKey，ServerSideEncryptionCustomerKeyMD5一起使用</para>
        /// </summary>
        public string ServerSideEncryptionCustomerAlgorithm { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的密钥 该密钥用于加密对象
        /// <para>由256-bit的密钥经过base64-encoded得到，需要和ServerSideEncryptionCustomerAlgorithm，ServerSideEncryptionCustomerKeyMD5一起使用</para>
        /// </summary>
        public string ServerSideEncryptionCustomerKey { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的密钥的MD5值 MD5值用于验证密钥传输过程中没有出错
        /// <para>由密钥的128-bit MD5值经过base64-encoded得到，需要和ServerSideEncryptionCustomerAlgorithm，ServerSideEncryptionCustomerKey一起使用</para>
        /// </summary>
        public string ServerSideEncryptionCustomerKeyMD5 { get; set; }

        /// <summary>
        /// 对象的过期时间，单位是天。过期之后对象会被自动删除。（从对象最后修改时间开始计算）
        /// </summary>
        public int? Expires { get; set; }
    }

    internal class InternalPostObjectOptions : BaseRequestHeaders
    {
        /// <summary>
        /// 对象key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 用来指明请求发起者的Access Key  请求包括安全策略参数policy或signature时，则必须包括此参数
        /// </summary>
        [XmlName("AccessKeyId")]
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 该请求的安全策略描述。policy格式请参考<see href="https://support.huaweicloud.com/api-obs/obs_04_0012.html">基于浏览器上传的表单中携带签名</see>章节中policy格式
        /// </summary>
        public string Policy { get; set; }

        /// <summary>
        /// 用来统一指明请求发起者的Access Key，请求签名和请求的安全策略。token的优先级高于单独指定的Access Key，请求签名和请求的安全策略
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 指定当此次请求操作成功响应后的重定向的地址
        /// <para>如果此参数值有效且操作成功，响应码为303，Location头域由此参数以及桶名、对象名、对象的ETag组成</para>
        /// <para>如果此参数值无效，则OBS忽略此参数的作用，响应码为204，Location头域为对象地址</para>
        /// </summary>
        [XmlName("success-action-redirect")]
        public string SuccessActionRedirect { get; set; }

        /// <summary>
        /// 自定义元数据 <see href="https://support.huaweicloud.com/ugobs-obs/obs_41_0025.html">官方文档</see>
        /// </summary>
        public Dictionary<string, string> CustomMeta { get; set; }

        /// <summary>
        /// 加入一个或多个自定义的响应头，当用户获取此对象或查询此对象元数据时，加入的自定义响应头将会在返回消息的头域中出现
        /// <para>x-obs-persistent-headers: key1:base64_encode(value1),key2:base64_encode(value2)....</para>
        /// <para>其中key1/key2等为自定义header，若含有非ASCII码或不可识别字符，可以采用URL编码或者Base64编码，服务端只会作为字符串处理，不会做解码。</para>
        /// <para>value1/value2等为对应自定义header的值，base64_encode指做base64编码，即将自定义header和对应值的base64编码作为一个key-value对用“:”连接，然后用“,”将所有的key-value对连接起来，放在x-obs-persistent-headers这个header中即可，服务端会对上传的value做解码处理</para>
        /// <para>约束：</para>
        /// <para>1. 通过该方式指定的自定义响应头不能以“x-obs-”为前缀，比如可以指定“key1”，但是不能指定“x-obs-key1”</para>
        /// <para>2. 不能指定http标准头，例如host/content-md5/origin/range/Content-Disposition等</para>
        /// <para>3. 此头域和自定义元数据总长度不能超过8KB</para>
        /// <para>4. 如果传入相同key，将value以“,”拼接后放入同一个key中返回</para>
        /// </summary>
        [XmlName("x-obs-persistent-headers")]
        public string PersistentHeaders { get; set; }


        /// <summary>
        /// 当桶设置了Website配置，可以将获取这个对象的请求重定向到桶内另一个对象或一个外部的URL，OBS将这个值从头域中取出，保存在对象的元数据中
        /// </summary>
        [XmlName("x-obs-website-redirect-location")]
        public string WebsiteRedirectLocation { get; set; }

        /// <summary>
        /// 服务端加密方式
        /// </summary>
        [XmlName("x-obs-server-side-encryption")]
        public string ServerSideEncryption { get; set; }

        /// <summary>
        /// SSE-KMS 主密钥，如果用户没有提供 那么默认的主密钥将会被使用
        /// <para>支持两种格式的描述方式： </para>
        /// <para>1. regionID:domainID(租户ID):key/key_id </para>
        /// <para>2.key_id 其中regionID是使用密钥所属region的ID；domainID是使用密钥所属租户的租户ID；key_id是从数据加密服务创建的密钥ID</para>
        /// </summary>
        [XmlName("x-obs-server-side-encryption-kms-key-id")]
        public string ServerSideEncryptionKmsKeyId { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的算法
        /// <para>需要和ServerSideEncryptionCustomerKey，ServerSideEncryptionCustomerKeyMD5一起使用</para>
        /// </summary>
        [XmlName("x-obs-server-side-encryption-customer-algorithm")]
        public string ServerSideEncryptionCustomerAlgorithm { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的密钥 该密钥用于加密对象
        /// <para>由256-bit的密钥经过base64-encoded得到，需要和ServerSideEncryptionCustomerAlgorithm，ServerSideEncryptionCustomerKeyMD5一起使用</para>
        /// </summary>
        [XmlName("x-obs-server-side-encryption-customer-key")]
        public string ServerSideEncryptionCustomerKey { get; set; }

        /// <summary>
        /// SSE-C方式 加密使用的密钥的MD5值 MD5值用于验证密钥传输过程中没有出错
        /// <para>由密钥的128-bit MD5值经过base64-encoded得到，需要和ServerSideEncryptionCustomerAlgorithm，ServerSideEncryptionCustomerKey一起使用</para>
        /// </summary>
        [XmlName("x-obs-server-side-encryption-customer-key-MD5")]
        public string ServerSideEncryptionCustomerKeyMD5 { get; set; }

        /// <summary>
        /// 对象的过期时间，单位是天。过期之后对象会被自动删除。（从对象最后修改时间开始计算）
        /// </summary>
        [XmlName("x-obs-expires")]
        public int? Expires { get; set; }

        public static InternalPostObjectOptions ConvertFromDTO(PostObjectOptions options, string sk)
        {
            var s = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(options.Policy)));
            return new InternalPostObjectOptions
            {
                Expires = options.Expires,
                AccessKeyId = options.AccessKeyId,
                Acl = options.Acl,
                CustomMeta = options.CustomMeta,
                GrantFullControl = options.GrantFullControl,
                GrantRead = options.GrantRead,
                GrantReadAcp = options.GrantReadAcp,
                GrantWriteAcp = options.GrantWriteAcp,
                Key = options.Key,
                PersistentHeaders = options.PersistentHeaders,
                ServerSideEncryption = options.ServerSideEncryption,
                ServerSideEncryptionCustomerAlgorithm = options.ServerSideEncryptionCustomerAlgorithm,
                ServerSideEncryptionCustomerKey = options.ServerSideEncryptionCustomerKey,
                ServerSideEncryptionCustomerKeyMD5 = options.ServerSideEncryptionCustomerKeyMD5,
                ServerSideEncryptionKmsKeyId = options.ServerSideEncryptionKmsKeyId,
                StorageClass = options.StorageClass,
                SuccessActionRedirect = options.SuccessActionRedirect,
                Token = options.Token,
                WebsiteRedirectLocation = options.WebsiteRedirectLocation,
                Policy = s,
                Signature = Utils.Signature.Sha1(s, sk)
            };
        }
    }
}
