// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Windows.Controls;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展ViewModel基类
    /// </summary>
    public class CodeViewModel : EditorViewModelBase<CodeModel>
    {
        /// <summary>
        ///     分类
        /// </summary>
        public CodeViewModel()
        {
            EditorName = "Code";
        }

        public DependencyAction WebBrowserBehavior => new DependencyAction
        {
            AttachAction = obj => Model.Browser = (WebBrowser)obj
        };
    }
}