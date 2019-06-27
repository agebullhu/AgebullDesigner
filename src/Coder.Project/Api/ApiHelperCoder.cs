using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// API代码辅助
    /// </summary>
    public class ApiHelperCoder : MomentCoderBase
    {
        /// <summary>
        /// 输入值转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string InputConvert(EntityConfig entity)
        {
            if (entity.IsUiReadOnly)
                return null;
            var fields = entity.ClientProperty.Where(p => p.CanUserInput || p.ExtendConfigListBool["easyui", "userFormHide"]).ToArray();
            var code = new StringBuilder();
            foreach (var group in fields.GroupBy(p => p.Group))
            {

                code.Append($@"
            //{group.Key ?? "普通字段"}");
                foreach (var field in group.OrderBy(p => p.Index))
                {
                    code.Append(@"
            if(");
                    if (field.IsPrimaryKey || field.KeepUpdate)
                    {
                        code.Append(@"!convert.IsUpdata && ");

                    }
                    if (field.KeepStorageScreen == StorageScreenType.Insert)
                    {
                        code.Append(@"convert.IsUpdata && ");
                    }
                    switch (field.DataType)
                    {
                        case "ByteArray" when field.IsImage:
                            code.Append($@"convert.TryGetValue(""{field.JsonName}"" , out string file))
            {{
                if (string.IsNullOrWhiteSpace(file))
                    data.{field.Name}_Base64 = null;
                else if(file != ""*"" && file.Length< 100 && file[0] == '/')
                {{
                    using(var call = new WebApiCaller(ConfigurationManager.AppSettings[""ManageAddress""]))
                    {{
                        var result = call.Get<string>(""api/v1/ueditor/action"", $""action=base64&url={{file}}"");
                        data.{field.Name}_Base64 = result.Success ? result.ResultData : null;
                    }}
                }}
            }}");
                            continue;
                        case "ByteArray":
                            code.Append($@"convert.TryGetValue(""{field.JsonName}"" , out string {field.Name}))
                data.{field.Name}_Base64 = {field.Name};");
                            continue;
                    }
                    if (field.IsEnum && !string.IsNullOrWhiteSpace(field.CustomType))
                    {
                        code.Append($@"convert.TryGetValue(""{field.JsonName}"" , out {field.CsType} {field.Name}))
                data.{field.Name} = ({field.CustomType}){field.Name};");
                    }
                    else if(!string.IsNullOrWhiteSpace(field.CustomType))
                    {
                        code.Append($@"convert.TryGetValue(""{field.JsonName}"" , out {field.CsType} {field.Name}))
                data.{field.Name} = ({field.CustomType}){field.Name};");
                    }
                    else
                    {
                        code.Append($@"convert.TryGetValue(""{field.JsonName}"" , out {field.CsType} {field.Name}))
                data.{field.Name} = {field.Name};");
                    }
                }
            }
            return code.ToString();
        }
        /*
         
        /// <summary>
        /// 输入值转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string InputConvert(EntityConfig entity)
        {
            if (entity.IsUiReadOnly)
                return null;

            var code = new StringBuilder();
            foreach (var group in entity.ClientProperty.Where(p =>
                p.KeepStorageScreen != StorageScreenType.All &&
               (p.ExtendConfigListBool["easyui", "userFormHide"] || p.CanUserInput)).GroupBy(p => p.Group))
            {

                code.Append($@"
            //{group.Key ?? "普通字段"}");
                foreach (var field in group.OrderBy(p => p.Index))
                {
                    if (field.IsPrimaryKey || field.KeepUpdate)
                    {
                        code.Append(@"
            if(!convert.IsUpdata)");

                    }
                    if (field.KeepStorageScreen == StorageScreenType.Insert)
                    {
                        code.Append(@"
            if(convert.IsUpdata)");

                    }
                    string type = field.CsType.ToLower();
                    switch (field.DataType)
                    {
                        case "ByteArray" when field.IsImage:
                            code.Append($@"
            {{
                var file = convert.ToString(""{field.JsonName}"");
                
                if (string.IsNullOrWhiteSpace(file))
                    data.{field.Name}_Base64 = null;
                else if(file != ""*"" && file.Length< 100 && file[0] == '/')
                {{
                    var call = new WebApiCaller(ConfigurationManager.AppSettings[""ManageAddress""]);
                    var result = call.Get<string>(""api/v1/ueditor/action"", $""action=base64&url={{file}}"");
                    data.{field.Name}_Base64 = result.Success ? result.ResultData : null;
                }}
            }}");
                            continue;
                        case "ByteArray":
                            type = "string";
                            code.Append($@"
            data.{field.Name}_Base64 = ");
                            break;
                        default:
                            code.Append($@"
            data.{field.Name} = ");
                            break;
                    }
                    if (field.IsEnum && !string.IsNullOrWhiteSpace(field.CustomType))
                    {
                        code.Append($@"({field.CustomType})convert.ToInteger(""{field.JsonName}"");");
                        continue;
                    }
                    switch (type)
                    {
                        case "short":
                        case "int16":
                        case "int":
                        case "int32":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullInteger(""{field.JsonName}"");"
                                : $@"convert.ToInteger(""{field.JsonName}"");");
                            break;
                        case "bigint":
                        case "long":
                        case "int64":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullLong(""{field.JsonName}"");"
                                : $@"convert.ToLong(""{field.JsonName}"");");
                            break;
                        case "decimal":
                        case "numeric":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullDecimal(""{field.JsonName}"");"
                                : $@"convert.ToDecimal(""{field.JsonName}"");");
                            break;
                        case "real":
                        case "double":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullDouble(""{field.JsonName}"");"
                                : $@"convert.ToDouble(""{field.JsonName}"");");
                            break;
                        case "float":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullSingle(""{field.JsonName}"");"
                                : $@"convert.ToSingle(""{field.JsonName}"");");
                            break;
                        case "datetime":
                        case "datetime2":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullDateTime(""{field.JsonName}"");"
                                : $@"convert.ToDateTime(""{field.JsonName}"");");
                            break;
                        case "bool":
                        case "boolean":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullBoolean(""{field.JsonName}"");"
                                : $@"convert.ToBoolean(""{field.JsonName}"");");
                            break;
                        case "byte":
                        case "Byte":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullByte(""{field.JsonName}"");"
                                : $@"convert.ToByte(""{field.JsonName}"");");
                            break;
                        case "sbyte":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullSByte(""{field.JsonName}"");"
                                : $@"convert.ToSByte(""{field.JsonName}"");");
                            break;
                        case "guid":
                        case "uniqueidentifier":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullGuid(""{field.JsonName}"");"
                                : $@"convert.ToGuid(""{field.JsonName}"");");
                            break;
                        default:
                            code.Append($@"convert.ToString(""{field.JsonName}"");");
                            break;
                    }
                }
            }
            return code.ToString();
        }
        public static string InputConvert2(EntityConfig entity)
        {
            var code = new StringBuilder();
            code.AppendFormat(@"
        static void ReadFormValue(I{0} entity, FormConvert convert)
        {{", entity.Name);
            foreach (PropertyConfig field in entity.PublishProperty.Where(p => !p.IsPrimaryKey))
            {
                switch (field.CsType.ToLower())
                {
                    case "short":
                    case "int16":
                    case "int":
                    case "int32":
                        code.AppendFormat(field.Nullable
                                ? @"
            entity.{0} = convert.ToNullInteger(""{0}"");"
                                : @"
            entity.{0} = convert.ToInteger(""{0}"");"
                            , field.JsonName);
                        break;
                    case "bigint":
                    case "long":
                    case "int64":
                        code.AppendFormat(field.Nullable
                                ? @"
            entity.{0} = convert.ToNullLong(""{0}"");"
                                : @"
            entity.{0} = convert.ToLong(""{0}"");"
                            , field.JsonName);
                        break;
                    case "decimal":
                    case "numeric":
                        code.AppendFormat(field.Nullable
                                ? @"
            entity.{0} = convert.ToNullDecimal(""{0}"");"
                                : @"
            entity.{0} = convert.ToDecimal(""{0}"");"
                            , field.JsonName);
                        break;
                    case "real":
                    case "double":
                        code.Append(field.Nullable
                            ? $@"
            entity.{field.JsonName} = convert.ToNullDouble(""{field.JsonName}"");"
                            : $@"
            entity.{field.JsonName} = convert.ToDouble(""{field.JsonName}"");");
                        break;
                    case "float":
                        code.Append(field.Nullable
                            ? $@"
            entity.{field.JsonName} = convert.ToNullSingle(""{field.JsonName}"");"
                            : $@"
            entity.{field.JsonName} = convert.ToSingle(""{field.JsonName}"");");
                        break;
                    case "datetime":
                    case "datetime2":
                        code.Append(field.Nullable
                            ? $@"
            entity.{field.JsonName} = convert.ToNullDateTime(""{field.JsonName}"");"
                            : $@"
            entity.{field.JsonName} = convert.ToDateTime(""{field.JsonName}"");");
                        break;
                    case "bool":
                    case "boolean":
                        code.Append(field.Nullable
                            ? $@"
            entity.{field.JsonName} = convert.ToNullBoolean(""{field.JsonName}"");"
                            : $@"
            entity.{field.JsonName} = convert.ToBoolean(""{field.JsonName}"");");
                        break;
                    case "guid":
                    case "uniqueidentifier":
                        code.Append(field.Nullable
                            ? $@"
            entity.{field.JsonName} = convert.ToNullGuid(""{field.JsonName}"");"
                            : $@"
            entity.{field.JsonName} = convert.ToGuid(""{field.JsonName}"");");
                        break;
                    //case "byte":
                    //case "char":
                    //case "nchar":
                    //case "varchar":
                    //case "nvarchar":
                    //case "string":
                    //case "text":
                    default:
                        code.Append($@"
            entity.{field.JsonName} = convert.ToString(""{field.JsonName}"",{(field.Nullable ? "true" : "false")});");
                        break;
                }
            }
            code.Append(@"
        }");
            return code.ToString();
        }


        public static string InputConvert3(PropertyConfig column, string arg)
        {
            switch (column.CsType)
            {
                case "string":
                case "String":
                    return string.Format("{0} == null ? null : {0}.ToString()", arg);
                case "long":
                case "Int64":
                    if (column.Nullable)
                        return string.Format("{0} == null ? null : (long?)Convert.ToInt64({0})", arg);
                    return $"Convert.ToInt64({arg})";
                case "int":
                case "Int32":
                    if (column.Nullable)
                        return string.Format("{0} == null ? null : (int?)Convert.ToInt32({0})", arg);
                    return $"Convert.ToInt32({arg})";
                case "decimal":
                case "Decimal":
                    if (column.Nullable)
                        return string.Format("{0} == null ? null : (decimal?)Convert.ToDecimal({0})", arg);
                    return $"Convert.ToDecimal({arg})";
                case "float":
                case "Float":
                    if (column.Nullable)
                        return string.Format("{0} == null ? null : (float?)Convert.ToSingle({0})", arg);
                    return $"Convert.ToSingle({arg})";
                case "bool":
                case "Boolean":
                    if (column.Nullable)
                        return string.Format("{0} == null ? null : (bool?)Convert.ToBoolean({0})", arg);
                    return $"Convert.ToBoolean({arg})";
                case "DateTime":
                    if (column.Nullable)
                        return string.Format("{0} == null ? null : (DateTime?)Convert.ToDateTime({0})", arg);
                    return $"Convert.ToDateTime({arg})";
            }
            return $"({column.LastCsType}){arg}";
        }
        */
    }
}
