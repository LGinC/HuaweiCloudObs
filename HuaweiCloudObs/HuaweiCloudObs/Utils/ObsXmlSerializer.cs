using System.IO;
using System.Xml.Serialization;

namespace HuaweiCloudObs.Utils
{
    public class ObsXmlSerializer
    {
        /// <summary>
        /// 序列化对象为xml字符串 
        /// <parp>编码utf-8 无命名空间</parp>
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        public static string Serialize<T>(T obj)
        {
            XmlSerializerNamespaces ns = new();
            ns.Add("", "");
            using Utf8StringWriter sw = new();
            new XmlSerializer(typeof(T)).Serialize(sw, obj, ns);
            return sw.ToString();
        }

        /// <summary>
        /// 将xml字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="xml">xml字符串</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(string xml)
        {
            using TextReader reader = new StringReader(xml);
            return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        }

        /// <summary>
        /// 将xml流反序列化为对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="s">对象流</param>
        /// <returns></returns>
        public static T Deserialize<T>(Stream s) => (T)new XmlSerializer(typeof(T)).Deserialize(s);
    }

}
