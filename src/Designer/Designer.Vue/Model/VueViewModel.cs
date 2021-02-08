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
using Agebull.EntityModel.Config.V2021;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#endregion

namespace Agebull.EntityModel.Designer
{
    internal class VueViewModel : EditorViewModelBase<VueModel>
    {
        public VueViewModel()
        {
            EditorName = "Vue";
        }

    }

    internal class VueModel : EntityExtendModel<PageConfig,UserInterfaceField>
    {

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            Extend = Entity?.Page;

        }
        #region 代码

        internal static void CheckUi(IEntityConfig entity)
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

        internal static void CheckKeyShow(IEntityConfig entity)
        {
            if (entity == null)
                return;
            var bl = new PropertyVueModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckKeyShow(field);
            }
        }
        internal static void CheckQuery(IEntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                field.CanUserQuery = field.UserSee;
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
        internal static void CheckSizeByLen(IEntityConfig entity)
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

        internal static void CheckSizeAuto(IEntityConfig entity)
        {
            entity.EnableUI = true;
            var bl = new PropertyVueModel();
            foreach (var field in entity.Properties)
            {
                bl.CheckSize(field, SizeOption.Auto);
            }
        }

        internal static void CheckExport(IEntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                field.ExtendConfigListBool["easyui", "CanExport"] = field.UserSee;
                field.ExtendConfigListBool["easyui", "CanImport"] = field.UserSee;
            }
        }

        internal static void CheckSimple(IEntityConfig entity)
        {
            var cap = entity.CaptionColumn;
            foreach (var property in entity.Properties)
            {
                var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
                if (property.IsSystemField || property.IsDiscard || !property.UserSee)
                {
                    property.IsRequired = false;
                    property.NoneGrid = true;
                    property.NoneDetails = true;
                    continue;
                }
                if(property == cap)
                {
                    property.CanEmpty = false;
                    property.IsRequired = true;
                }
                if (field.IsLinkKey)
                {
                    property.NoneGrid = true;
                    property.NoneDetails = false;
                    property.InputType = "easyui-combobox";
                    property.ComboBoxUrl = null;
                    property.FormOption = "valueField:'id', textField:'text'";
                }

                if (field.IsLinkCaption)
                {
                    property.NoneGrid = false;
                    property.NoneDetails = true;
                    property.CanEmpty = true;
                    property.IsRequired = false;
                }

                if (field.IsBlob || property.DataType == "ByteArray")
                {
                    property.CanEmpty = true;
                    property.IsRequired = false;
                    property.NoneJson = !property.IsImage;
                    property.NoneGrid = true;
                    property.NoneDetails = !property.IsImage;
                    if (entity.PrimaryColumn != null)
                        property.Index = entity.PrimaryColumn.Index;
                }
            }

            foreach (var property in entity.Properties)
            {
                if (property.IsPrimaryKey || !property.NoneGrid ||
                    property.Name == "DataState" || property.Name == "AuditState" || property.Name == "IsFreeze")
                    property.ExtendConfigListBool["easyui", "simple"] = true;
            }
            if (entity.Properties.Count(p => !p.NoneDetails) > 12)
                entity.FormCloumn = 2;
        }

        #endregion
    }
}
