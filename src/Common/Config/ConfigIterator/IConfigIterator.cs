using System;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置遍历器
    /// </summary>
    public interface IConfigIterator
    {
        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Foreach<T>(Action<T> action);


        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="doAll">为true,所有点上都执行一次后再向下遍历，否则的话，执行一次就中止遍历</param>
        void Look<T>(Action<T> action, bool doAll);
    }
}