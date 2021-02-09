using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 类型操作的便利方法
    /// </summary>
    public static class TypeExtenssions
    {
        /// <summary>
        /// 是否null或长度为0的数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static bool IsEmptyOrNull<T>(this IEnumerable<T> array) => array == null || !array.Any();


        /// <summary>
        /// 页面代码路径
        /// </summary>
        public static string CheckPath(this string path, char a, char b)
        {
            return string.IsNullOrWhiteSpace(path) ? path : path.Replace(a, b);
        }
        /// <summary>
        /// 页面代码路径
        /// </summary>
        public static string CheckUrlPath(this string path)
        {
            return string.IsNullOrWhiteSpace(path) ? path : path.Replace('\\', '/');
        }

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
        public static IList<TSource> Append<TSource, TKey>(this IList<TSource> self, IEnumerable<TSource> target, Func<TSource, TKey> func)
        {
            if (target == null)
                return self;
            foreach (var item in target)
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
        public static IList<TSource> Append<TSource>(this IList<TSource> self, IEnumerable<TSource> target)
            where TSource : IKey
        {
            if (target == null)
                return self;
            foreach (var item in target)
            {
                if (!self.Any(p => p.Key == item.Key))
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
        public static IList<TSource> Append<TSource>(this IList<TSource> self, params TSource[] target)
            where TSource : IKey
        {
            if (target == null)
                return self;
            foreach (var item in target)
            {
                if (!self.Any(p => p.Key == item.Key))
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

        /// <summary>
        /// 条件合并
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="self"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IList<TSource> TryAdd<TSource>(this IList<TSource> self, TSource item)
             where TSource : IKey
        {
            if (item == null)
                return self;
            if (!self.Any(p => p.Key == item.Key))
                self.Add(item);
            return self;
        }
    }
}