using HuaweiCloudObs.Models;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HuaweiCloudObs
{
    public class ObsBucketApi : BaseObsApi, IObsBucketApi
    {
        public ObsBucketApi(IOptionsSnapshot<HuaweicloudObsOptions> options, IHttpClientFactory factory) : base(options, factory)
        {
        }

        public async Task<ListAllMyBucketsResult> GetBucketsAsync(CancellationToken cancellationToken)
        {
            HttpRequestMessage request = new (HttpMethod.Get, $"https://{Options.Value.EndPoint}");
            request.Headers.Add("x-obs-bucket-type", "OBJECT");
            var r = await SendAsync(request, "\n/", cancellationToken: cancellationToken);
            System.Console.WriteLine(r);
            using TextReader reader = new StringReader(r);
            try
            {
                return (ListAllMyBucketsResult)(new XmlSerializer(typeof(ListAllMyBucketsResult)).Deserialize(reader));
            }
            catch (System.Exception)
            {
                using TextReader reader1 = new StringReader(r);
                var e = (ErrorResult)(new XmlSerializer(typeof(ErrorResult)).Deserialize(reader1));
                System.Console.WriteLine(e.Code);
                System.Console.WriteLine(e.Message);
                //TODO: 错误处理
                return null;
            }
        }
    }
}
