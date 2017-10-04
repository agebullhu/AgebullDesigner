using System;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    internal class PropertyBusinessModel : ConfigModelBase
    {
        #region 定义

        public TraceMessage Trace { get; set; }

        public PropertyConfig Property { get; set; }
        #endregion
        
        #region 修复

        /// <summary>
        ///     修复数组长度
        /// </summary>
        public void RepairByArrayLen()
        {
            var type = CppTypeHelper.ToCppLastType(Property.CppLastType) as TypedefItem;
            if (type != null)
            {
                if (type.KeyWork == "char" && !string.IsNullOrWhiteSpace(type.ArrayLen))
                {
                    try
                    {
                        Property.Datalen = Int32.Parse(type.ArrayLen);
                    }
                    catch (Exception)
                    {
                        Property.Datalen = 5000;
                    }
                }
            }
            else if (!Property.Parent.IsReference && Property.CsType == "string" && Property.Datalen <= 0)
            {
                Property.Datalen = 500;
            }
        }


        internal void CheckDouble()
        {
            //Property.IsIntDecimal = false;
            //string tag = Property.Tag ?? "";
            //var link = friend.Properties.FirstOrDefault(p => tag == p.Tag) ??
            //    friend.Properties.FirstOrDefault(p => p.Name == Property.Name);
            //if (link != null)
            //{
            //    link.CppLastType = CppTypeHelper.CppLastType(link.CppType);
            //    if (link.CppLastType == "double")
            //    {
            //        Property.IsIntDecimal = true;
            //        Property.CppType = "__int64";
            //        Property.CppLastType = "__int64";
            //        Property.CsType = "decimal";
            //        Property.DbType = "decimal";
            //    }
            //}
            if (Property.CppLastType == "double")
            {
                Property.CppType = "__int64";
                Property.CppLastType = "__int64";
                Property.CsType = "decimal";
                Property.DbType = "decimal";
            }
        }
        

        internal void RepairCpp(bool repair, EntityConfig friend = null)
        {
            if (friend != null)
            {
                if (RepairTag(friend, Property.Parent.Tag))
                {
                    Property.CsType = CppTypeHelper.CppTypeToCsType(Property);
                }
            }
            if (!repair)
            {
                //FindOld();
                CheckEnum();
                CheckStruct();
            }
        }

        private void CheckStruct()
        {
            if (Property.IsSystemField || Property.EnumConfig != null)
                return;
            if (Property.CppType == null)
                Property.CppType = CppTypeHelper.CsTypeToCppType(Property);
            if (Property.CppLastType == null)
                Property.CppLastType = Property.CppType;

            var type = CppTypeHelper.ToCppLastType(Property.CppType);
            var entity = type as EntityConfig;
            if (entity != null)
            {
                Property.CppLastType = entity.Name;
                Property.CsType = entity.Name + "Data";
                if (!entity.IsReference)
                    return;
                var friend = GetEntity(p => !p.IsReference && p != entity && p.Tag == entity.Tag);
                if (friend != null)
                {
                    Property.CppLastType = friend.Name;
                    Property.CsType = friend.Name + "Data";
                }
                return;
            }
            var typedef = type as TypedefItem;
            if (typedef != null)
            {
                ReBindingEnum(typedef);
            }
            else
            {
                Property.CppLastType = type.ToString();
            }
        }
        
        
        internal bool RepairTag(EntityConfig friend, string head)
        {
            if (Property.IsSystemField)
            {
                Property.Tag = "[SYSTEM]";
                return false;
            }
            if (friend == null)
            {
                Property.Tag = null;
                return false;
            }
            string tag = Property.Tag ?? "";
            var link = friend.Properties.FirstOrDefault(p => tag == p.Tag) ??
                friend.Properties.FirstOrDefault(p => p.Name == Property.Name);
            if (link == null)
            {
                Property.Tag = null;
                //Property.CppName = null;
                if (Property.EnumConfig != null && Property.EnumConfig.Items.Count <= 1)
                {
                    Property.EnumConfig.IsDelete = true;
                    Property.EnumConfig = null;
                    Property.CustomType = null;
                }
                else if (Property.EnumConfig == null && Property.CustomType != null)
                {
                    Property.CustomType = null;
                }
                return false;
            }
            tag = head + "," + link.CppType + "," + link.Name;
            if (friend.Tag != null && friend.Tag.Contains(tag + ",[Skip]"))
                return false;
            Property.Tag = tag;
            var cpptype = CppTypeHelper.ToCppLastType(link.CppType);

            var stru = cpptype as EntityConfig;
            if (stru != null)
            {
                link.CppLastType = Property.CppType = GetEntity(p => p.Tag == stru.Tag && p != stru)?.Name;
                return true;
            }
            var type = cpptype as TypedefItem;
            if (type == null)
            {
                Property.CppType = link.CppType;
                Property.CppLastType = link.CppType;
                return true;
            }
            Property.CppType = type.KeyWork;
            Property.CppLastType = type.KeyWork;
            if (type.ArrayLen != null)
            {
                if (type.KeyWork == "char" && !string.IsNullOrWhiteSpace(type.ArrayLen))
                {
                    int len;
                    if (Int32.TryParse(type.ArrayLen, out len))
                    {
                        link.Datalen = len;
                        Property.Datalen = len;
                    }
                    else
                    {
                        link.Datalen = 100;
                        Property.Datalen = 100;
                        link.ArrayLen = type.ArrayLen;
                    }
                }
                else
                {
                    link.ArrayLen = type.ArrayLen;
                    Property.ArrayLen = type.ArrayLen;
                }
            }

            if (type.Items.Count <= 0)
            {
                Property.EnumConfig = null;
                Property.CustomType = null;
                return true;
            }
            ReBindingEnum(type);
            return false;
        }

        private void ReBindingEnum(TypedefItem type)
        {
            var enumcfg = EnumBusinessModel.RepairByTypedef(type);
            if (enumcfg == null)
            {
                Property.CustomType = null;
                Property.EnumConfig = null;
            }
            else
            {
                Property.CsType = "int";
                Property.CustomType = enumcfg.Name;
                Property.EnumConfig = enumcfg;
                enumcfg.LinkField = Property.Key;
                Property.CppType = type.KeyWork;
                Property.CppLastType = enumcfg.Name;
            }
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
                Property.EnumConfig.Items.Add(new EnumItem
                {
                    Name = "None",
                    Caption = "未知",
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
