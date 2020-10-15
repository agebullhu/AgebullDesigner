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
    public class PropertyEasyUiModel : ConfigModelBase
    {
        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="field"></param>
        /// <param name="repair">是否修复</param>
        /// <returns></returns>
        public void CheckField(PropertyConfig field, bool repair = false)
        {
            if (field.IsPrimaryKey && field.IsIdentity)
            {
                field.NoneDetails = true;
                field.ExtendConfigListBool["easyui", "userFormHide"] = true;
            }
            if (field.DenyClient)
            {
                return;
            }
            if (field.InputType == "editor")
            {
                field.MulitLine = true;
            }
            else if (field.IsMemo || field.IsBlob || field.InputType == "mulit")
            {
                field.InputType = "easyui-textbox";
                field.MulitLine = true;
            }
            if (repair)
            {
                if (field.Parent.Interfaces?.Contains("IAuditData") ?? false)
                {
                    field.UiRequired = !field.CanEmpty;
                }
                else
                {

                    field.UiRequired = !field.IsUserReadOnly && (field.IsRequired || !field.CanEmpty);
                }
                RepairInputConfig(field);
                RepairListConfig(field);
            }
            else
            {
                CheckInputConfig(field);
            }

            if (field.NoneGrid)
            {
                field.GridWidth = 0;
                field.DataFormater = null;
            }
            if (field.NoneDetails)
            {
                field.FormCloumnSapn = 0;
                field.InputType = null;
                field.FormOption = null;
                field.ComboBoxUrl = null;
            }
        }

        public void CheckSize(PropertyConfig field, SizeOption option)
        {
            switch (option)
            {
                case SizeOption.None:
                    field.GridWidth = 0;
                    field.FormCloumnSapn = 0;
                    return;
                case SizeOption.Auto:
                    field.GridWidth = -1;
                    return;
            }

            if (field.MulitLine)
            {
                field.GridWidth = 3;
            }
            else if (field.CsType == "string")
            {
                field.GridWidth = field.Datalen / 50;
                if (field.GridWidth > 5)
                    field.GridWidth = 5;
                else if (field.GridWidth == 0)
                    field.GridWidth = 1;
            }
            else
            {
                field.GridWidth = 1;
            }
            if (field.GridWidth <= 0)
            {
                field.GridWidth = 1;
            }
            else if (field.GridWidth > 3)
            {
                field.GridWidth = 3;
            }
        }
        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static void CheckInputConfig(PropertyConfig field)
        {
            if (field.NoneDetails)
                return;
            if (string.IsNullOrWhiteSpace(field.InputType) || string.IsNullOrWhiteSpace(field.FormOption) && field.InputType == "easyui-combobox")
                ResetInputType(field);
        }

        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public void CheckFieldShow(PropertyConfig field)
        {
            if (field.IsPrimaryKey || field.IsLinkKey)
            {
                field.GridDetails = true;
            }
            if (field.Name.ToLower().Contains("memo") || field.Name.ToLower().Contains("remark"))
            {
                field.IsMemo = true;
            }
            if (field.IsMemo)
            {
                field.GridDetails = true;
                field.MulitLine = true;
                field.NoneGrid = true;
                field.FormCloumnSapn = 2;
                field.Index = 999;
            }
            if (field.LinkTable == "Site" && field.IsLinkCaption)
            {
                field.GridDetails = false;
                field.NoneGrid = false;
                field.Caption = "站点名称";
            }
            if (field.LinkTable == "Organization" && field.IsLinkCaption)
            {
                field.GridDetails = false;
                field.NoneGrid = false;
                field.Caption = "组织单元名称";
            }
            if (field.LinkTable == "User" && field.IsLinkCaption)
            {
                field.GridDetails = false;
                field.NoneGrid = false;
                field.Caption = "顾客昵称";
            }
            if (field.IsLinkKey &&!field.NoneGrid)
            {
                field.GridDetails = true;
                field.NoneGrid = true;
            }
            CheckKeyShow(field);
        }

        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public void CheckKeyShow(PropertyConfig field)
        {
            if (field.IsPrimaryKey)
            {
                field.NoneGrid = true;
                field.NoneDetails = true;
            }
            if (field.IsLinkKey)
            {
                field.NoneGrid = true;
                field.NoneDetails = false;
                field.ExtendConfigListBool["easyui", "userFormHide"] = true;
            }
            if (field.IsLinkCaption)
            {
                field.NoneGrid = false;
                field.NoneDetails = true;
            }
            else if (field.IsLinkField && field.IsCompute)
            {
                field.IsUserReadOnly = true;
            }
        }
        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static void RepairInputConfig(PropertyConfig field)
        {
            if (field.DenyClient || field.IsSystemField || field.IsCompute || field.IsIdentity && field.IsIdentity)
            {
                field.NoneDetails = true;
                field.IsUserReadOnly = true;
                return;
            }
            field.NoneDetails = false;
            ResetInputType(field);
        }

        private static void ResetInputType(PropertyConfig field)
        {
            if (field.MulitLine || field.InputType == "editor")
                field.FormCloumnSapn = field.Parent.FormCloumn;
            else
                field.FormCloumnSapn = 1;
            if (field.MulitLine)
            {
                field.FormCloumnSapn = field.Parent.FormCloumn;
                field.InputType = "easyui-textbox";
                field.FormOption = null;
                field.ComboBoxUrl = null;
                return;
            }
            if (field.IsBlob && field.CsType == "string")
            {
                field.MulitLine = true;
                field.FormCloumnSapn = field.Parent.FormCloumn;
                field.InputType = "editor";
                field.FormOption = null;
                field.ComboBoxUrl = null;
                return;
            }
            if (!string.IsNullOrWhiteSpace(field.CustomType))
            {
                field.NoneDetails = false;
                field.InputType = "easyui-combobox";
                field.ComboBoxUrl = null;
                field.FormOption = $"valueField:'value',textField:'text',data:{field.CustomType.ToLWord()}";
                return;
            }
            field.FormOption = null;
            if (field.IsLinkKey)
            {
                var entity = Find(p => p.SaveTable == field.LinkTable);
                if (entity != null)
                {
                    field.InputType = "easyui-combobox";
                    field.ComboBoxUrl = null;
                    field.FormOption = "valueField:'id', textField:'text'";
                    var title = field.Parent.ClientProperty.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (title != null)
                    {
                        field.NoneDetails = false;
                        field.NoneGrid = true;

                        title.FormCloumnSapn = 0;
                        title.InputType = null;
                        title.NoneGrid = false;
                        title.NoneDetails = true;
                        return;
                    }
                }
                field.InputType = null;
                field.NoneDetails = true;
                return;
            }
            switch (field.CsType)
            {
                case "DateTime":
                    field.InputType = "easyui-datebox";
                    field.FormOption = null;
                    field.ComboBoxUrl = null;
                    break;
                case "bool":
                    field.FormOption = "valueField:'value',textField:'text',data:yesnoType";
                    field.InputType = "easyui-combobox";
                    break;
                default:
                    field.InputType = "easyui-textbox";
                    field.FormOption = null;
                    field.ComboBoxUrl = null;
                    break;
            }
        }

        private static void RepairListConfig(PropertyConfig field)
        {
            if (field.IsSystemField || field.IsIdentity)
            {
                field.NoneGrid = true;
                field.GridWidth = -1;
                return;
            }
            if (field.IsBlob)
            {
                field.NoneGrid = true;
                field.GridWidth = -1;
            }

            if (!field.IsLinkKey)
            {
                field.NoneGrid = false;
                return;
            }

            var title = field.Parent.Properties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
            if (title != null)
            {
                field.NoneGrid = true;
                title.NoneGrid = false;
                field.NoneDetails = false;
                title.NoneDetails = true;
                return;
            }

            field.NoneGrid = true;
        }

    }
}