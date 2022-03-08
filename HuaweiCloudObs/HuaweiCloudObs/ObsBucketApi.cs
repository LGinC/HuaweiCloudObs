using HuaweiCloudObs.Models;
using HuaweiCloudObs.Models.Buckets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace HuaweiCloudObs
{
    public class ObsBucketApi : BaseObsApi, IObsBucketApi
    {
        public ObsBucketApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory, ILogger<ObsBucketApi> logger) : base(options, factory, logger)
        {
        }

        public Task CreateAsync(CreateBucketRequest input, CreateBucketHeaders headers, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new(HttpMethod.Put, $"https://{input.BucketName}.{Options.Value.EndPoint}")
            {
                Content = new StringContent(ObsXmlSerializer.Serialize(new CreateBucketConfiguration { Location = input.Location }).ToString(), Encoding.UTF8, "application/xml")
            };
            if(headers != null)
            {
                request.SetHeaders(headers);
            }
            return SendNoReturnAsync(request, cancellationToken: cancellationToken);
        }

        public Task<ListAllMyBucketsResult> GetBucketsAsync(string bucketType = "OBJECT", CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new (HttpMethod.Get, $"https://{Options.Value.EndPoint}");
            request.Headers.Add("x-obs-bucket-type", bucketType);
            return SendAsync<ListAllMyBucketsResult>(request,  cancellationToken: cancellationToken);
        }

        public Task<BucketMetadata> GetMetadataAsync(string bucket, GetBucketMetadataRequest headers = null, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new(HttpMethod.Head, $"https://{bucket}.{Options.Value.EndPoint}");
            if (headers != null)
            {
                request.SetHeaders(headers);
            }
            return SendAndReturnByHeaders<BucketMetadata>(request,  cancellationToken: cancellationToken);
        }

        public Task<ListBucketResult> GetObjectsAsync(string bucket, GetObjectsRequest headers = null, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{bucket}.{Options.Value.EndPoint}");
            request.SetQueryParam(headers);
            return SendAsync<ListBucketResult>(request,  cancellationToken: cancellationToken);
        }

        public Task<ListBucketResult> GetObjectsV2Async(string bucket, GetObjectsV2Request requestParameters = null, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{bucket}.{Options.Value.EndPoint}");
            request.SetQueryParam(requestParameters);
            return SendAsync<ListBucketResult>(request,  cancellationToken: cancellationToken);
        }

        public async Task<string> GetLocationAsync(string bucket, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new(HttpMethod.Get, $"https://{bucket}.{Options.Value.EndPoint}/?location");
            var response = await  SendAndReturnResponseAsync(request,  cancellationToken: cancellationToken);
            var s = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            XmlDocument doc  = new();
            doc.LoadXml(s);
            if(doc.ChildNodes.Count != 2)
            {
                Logger.LogError(s);
                return null;
            }
            return doc.ChildNodes[1].InnerText;
        }

        public Task DeleteAsync(string bucket, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new(HttpMethod.Delete, $"https://{bucket}.{Options.Value.EndPoint}");
            return SendNoReturnAsync(request,  cancellationToken: cancellationToken);
        }
    }
}
