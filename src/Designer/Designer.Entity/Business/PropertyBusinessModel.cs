using System;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    internal class PropertyBusinessModel : ConfigModelBase
    {
        #region ¶¨Òå

        public TraceMessage Trace { get; set; }

        public FieldConfig Property { get; set; }
        #endregion
        
        #region ÐÞ¸´
        
        private void RepairCaption()
        {
            if (Property.IsSystemField)
                return;
            RepairConfigName(Property);
            RepairConfigName(Property.EnumConfig);

            if (Property.EnumConfig != null)
            {
                foreach (var item in Property.EnumConfig.Items)
                {
                    RepairConfigName(item);
                }
            }

            if (!string.IsNullOrWhiteSpace(Property.Description))
            {
                Property.Description = Property.Description.Trim(NoneLanguageChar);
            }
            if (!string.IsNullOrWhiteSpace(Property.Caption))
            {
                Property.Caption = Property.Caption.Trim(NoneLanguageChar);
            }
            if (string.IsNullOrWhiteSpace(Property.Caption))
            {
                Property.Caption = !string.IsNullOrWhiteSpace(Property.Description) ? Property.Description : Property.Name;
            }
            if (string.IsNullOrWhiteSpace(Property.Description))
            {
                Property.Description = Property.Caption;
            }

            if (Property.Description != null && Property.Caption == Property.Name)
            {
                Property.Caption = Property.Description.Split(new[] { '\n', ',', '£¬', ':', '£º', '£®' }
                        , StringSplitOptions.RemoveEmptyEntries)[0].Trim();
            }
        }

        internal void RepairByModel(bool isReference, EntityConfig friend = null)
        {
            if (!isReference)
            {
                //FindOld();
                CheckEnum();
                CheckName();
            }
            RepairCaption();
            CheckRegular();
        }
        
        private void CheckRegular()
        {
            if (Property.IsSystemField)
                return;
            Property.CsType =CsharpHelper. GetStdCsType(Property.CsType);
            Property.Nullable = Property.CsType == "string";
            if (Property.CsType == "string")
            {
                if (Property.Datalen <= 0)
                    Property.Datalen = 100;
                Property.ArrayLen = "0";
                Property.Nullable = false;
            }
            else
            {
                Property.Datalen = 0;
            }
            Property.Scale = 0;
            Property.CanEmpty = Property.DbNullable;

            if (Property.InnerField)
            {
                Property.NoneJson = true;
            }
        }

        private void CheckName()
        {
            if (Property.IsSystemField || string.IsNullOrWhiteSpace(Property.Name))
                return;

            Property.Name = Property.Name.Trim(NoneNameChar).ToUWord();
            if (!Property.Name.Contains('['))
                return;
            var strs = Property.Name.Split(new[] { ']', '[' }, StringSplitOptions.RemoveEmptyEntries);
            Property.Name = strs[0];
            if (strs.Length > 0 && int.TryParse(strs[1], out int len))
            {
                if (Property.CsType == "string")
                    Property.Datalen = len;
                else
                {
                    Property.ArrayLen = strs[1];
                    Property.CsType = $"List<{Property.CsType}>";
                }
            }
            if (Property.CsType != "string")
                Property.CsType = $"List<{Property.CsType}>";
        }
        private void CheckEnum()
        {
            if (Property.EnumConfig == null)
            {
                Property.CustomType = null;
                return;
            }
            if (Property.EnumConfig.Items.All(p => !string.Equals(p.Name, "None", StringComparison.OrdinalIgnoreCase)))
            {
                Property.EnumConfig.Add(new EnumItem
                {
                    Name = "None",
                    Caption = "Î´Öª",
                    Value = "0"
                });
            }
            //int id = 0;
            //foreach (var item in Property.EnumConfig.Items)
            //{
            //    item.Value = (++id).ToString();
            //}
            Property.CsType = "int";
            if (Property.CppLastType != "int")
                Property.CppLastType = "int";
            Property.CustomType = Property.EnumConfig.Name;
        }
        #endregion
    }
}
