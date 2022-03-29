using HuaweiCloudObs.Models;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HuaweiCloudObs.Utils
{
    public static class FormDataExtension
    {
        public static Dictionary<string, string> GetDictionary([NotNull]object input)
        {
            Dictionary<string, string> dict = new();
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

                if (value is Dictionary<string, string>)
                {
                    foreach (var item in value as Dictionary<string, string>)
                    {
                        dict.Add(item.Key, item.Value);
                    }
                    continue;
                }

                if (v == string.Empty || v.Trim() == string.Empty)
                {
                    continue;
                }

                if (v == null)
                {
                    foreach (var item in GetDictionary(v))
                    {
                        dict.Add(item.Key, item.Value);
                    }
                    continue;
                }
                var attrs = p.GetCustomAttributes(typeof(XmlNameAttribute), false);
                dict.Add(attrs != null && attrs.Length > 0 ? (attrs[0] as XmlNameAttribute).Name : p.Name.ToCamelCase(), v);
            }
            return dict;
        } 

        public static MultipartFormDataContent GetFormData([NotNull] object input)
        {
            var r = new MultipartFormDataContent();
            foreach (var item in GetDictionary(input))
            {
                r.Add(new StringContent(item.Value), item.Key);
            }
            return r;
        }

    }
}
