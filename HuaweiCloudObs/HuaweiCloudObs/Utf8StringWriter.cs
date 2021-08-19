using System.IO;
using System.Text;

namespace HuaweiCloudObs
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
