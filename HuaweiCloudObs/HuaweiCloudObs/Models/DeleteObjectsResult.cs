using System.Collections.Generic;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{
    [XmlRoot("DeleteResult", Namespace = "http://obs.myhwclouds.com/doc/2015-06-30/")]
    public class DeleteObjectsResult
    {
        /// <summary>
        /// 删除成功对象列表
        /// </summary>
        [XmlElement("Deleted")]
        public List<DeleteObjectResult> Deletes { get; set; }

        /// <summary>
        /// 删除失败列表
        /// </summary>
        [XmlElement("Error")]
        public List<DeleteObject> Errors { get; set; }
    }


    public class DeleteObjectResult
    {
        /// <summary>
        /// 被删除对象Key
        /// </summary>
        public string Key { get; set; }
    }

    public class DeleteObjectError
    {
        /// <summary>
        /// 被删除对象Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }
}
