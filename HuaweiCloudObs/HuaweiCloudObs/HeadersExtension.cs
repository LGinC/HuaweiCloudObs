﻿using HuaweiCloudObs.Models;
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
                    Enum t => t.ToString(),
                    IEnumerable<string> t => string.Join(",", t),
                    _ => null
                };

                //嵌套对象 递归设置
                if (v == null)
                {
                    SetHeaders(request, value);
                    continue;
                }

                request.Headers.TryAddWithoutValidation((p.GetCustomAttributes(typeof(XmlNameAttribute), false)[0] as XmlNameAttribute).Name, v);
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
            Type t = typeof(T);
            T result = (T)Activator.CreateInstance(t);
            foreach (var p in t.GetProperties().Where(p => p.PropertyType.IsPublic))
            {
                var attributes = p.GetCustomAttributes(typeof(XmlNameAttribute), false);
                if (attributes == null || !attributes.Any())
                {
                    continue;
                }

                string key = (attributes.First() as XmlNameAttribute).Name;
                if (headers.TryGetValues(key, out var value))
                {
                    p.SetValue(result, value);
                }
            }

            return result;
        }
    }
}