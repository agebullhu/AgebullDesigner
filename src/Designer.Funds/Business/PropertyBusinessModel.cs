using System;
using System.Linq;
using Agebull.EntityModel.RobotCoder;

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
            if (CppTypeHelper2.ToCppLastType(Property.CppLastType) is TypedefItem type)
            {
                if (type.KeyWork == "char" && !string.IsNullOrWhiteSpace(type.ArrayLen))
                {
                    try
                    {
                        Property.Datalen = int.Parse(type.ArrayLen);
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
            //string tag = Property.Option.ReferenceTag ?? "";
            //var link = friend.Properties.FirstOrDefault(p => tag == p.Option.ReferenceTag) ??
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
                if (RepairTag(friend, Property.Parent.Option.ReferenceTag))
                {
                    Property.CsType = CppTypeHelper2.CppTypeToCsType(Property);
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
                Property.CppType = CppTypeHelper2.CsTypeToCppType(Property);
            if (Property.CppLastType == null)
                Property.CppLastType = Property.CppType;

            var type = CppTypeHelper2.ToCppLastType(Property.CppType);
            if (type is EntityConfig entity)
            {
                Property.CppLastType = entity.Name;
                Property.CsType = entity.Name + "Data";
                if (!entity.IsReference)
                    return;
                var friend = GetEntity(p => !p.IsReference && p != entity && p.Option.ReferenceTag == entity.Option.ReferenceTag);
                if (friend != null)
                {
                    Property.CppLastType = friend.Name;
                    Property.CsType = friend.Name + "Data";
                }
                return;
            }

            if (type is TypedefItem typedef)
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
                Property.Option.ReferenceTag = "[SYSTEM]";
                return false;
            }
            if (friend == null)
            {
                Property.Option.ReferenceTag = null;
                return false;
            }
            string tag = Property.Option.ReferenceTag ?? "";
            var link = friend.Properties.FirstOrDefault(p => tag == p.Option.ReferenceTag) ??
                friend.Properties.FirstOrDefault(p => p.Name == Property.Name);
            if (link == null)
            {
                Property.Option.ReferenceTag = null;
                //Property.CppName = null;
                if (Property.EnumConfig != null && Property.EnumConfig.Items.Count <= 1)
                {
                    Property.EnumConfig.Option.IsDelete = true;
                    Property.CustomType = null;
                }
                else if (Property.EnumConfig == null && Property.CustomType != null)
                {
                    Property.CustomType = null;
                }
                return false;
            }
            tag = head + "," + link.CppType + "," + link.Name;
            if (friend.Option.ReferenceTag != null && friend.Option.ReferenceTag.Contains(tag + ",[Skip]"))
                return false;
            Property.Option.ReferenceTag = tag;
            var cpptype = CppTypeHelper2.ToCppLastType(link.CppType);

            if (cpptype is EntityConfig stru)
            {
                link.CppLastType = Property.CppType = GetEntity(p => p.Option.ReferenceTag == stru.Option.ReferenceTag && p != stru)?.Name;
                return true;
            }
            if (!(cpptype is TypedefItem type))
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
                    if (int.TryParse(type.ArrayLen, out int len))
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
                Property.CustomType = null;
                return true;
            }
            ReBindingEnum(type);
            return false;
        }

        private void ReBindingEnum(TypedefItem type)
        {
            var enumcfg = EnumBusinessModel.RepairByTypedef(type.Parent, type);
            if (enumcfg == null)
            {
                Property.CustomType = null;
            }
            else
            {
                Property.CsType = "int";
                Property.DataType = "Enum";
                DataTypeHelper.ToStandard(Property);
                Property.CppType = type.KeyWork;
                Property.CppLastType = enumcfg.Name;
                Property.EnumConfig = enumcfg;
            }
        }

        private void CheckEnum()
        {
            if (Property.EnumConfig == null)
            {
                return;
            }
            if (Property.EnumConfig.Items.All(p => !string.Equals(p.Name, "None", StringComparison.OrdinalIgnoreCase)))
            {
                Property.EnumConfig.Add(new EnumItem
                {
                    Name = "None",
                    Caption = "未知",
                    Value = "0"
                });
            }
            Property.DataType = "Enum";
            DataTypeHelper.StandardDataType(Property);
            Property.CustomType = Property.EnumConfig.Name;
        }
        #endregion
    }
}
