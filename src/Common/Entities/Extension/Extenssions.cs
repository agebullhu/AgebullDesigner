using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.Common
{
    /// <summary>
    /// 文本扩展
    /// </summary>
    public static class Extenssions
    {
        /// <summary>
        /// 是否空或空白文本
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns></returns>
        public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// 非空或空白文本则进行格式化(格式化参数为此文本)
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns></returns>
        public static string FromatByNotEmpty(this string str, string fmt) =>
            string.IsNullOrWhiteSpace(str)
            ? null
            : string.Format(fmt, str);

        /// <summary>
        /// 条件合并
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="self"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IList<TSource> Append<TSource,TKey>(this IList<TSource> self, IEnumerable<TSource> target,Func<TSource, TKey> func)
        {
            if (target == null)
                return self;
            foreach(var item in target)
            {
                if (!self.Any(p => Equals(func(p), func(item))))
                    self.Add(item);
            }
            return self;
        }
        /// <summary>
        /// 条件合并
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="self"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IList<TSource> TryAdd<TSource, TKey>(this IList<TSource> self, TSource target, Func<TSource, TKey> func)
        {
            if (target == null)
                return self;
            if (!self.Any(p => Equals(func(p), func(target))))
                self.Add(target);
            return self;
        }
        
    }
}