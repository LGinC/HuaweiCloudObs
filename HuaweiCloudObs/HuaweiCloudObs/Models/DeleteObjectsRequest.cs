using System.Collections.Generic;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Models
{
    [XmlRoot("Delete")]
    public class DeleteObjectsRequest
    {
        /// <summary>
        /// 用于指定使用quiet模式，只返回删除失败的对象结果；如果有此字段，则必被置为True，如果为False则被系统忽略掉
        /// </summary>
        public bool Quiet { get; set; }

        /// <summary>
        /// 用于指定待删除的对象Key和响应中的对象Key的编码类型。如果Key包含xml 1.0标准不支持的控制字符，可通过设置该元素指定对象Key的编码类型
        /// </summary>
        public string EncodingType { get; set; }

        /// <summary>
        /// 待删除对象列表, 最大长度 1000
        /// </summary>
        [XmlElement("Object")]
        public List<DeleteObject> Objects { get; set; }
    }

    public class DeleteObject
    {
        /// <summary>
        /// 对象key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionId { get; set; }
    }
}
