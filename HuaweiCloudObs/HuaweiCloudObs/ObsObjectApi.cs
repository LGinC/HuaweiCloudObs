using HuaweiCloudObs.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public class ObsObjectApi : BaseObsApi, IObsObjectApi
    {
        public ObsObjectApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory) : base(options, factory)
        {
        }

        public string Bucket { get; set; }

        public Task PutAsync(string name, byte[] data, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrWhiteSpace(Bucket))
            {
                throw new ArgumentNullException(nameof(Bucket), "请先设置Bucket");
            }
            HttpRequestMessage request = new(HttpMethod.Put, $"https://{Bucket}.{Options.Value.EndPoint}/{name}");
            request.Content = new ByteArrayContent(data);
            request.Content.Headers.Add("Content-MD5", Signature.Md5(data));
            return SendAsync<BaseResult>(request, $"/{Bucket}/{name}", cancellationToken: cancellationToken);
        }
    }
}
