using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityDictionaryBuilder : EntityBuilderBase
    {
        #region 基础

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => false;

        public override string BaseCode => $@"
        #region 名称的属性操作
{GetSetValues()}
        #endregion";

        protected override string Folder => "Dictionary";

        #endregion


        #region 名称值取置

        /// <summary>
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        private string GetSetValues()
        {
            var code = new StringBuilder();
            SetStringValues(code);
            SetValues(code);
            GetValues(code);

            return code.ToString();
        }

        private void GetValues(StringBuilder code)
        {
            code.Append(@"

        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name=""property""></param>
        protected override object GetValueInner(string property)
        {
            switch(property)
            {");

            foreach (PropertyConfig field in Columns)
            {
                var names = field.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = field.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alias in names)
                    code.Append($@"
            case ""{alias}"":");
                code.Append(field.EnumConfig == null
                    ? $@"
                return this.{field.Name};"
                    : $@"
                return this.{field.Name}.ToCaption();");
            }
            code.AppendLine(@"
            }");
            code.AppendLine(!string.IsNullOrWhiteSpace(Entity.ModelBase)
                ? @"
            return base.GetValueInner(property);
        }"
                : @"
            return null;
        }");
            if (IsClient)
                return;
            code.Append(@"

        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name=""index""></param>
        protected override object GetValueInner(int index)
        {
            switch(index)
            {");

            foreach (PropertyConfig property in Entity.PublishProperty)
            {
                code.AppendFormat(@"
                case _DataStruct_.{0}:
                    return this.{0};", property.Name);
            }
            code.AppendLine(@"
            }");
            code.Append(!string.IsNullOrWhiteSpace(Entity.ModelBase)
                ? @"
            return base.GetValueInner(index);"
                : @"
            return null;");
            code.AppendLine(@"
        }");

        }


        /// <summary>
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        private void SetValues(StringBuilder code)
        {
            code.Append(@"
    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name=""property""></param>
        /// <param name=""value""></param>
        protected override void SetValueInner(string property, object value)
        {
            if(property == null) return;
            switch(property.Trim().ToLower())
            {");

            foreach (PropertyConfig field in ReadWriteColumns)
            {
                var names = field.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = field.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alia in names)
                    code.Append($@"
            case ""{alia}"":");

                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
                if (value != null)
                {{
                    if(value is int)
                    {{
                        this.{field.Name} = ({field.CustomType})(int)value;
                    }}
                    else if(value is {field.CustomType})
                    {{
                        this.{field.Name} = ({field.CustomType})value;
                    }}
                    else
                    {{
                        var str = value.ToString();
                        {field.CustomType} val;
                        if ({field.CustomType}.TryParse(str, out val))
                        {{
                            this.{field.Name} = val;
                        }}
                        else
                        {{
                            int vl;
                            if (int.TryParse(str, out vl))
                            {{
                                this.{field.Name} = ({field.CustomType})vl;
                            }}
                        }}
                    }}
                }}
                return;");
                    continue;
                }

                switch (field.CsType)
                {
                    case "bool":
                    case "Boolean":
                        code.Append($@"
                if (value != null)
                {{
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {{
                        this.{field.Name} = vl != 0;
                    }}
                    else
                    {{
                        this.{field.Name} = Convert.ToBoolean(value);
                    }}
                }}
                return;");
                        continue;
                    case "int":
                    case "long":
                        code.Append($@"
                this.{field.Name} = ({field.CsType})Convert.ToDecimal(value);
                return;");
                        break;
                    default:
                        code.Append($@"
                this.{field.Name} = {ConvertCode(field, "value")};
                return;");
                        break;
                }
            }
            code.AppendLine(@"
            }");

            code.AppendLine(!string.IsNullOrWhiteSpace(Entity.ModelBase)
                ? @"
            base.SetValueInner(property,value);"
                : @"
            //System.Diagnostics.Trace.WriteLine(property + @""=>"" + value);");
            code.AppendLine(@"
        }");

            if (IsClient)
                return;
            code.Append(@"
    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name=""index""></param>
        /// <param name=""value""></param>
        protected override void SetValueInner(int index, object value)
        {
            switch(index)
            {");

            foreach (PropertyConfig field in ReadWriteColumns)
            {
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
            case _DataStruct_.{field.Name}:
                this.{field.Name} = ({field.CustomType})value;
                return;");
                    continue;
                }
                code.Append($@"
            case _DataStruct_.{field.Name}:
                this.{field.Name} = {ConvertCode(field, "value")};
                return;");
            }
            code.Append(@"
            }");
            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.Append(@"
            base.SetValueInner(index,value);");
            code.AppendLine(@"
        }");
        }
        private string ConvertCode(PropertyConfig column, string arg)
        {
            switch (column.CsType)
            {
                case "string":
                case "String":
                    return $"{arg} == null ? null : {arg}.ToString()";
                case "long":
                case "Int64":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (long?)Convert.ToInt64({arg})";
                    return $"Convert.ToInt64({arg})";
                case "int":
                case "Int32":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (int?)Convert.ToInt32({arg})";
                    return $"Convert.ToInt32({arg})";
                case "decimal":
                case "Decimal":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (decimal?)Convert.ToDecimal({arg})";
                    return $"Convert.ToDecimal({arg})";
                case "float":
                case "Float":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (float?)Convert.ToSingle({arg})";
                    return $"Convert.ToSingle({arg})";
                case "bool":
                case "Boolean":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (bool?)Convert.ToBoolean({arg})";
                    return $"Convert.ToBoolean({arg})";
                case "DateTime":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (DateTime?)Convert.ToDateTime({arg})";
                    return $"Convert.ToDateTime({arg})";
            }
            return $"({column.LastCsType}){arg}";
        }



        /// <summary>
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        private void SetStringValues(StringBuilder code)
        {
            code.Append(@"
    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name=""property""></param>
        /// <param name=""value""></param>
        protected override bool SetValueInner(string property, string value)
        {
            if(property == null) return false;
            switch(property.Trim().ToLower())
            {");

            foreach (PropertyConfig field in ReadWriteColumns)
            {
                var names = field.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = field.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alia in names)
                    code.Append($@"
            case ""{alia}"":");

                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
                if (!string.IsNullOrWhiteSpace(value))
                {{
                    if ({field.CustomType}.TryParse(value, out {field.CustomType} val))
                    {{
                        this.{field.Name} = val;
                        return true;
                    }}
                    else if (int.TryParse(value, out int vl))
                    {{
                        this.{field.Name} = ({field.CustomType})vl;
                        return true;
                    }}
                }}
                return false;");
                    continue;
                }

                switch (field.CsType)
                {
                    case "string":
                    case "String":
                        code.Append($@"
                this.{field.Name} = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;");
                        continue;
                    case "byte[]":
                    case "Byte[]":
                        code.Append($@"
                this.{field.Name} = string.IsNullOrWhiteSpace(value) ? null : Convert.FromBase64String(value);
                return true;");
                        continue;
                    default:
                        code.Append($@"
                if (!string.IsNullOrWhiteSpace(value))
                {{
                    if ({field.CsType}.TryParse(value, out var vl))
                    {{
                        this.{field.Name} = vl;
                        return true;
                    }}
                }}
                return false;");
                        continue;
                }
            }
            code.AppendLine(@"
            }
            return false;
        }");
        }
        #endregion
    }
}