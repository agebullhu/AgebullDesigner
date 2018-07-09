// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Windows;

#endregion

namespace Agebull.EntityModel.Designer
{
    internal class EasyUiViewModel : ExtendViewModelBase<EasyUiModel>
    {
        public EasyUiViewModel()
        {
            EditorName = "EasyUi";
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new UiPanel();
    }
}
