using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{

    public class EasyUiHelperCoder : MomentCoderBase
    {
        public string InputConvert()
        {
            var code = new StringBuilder();
            foreach (var group in Entity.ClientProperty.Where(p => p["user_form_hide"] == "True" || p.CanUserInput && !p.IsUserReadOnly).GroupBy(p => p.Group))
            {
                code.Append($@"
            //{group.Key ?? "数据֪"}");
                foreach (var field in group.OrderBy(p => p.Index))
                {
                    code.Append($@"
            data.{field.Name} = ");
                    if (!string.IsNullOrWhiteSpace(field.CustomType))
                    {
                        code.AppendFormat(@"({0})convert.ToInteger(""{1}"");", field.CustomType, field.Name);
                        continue;
                    }
                    switch (field.CsType.ToLower())
                    {
                        case "short":
                        case "int16":
                        case "int":
                        case "int32":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullInteger(""{field.Name}"");"
                                : $@"convert.ToInteger(""{field.Name}"");");
                            break;
                        case "bigint":
                        case "long":
                        case "int64":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullLong(""{field.Name}"");"
                                : $@"convert.ToLong(""{field.Name}"");");
                            break;
                        case "decimal":
                        case "numeric":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullDecimal(""{field.Name}"");"
                                : $@"convert.ToDecimal(""{field.Name}"");");
                            break;
                        case "real":
                        case "double":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullDouble(""{field.Name}"");"
                                : $@"convert.ToDouble(""{field.Name}"");");
                            break;
                        case "float":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullSingle(""{field.Name}"");"
                                : $@"convert.ToSingle(""{field.Name}"");");
                            break;
                        case "datetime":
                        case "datetime2":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullDateTime(""{field.Name}"");"
                                : $@"convert.ToDateTime(""{field.Name}"");");
                            break;
                        case "bool":
                        case "boolean":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullBoolean(""{field.Name}"");"
                                : $@"convert.ToBoolean(""{field.Name}"");");
                            break;
                        case "byte":
                        case "Byte":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullByte(""{field.Name}"");"
                                : $@"convert.ToByte(""{field.Name}"");");
                            break;
                        case "sbyte":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullSByte(""{field.Name}"");"
                                : $@"convert.ToSByte(""{field.Name}"");");
                            break;
                        case "guid":
                        case "uniqueidentifier":
                            code.Append(field.Nullable
                                ? $@"convert.ToNullGuid(""{field.Name}"");"
                                : $@"convert.ToGuid(""{field.Name}"");");
                            break;
                        //case "byte":
                        //case "char":
                        //case "nchar":
                        //case "varchar":
                        //case "nvarchar":
                        //case "string":
                        //case "text":
                        default:
                            code.Append($@"convert.ToString(""{field.Name}"");");
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
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullInteger(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToInteger(""{0}"");"
                                , field.Name);
                        break;
                    case "bigint":
                    case "long":
                    case "int64":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullLong(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToLong(""{0}"");"
                                , field.Name);
                        break;
                    case "decimal":
                    case "numeric":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDecimal(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDecimal(""{0}"");"
                                , field.Name);
                        break;
                    case "real":
                    case "double":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDouble(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDouble(""{0}"");"
                                , field.Name);
                        break;
                    case "float":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullSingle(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToSingle(""{0}"");"
                                , field.Name);
                        break;
                    case "datetime":
                    case "datetime2":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDateTime(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDateTime(""{0}"");"
                                , field.Name);
                        break;
                    case "bool":
                    case "boolean":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullBoolean(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToBoolean(""{0}"");"
                                , field.Name);
                        break;
                    case "guid":
                    case "uniqueidentifier":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullGuid(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToGuid(""{0}"");"
                                , field.Name);
                        break;
                    //case "byte":
                    //case "char":
                    //case "nchar":
                    //case "varchar":
                    //case "nvarchar":
                    //case "string":
                    //case "text":
                    default:
                        code.AppendFormat(@"
            entity.{0} = convert.ToString(""{0}"",{1});"
                            , field.Name
                            , field.Nullable ? "true" : "false");
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


        public static string InputConvert4(EntityConfig entity)
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
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullInteger(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToInteger(""{0}"");"
                                , field.Name);
                        break;
                    case "bigint":
                    case "long":
                    case "int64":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullLong(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToLong(""{0}"");"
                                , field.Name);
                        break;
                    case "decimal":
                    case "numeric":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDecimal(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDecimal(""{0}"");"
                                , field.Name);
                        break;
                    case "real":
                    case "double":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDouble(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDouble(""{0}"");"
                                , field.Name);
                        break;
                    case "float":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullSingle(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToSingle(""{0}"");"
                                , field.Name);
                        break;
                    case "datetime":
                    case "datetime2":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullDateTime(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToDateTime(""{0}"");"
                                , field.Name);
                        break;
                    case "bool":
                    case "boolean":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullBoolean(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToBoolean(""{0}"");"
                                , field.Name);
                        break;
                    case "guid":
                    case "uniqueidentifier":
                        if (field.Nullable)
                            code.AppendFormat(@"
            entity.{0} = convert.ToNullGuid(""{0}"");"
                                , field.Name);
                        else
                            code.AppendFormat(@"
            entity.{0} = convert.ToGuid(""{0}"");"
                                , field.Name);
                        break;
                    //case "byte":
                    //case "char":
                    //case "nchar":
                    //case "varchar":
                    //case "nvarchar":
                    //case "string":
                    //case "text":
                    default:
                        code.AppendFormat(@"
            entity.{0} = convert.ToString(""{0}"",{1});"
                            , field.Name
                            , field.Nullable ? "true" : "false");
                        break;
                }
            }
            code.Append(@"
        }");
            return code.ToString();
        }
    }
}
