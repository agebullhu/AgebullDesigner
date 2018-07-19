using System;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// C++与C#类型转换辅助类
    /// </summary>
    public static class CppTypeHelper2
    {
        #region 类型处理通用方法

        /// <summary>
        /// 按C++类型不同的不同简化操作
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="entityAction">实体类型的动作(最后一个参数为是否数组)</param>
        /// <param name="stringAction">文本类型的动作(最后一个参数为是否数组)</param>
        /// <param name="generalAction">一般类型的动作(最后一个参数为是否数组)</param>
        public static void DoByCppType(PropertyConfig field,
            Action<PropertyConfig, EntityConfig, bool> entityAction,
            Action<PropertyConfig> stringAction,
            Action<PropertyConfig, string, bool> generalAction)
        {
            var type = ToCppLastType(field.CppLastType ?? field.CppType);
            switch (type)
            {
                case EntityConfig stru:
                    entityAction(field, stru, !string.IsNullOrWhiteSpace(field.ArrayLen));
                    return;
                case TypedefItem tdef:
                    if (tdef.KeyWork == "char" && !string.IsNullOrWhiteSpace(tdef.ArrayLen))
                    {
                        stringAction(field);
                    }
                    else
                    {
                        generalAction(field, tdef.KeyWork, !string.IsNullOrWhiteSpace(tdef.ArrayLen));
                    }
                    return;
            }

            var tp = type.ToString();
            if (tp == "char" && field.Datalen > 1)
            {
                stringAction(field);
            }
            else
            {
                generalAction(field, tp, !string.IsNullOrWhiteSpace(field.ArrayLen));
            }
        }

        /// <summary>
        /// 按C++类型不同的不同简化操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="field">字段</param>
        /// <param name="entityAction">实体类型的动作(最后一个参数为是否数组)</param>
        /// <param name="stringAction">文本类型的动作(最后一个参数为是否数组)</param>
        /// <param name="typeAction">自定义类型</param>
        /// <param name="enumAction">枚举类型的处理</param>
        /// <param name="generalAction">一般类型的动作(最后一个参数为是否数组)</param>
        /// <code>
        /// CppTypeHelper.DoByCppType(field.Parent, field,
        ///	(pro, len) =>
        ///	{
        ///	},
        ///	(pro, type, len) =>
        ///	{
        ///	},
        ///	(pro, type, len) =>
        ///	{
        ///	},
        ///	(pro, type, len) =>
        ///	{
        ///	},
        ///	(pro, type, len) =>
        ///	{
        ///	});
        /// </code>
        public static void DoByCppType(EntityConfig entity, PropertyConfig field,
            Action<PropertyConfig, int> stringAction,
            Action<PropertyConfig, string, int> generalAction,
            Action<PropertyConfig, EntityConfig, int> entityAction,
            Action<PropertyConfig, TypedefItem, int> typeAction,
            Action<PropertyConfig, EnumConfig, int> enumAction)
        {
            var type = ToCppLastType(field.CppLastType ?? field.CppType);
            if (type == null)
                return;
            int len;
            if (type is EntityConfig stru)
            {
                int.TryParse(field.ArrayLen, out len);
                entityAction(field, stru, len);
                return;
            }
            if (type is TypedefItem tdef)
            {
                int.TryParse(tdef.ArrayLen, out len);
                if (tdef.KeyWork == "char")
                {
                    if (len == 0)
                        len = field.Datalen;
                    if (len > 1)
                        stringAction(field, len);
                    else
                        generalAction(field, tdef.KeyWork, len);
                }
                else
                {
                    if (len == 0)
                        int.TryParse(field.ArrayLen, out len);
                    if (tdef.Items.Count > 0)
                    {
                        typeAction(field, tdef, len);
                    }
                    else
                    {
                        generalAction(field, tdef.KeyWork, len);
                    }
                }
                return;
            }
            var tp = type.ToString();
            if (tp == "char" && field.Datalen > 1)
            {
                stringAction(field, field.Datalen);
            }
            else
            {
                int.TryParse(field.ArrayLen, out len);
                if (field.EnumConfig != null)
                {
                    enumAction(field, field.EnumConfig, len);
                }
                else
                {
                    generalAction(field, tp, len);
                }
            }
        }
        #endregion

        #region C++类型辅助

        /// <summary>
        /// 转为基本C++类型
        /// </summary>
        /// <param name="cppType">C++类型</param>
        /// <returns></returns>
        public static object ToCppLastType(string cppType)
        {
            if (string.IsNullOrEmpty(cppType))
            {
                return null;
            }
            cppType = cppType.Trim();
            var typedef = CppProject.Instance.TypedefItems.FirstOrDefault(p => p.Name == cppType);
            if (typedef != null)
            {
                typedef.KeyWork = typedef.KeyWork.Trim();
                var type = ToCppLastType(typedef.KeyWork);
                return type.ToString() == typedef.KeyWork ? typedef : type;
            }
            var stru = GlobalConfig.GetEntity(cppType);
            if (stru != null)
            {
                return stru;
            }
            return cppType;
        }


        public static string CppLastType(string cppType)
        {
            cppType = cppType.Trim();
            var stru = GlobalConfig.GetEntity(cppType);
            if (stru != null)
            {
                return stru.Name;
            }
            var typedef = CppProject.Instance.TypedefItems.FirstOrDefault(p => p.Name == cppType);
            if (typedef == null)
                return cppType;
            typedef.KeyWork = typedef.KeyWork.Trim();
            return CppLastType(typedef.KeyWork);
        }

        /// <summary>
        /// 得到标准的C#类型
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string CsTypeToCppType(PropertyConfig property)
        {
            switch (property.CsType.ToLower())
            {
                case "string":
                    if (property.Datalen <= 0)
                        property.Datalen = 100;
                    return "char";
                case "datetime":
                    return "tm";
            }
            return property.CsType.ToLower();
        }

        /// <summary>
        /// 得到标准的C#类型
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string CppTypeToCsType(PropertyConfig property)
        {
            var type = property.CppTypeObject = ToCppLastType(property.CppLastType ?? property.CppType);
            if (type == null)
                return null;
            if (type is EntityConfig stru)
            {
                return stru.Name;
            }
            var typedef = type as TypedefItem;
            string typ = typedef == null ? type.ToString() : typedef.KeyWork;
            var len = typedef == null
                ? typ == "char"
                    ? property.Datalen.ToString()
                    : property.ArrayLen
                : typedef.ArrayLen;
            var arr = int.TryParse(len, out int l) && l > 1;

            switch (typ)
            {
                case "char":
                    if (arr)
                        return "string";
                    break;
                case "tm":
                    return "DateTime";
            }
            var tp = CppTypeToCs(typ);
            return arr ? $"List<{tp}>" : tp;
        }
        #endregion


        #region C++类型帮助
        /// <summary>
        /// C++类型与C#类型对应
        /// </summary>
        /// <param name="type">C++类型</param>
        /// <returns>C#类型</returns>
        public static string CppTypeToCs(string type)
        {
            type = type.MulitReplace2("", " ", "\t").ToLower();
            switch (type)
            {
                case "char":
                case "__int8":
                    return "byte";
                case "unsigned__int8":
                    return "ubyte";
                case "wchar":
                case "wchar_t":
                case "char16_t":
                    return "char";
                case "__int16":
                    return "short";
                case "unsignedlong":
                case "unsignedint":
                case "unsigned__int32":
                    return "uint";
                case "unsigned__int16":
                case "unsignedshort":
                    return "ushort";
                case "longlong":
                case "__int64":
                    return "long";
                case "unsigned__int64":
                case "unsignedlonglong":
                    return "ulong";
                case "__int32":
                    return "int";
                default:
                    return type;
            }
        }
        /// <summary>
        /// 是否C++的基本类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCppBaseType(string type)
        {
            switch (type.MulitReplace2("", " ", "\t").ToLower())
            {
                case "bool":
                case "double":
                case "float":

                case "char":
                case "wchar":
                case "wchar_t":
                case "char16_t":
                case "char32_t":
                case "unsignedchar":

                case "uint":
                case "int":
                case "unsignedint":
                case "short":
                case "ushort":
                case "unsignedshort":
                case "long":
                case "ulong":
                case "unsignedlong":
                case "longlong":
                case "unsignedlonglong":


                case "__int8":
                case "unsigned__int8":
                case "__int16":
                case "unsigned__int16":
                case "__int32":
                case "unsigned__int32":
                case "__int64":
                case "unsigned__int64":
                    return true;
                default:
                    return false;
            }
        }


        #endregion
    }
}
