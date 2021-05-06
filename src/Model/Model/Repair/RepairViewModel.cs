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
using System.Collections.Generic;
using System.Windows.Controls;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class RepairViewModel : EditorViewModelBase<RepairModel>, IGridSelectionBinding
    {
        #region 基础

        IList IGridSelectionBinding.SelectColumns
        {
            set => Model.Context.SelectConfigs = value;
        }


        #region 扩展

        override 

        public DelegateCommand RepairCommand => new(Model.ConfigIo.SaveProject);

        public List<RepairCommand> RepairCommands => RepairManager.RepairCommands;

        #endregion
    }

    public sealed class RepairModel : DataModelDesignModel
    {


        #region 基础

        IList IGridSelectionBinding.SelectColumns
        {
            set => Model.Context.SelectConfigs = value;
        }

        #endregion


        #region 扩展

        override

        public DelegateCommand RepairCommand => new(Model.ConfigIo.SaveProject);

        public List<RepairCommand> RepairCommands => RepairManager.RepairCommands;

        #endregion
    }
}