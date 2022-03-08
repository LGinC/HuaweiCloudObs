using HuaweiCloudObs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HuaweiCloudObs
{
    public static class HeadersExtension
    {
        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="request"></param>
        /// <param name="input"></param>
        public static void SetHeaders(this HttpRequestMessage request, object input)
        {
            if (input == null)
            {
                return;
            }
            foreach (var p in input.GetType().GetProperties().Where(p => p.PropertyType.IsPublic))
            {
                object value = p.GetValue(input);
                if (value == null)
                {
                    continue;
                }

                string v = value switch
                {
                    DateTimeOffset t => t.ToString("r"),
                    string t => t,
                    int t => t.ToString(),
                    float t => t.ToString(),
                    decimal t => t.ToString(),
                    Enum t => t.GetEnumMemberOrDefault(),
                    IEnumerable<string> t => string.Join(",", t),
                    _ => null
                };

                if (v == string.Empty || v.Trim() == string.Empty)
                {
                    continue;
                }

                //嵌套对象 递归设置
                if (v == null)
                {
                    SetHeaders(request, value);
                    continue;
                }

                var attrs = p.GetCustomAttributes(typeof(XmlNameAttribute), false);
                request.Headers.TryAddWithoutValidation(attrs != null && attrs.Length > 0 ? (attrs[0] as XmlNameAttribute).Name : p.Name, v);
            }
        }

        /// <summary>
        /// 从请求头中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T GetByHeader<T>(this HttpHeaders headers)
        {
            if (headers == null)
            {
                return default;
            }
            Type t = typeof(T);
            T result = (T)Activator.CreateInstance(t);
            foreach (var p in t.GetProperties().Where(p => p.PropertyType.IsPublic))
            {
                var attributes = p.GetCustomAttributes(typeof(XmlNameAttribute), false);

                string key = (attributes.FirstOrDefault() as XmlNameAttribute)?.Name ?? p.Name;
                if (headers.TryGetValues(key, out var values))
                {
                    p.SetValue(result, ConvertValue(p.PropertyType, values));
                }
            }
            return result;
        }

        private static object ConvertValue(Type type, IEnumerable<string> values) => type switch
        {
            Type t when t == typeof(string) => values.Count() == 1 ? values.First() : values,
            Type t when t == typeof(DateTimeOffset) => GetFirstOrAll(values, v=> DateTimeOffset.Parse(v)),
            Type t when t.IsEnum => GetFirstOrAll(values, v=>Enum.Parse(type,v)),
            _ => values.Count() == 1 ? values.First() : values,
        };

        private static object GetFirstOrAll(IEnumerable<string> values, Func<string, object> parse) =>
            values.Count() == 1 ? parse(values.First()) : values.Select(v=> parse(v));
    }
    

    public static class RequestExtenstion
    {
        public static void SetQueryParam(this HttpRequestMessage request, object parameterObject)
        {
            if (parameterObject == null)
            {
                return;
            }

            Dictionary<string, string> parameters = new ();
            foreach (var p in parameterObject.GetType().GetProperties().Where(p => p.PropertyType.IsPublic))
            {
                object value = p.GetValue(parameterObject);
                if (value == null)
                {
                    continue;
                }

                string v = value switch
                {
                    DateTimeOffset t => t.ToString("r"),
                    string t => t,
                    int t => t.ToString(),
                    float t => t.ToString(),
                    decimal t => t.ToString(),
                    Enum t => t.GetEnumMemberOrDefault(),
                    IEnumerable<string> t => string.Join(",", t),
                    _ => null
                };

                if (v == string.Empty || v.Trim() == string.Empty)
                {
                    continue;
                }

                //嵌套对象 递归设置
                if (v == null)
                {
                    SetQueryParam(request, value);
                    continue;
                }
                var attributes = p.GetCustomAttributes(typeof(XmlNameAttribute), false);
                string key = attributes == null || !attributes.Any()
                    ? p.Name.ToCamelCase()
                    : (attributes.First() as XmlNameAttribute).Name;
                parameters.Add(key, v);
            }

            if (parameters.Count == 0)
            {
                return;
            }

            request.RequestUri = new Uri($"{request.RequestUri.AbsoluteUri}?{string.Join('&', parameters.Select(p => $"{p.Key}={p.Value}"))}");
        }
    }
}
