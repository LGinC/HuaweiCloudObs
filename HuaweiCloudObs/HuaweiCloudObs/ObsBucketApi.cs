using HuaweiCloudObs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public class ObsBucketApi : BaseObsApi, IObsBucketApi
    {
        public ObsBucketApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory, ILogger<ObsBucketApi> logger) : base(options, factory, logger)
        {
        }

        public Task<ListAllMyBucketsResult> GetBucketsAsync(string bucketType = "OBJECT", CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new (HttpMethod.Get, $"https://{Options.Value.EndPoint}");
            request.Headers.Add("x-obs-bucket-type", bucketType);
            return SendAsync<ListAllMyBucketsResult>(request, "\n/", cancellationToken: cancellationToken);
        }

        public Task<ListBucketResult> GetObjectsAsync(string bucket, GetObjectsRequest requestParameters = null, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{bucket}.{Options.Value.EndPoint}");
            request.SetQueryParam(requestParameters);
            return SendAsync<ListBucketResult>(request, $"/{bucket}/", cancellationToken: cancellationToken);
        }
    }
}
