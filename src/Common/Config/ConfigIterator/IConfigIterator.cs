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
        /// <param name="preorder">true:先根遍历 false:后根遍历</param>
        void Foreach<T>(Action<T> action, bool preorder)
            where T : class;

        /// <summary>
        /// 先根遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Preorder<T>(Action<T> action)
            where T : class;

        /// <summary>
        /// 后根遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Postorder<T>(Action<T> action)
            where T : class;
    }
}