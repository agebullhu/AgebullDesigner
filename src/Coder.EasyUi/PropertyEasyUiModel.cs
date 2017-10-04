using System.Linq;
using System.Text;

namespace Gboxt.Common.DataAccess.Schemas
{
    public class PropertyEasyUiModel : ConfigModelBase
    {
        /// <summary>
        ///     取字段录入类型（EasyUi）
        /// </summary>
        /// <param name="field"></param>
        /// <param name="repair">是否修复</param>
        /// <returns></returns>
        public static void CheckField(PropertyConfig field, bool repair = false)
        {
            if (field.DenyClient)
            {
                field.IsUserReadOnly = true;
                field.FormCloumnSapn = 0;
                field.InputType = null;
                field.NoneDetails = true;
                field.NoneGrid = true;
                field.GridWidth = 0;
                return;
            }
            if (field.InputType == "editor")
            {
                field.MulitLine = true;
            }
            else if (field.IsMemo || field.InputType == "mulit")
            {
                field.InputType = "easyui-textbox";
                field.MulitLine = true;
            }
            if (repair)
            {
                RepairInputConfig(field);
                RepairListConfig(field);
            }
            else
            {
                CheckInputConfig(field);
                CheckListConfig(field);
            }
            if (field.NoneGrid)
            {
                field.GridWidth = 0;
            }
            else if (field.GridWidth <= 0)
            {
                field.GridWidth = 1;
            }
            else if (field.GridWidth > 3)
            {
                field.GridWidth = 3;
            }
            if (field.NoneDetails)
            {
                field.FormCloumnSapn = 0;
                field.InputType = null;
                field.FormOption = null;
                field.ComboBoxUrl = null;
            }
            if (!field.CanEmpty)
            {
                field.IsRequired = true;
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
        private static void RepairInputConfig(PropertyConfig field)
        {
            if (field.DenyClient || field.IsSystemField || field.IsCompute || (field.IsIdentity && field.IsIdentity))
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
                    field.ComboBoxUrl = $"/Api/Index.aspx?action={entity.Name.ToLower()}";
                    field.FormOption = "valueField:'id', textField:'text'";
                    var title = field.Parent.Properties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (title != null)
                    {
                        field.InputType = "easyui-combobox";
                        field.NoneDetails = false;
                        title.NoneDetails = true;
                        title.FormCloumnSapn = 0;
                        title.InputType = null;
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

        public static void CheckListConfig(PropertyConfig field)
        {
            if (field.DenyClient)
            {
                field.NoneGrid = true;
                field.GridWidth = -1;
                return;
            }
            if (field.GridWidth > 0)
                return;
            ResetSetGridWidth(field);
        }
        public static void RepairListConfig(PropertyConfig field)
        {
            if (field.DenyClient || field.IsSystemField || field.IsIdentity)
            {
                field.NoneGrid = true;
                field.GridWidth = -1;
                return;
            }
            if (field.IsBlob || field.IsSystemField)
            {
                field.NoneGrid = true;
                field.GridWidth = -1;
                return;
            }
            if (field.IsLinkKey)
            {
                var entity = Find(p => p.SaveTable == field.LinkTable);
                if (entity != null)
                {
                    var title = field.Parent.Properties.FirstOrDefault(p => p.LinkTable == field.LinkTable && p.IsLinkCaption);
                    if (title != null)
                    {
                        field.NoneGrid = true;
                        field.GridWidth = -1;
                        ResetSetGridWidth(title);
                        title.NoneGrid = false;
                        return;
                    }
                }
                field.NoneGrid = true;
                field.GridWidth = -1;
                return;
            }
            field.NoneGrid = false;
            ResetSetGridWidth(field);
        }

        private static void ResetSetGridWidth(PropertyConfig field)
        {
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
        }
    }
}