// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.WpfMvvmBase
// 建立:2014-11-29
// 修改:2014-11-29
// *****************************************************/

#region 引用

using Agebull.Common.Base;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    /// 数据载入范围
    /// </summary>
    public sealed class LoadingModeScope : ScopeBase
    {
        /// <summary>
        ///     锁定完成后是否发出属性修改事件
        /// </summary>
        private readonly bool _preLoadingMode;

        /// <summary>
        ///     构建编辑范围
        /// </summary>
        private LoadingModeScope()
        {
            _preLoadingMode = NotificationObject.IsLoadingMode;
            if (!_preLoadingMode)
                NotificationObject.IsLoadingMode = true;
        }

        /// <summary>
        ///     构建编辑范围
        /// </summary>
        public static LoadingModeScope CreateScope()
        {
            return new LoadingModeScope();
        }

        /// <summary>
        ///     清理资源
        /// </summary>
        protected override void OnDispose()
        {
            NotificationObject.IsLoadingMode = _preLoadingMode;
        }
    }
}
