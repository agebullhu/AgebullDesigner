// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Windows;
using Agebull.Common.SimpleDesign;

#endregion

namespace Agebull.CodeRefactor.SolutionManager
{
    internal class AllFieldsViewModel : ExtendViewModelBase<EntityDesignModel>
    {
        /// <summary>
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new AllEntityFieldsControl();
    }

}
