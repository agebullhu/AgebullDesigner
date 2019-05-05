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
using System.ComponentModel.Composition;
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
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UiCommondModel : DesignCommondBase<EntityConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Catalog = "工具",
                SignleSoruce = false,
                IsButton = false,
                Caption = "UI快速组建",
                Action = CheckUi,
                SoruceView = "argument",
                IconName = "tree_Open"
            });
            commands.AddRange(new[]
            {
                new CommandItemBuilder<EntityConfig>
                {
                    Action = CheckUiType,
                    //IsButton=true,
                    Catalog="用户界面",
                    Editor = "EasyUi",
                    WorkView = "model",
                    Caption = "控件类型修复"
                },
                new CommandItemBuilder<EntityConfig>
                {
                    Action = CheckKeyShow,
                    Catalog="用户界面",
                    WorkView = "model",
                    Caption = "隐藏主外键"
                },
                new CommandItemBuilder<EntityConfig>
                {
                    Action = CheckSizeByLen,
                    //IsButton=true,
                    Catalog="用户界面",
                    Editor = "EasyUi",
                    WorkView = "model",
                    Caption = "按文字计算宽度",
                    ConfirmMessage="是否继续?"
                },
                new CommandItemBuilder<EntityConfig>
                {
                    Action = CheckSizeAuto,
                    //IsButton=true,
                    Catalog="用户界面",
                    Editor = "EasyUi",
                    WorkView = "model",
                    Caption = "自适应宽度",
                    ConfirmMessage="是否继续?"
                },
                new CommandItemBuilder<EntityConfig>
                {
                    Action = CheckExport,
                    Caption = "导出导出初始化",
                    //IsButton=true,
                    WorkView = "model",
                    Catalog="用户界面",
                    Editor = "EasyUi",
                    ConfirmMessage="是否继续?"
                },
                new CommandItemBuilder<EntityConfig>
                {
                    Action = CheckSimple,
                    Caption = "界面字段初始化",
                    //IsButton=true,
                    WorkView = "model",
                    Catalog="用户界面",
                    Editor = "EasyUi",
                    ConfirmMessage="是否继续?"
                }
            });
        }

        #region 代码

        private void CheckUi(EntityConfig entity)
        {
            if (entity == null)
                return;
            entity.HaseEasyUi = true;
            var bl = new PropertyEasyUiModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckFieldShow(field);
            }
        }

        private void CheckKeyShow(EntityConfig entity)
        {
            if (entity == null)
                return;
            var bl = new PropertyEasyUiModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckKeyShow(field);
            }
        }

        private void CheckUiType(EntityConfig entity)
        {
            if (entity == null)
                return;
            bool repair = true;
            if (entity.HaseEasyUi)
            {
                var result = MessageBox.Show("点是执行重置,点否执行修复", "控件类型修复", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
                repair = result == MessageBoxResult.Yes;
            }
            entity.HaseEasyUi = true;
            var bl = new PropertyEasyUiModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckField(field, repair);
            }
        }
        private void CheckSizeByLen(EntityConfig entity)
        {
            if (entity == null)
                return;
            entity.HaseEasyUi = true;
            var bl = new PropertyEasyUiModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckSize(field, SizeOption.ByLen);
            }
        }

        private void CheckSizeAuto(EntityConfig entity)
        {
            entity.HaseEasyUi = true;
            var bl = new PropertyEasyUiModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckSize(field, SizeOption.Auto);
            }
        }

        private void CheckExport(EntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                if (field.IsPrimaryKey ||
                    !field.IsDiscard && !field.IsLinkKey && !field.DbInnerField &&
                    !field.InnerField && !field.IsSystemField && !field.DenyClient)
                {
                    field.ExtendConfigListBool["easyui", "CanExport"] = true;
                    field.ExtendConfigListBool["easyui", "CanImport"] = true;
                }
            }
        }

        private void CheckSimple(EntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                if (field.IsSystemField || field.IsDiscard || field.DbInnerField || field.InnerField || field.DenyClient)
                {
                    field.IsRequired = false;
                    field.NoneGrid = true;
                    field.NoneDetails = true;
                    continue;
                }
                field.CanEmpty = !field.IsCaption;
                field.IsRequired = field.IsCaption;

                if (field.IsLinkKey)
                {
                    field.NoneGrid = true;
                    field.NoneDetails = false;
                    field.InputType = "easyui-combobox";
                    field.ComboBoxUrl = null;
                    field.FormOption = "valueField:'id', textField:'text'";
                }

                if (field.IsLinkCaption)
                {
                    field.NoneGrid = false;
                    field.NoneDetails = true;
                    field.CanEmpty = true;
                    field.IsRequired = false;
                }

                if (field.IsBlob || field.DataType == "ByteArray")
                {
                    field.CanEmpty = true;
                    field.IsRequired = false;
                    field.NoneJson = !field.IsImage;
                    field.NoneGrid = true;
                    field.NoneDetails = !field.IsImage;
                    if (entity.PrimaryColumn != null)
                        field.Index = entity.PrimaryColumn.Index;
                }
            }

            foreach (var field in entity.Properties)
            {
                if (field.IsPrimaryKey || !field.NoneGrid ||
                    field.Name == "DataState" || field.Name == "AuditState" || field.Name == "IsFreeze")
                    field.ExtendConfigListBool["easyui", "simple"] = true;
            }
            if (entity.Properties.Count(p => !p.NoneDetails) > 12)
                entity.FormCloumn = 2;
        }

        #endregion

        /*AuditDate AuditorId AuditState LastModifyDate LastReviserID AddDate AuthorID EntityType LinkId ParentId*/
    }
}
