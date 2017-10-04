using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.EasyUi;

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
        protected string GetSetValues()
        {
            var code = new StringBuilder();
            SetValues(code);
            GetValues(code);

            return code.ToString();
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
            switch(property.Trim().ToLower())
            {");

            foreach (PropertyConfig field in ReadWriteColumns)
            {
                var names = field.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = field.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alias in names)
                    code.Append($@"
            case ""{alias}"":");

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

                if (field.CsType == "bool" || field.CsType == "Boolean")
                {

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
                }
                if (field.CsType == "int" || field.CsType == "long")
                {
                    code.Append($@"
                this.{field.Name} = ({field.CsType})Convert.ToDecimal(value);
                return;");
                }
                else
                {
                    code.Append($@"
                this.{field.Name} = {ConvertCode(field, "value")};
                return;");
                }
            }
            code.AppendLine(@"
            }");

            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.AppendLine(@"
            base.SetValueInner(property,value);");
            else
                code.AppendLine(@"
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
            /*switch(index)
            {");

            foreach (PropertyConfig field in ReadWriteColumns)
            {
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.AppendFormat(@"
            case Index_{0}:
                this.{0} = ({1})value;
                return;", field.Name, field.CustomType);
                    continue;
                }
                code.AppendFormat(@"
            case Index_{0}:
                this.{0} = {1};
                return;", field.Name, ConvertCode(field, "value"));
            }
            code.Append(@"
            }");
            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.Append(@"
            base.SetValueInner(index,value);");
            code.AppendLine(@"*/
        }");
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
                if (field.EnumConfig == null)
                    code.AppendFormat(@"
                return this.{0};", field.Name);
                else
                    code.AppendFormat(@"
                return this.{0}.ToCaption();", field.Name);
            }
            code.AppendLine(@"
            }");
            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.AppendLine(@"
            return base.GetValueInner(property);
        }");
            else
                code.AppendLine(@"
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
            /*switch(index)
            {");

            foreach (PropertyConfig property in Entity.PublishProperty)
            {
                code.AppendFormat(@"
                case Index_{0}:
                    return this.{0};", property.Name);
            }
            code.AppendLine(@"
            }*/");
            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.Append(@"
            return base.GetValueInner(index);");
            else
                code.Append(@"
            return null;");
            code.AppendLine(@"
        }");

        }

        private string ConvertCode(PropertyConfig column, string arg)
        {
            return EasyUiHelperCoder.InputConvert3(column, arg);
        }
        #endregion
    }
}