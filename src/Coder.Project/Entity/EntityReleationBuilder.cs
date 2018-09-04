using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityReleationBuilder : EntityBuilderBase
    {
        #region ����
        

        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => false;

        public override string BaseCode => $@"
{Releations()}
{Extend()}";

        protected override string Folder => "Releation";

        #endregion

        #region ��ϵ


        private string Releations()
        {
            var code = new StringBuilder();
            foreach (EntityReleationConfig rl in Entity.Releations)
            {
                EntityReleationConfig releation = rl;
                EntityConfig friend = Entities.FirstOrDefault(p => p.Name == releation.Friend);
                if (friend == null)
                    continue;
                switch (releation.Releation)
                {
                    case 0:
                    case 1:
                        code.Append(Releation1V1Code(releation));
                        break;
                    case 2:
                        code.Append(ReleationCode2(releation));
                        break;
                }
            }
            foreach (EntityConfig friend in Entities.Where(p => p != Entity))
            {
                foreach (EntityReleationConfig releation in friend.Releations.Where(p => p.Friend == Entity.Name))
                {
                    code.Append(Releationx1V1reversionCode(releation, friend));
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     ��Friend��ƽ�ȹ�ϵ��1��1
        /// </summary>
        /// <param name="releation"></param>
        /// <returns></returns>
        private string Releation1V1Code(EntityReleationConfig releation)
        {
            return string.Format(@"

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember]
        private ObjectFriend<{1}, {1}DataAccess> _{1}Helper;

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public ObjectFriend<{1}, {1}DataAccess> {1}Helper
        {{
            get
            {{
                return this._{1}Helper ?? (this._{1}Helper = new ObjectFriend<{1}, {1}DataAccess>(this,""{3}"",""{4}"",true,{6}.Default.{5}));
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public {1} {2}
        {{
            get
            {{
                return this.{1}Helper.Friend;
            }}
        }}"
                , releation.Description
                , releation.Friend
                , releation.Name ?? releation.Friend
                , releation.PrimaryKey
                , releation.ForeignKey
                , releation.Friend.ToPluralism()
                , Project.DataBaseObjectName);
        }


        private string Releationx1V1reversionCode(EntityReleationConfig releation, EntityConfig friend)
        {
            return string.Format(@"

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember]
        private ObjectFriend<{1}, {1}DataAccess> _{1}Helper;

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public ObjectFriend<{1}, {1}DataAccess> {1}Helper
        {{
            get
            {{
                return this._{1}Helper ?? (this._{1}Helper = new ObjectFriend<{1}, {1}DataAccess>(this,""{2}"",""{3}"",{4},{6}.Default.{5}));
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public {1} {1}
        {{
            get
            {{
                return this.{1}Helper.Friend;
            }}
        }}"
                , friend.Description
                , friend.Name
                , releation.ForeignKey
                , releation.PrimaryKey
                , releation.Releation == 1 ? "false" : "true"
                , friend.Name.ToPluralism()
                , Project.DataBaseObjectName);
        }

        private string ReleationCode2(EntityReleationConfig releation)
        {
            return string.Format(@"

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember]
        private DataChildren<{1}, {1}DataAccess> _{1}DataView;

        /// <summary>
        /// {0}������
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public DataChildren<{1}, {1}DataAccess> {1}DataView
        {{
            get
            {{
                return this._{1}DataView ?? (this._{1}DataView = new DataChildren<{1}, {1}DataAccess>(this,""{3}"",""{4}"",{5},{7}.Default.{6}));
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public bool Hase{2}
        {{
            get
            {{
                return this.{1}DataView.HaseValues();
            }}
        }}

        /// <summary>
        /// {0}
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public NotificationList<{1}> {2}
        {{
            get
            {{
                if (!this.{1}DataView._isLoaded)
                    this.{1}DataView.LoadValues();
                return this.{1}DataView;
            }}
        }}"
                , releation.Description
                , releation.Friend
                , releation.Name.ToPluralism()
                , releation.PrimaryKey
                , releation.ForeignKey
                , string.IsNullOrEmpty(releation.Condition) ? "null" : $@"""{releation.Condition}"""
                , releation.Friend.ToPluralism()
                , Project.DataBaseObjectName);
        }

        #endregion

        #region ��չ����

        private string Extend()
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
            var fields = Entity.PublishProperty.Where(p => p.CanUserInput).ToArray();
            foreach (PropertyConfig field in fields.Where(p => !string.IsNullOrWhiteSpace(p.ExtendRole)))
            {
                ExtendValidateCode(code, field);
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
                    , string.IsNullOrWhiteSpace(field.ExtendPropertyName) ? field.Name + friend.Name : field.ExtendPropertyName
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
                , friend.Caption, friend.Name, field.Name, field.ExtendPropertyName ?? "V_" + field.Name);
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
                ExCode(code, field, chFields);
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
        {{", field.ExtendClassName ?? "X_" + field.Name);
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
                , field.ExtendClassName ?? "X_" + field.Name
                , field.ExtendPropertyName ?? "V_" + field.Name);
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
        }}", field.ExtendPropertyName ?? "V_" + field.Name);
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
                    , field.ExtendPropertyName ?? "V_" + field.Name);
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
                , "X_" + field.Name
                , field.ExtendPropertyName ?? "V_" + field.Name);
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
        }}", field.ExtendPropertyName ?? "V_" + field.Name);
        }



        private void ExCode(StringBuilder code, PropertyConfig field, Dictionary<string, object> chFields)
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
                    , field.ExtendPropertyName ?? "V_" + field.Name);
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
                , field.ExtendClassName ?? "X_" + field.Name
                , field.ExtendPropertyName ?? "V_" + field.Name);
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

        #region ��չУ��


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
                    else if({Project.DataBaseObjectName}.Default.{friend.Name.ToPluralism()}.LoadByPrimaryKey(id) == null)
                        result.Add(""{field.Caption}"",nameof({field.Name}),$""ֵ[{{id}}]�޷���{friend.Caption}�ҵ�������ֵ"");
                }}
            }}");
            }
            return string.Format($@"
            if({field.Name} > 0 && {Project.DataBaseObjectName}.Default.{friend.Name.ToPluralism()}.LoadByPrimaryKey({field.Name}) == null)
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
                                else if(vl > 0 && {Project.DataBaseObjectName}.Default.{tableSchema.Name.ToPluralism()}.LoadByPrimaryKey(vl) == null)
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
                        else if(vl > 0 && {Project.DataBaseObjectName}.Default.{tableSchema.Name.ToPluralism()}.LoadByPrimaryKey(vl) == null)
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

/*

        string MemonyQueryCode()
        {
            return string.Format(@"
        #region ��չ�������

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public static {0}Entity GetModel(long id)
        {{
            return GetById(id);
        }}

        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""id"">ID</param>
        /// <returns>�ջ���ҵ��Ķ���</returns>
        public static {0}Entity GetById(long id)
        {{
            return ReadOnlyEntityPool<{0}Entity>.GetById(id);
        }}

        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""ids"">ID����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static List<{0}Entity> GetByIds(IEnumerable<long> ids)
        {{
            return ReadOnlyEntityPool<{0}Entity>.GetByIds(ids);
        }}

        /// <summary>
        /// ȡ�����ж���
        /// </summary>
        /// <returns>��Ϊnull�ļ���</returns>
        public static IEnumerable<{0}Entity> GetAll()
        {{
            return ReadOnlyEntityPool<{0}Entity>.GetAll();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
        public static List<{0}Entity> GetModelList(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().Where(lambda).ToList();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static IEnumerable<{0}Entity> Find(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().Where(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
        public static List<{0}Entity> GetModelList(Func<{0}Entity, bool> lambda,int take)
        {{
            var all = GetAll().Where(lambda).Take(take).ToArray();
            return all.Length > 0 ? all.ToList() : new List<{0}Entity>();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>����Ϊ��</returns>
		[Obsolete]
        public static {0}Entity First(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().First(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
        public static {0}Entity FirstOrDefault(Func<{0}Entity, bool> lambda)
        {{
            return GetAll().FirstOrDefault(lambda);
        }}

        #endregion
", Entity.Name);
        }

        string RedisQueryCode()
        {
            return string.Format(@"
        #region ��չ�������

		/// <summary>
		/// ����һ������
		/// </summary>
		public static bool Update({0}Entity entity)
        {{
            return entity.Update();
        }}

		/// <summary>
		/// ����
		/// </summary>
		public bool Update()
        {{
            EntityPool<{0}Entity>.Current.Update(this);
            return true;
        }}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		[Obsolete]
public static int GetRecordCount(string strWhere)
{
    {
        return EntityPool <{ 0}
        Entity >.Current.Count;
    }
}

/// <summary>
/// ��ҳ��ȡ�����б�
/// </summary>
[Obsolete]
public static {0}Entity GetModel(string strWhere)
{
    {
        return null;
    }
}

/// <summary>
/// ȫ���ѯ
/// </summary>
/// <param name=""lambda"">��ѯ����</param>
/// <returns>��Ϊnull�ļ���</returns>
[Obsolete]
public static List<{0}Entity> GetModelList(string lambda)
{
    {
        return EntityPool <{ 0}
        Entity >.EmptyList;
    }
}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public static {0}Entity GetModel(long id)
{
    {
        return GetById(id);
    }
}

/// <summary>
/// ����Ϊ������,������Ψһ����,����Ϊ��������
/// </summary>
public long SetIsAdded()
{
    {
        EntityPool <{ 0}
        Entity >.Current.Add(this);
        return Id;
    }
}


/// <summary>
/// ����Ϊ������,������Ψһ����,����Ϊ��������
/// </summary>
public static long Add({ 0}
Entity value)
        {{
            return value.SetIsAdded();
        }}

        /// <summary>
        /// ȡ��һ��ID��Ӧ�Ķ���
        /// </summary>
        /// <param name=""id"">ID</param>
        /// <returns>�ջ���ҵ��Ķ���</returns>
        public static {0}Entity GetById(long id)
{
    {
        return EntityPool <{ 0}
        Entity >.Current.GetById(id);
    }
}

/// <summary>
/// ȡ��һ��ID��Ӧ�Ķ���
/// </summary>
/// <param name=""ids"">ID����</param>
/// <returns>��Ϊnull�ļ���</returns>
public static LazyEntityList<{0}Entity> GetByIds(IEnumerable<long> ids)
{
    {
        return new LazyEntityList<{ 0 }Entity > (ids);
    }
}

/// <summary>
/// ȡ�����ж���
/// </summary>
/// <returns>��Ϊnull�ļ���</returns>
public static IList<{0}Entity> GetAll()
{
    {
        return EntityPool <{ 0}
        Entity >.Current.GetAll();
    }
}

/// <summary>
/// ȫ���ѯ
/// </summary>
/// <param name=""lambda"">��ѯ����</param>
/// <returns>��Ϊnull�ļ���</returns>
[Obsolete]
public static List<{0}Entity> GetModelList(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().Where(lambda).ToList();
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
        public static IEnumerable<{0}Entity> Find(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().Where(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <param name=""take"">Ҫ��������������</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
public static List<{0}Entity> GetModelList(Func<{ 0}
Entity, bool> lambda,int take)
        {{
            var all = GetAll().Where(lambda).Take(take).ToArray();
            return all.Length > 0 ? all.ToList() : EntityPool<{0}Entity>.EmptyList;
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>����Ϊ��</returns>
		[Obsolete]
public static {0}Entity First(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().First(lambda);
        }}

        /// <summary>
        /// ȫ���ѯ
        /// </summary>
        /// <param name=""lambda"">��ѯ����</param>
        /// <returns>��Ϊnull�ļ���</returns>
		[Obsolete]
public static {0}Entity FirstOrDefault(Func<{ 0}
Entity, bool> lambda)
        {{
            return GetAll().FirstOrDefault(lambda);
        }}

        #endregion
", Entity.Name);
        }
*/
