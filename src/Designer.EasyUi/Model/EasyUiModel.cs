// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

#endregion

namespace Agebull.EntityModel.Designer
{
    internal class EasyUiModel : DesignModelBase
    {
        protected override void CreateCommands(ObservableCollection<CommandItemBase> commandItems)
        {
            commandItems.AddRange(new[]
            {
                new CommandItem
                {
                    Action = CheckUiType,
                    Caption = "控件类型修复",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = (CheckExport),
                    Caption = "导出导出初始化",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = (CheckSimple),
                    Caption = "列表字段初始化",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = (CreateUiCode),
                    Caption = "生成UI代码（WEB）",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                }
            });
        }

        #region 代码

        internal void CreateUiCode(object arg)
        {
            if (Context.SelectEntity == null)
            {
                return;
            }
            Context.StateMessage = "UI代码生成中...";

            var builder = ProjectBuilder.CreateBuilder("EasyUi");
            builder.CreateEntityCode(Context.SelectEntity.Parent, Context.SelectEntity);

            Context.StateMessage = "UI代码已生成";
        }


        #endregion
        private void CheckUiType(object arg)
        {
            if (Context.SelectEntity == null)
                return;
            var result = MessageBox.Show(Application.Current.MainWindow, "点是执行重置,点否执行修复", "控件类型修复",
                MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
                return;
            foreach (var field in Context.SelectEntity.Properties)
            {
                PropertyEasyUiModel.CheckField(field, result == MessageBoxResult.Yes);
            }
        }

        private void CheckExport(object arg)
        {
            var result = MessageBox.Show(Application.Current.MainWindow, "是否继续?", "导出导出初始化",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            Foreach(field => field.IsPrimaryKey ||
                            !field.IsDiscard && !field.IsLinkKey && !field.DbInnerField &&
                            !field.InnerField && !field.IsSystemField && !field.DenyClient,
                    field =>
            {
                field.ExtendConfigListBool["CanExport"] = true;
                field.ExtendConfigListBool["CanImport"] = true;
            });
        }

        private static readonly List<string> InnerFields = new List<string>
        {
            "AuditDate", "AuditorId", "AuditState", "LastModifyDate", "LastReviserID", "AddDate", "AuthorID"
        };

        private void CheckSimple(object arg)
        {
            var result = MessageBox.Show(Application.Current.MainWindow, "是否继续?", "列表字段初始化",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            Foreach(field => InnerFields.Contains(field.Name),
                field =>
                {
                    field.NoneGrid = true;
                    field.NoneDetails = true;
                });

            Foreach(field => !field.IsPrimaryKey && (field.IsDiscard || field.IsLinkKey || field.DbInnerField ||
                             field.InnerField || field.DenyClient),
                    field => field.NoneGrid = true);

            Foreach(field => !field.IsPrimaryKey && (field.IsDiscard || field.DbInnerField || field.InnerField || field.DenyClient),
                    field => field.NoneDetails = true);

            Foreach(field => field.IsPrimaryKey || !field.NoneGrid ||
                             field.Name == "DataState" || field.Name == "AuditState" || field.Name == "IsFreeze",
                field => field.ExtendConfigListBool["db_simple"] = true);
        }

        /*AuditDate AuditorId AuditState LastModifyDate LastReviserID AddDate AuthorID EntityType LinkId ParentId*/
    }
}
