// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using Agebull.Common.Mvvm;
using System.Collections;
using System.Windows.Controls;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class DataModelDesignViewModel : ViewModelBase<DataModelDesignModel>, IGridSelectionBinding
    {
        #region 基础

        IList IGridSelectionBinding.SelectColumns
        {
            set => Model.Context.SelectConfigs = value;
        }

        /// <summary>
        /// 视图绑定成功后的初始化动作
        /// </summary>
        protected override void OnViewSeted()
        {
            GlobalTrigger.Dispatcher = Dispatcher;
            base.OnViewSeted();
        }

        #endregion


        #region 扩展

        public DelegateCommand SaveCommand => new DelegateCommand(Model.ConfigIo.SaveProject);

        public DependencyAction ExtendEditorBehavior => new DependencyAction
        {
            AttachAction = obj => EditorManager.ExtendEditorPanel = (Border)obj
        };

        #endregion
    }
}