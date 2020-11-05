using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Config
{


    /// <summary>
    /// 代码风格管理器
    /// </summary>
    public class CodeStyleManager
    {
        /// <summary>
        /// 标准风格
        /// </summary>
        public const string General = "标准风格";

        /// <summary>
        /// 精简风格
        /// </summary>
        public const string Succinct = "精简风格";

        static readonly List<ICodeStyle> CodeStyles = new List<ICodeStyle>();

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TCodeStyle"></typeparam>
        public static void Regist<TCodeStyle>() where TCodeStyle : class, ICodeStyle, new()
        {
            CodeStyles.Add(new TCodeStyle());
        }

        /// <summary>
        /// 取得风格处理器
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ICodeStyle GetStyle(Func<ICodeStyle, bool> filter) => CodeStyles.FirstOrDefault(filter);

        /// <summary>
        /// 查找数据库风格对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static ICodeStyle GetStyle(string style, string target)
        {
            return CodeStyles.FirstOrDefault(p => p.StyleName == style && p.StyleTarget == target);
        }

        /// <summary>
        /// 查找数据库风格对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static ICodeStyle GetStyle(string style, string target, Func<ICodeStyle, bool> filter)
        {
            return CodeStyles.FirstOrDefault(p => p.StyleName == style && p.StyleTarget == target && filter(p));
        }

        /// <summary>
        /// 取得风格处理器
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static TCodeStyle GetStyle<TCodeStyle>(Func<TCodeStyle, bool> filter)
            where TCodeStyle : ICodeStyle
        {
            return CodeStyles.OfType<TCodeStyle>().FirstOrDefault(filter);
        }

        /// <summary>
        /// 查找数据库风格对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static TCodeStyle GetStyle<TCodeStyle>(string style, string target)
            where TCodeStyle : ICodeStyle
        {
            return CodeStyles.OfType<TCodeStyle>().FirstOrDefault(p => p.StyleTarget == target && p.StyleName == style);
        }

        /// <summary>
        /// 查找数据库风格对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static TCodeStyle GetStyle<TCodeStyle>(string style, string target, Func<TCodeStyle, bool> filter)
            where TCodeStyle : ICodeStyle
        {
            return CodeStyles.OfType<TCodeStyle>().FirstOrDefault(p => p.StyleTarget == target && p.StyleName == style && filter(p));
        }
        /// <summary>
        /// 查找数据库风格对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static IDatabaseCodeStyle GetDatabaseStyle(string style, DataBaseType type)
        {
            return CodeStyles.OfType<IDatabaseCodeStyle>().FirstOrDefault(p => p.DataBase == type && p.StyleName == style);
        }

        /// <summary>
        /// 查找数据库风格对象
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static IDatabaseCodeStyle GetDatabaseStyle(string style, DataBaseType type, Func<IDatabaseCodeStyle, bool> filter)
        {
            return CodeStyles.OfType<IDatabaseCodeStyle>().FirstOrDefault(p => p.DataBase == type && p.StyleName == style && filter(p));
        }

        /// <summary>
        /// 查找数据库风格对象
        /// </summary>
        /// <param name="style">风格</param>
        /// <param name="type">数据库类型</param>
        /// <param name="target">目标</param>
        /// <returns></returns>
        public static IDatabaseCodeStyle GetDatabaseStyle(string style, DataBaseType type, string target)
        {
            return CodeStyles.OfType<IDatabaseCodeStyle>().FirstOrDefault(p => p.DataBase == type && p.StyleName == style && p.StyleTarget == target);
        }
    }
}
