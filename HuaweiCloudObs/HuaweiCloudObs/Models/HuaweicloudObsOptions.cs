namespace HuaweiCloudObs.Models
{
    public class HuaweicloudObsOptions
    {
        public string EndPoint { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string DefaultBucket { get; set; }

        public int MaxRetry { get; set; } = 3;
    }
}
