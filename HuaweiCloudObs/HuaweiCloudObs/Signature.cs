using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HuaweiCloudObs
{
    public class Signature
    {
		/// <summary>
		/// 签名
		/// </summary>
		/// <param name="accessKey">AK</param>
		/// <param name="secretKey">SK</param>
		/// <param name="method">Http请求方法</param>
		/// <param name="headers">http请求头</param>
		/// <param name="resource">访问的资源</param>
		/// <returns></returns>
		public static string GetSign(string accessKey, string secretKey, string method, Dictionary<string, IEnumerable<string>> headers, SortedDictionary<string, string> queries)
		{
			return GetSignInternal(accessKey, secretKey, method, headers, GetResource(queries));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
        public static string Md5(byte[] data)
        {
			using var md5 = new MD5CryptoServiceProvider();
			return Convert.ToBase64String(md5.ComputeHash(data));
        }

		public static string Sha256(string body)
        {
            //Console.WriteLine(body);
			if (string.IsNullOrWhiteSpace(body))
			{
				return string.Empty;
			}
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(body)));
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="accessKey">AK</param>
        /// <param name="secretKey">SK</param>
        /// <param name="method">Http请求方法</param>
        /// <param name="headers">http请求头</param>
        /// <param name="resource">访问的资源</param>
        /// <returns></returns>
        public static string GetSign(string accessKey, string secretKey, string method, Dictionary<string, IEnumerable<string>> headers, string query)
		{
			return GetSignInternal(accessKey, secretKey, method, headers, query);	
		}

		static string GetSignInternal(string accessKey, string secretKey, string method, Dictionary<string, IEnumerable<string>> headers, string resource)
        {
			string md5 = headers.FirstOrDefault(h => h.Key == "content-md5").Value?.First();
			string contentType = headers.FirstOrDefault(h => h.Key == "content-type").Value?.First();
			//当有自定义字段x-obs-date时，参数date按照空字符串处理；
			string date = headers.ContainsKey("x-obs-date") ? string.Empty : headers.FirstOrDefault(h => h.Key == "date").Value?.First();
			string stringToSign = GetStringToSign(method, md5, contentType, date, GetCanonicalizedHeaders(headers), resource);
            return $"OBS {accessKey}:{HmacSha1(secretKey, stringToSign)}";
		}

		static string GetResource(SortedDictionary<string, string> queries)
        {
			return queries == null ? string.Empty : string.Join("&", queries.Select(s => $"{s.Key}{(string.IsNullOrEmpty(s.Value) ? string.Empty : $"?{s.Value}")}"));
        }

		static string GetStringToSign(string method, string contentMd5, string contentType, string date, string headers, string resource)
		{
			if(!string.IsNullOrEmpty(headers) && !headers.EndsWith("\n"))
            {
				headers += "\n";
			}
			return $"{method}\n{contentMd5}\n{contentType}\n{date}\n{headers}{resource}";
		}

		static string HmacSha1(string key, string body)
		{
			using var sha = new HMACSHA1(Encoding.UTF8.GetBytes(key));
			return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(body)));
		}

		static string GetCanonicalizedHeaders(Dictionary<string, IEnumerable<string>> headers)
		{
			var effectiveHeaders = headers.Where(h => h.Key.StartsWith("x-obs-", StringComparison.OrdinalIgnoreCase));
			if (!effectiveHeaders.Any())
			{
				return string.Empty;
			}
			var dict = new SortedDictionary<string, string>();
			foreach (var h in effectiveHeaders)
			{
				dict.Add(h.Key, string.Join(",", h.Value));
			}

			StringBuilder sb = new();
			bool isFirstLine = true;
			foreach (var d in dict)
			{
				if (isFirstLine)
				{
					isFirstLine = false;
				}
				else
				{
					//第二行开始 开头添加换行
					sb.Append('\n');
				}
				sb.AppendFormat("{0}:{1}", d.Key, d.Value);
			}

			return sb.ToString();
		}
	}
}
