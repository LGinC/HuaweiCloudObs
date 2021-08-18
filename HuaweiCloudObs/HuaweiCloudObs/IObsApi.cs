using HuaweiCloudObs.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public interface IObsBucketApi
    {
        Task<ListAllMyBucketsResult> GetBucketsAsync(CancellationToken cancellationToken);
    }
}
