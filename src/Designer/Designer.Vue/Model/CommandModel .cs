// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用


#endregion

namespace Agebull.EntityModel.Designer
{
    internal class CommandViewModel : EditorViewModelBase<CommandModel>
    {
        public CommandViewModel()
        {
            EditorName = "Command";
        }

    }

    internal class CommandModel : DesignModelBase
    {
        #region 操作命令

        public CommandModel()
        {
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        #endregion

    }
}
