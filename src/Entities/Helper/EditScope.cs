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

namespace Agebull.Common.DataModel
{
    /// <summary>
    ///     实体编辑范围,在范围中,不发出任何属性事件,结束后可选择是否重发事件
    /// </summary>
    public sealed class EditScope : ScopeBase
    {
        /// <summary>
        ///     锁定完成后是否发出属性修改事件
        /// </summary>
        private readonly bool _endRaiseEvent;

        /// <summary>
        ///     锁定的编辑对象
        /// </summary>
        private readonly EditStatus _status;

        /// <summary>
        ///     在这之前是否也在进行初始化
        /// </summary>
        private readonly EditArrestMode _oldArrest;

        /// <summary>
        ///     构建编辑范围
        /// </summary>
        /// <param name="status">锁定的编辑对象</param>
        /// <param name="arrestMode">编辑阻止模式</param>
        /// <param name="endRaiseEvent">锁定完成后是否发出属性修改事件</param>
        public EditScope(EditStatus status, EditArrestMode arrestMode = EditArrestMode.All, bool endRaiseEvent = true)
        {
            this._status = status;
            this._oldArrest = this._status.Arrest;
            this._status.Arrest = arrestMode;
            this._endRaiseEvent = endRaiseEvent;
        }

        /// <summary>
        ///     清理资源
        /// </summary>
        protected override void OnDispose()
        {
            this._status.Arrest = this._oldArrest;
#if CLIENT
            if (!this._oldArrest.HasFlag(EditArrestMode.PropertyChangingEvent) && this._endRaiseEvent)
            {
                this._status.RaiseModifiedPropertiesChanged();
            }
#endif
        }
    }
}
