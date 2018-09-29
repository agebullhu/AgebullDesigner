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
    public class EasyUiModel : DesignModelBase
    {
        protected override void CreateCommands(NotificationList<CommandItemBase> commandItems)
        {
            commandItems.AddRange(new[]
            {
                new CommandItem
                {
                    Action = CheckUiType,
                    IsButton=true,
                    Editor = "EasyUi",
                    Caption = "控件类型修复",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = CheckSizeByLen,
                    IsButton=true,
                    Editor = "EasyUi",
                    Caption = "按文字计算宽度",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = CheckSizeAuto,
                    IsButton=true,
                    Editor = "EasyUi",
                    Caption = "自适应宽度",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = CheckExport,
                    Caption = "导出导出初始化",
                    IsButton=true,
                    Editor = "EasyUi",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = CheckSimple,
                    Caption = "列表字段初始化",
                    IsButton=true,
                    Editor = "EasyUi",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Action = CreateUiCode,
                    Caption = "生成UI代码（WEB）",
                    Editor = "EasyUi",
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

        private void CheckUiType(object arg)
        {
            if (Context.SelectEntity == null)
                return;
            bool repair = true;
            if (Context.SelectEntity.HaseEasyUi)
            {
                var result = MessageBox.Show("点是执行重置,点否执行修复", "控件类型修复", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
                repair = result == MessageBoxResult.Yes;
            }
            Context.SelectEntity.HaseEasyUi = true;
            var bl = new PropertyEasyUiModel();
            foreach (var field in Context.SelectEntity.Properties)
            {
                bl.CheckField(field, repair);
            }
        }
        private void CheckSizeByLen(object arg)
        {
            if (Context.SelectEntity == null)
                return;
            Context.SelectEntity.HaseEasyUi = true;
            var bl = new PropertyEasyUiModel();
            foreach (var field in Context.SelectEntity.Properties)
            {
                bl.CheckSize(field, SizeOption.ByLen);
            }
        }

        private void CheckSizeAuto(object arg)
        {
            if (Context.SelectEntity == null)
                return;
            Context.SelectEntity.HaseEasyUi = true;
            var bl = new PropertyEasyUiModel();
            foreach (var field in Context.SelectEntity.Properties)
            {
                bl.CheckSize(field, SizeOption.Auto);
            }
        }
        private void CheckExport(object arg)
        {
            if (MessageBox.Show("是否继续?", "导出导出初始化", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            Foreach(field => field.IsPrimaryKey ||
                            !field.IsDiscard && !field.IsLinkKey && !field.DbInnerField &&
                            !field.InnerField && !field.IsSystemField && !field.DenyClient,
                    field =>
                    {
                        field.ExtendConfigListBool["easyui", "CanExport"] = true;
                        field.ExtendConfigListBool["easyui", "CanImport"] = true;
                    });
        }
        

        private void CheckSimple(object arg)
        {
            if (MessageBox.Show("是否继续?", "列表字段初始化", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            Foreach(field => field.IsSystemField , field =>
                 {
                     field.NoneGrid = true;
                     field.NoneDetails = true;
                 });

            Foreach(field => !field.IsPrimaryKey &&
                             (field.IsDiscard || field.IsLinkKey || field.DbInnerField || field.InnerField || field.DenyClient),
                    field => field.NoneGrid = true);

            Foreach(field => !field.IsPrimaryKey &&
                             (field.IsDiscard || field.DbInnerField || field.InnerField || field.DenyClient),
                    field => field.NoneDetails = true);

            Foreach(field => field.IsPrimaryKey || !field.NoneGrid ||
                             field.Name == "DataState" || field.Name == "AuditState" || field.Name == "IsFreeze",
                    field => field.ExtendConfigListBool["easyui", "simple"] = true);
        }

        #endregion

        /*AuditDate AuditorId AuditState LastModifyDate LastReviserID AddDate AuthorID EntityType LinkId ParentId*/
    }
}
