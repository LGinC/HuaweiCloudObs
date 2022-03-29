using System;
using System.Collections;

namespace HuaweiCloudObs.Models
{
    /// <summary>
    /// 上传策略
    /// </summary>
    public class UploadPolicy
    {
        /// <summary>
        /// 策略过期时间
        /// </summary>
        public DateTimeOffset Expiration { get; set; }

        /// <summary>
        /// 条件限制
        /// <code>
        /// bucket为test中以user/开头的所有对象
        /// Conditions = new ArrayList{ new { bucket = "test" }, new[] { "starts-with", "$key", "user/" } }
        /// </code>
        /// </summary>
        public ArrayList Conditions { get; set; }        
    }
}
