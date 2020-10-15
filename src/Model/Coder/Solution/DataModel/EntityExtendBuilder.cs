using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.WebApplition
{
    public sealed class EntityExtendBuilder : CoderWithEntity
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string Name => "File_Model_EntityExtend_cs";

        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            string file = Path.Combine(path, Entity.Name + ".Designer.cs");

            string code =
                $@"using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Gboxt.Common.DataModel;

namespace {NameSpace}
{{
	partial class {Entity.EntityName}
	{{
{ExtendCode()}
{ValidateCode()}
    }}
}}";

            SaveCode(file, code);
        }
        /// <summary>
        ///     ���ɴ���
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string file = Path.Combine(path, Entity.Name + ".cs");

            string code =
                $@"using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Gboxt.Common.DataModel;

namespace {NameSpace}
{{
	partial class {Entity.EntityName}
	{{
        /*// <summary>
        /// ��չУ��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        partial void ValidateEx(ValidateResult result)
        {{
            
        }}*/
    }}
}}";

            SaveCode(file, code);
        }

        #region ��չ����

        private string ExtendCode()
        {
            var code = new StringBuilder();
            foreach (PropertyConfig field in Entity.PublishProperty.Where(p => !string.IsNullOrWhiteSpace(p.ExtendRole)))
            {
                code.AppendFormat(@"
        #region {0}{1}
        #endregion
"
                    , field.Caption
                    , ExtendCodeCode(field));
            }

            return code.ToString();
        }

        private string ExtendCodeCode(PropertyConfig field)
        {
            return field.IsRelation ? RelationCode(field) : SubClassCode(field);
        }

        private string RelationCode(PropertyConfig field)
        {
            var friend = Entities.FirstOrDefault(p => p.Name.Equals(field.ExtendRole, StringComparison.OrdinalIgnoreCase));
            if (friend == null)
            {
                return null;// throw new Exception(string.Format("{0}���ֶ�{1}�������ı�{2}������", Entity.Caption, field.Caption, field.ExtendRole));
            }

            if (field.ExtendArray)
            {
                return string.Format(@"
        List<long> _l{3};
        /// <summary>
        /// ��Ӧ��{0}��
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public List<long> {5}
        {{
            get
            {{
                if(_l{3} != null)
                    return _l{3};
                if(string.IsNullOrWhiteSpace({2}))
                    return _l{3}=new List<long>();
                return _l{3}= {2}.Split(new[] {{ '{4}' }}, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            }}
        }}
        IList<{1}> _{3};
        /// <summary>
        /// ��Ӧ��{0}��
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public IList<{1}> {3}
        {{
            get
            {{
                if(_{3} != null)
                    return _{3};
                if({5}.Count == 0)
                    return _{3}=new List<{1}>();
                return _{3}={1}.GetByIds({5});
            }}
        }}"
                       , friend.Caption
                       , friend.Name
                       , field.Name
                       , string.IsNullOrWhiteSpace(field.ExtendPropertyName) ? (field.Name + friend.Name) : field.ExtendPropertyName
                       , string.IsNullOrWhiteSpace(field.ValueSeparate) ? "," : field.ValueSeparate
                       , field.Name.ToPluralism());
            }


            return string.Format(@"
        {1} _{3};
        /// <summary>
        /// ��Ӧ��{0}��
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public {1} {3}
        {{
            get
            {{
                return {2} <= 0 ? null : (_{3} ?? (_{3} = {1}.GetById({2})));
            }}
        }}"
                   , friend.Caption, friend.Name, field.Name, field.ExtendPropertyName ?? ("V_" + field.Name));
        }

        private string SubClassCode(PropertyConfig field)
        {
            StringBuilder code = new StringBuilder();
            var chFields = GetChildFields(field);
            if (field.IsKeyValueArray)
            {
                SubClassDefault(code, field, chFields);
                KeyValueArrayCode(code, field, chFields);
            }
            else if (field.ExtendArray)
            {
                if (chFields.Count == 1)
                    SignleArrayCode(code, field);
                else
                {
                    SubClassDefault(code, field, chFields);
                    ExtendArrayCode(code, field, chFields);
                }
            }
            else
            {
                SubClassDefault(code, field, chFields);
                ExtendCode(code, field, chFields);
            }
            return code.ToString();
        }

        private Dictionary<string, object> GetChildFields(PropertyConfig field)
        {
            var propertys = field.ExtendRole.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var chFields = new Dictionary<string, object>();
            foreach (var property in propertys)
            {
                var pp = property.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                object type;
                if (pp.Length == 1 || string.IsNullOrWhiteSpace(pp[1]))
                    type = "string";
                else
                {
                    pp[1] = pp[1].Trim();
                    switch (pp[1][0])
                    {
                        case '#': //����
                            var friend = Entities.FirstOrDefault(p => p.Name.Equals(pp[1].Substring(1), StringComparison.OrdinalIgnoreCase));
                            if (friend == null)
                            {
                                throw new Exception($"{Entity.Caption}���ֶ�{field.Caption}�������ı�{pp[1].Substring(1)}������");
                            }
                            type = friend;
                            break;
                        case '%': //С��
                            type = "decimal";
                            break;
                        case '*': //����
                            type = "int";
                            break;
                        case '@': //����
                            type = "DataTime";
                            break;
                        default: //�ı�
                            type = "string";
                            break;
                    }
                }
                chFields.Add(pp[0], type);
            }
            return chFields;
        }

        private void SubClassDefault(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
        {
            if (field.ExtendClassIsPredestinate)
                return;
            code.AppendFormat(@"
        public sealed class {0}
        {{", field.ExtendClassName ?? ("X_" + field.Name));
            foreach (var property in chFields)
            {
                var tableSchema = property.Value as EntityConfig;
                if (tableSchema != null)
                {
                    code.AppendFormat(@"
            private {0} _{1};
            public {0} {2} 
            {{  
                get
                {{
                    return _{1};
                }}
                set
                {{
                    _{1} = value;
                }}
            }}
            
            /// <summary>
            /// {5}�Ľ���ֵ
            /// </summary>
            public {3} {3}_{2}
            {{  
                get
                {{
                    return {3}.GetById({2});
                }}
                set
                {{
                    {2} = value == null ? 0 : value.{4};
                }}
            }}"
                        , tableSchema.PrimaryColumn.LastCsType
                        , property.Key.ToLower()
                        , property.Key
                        , tableSchema.Name
                        , tableSchema.PrimaryColumn
                        , tableSchema.Caption);
                }
                else
                {
                    code.AppendFormat(@"

            public {0} {1} {{ get;set;}}", property.Value, property.Key);
                }

            }
            code.Append(@"
        }");
        }

        private void KeyValueArrayCode(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
        {
            code.AppendFormat(@"

        List<{4}> _{5};
        /// <summary>
        /// {0}�Ľ���ֵ
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public List<{4}> {5}
        {{
            get
            {{
                if(_{5} != null)
                    return _{5};
                _{5} = new List<{4}>();
                if(string.IsNullOrWhiteSpace({1}))
                    return _{5};
                var ov = {1}.Split(new[] {{ '{2}' }}, StringSplitOptions.RemoveEmptyEntries);
                var ovs = ov.Select(p => p.Split(new[] {{ '{3}' }}, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                for(int index = 0;index < ovs[0].Length;index++)
                {{
                    _{5}.Add(new {4}
                    {{"
                        , field.Caption
                        , field.Name
                        , string.IsNullOrWhiteSpace(field.ArraySeparate) ? "#" : field.ArraySeparate
                        , string.IsNullOrWhiteSpace(field.ValueSeparate) ? "," : field.ValueSeparate
                        , field.ExtendClassName ?? ("X_" + field.Name)
                        , field.ExtendPropertyName ?? ("V_" + field.Name));
            int idx = 0;
            foreach (var kv in chFields)
            {
                var tableSchema = kv.Value as EntityConfig;
                if (tableSchema != null)
                {
                    code.AppendFormat(@"
                    {0} = ovs.Length <= {2} ? default({1}) : {1}.Parse(ovs[{2}][index]),", kv.Key, tableSchema.PrimaryColumn.CsType, idx++);
                }
                else
                {
                    string type = kv.Value.ToString();
                    if (type == "string")
                        code.AppendFormat(@"
                    {0} = ovs.Length <= {1} ? null : ovs[{1}][index],", kv.Key, idx++);
                    else
                        code.AppendFormat(@"
                    {0} = ovs.Length <= {2} ? default({1}) : {1}.Parse(ovs[{2}][index]),", kv.Key, type, idx++);
                }
            }
            code.AppendFormat(@"
                    }});
                }}
                return _{0};
            }}
        }}", field.ExtendPropertyName ?? ("V_" + field.Name));
        }

        private void SignleArrayCode(StringBuilder code, PropertyConfig field)
        {
            code.AppendFormat(@"

        List<{4}> _l{2};

        /// <summary>
        /// ��Ӧ��{0}�б�ֵ
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public List<{4}> {2}
        {{
            get
            {{
                if(_l{2} != null)
                    return _l{2};
                if(string.IsNullOrWhiteSpace({1}))
                    return _l{2}=new List<{4}>();
                return _l{2}= {1}.Split(new[] {{ '{3}' }}, StringSplitOptions.RemoveEmptyEntries).Select({4}.Parse).ToList();
            }}
        }}"
                   , field.Caption
                   , field.Name
                   , string.IsNullOrWhiteSpace(field.ExtendPropertyName) ? field.Name.ToPluralism() : field.ExtendPropertyName
                   , string.IsNullOrWhiteSpace(field.ValueSeparate) ? "," : field.ValueSeparate
                   , string.IsNullOrWhiteSpace(field.ExtendClassName) ? "int" : field.ExtendClassName);
        }

        private void ExtendArrayCode(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
        {
            if (field.ExtendClassIsPredestinate)
            {
                code.AppendFormat(@"

        List<{3}> _{4};
        /// <summary>
        /// {0}�Ľ���ֵ
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public List<{3}> {4}
        {{
            get
            {{
                if(_{4} != null)
                    return _{4};
                _{4} = new List<{3}>();
                if(string.IsNullOrWhiteSpace({1}))
                    return _{4};
                var ov = {1}.Split(new[] {{ '{2}' }}, StringSplitOptions.RemoveEmptyEntries);
                foreach(var vl in ov)
                {{
                    _{4}.Add(new {3}(vl));
                }}
                return _{4};
            }}
        }}"
                        , field.Caption
                        , field.Name
                        , string.IsNullOrWhiteSpace(field.ArraySeparate) ? "#" : field.ArraySeparate
                        , field.ExtendClassName
                        , field.ExtendPropertyName ?? ("V_" + field.Name));
                return;
            }
            code.AppendFormat(@"
        List<{4}> _{5};
        /// <summary>
        /// {0}�Ľ���ֵ
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public List<{4}> {5}
        {{
            get
            {{
                if(_{5} != null)
                    return _{5};
                _{5} = new List<{4}>();
                if(string.IsNullOrWhiteSpace({1}))
                    return _{5};
                var ov = {1}.Split(new[] {{ '{2}' }}, StringSplitOptions.RemoveEmptyEntries);
                var ovs = ov.Select(p => p.Split(new[] {{ '{3}' }}, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                for(int index = 0;index < ovs.Length;index++)
                {{
                    _{5}.Add(new {4}
                    {{"
                    , field.Caption
                    , field.Name
                    , string.IsNullOrWhiteSpace(field.ArraySeparate) ? "#" : field.ArraySeparate
                    , string.IsNullOrWhiteSpace(field.ValueSeparate) ? "," : field.ValueSeparate
                    , ("X_" + field.Name)
                    , field.ExtendPropertyName ?? ("V_" + field.Name));
            int idx = 0;
            foreach (var kv in chFields)
            {
                var tableSchema = kv.Value as EntityConfig;
                if (tableSchema != null)
                {
                    code.AppendFormat(@"
                        {0} = ovs[index].Length <= {2} ? default({1}) : {1}.Parse(ovs[index][{2}]),", kv.Key, tableSchema.PrimaryColumn.CsType, idx++);
                }
                else
                {
                    string type = kv.Value.ToString();
                    if (type == "string")
                        code.AppendFormat(@"
                        {0} = ovs[index].Length <= {1} ? null : ovs[index][{1}],", kv.Key, idx++);
                    else
                        code.AppendFormat(@"
                        {0} = ovs[index].Length <= {2} ? default({1}) : {1}.Parse(ovs[index][{2}]),", kv.Key, type, idx++);
                }
            }
            code.AppendFormat(@"
                    }});
                }}
                return _{0};
            }}
        }}", field.ExtendPropertyName ?? ("V_" + field.Name));
        }



        private void ExtendCode(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
        {
            if (field.ExtendClassIsPredestinate)
            {
                code.AppendFormat(@"

        {2} _{3};
        /// <summary>
        /// {0}�Ľ���ֵ
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public {2} {3}
        {{
            get
            {{
                if(_{3} != null)
                    return _{3};
                if(string.IsNullOrWhiteSpace({1}))
                    return _{3} = new {2}();
                return _{3} = new {2}({1});
            }}
        }}"
                        , field.Caption
                        , field.Name
                        , field.ExtendClassName
                        , field.ExtendPropertyName ?? ("V_" + field.Name));
                return;
            }
            code.AppendFormat(@"

        {3} _{4};
        /// <summary>
        /// {0}�Ľ���ֵ
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public {3} {4}
        {{
            get
            {{
                if(_{4} != null)
                    return _{4};
                
                if(string.IsNullOrWhiteSpace({1}))
                    return _{4} = new {3}();
                var ov = {1}.Split(new[] {{ '{2}' }}, StringSplitOptions.RemoveEmptyEntries);
                return _{4} = new {3}
                {{"
                               , field.Caption
                               , field.Name
                               , string.IsNullOrWhiteSpace(field.ValueSeparate) ? "," : field.ValueSeparate
                       , field.ExtendClassName ?? ("X_" + field.Name)
                       , field.ExtendPropertyName ?? ("V_" + field.Name));
            int idx = 0;
            foreach (var kv in chFields)
            {
                var tableSchema = kv.Value as EntityConfig;
                if (tableSchema != null)
                {
                    code.AppendFormat(@"
                    {0} = ov.Length <= {2} ? default({1}) :{1}.Parse(ov[{2}]),", kv.Key, tableSchema.PrimaryColumn.CsType, idx++);
                }
                else
                {
                    string type = kv.Value.ToString();
                    if (type == "string")
                        code.AppendFormat(@"
                    {0} = ov.Length <= {1} ? null : ov[{1}],", kv.Key, idx++);
                    else
                        code.AppendFormat(@"
                    {0} = ov.Length <= {2} ? default({1}) :{1}.Parse(ov[{2}]),", kv.Key, type, idx++);
                }
            }
            code.Append(@"
                };
            }
        }");
        }
        #endregion

        #region ����У��

        public string ValidateCode()
        {
            return $@"

        /// <summary>
        /// ��չУ��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// ����У��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        public override void Validate(ValidateResult result)
        {{
            result.Id = Id; 
            base.Validate(result);{Code()}
            ValidateEx(result);
        }}";
        }

        public string Code()
        {
            var code = new StringBuilder();
            var fields = Entity.PublishProperty.Where(p => p.CanUserInput).ToArray();
            foreach (PropertyConfig field in fields.Where(p => !string.IsNullOrWhiteSpace(p.EmptyValue)))
            {
                ConvertEmptyValue(code, field);
            }

            foreach (PropertyConfig field in fields.Where(p => !string.IsNullOrWhiteSpace(p.ExtendRole)))
            {
                ExtendValidateCode(code, field);
            }
            foreach (PropertyConfig field in fields.Where(p => string.IsNullOrWhiteSpace(p.ExtendRole)))
            {
                switch (field.CsType)
                {
                    case "string":
                        StringCheck(code, field);
                        continue;
                    case "int":
                    case "long":
                    case "decimal":
                        NumberCheck(code, field);
                        break;
                    case "DateTime":
                        DateTimeCheck(code, field);
                        break;
                }
            }
            return code.ToString();
        }

        private static void DateTimeCheck(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty)
            {
                code.Append($@"
            if({field.Name} == DateTime.MinValue)
                 result.AddNoEmpty(""{field.Caption}"",nameof({field.Name}));");
            }
            if (field.Max == null && field.Min == null)
                return;
            if (field.CanEmpty)
                code.Append($@"
            if({field.Name} != null)
            {{");
            else
                code.Append(@"
            else 
            {");

            if (field.Max != null && field.Min != null)
            {
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}) ||{field.Name} < new DateTime({field.Min}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Max}��С��{field.Min}"");");
            }
            else if (field.Max != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Max}"");");
            }
            else if (field.Min != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} < new DateTime({field.Min}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""����С��{field.Min}"");");
            }
            code.Append(@"
            }");
        }

        private static void NumberCheck(StringBuilder code, PropertyConfig field)
        {
            if (field.Datalen > 0 || field.Min != null)
            {
                if (field.CanEmpty)
                    code.Append($@"
            if({field.Name} != null)
            {{");
                if (field.Datalen > 0 && field.Min != null)
                {
                    code.Append($@"
            if({field.Name} > {field.Datalen} ||{field.Name} < {field.Min})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Datalen}��С��{field.Min}"");");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
            if({field.Name} > {field.Datalen})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Datalen}"");");
                }
                else if (field.Min != null)
                {
                    code.Append($@"
            if({field.Name} < {field.Min})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""����С��{field.Min}"");");
                }
                if (field.CanEmpty)
                    code.Append(@"
            }");
            }
        }

        private static void StringCheck(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty)
            {
                code.Append($@"
            if(string.IsNullOrWhiteSpace({field.Name}))
                 result.AddNoEmpty(""{field.Caption}"",nameof({field.Name}));");
            }
            if (field.Datalen > 0 || field.Min != null)
            {
                if (field.CanEmpty)
                    code.Append($@"
            if({field.Name} != null)
            {{");
                else
                    code.Append(@"
            else 
            {");

                if (field.Datalen > 0 && field.Min != null)
                {
                    code.Append($@"
                if({field.Name}.Length > {field.Datalen} ||{field.Name}.Length < {field.Min})
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""��������{field.Datalen}����{field.Min}����"");");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
                if({field.Name}.Length > {field.Datalen})
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܶ���{field.Datalen}����"");");
                }
                else
                {
                    code.Append($@"
                if({field.Name}.Length < {field.Min})
                   result.Add(""{field.Caption}"",nameof({field.Name}),$""��������{field.Min}����"");");
                }
                code.Append(@"
            }");
            }
        }

        private static void ConvertEmptyValue(StringBuilder code, PropertyConfig field)
        {
            var ems = field.EmptyValue.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            code.Append(@"
            if(");
            bool isFirst = true;
            foreach (var em in ems)
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(@" || ");
                switch (field.CsType)
                {
                    case "string":
                        code.Append($@"{field.Name} == ""{em}""");
                        break;
                    case "Guid":
                        code.Append($@"{field.Name} == new Guid(""{em}"")");
                        break;
                    case "DataTime":
                        code.Append($@"{field.Name} == DataTime.Parse(""{em}"")");
                        break;
                    //case "int":
                    //case "long":
                    //case "decimal":
                    //case "float":
                    //case "double":
                    default:
                        code.Append($@"{field.Name} == {em}");
                        break;
                }
            }
            if (field.CanEmpty || field.CsType == "string")
            {
                code.Append($@")
                {field.Name} = null;");
            }
            else
            {
                code.Append($@")
                {field.Name} = default({field.CsType});");
            }
        }

        private void ExtendValidateCode(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty)
            {
                switch (field.CsType)
                {
                    case "string":
                        code.Append($@"
            if(string.IsNullOrWhiteSpace({field.Name}))
            {{
                 result.AddNoEmpty(""{field.Caption}"",nameof({field.Name}));
            }}
            else");
                        break;
                }
            }
            else
            {
                switch (field.CsType)
                {
                    case "string":
                        code.Append($@"
            if(!string.IsNullOrWhiteSpace({field.Name}))");
                        break;
                }
            }
            code.Append(field.IsRelation ? RelationValidateCode(field) : SubClassValidateCode(field));
        }

        private string RelationValidateCode(PropertyConfig field)
        {

            var friend = Entities.FirstOrDefault(p => p.Name.Equals(field.ExtendRole, StringComparison.OrdinalIgnoreCase));
            if (friend == null)
            {
                return $@"result.Add(""{field.Caption}"",nameof({field.Name}),$""�������ı�{field.ExtendRole}������"");";
            }
            if (field.ExtendArray)
            {
                return string.Format($@"
            {{
                var ids = {field.Name}.Split(new[] {{ '{field.ValueSeparate ?? ","}' }});
                if(ids.Length == 0)
                {{
                    result.Add(""[{field.Caption}]Ϊ��"");
                }}
                else foreach(var vl in ids)
                {{
                    long id;
                    if(!long.TryParse(vl,out id))
                        result.Add(""{field.Caption}"",nameof({field.Name}),$""ֵ[{{vl}}]����ת��Ϊ����"");
                    else if({this.Project.DataBaseObjectName}.Default.{friend.Name.ToPluralism()}.LoadByPrimaryKey(id) == null)
                        result.Add(""{field.Caption}"",nameof({field.Name}),$""ֵ[{{id}}]�޷���{friend.Caption}�ҵ�������ֵ"");
                }}
            }}");
            }
            return string.Format($@"
            if({field.Name} > 0 && {this.Project.DataBaseObjectName}.Default.{friend.Name.ToPluralism()}.LoadByPrimaryKey({field.Name}) == null)
            {{
                result.Add(""{field.Caption}"",nameof({field.Name}),$""ֵ[{{{field.Name}}}]�޷���{friend.Caption}�ҵ�������ֵ"");
            }}");
        }

        private string SubClassValidateCode(PropertyConfig field)
        {
            StringBuilder code = new StringBuilder();
            var chFields = GetChildFields(field);
            if (field.IsKeyValueArray)
            {
                KeyValueArrayValidatCode(code, field, chFields);
            }
            else if (field.ExtendArray)
            {
                ExtendArrayValidateCode(code, field, chFields);
            }
            else
            {
                ExtendValidateCode(code, field, chFields);
            }
            return code.ToString();
        }



        private void KeyValueArrayValidatCode(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
        {
            code.Append($@"
            {{
                var ov = {field.Name}.Split(new[] {{ '{(string.IsNullOrWhiteSpace(field.ArraySeparate) ? "#" : field.ArraySeparate)}' }});
                if(ov.Length == 0)
                {{
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""���ݲ��ϸ�,����Ӧ��Ϊ{chFields.Count}��"");
                }}
                else
                {{
                    var ovs = ov.Select(p => p.Split(new[] {{ '{(string.IsNullOrWhiteSpace(field.ValueSeparate) ? "," : field.ValueSeparate)}' }})).ToArray();
                    bool chItem=true;
                    for(int index = 1;index < {chFields.Count};index++)
                    {{
                        if(ovs[0].Length != ovs[index].Length)
                        {{
                            result.Add(""{field.Caption}"",nameof({field.Name}),$""���ݲ��ϸ�,ÿ������Ӧ����ͬ"");
                            chItem = false;
                        }}
                    }}
                    if(chItem)
                    {{
                        for(int index = 1;index < {chFields.Count};index++)
                        {{");


            int idx = 0;
            foreach (var kv in chFields)
            {
                var tableSchema = kv.Value as EntityConfig;
                if (tableSchema != null)
                {
                    code.Append($@"
                            {{
                                {tableSchema.PrimaryColumn.CsType} vl;
                                if(!{tableSchema.PrimaryColumn.CsType}.TryParse(ovs[{idx++}][index],out vl))
                                    result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}Ӧ��Ϊ{tableSchema.PrimaryColumn.CsType},��ת��ʧ��"");
                                else if(vl > 0 && {this.Project.DataBaseObjectName}.Default.{tableSchema.Name.ToPluralism()}.LoadByPrimaryKey(vl) == null)
                                    result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}��{tableSchema.Caption ?? tableSchema.Name}���Ҳ�����Ӧ������"");
                            }}");
                }
                else
                {
                    string type = kv.Value.ToString();
                    if (type == "string")
                    {
                        code.Append($@"
                            {{
                                if(string.IsNullOrWhiteSpace(ovs[{idx++}][index]))
                                    result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}��Ӧ��Ϊ��"");
                            }}");
                    }
                    else
                    {
                        code.Append($@"
                            {{
                                {type} vl;
                                if(!{type}.TryParse(ovs[{idx++}][index],out vl))
                                    result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}Ӧ��Ϊ{type},��ת��ʧ��"");
                            }}");
                    }
                }
            }
            code.Append(@"
                        }
                    }
                }
            }");
        }

        private void ExtendArrayValidateCode(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
        {
            code.Append($@"
            {{
                var ov = {field.Name}.Split(new[] {{ '{(string.IsNullOrWhiteSpace(field.ArraySeparate) ? "#" : field.ArraySeparate)}' }});
                var ovs = ov.Select(p => p.Split(new[] {{ '{(string.IsNullOrWhiteSpace(field.ValueSeparate) ? "," : field.ValueSeparate)}' }})).ToArray();
                for(int index = 0;index < ovs.Length;index++)
                {{
                    if(ovs[index].Length < {chFields.Count})
                    {{
                        result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}�����ݲ��ϸ�,����Ӧ��Ϊ{chFields.Count}"");
                        continue;
                    }}");


            int idx = 0;
            foreach (var kv in chFields)
            {
                var tableSchema = kv.Value as EntityConfig;
                if (tableSchema != null)
                {
                    code.Append($@"
                    {{
                        {tableSchema.PrimaryColumn.CsType} vl;
                        if(!{tableSchema.PrimaryColumn.CsType}.TryParse(ovs[index][{idx++}],out vl))
                            result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}Ӧ��Ϊ{tableSchema.PrimaryColumn.CsType},��ת��ʧ��"");
                        else if(vl > 0 && {this.Project.DataBaseObjectName}.Default.{tableSchema.Name.ToPluralism()}.LoadByPrimaryKey(vl) == null)
                            result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}��{tableSchema.Caption ?? tableSchema.Name}���Ҳ�����Ӧ������"");
                    }}");
                }
                else
                {
                    string type = kv.Value.ToString();
                    if (type == "string")
                    {
                        code.Append($@"
                    {{
                        if(string.IsNullOrWhiteSpace(ovs[index][{idx++}]))
                            result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}��Ӧ��Ϊ��"");
                    }}");
                    }
                    else
                    {
                        code.Append($@"
                    {{
                        {type} vl;
                        if(!{type}.TryParse(ovs[index][{idx++}],out vl))
                            result.Add(""{field.Caption}"",nameof({field.Name}),$""��{{index}}���{idx++}������ֵ{{ovs[index][{idx++}]}}Ӧ��Ϊ{type},��ת��ʧ��"");
                    }}");
                    }
                }
            }
            code.Append(@"
                }
            }");
        }



        private void ExtendValidateCode(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
        {
            code.Append($@"
            {{
                var ov = {field.Name}.Split(new[] {{ '{(string.IsNullOrWhiteSpace(field.ValueSeparate) ? "#" : field.ValueSeparate)}' }});
                if(ov.Length < {chFields.Count})
                {{
                    result.AddWarning(""{field.Caption}"",nameof({field.Name}),$""����Ӧ��Ϊ{chFields.Count},����������Ϊ��"");
                    {field.Name} = null;
                }}
                else
                {{");
            int idx = 0;
            foreach (var kv in chFields)
            {
                var tableSchema = kv.Value as EntityConfig;
                if (tableSchema != null)
                {
                    code.Append($@"
                    if(ov.Length > {idx++})
                    {{
                        {tableSchema.PrimaryColumn.CsType} vl;
                        if(!{tableSchema.PrimaryColumn.CsType}.TryParse(ov[{idx++}],out vl))
                        {{
                            result.AddWarning(""{field.Caption}"",nameof({field.Name}),$""��{idx++}��ֵӦ��Ϊ{tableSchema.PrimaryColumn.CsType},��ת��ʧ��,����������Ϊ��"");
                            {field.Name} = null;
                        }}
                        else if(vl > 0 && {tableSchema.Name}.GetById(vl) == null)
                        {{
                            result.AddWarning(""{field.Caption}"",nameof({field.Name}),$""��{idx++}��ֵ{tableSchema.PrimaryColumn.CsType}�޷���{tableSchema.Caption ?? tableSchema.Name}���ҵ���Ӧ������,����������Ϊ��"");
                            {field.Name} = null;
                        }}
                    }}");
                }
                else
                {
                    string type = kv.Value.ToString();
                    if (type == "string")
                    {
                        code.Append($@"
                    if(ov.Length > {idx++})
                    {{
                        if(string.IsNullOrWhiteSpace(ov[{idx++}]))
                        {{
                            result.AddWarning(""{field.Caption}"",nameof({field.Name}),$""��{idx++}��ֵ��Ӧ��Ϊ��,����������Ϊ��"");
                            {field.Name} = null;
                        }}
                    }}");
                    }
                    else
                    {
                        code.Append($@"
                    if(ov.Length > {idx++})
                    {{
                        {type} vl;
                        if(!{type}.TryParse(ov[{idx++}],out vl))
                        {{
                            result.AddWarning(""{field.Caption}"",nameof({field.Name}),$""��{idx++}��ֵӦ��Ϊ{type},��ת��ʧ��,����������Ϊ��"");
                            {field.Name} = null;
                        }}
                    }}");
                    }
                }
            }
            code.Append(@"
                }
            }");
        }
        #endregion

    }
}