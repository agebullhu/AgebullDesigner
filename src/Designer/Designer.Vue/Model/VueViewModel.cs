// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#endregion

namespace Agebull.EntityModel.Designer
{
    internal class VueViewModel : ExtendViewModelBase<VueModel>
    {
        public VueViewModel()
        {
            EditorName = "Vue";
        }

    }

    internal class VueModel : DesignModelBase
    {
        #region 操作命令

        public VueModel()
        {
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Append(new CommandItem<EntityConfig>
            {
                Action = CheckUiType,
                IsButton = true,
                Catalog = "用户界面",
                Editor = "Vue",
                WorkView = "entity",
                Caption = "控件类型修复"
            },
                new CommandItem<EntityConfig>
                {
                    Action = CheckKeyShow,
                    IsButton = true,
                    Catalog = "用户界面",
                    WorkView = "entity",
                    Caption = "隐藏主外键"
                },
                new CommandItem<EntityConfig>
                {
                    Action = CheckSizeByLen,
                    IsButton = true,
                    Catalog = "用户界面",
                    Editor = "Vue",
                    WorkView = "entity",
                    Caption = "按文字计算宽度",
                    ConfirmMessage = "是否继续?"
                },
                new CommandItem<EntityConfig>
                {
                    Action = CheckSizeAuto,
                    IsButton = true,
                    Catalog = "用户界面",
                    Editor = "Vue",
                    WorkView = "entity",
                    Caption = "自适应宽度",
                    ConfirmMessage = "是否继续?"
                },
                new CommandItem<EntityConfig>
                {
                    Action = CheckExport,
                    Caption = "导出导出初始化",
                    IsButton = true,
                    WorkView = "entity",
                    Catalog = "用户界面",
                    Editor = "Vue",
                    ConfirmMessage = "是否继续?"
                });
        }

        #endregion


        #region 代码

        internal static void CheckUi(EntityConfig entity)
        {
            if (entity == null)
                return;
            entity.EnableUI = true;
            var bl = new PropertyVueModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckFieldShow(field);
            }
        }

        internal static void CheckKeyShow(EntityConfig entity)
        {
            if (entity == null)
                return;
            var bl = new PropertyVueModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckKeyShow(field);
            }
        }

        internal static void CheckUiType(IEntityConfig entity)
        {
            if (entity == null)
                return;
            bool repair = true;
            if (entity.EnableUI)
            {
                var result = MessageBox.Show("点是执行重置,点否执行修复", "控件类型修复", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
                repair = result == MessageBoxResult.Yes;
            }
            entity.EnableUI = true;
            var bl = new PropertyVueModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckField(field, repair);
            }
        }
        internal static void CheckSizeByLen(EntityConfig entity)
        {
            if (entity == null)
                return;
            entity.EnableUI = true;
            var bl = new PropertyVueModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckSize(field, SizeOption.ByLen);
            }
        }

        internal static void CheckSizeAuto(EntityConfig entity)
        {
            entity.EnableUI = true;
            var bl = new PropertyVueModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckSize(field, SizeOption.Auto);
            }
        }

        internal static void CheckExport(EntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                //if (field.IsPrimaryKey ||
                //    !field.IsDiscard && !field.IsLinkKey && !field.DbInnerField &&
                //    !field.InnerField && !field.IsSystemField && !field.DenyClient)
                {
                    field.ExtendConfigListBool["easyui", "CanExport"] = true;
                    field.ExtendConfigListBool["easyui", "CanImport"] = true;
                }
            }
        }

        internal static void CheckSimple(EntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                if (field.IsSystemField || field.IsDiscard || field.DbInnerField || field.InnerField || field.InnerField)
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
    }
}
