// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Designer.View;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class ApiRefactorViewModel : ViewModelBase<ApiRefactorModel>
    {
        public static void ApiRefactor(ProjectConfig project)
        {
            var win = new ApiRefactorWindow();
            var vm = (ApiRefactorViewModel)win.DataContext;
            vm.Model.Project = project;
            win.Show();
        }

        private List<CommandItemBase> _exCommands;

        public IEnumerable<CommandItemBase> ExCommands => _exCommands ?? (_exCommands = new List<CommandItemBase>
        {
            new AsyncCommandItem<string, List<ApiItem>>(Model.CheckApiPrepare, Model.DoCheckApi, Model.CheckApiEnd)
            {
                IsButton=true,
                Caption = "分析接口",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Action =Model.End,
                Caption = "接收到系统中",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            }
        });
    }
}