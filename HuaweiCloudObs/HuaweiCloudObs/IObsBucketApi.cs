using HuaweiCloudObs.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HuaweiCloudObs
{
    public interface IObsBucketApi
    {
        /// <summary>
        /// 获取桶列表
        /// </summary>
        /// <param name="bucketType">桶类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ListAllMyBucketsResult> GetBucketsAsync(string bucketType = "OBJECT", CancellationToken cancellationToken = default);

        /// <summary>
        /// 列举桶内对象
        /// </summary>
        /// <param name="bucket">桶名称</param>
        /// <param name="request">附加参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ListBucketResult> GetObjectsAsync(string bucket, GetObjectsRequest request = null, CancellationToken cancellationToken = default);
    }
}
