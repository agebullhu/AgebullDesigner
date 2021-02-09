using System;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    public enum SizeOption
    {
        None,
        Auto,
        ByLen
    }
    public class PropertyVueModel : ConfigModelBase
    {
        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="property"></param>
        /// <param name="repair">是否修复</param>
        /// <returns></returns>
        public void CheckField(IPropertyConfig property, bool repair = false)
        {
            if (!property.UserSee)
            {
                property.InputType = null;
                property.UiRequired = false;
                return;
            }
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.IsPrimaryKey && field.IsIdentity)
            {
                property.NoneDetails = true;
                property.ExtendConfigListBool["easyui", "userFormHide"] = true;
            }
            if (property.InputType == "editor")
            {
                property.MulitLine = true;
            }
            else if (field.IsText || field.IsBlob || property.InputType == "mulit")
            {
                property.InputType = "easyui-textbox";
                property.MulitLine = true;
            }
            if (repair)
            {
                if (property.Entity.Interfaces?.Contains("IAuditData") ?? false)
                {
                    property.UiRequired = !property.CanEmpty;
                }
                else
                {

                    property.UiRequired = !property.IsUserReadOnly && (property.IsRequired || !property.CanEmpty);
                }
                RepairInputConfig(property);
                RepairListConfig(property);
            }
            else
            {
                CheckInputConfig(property);
            }

            if (property.NoneGrid)
            {
                property.GridWidth = 0;
                property.DataFormater = null;
            }
            if (property.NoneDetails)
            {
                property.FormCloumnSapn = 0;
                property.InputType = null;
                property.FormOption = null;
                property.ComboBoxUrl = null;
            }
        }

        public void CheckSize(IPropertyConfig property, SizeOption option)
        {
            switch (option)
            {
                case SizeOption.None:
                    property.GridWidth = 0;
                    property.FormCloumnSapn = 0;
                    return;
                case SizeOption.Auto:
                    property.GridWidth = -1;
                    return;
            }

            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.MulitLine)
            {
                property.GridWidth = 3;
            }
            else if (property.CsType == "string")
            {
                property.GridWidth = field.Datalen / 50;
                if (property.GridWidth > 5)
                    property.GridWidth = 5;
                else if (property.GridWidth == 0)
                    property.GridWidth = 1;
            }
            else
            {
                property.GridWidth = 1;
            }
            if (property.GridWidth <= 0)
            {
                property.GridWidth = 1;
            }
            else if (property.GridWidth > 3)
            {
                property.GridWidth = 3;
            }
        }
        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static void CheckInputConfig(IPropertyConfig property)
        {
            if (property.NoneDetails)
                return;
            if (string.IsNullOrWhiteSpace(property.InputType) || string.IsNullOrWhiteSpace(property.FormOption) && property.InputType == "easyui-combobox")
                ResetInputType(property);
        }

        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public void CheckFieldShow(IPropertyConfig property)
        {
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.IsPrimaryKey || field.IsLinkKey)
            {
                property.GridDetails = true;
            }
            if (property.Name.ToLower().Contains("memo") || property.Name.ToLower().Contains("remark"))
            {
                property.MulitLine = true;
            }
            if (property.MulitLine)
            {
                property.GridDetails = true;
                property.MulitLine = true;
                property.NoneGrid = true;
                property.FormCloumnSapn = 2;
                property.Index = 999;
            }

            if (field.IsLinkKey &&!property.NoneGrid)
            {
                property.GridDetails = true;
                property.NoneGrid = true;
            }
            CheckKeyShow(property);
        }

        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public void CheckKeyShow(IPropertyConfig property)
        {
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.IsPrimaryKey)
            {
                property.NoneGrid = true;
                property.NoneDetails = true;
            }
            if (field.IsLinkKey)
            {
                property.NoneGrid = true;
                property.NoneDetails = false;
                property.ExtendConfigListBool["easyui", "userFormHide"] = true;
            }
            if (field.IsLinkCaption)
            {
                property.NoneGrid = false;
                property.NoneDetails = true;
            }
            else if (field.IsLinkField && property.IsCompute)
            {
                property.IsUserReadOnly = true;
            }
        }
        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static void RepairInputConfig(IPropertyConfig property)
        {
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.IsSystemField || property.IsCompute || field.IsIdentity)
            {
                property.NoneDetails = true;
                property.IsUserReadOnly = true;
                return;
            }
            property.NoneDetails = false;
            ResetInputType(property);
        }

        private static void ResetInputType(IPropertyConfig property)
        {
            if (property.MulitLine || property.InputType == "editor")
                property.FormCloumnSapn = property.Entity.FormCloumn;
            else
                property.FormCloumnSapn = 1;
            if (property.MulitLine)
            {
                property.FormCloumnSapn = property.Entity.FormCloumn;
                property.InputType = "easyui-textbox";
                property.FormOption = null;
                property.ComboBoxUrl = null;
                return;
            }
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (field.IsBlob && property.CsType == "string")
            {
                property.MulitLine = true;
                property.FormCloumnSapn = property.Entity.FormCloumn;
                property.InputType = "editor";
                property.FormOption = null;
                property.ComboBoxUrl = null;
                return;
            }
            if (!string.IsNullOrWhiteSpace(property.CustomType))
            {
                property.NoneDetails = false;
                property.InputType = "easyui-combobox";
                property.ComboBoxUrl = null;
                property.FormOption = $"valueField:'value',textField:'text',data:{property.CustomType.ToLWord()}";
                return;
            }
            property.FormOption = null;
            if (field.IsLinkKey)
            {
                var entity = Find(p => p.SaveTableName == field.LinkTable);
                if (entity != null)
                {
                    property.InputType = "easyui-combobox";
                    property.ComboBoxUrl = null;
                    property.FormOption = "valueField:'id', textField:'text'";
                    var title = property.Entity.DataTable.Fields.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (title != null)
                    {
                        property.NoneDetails = false;
                        property.NoneGrid = true;

                        title.Property.FormCloumnSapn = 0;
                        title.Property.InputType = null;
                        title.Property.NoneGrid = false;
                        title.Property.NoneDetails = true;
                        return;
                    }
                }
                property.InputType = null;
                property.NoneDetails = true;
                return;
            }
            switch (property.CsType)
            {
                case "DateTime":
                    property.InputType = "easyui-datebox";
                    property.FormOption = null;
                    property.ComboBoxUrl = null;
                    break;
                case "bool":
                    property.FormOption = "valueField:'value',textField:'text',data:yesnoType";
                    property.InputType = "easyui-combobox";
                    break;
                default:
                    property.InputType = "easyui-textbox";
                    property.FormOption = null;
                    property.ComboBoxUrl = null;
                    break;
            }
        }

        private static void RepairListConfig(IPropertyConfig property)
        {
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (property.IsSystemField || field.IsIdentity)
            {
                property.NoneGrid = true;
                property.GridWidth = -1;
                return;
            }
            if (field.IsBlob)
            {
                property.NoneGrid = true;
                property.GridWidth = -1;
            }

            if (!field.IsLinkKey)
            {
                property.NoneGrid = false;
                return;
            }

            var title = property.Entity.DataTable.Fields.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
            if (title != null)
            {
                property.NoneGrid = true;
                title.Property.NoneGrid = false;
                property.NoneDetails = false;
                title.Property.NoneDetails = true;
                return;
            }

            property.NoneGrid = true;
        }

    }
}