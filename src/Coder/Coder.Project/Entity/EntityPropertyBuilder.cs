using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityPropertyBuilder<TModel> : ModelBuilderBase<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        #region 主体代码

        /// <summary>
        /// 扩展的Using
        /// </summary>
        protected override string ExtendUsing => $@"
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
{Project.UsingNameSpaces}
";

        /// <summary>
        /// 类定义之前的代码
        /// </summary>
        protected override string ClassHead => $@"/// <summary>
    /// {ToRemString(Model.Description)}
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public ";

        /// <summary>
        /// 类继承的扩展
        /// </summary>
        protected override string ClassExtend => ExtendInterface();

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        protected override string Folder => "Base";


        /// <summary>
        /// 基本代码
        /// </summary>
        public override string BaseCode => $@"
        #region 基本属性
{Properties()}
        #endregion
{InterfaceProperty()}
{EntityEditStatus()}";

        /// <summary>
        /// 
        /// </summary>
        public override string ExtendCode => $@"/// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {{
            /*{ DefaultValueCode()}*/
        }}";

        #endregion


        #region 扩展

        internal string InterfaceProperty()
        {
            if (Model.IsInterface || Model.IsQuery)
                return "";
            var code = new StringBuilder();
            if (Model.PrimaryColumn != null)
            {
                if (Model.PrimaryColumn.IsGlobalKey)
                {
                    code.AppendFormat($@"

        /// <summary>
        /// Key键
        /// </summary>
        Guid IsGlobalKey.Key 
        {{
            get => this.{Model.PrimaryColumn.Name};
            set => this.{Model.PrimaryColumn.Name} = value;
        }}");
                }
                else
                {
                    code.Append($@"

        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        {Model.PrimaryColumn.LastCsType} IIdentityData<{Model.PrimaryColumn.CsType}>.Id
        {{
            get => this.{Model.PrimaryColumn.Name};
            set => this.{Model.PrimaryColumn.Name} = value;
        }}");
                }
            }
            var uniques = Columns.Where(p => p.UniqueIndex > 0).ToArray();
            if (uniques.Length != 0)
            {
                code.Append(@"

        /// <summary>
        /// 组合后的唯一值
        /// </summary>
        string IUnionUniqueEntity.UniqueValue => $""");
                bool first = true;
                foreach (var property in uniques.OrderBy(p => p.UniqueIndex))
                {
                    if (first)
                        first = false;
                    else
                        code.Append('|');
                    code.Append($"{{{property.Name}}}");
                }
                code.Append(@""";");
                return

                    code.ToString();
            }
            return code.Length == 0
                ? null
                : $@"

        #region 接口属性
{code}
        #endregion";
        }

        internal string EntityEditStatus()
        {
            if (Model.IsInterface || Model.IsQuery)
                return "";
            return Model.IsQuery
                ? null
                : @"

        #region 修改记录

        [DataMember, JsonProperty(""editStatusRedorder"",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        EntityEditStatus _editStatusRedorder;

        /// <summary>
        /// 修改状态
        /// </summary>
        EntityEditStatus IEditStatus.EditStatusRedorder { get => _editStatusRedorder; set=>_editStatusRedorder = value; }

        /// <summary>
        /// 发出标准属性修改事件
        /// </summary>
        [Conditional(""StandardPropertyChanged"")]
        partial void InitEntityEditStatus()
        {
            _editStatusRedorder = new EntityEditStatus();
        }

        /// <summary>
        /// 发出标准属性修改事件
        /// </summary>
        [Conditional(""StandardPropertyChanged"")]
        void OnSeted(string name)
        {
            _editStatusRedorder.SetModified(name);
        }
        #endregion";
        }
        #endregion


        #region 属性


        private Dictionary<string, string> ExistProperties;

        private string Properties()
        {
            ExistProperties = new Dictionary<string, string>();
            var code = new StringBuilder();
            code.Append(PrimaryKeyPropertyCode());
            ExistProperties.TryAdd(Model.PrimaryField, Model.PrimaryField);
            foreach (var property in Columns.Where(p => p != PrimaryProperty).OrderBy(p => p.Index))
            {
                if (property.DbInnerField)
                    DbInnerProperty(property, code);
                else if (property.IsCompute)
                    ComputePropertyCode(property, code);
                else
                    PropertyCode(property, code);
                ExistProperties.TryAdd(property.Name, property.Name);
                AliasPropertyCode(property, code);
                AccessProperties(property, code);
            }
            if (Model is ModelConfig model)
                foreach (var relation in model.Releations.Where(p => p.ModelType != ReleationModelType.ExtensionProperty).OrderBy(p => p.Index))
                {
                    RelationPropertyCode(relation, code);
                }
            return code.ToString();
        }

        private string PrimaryKeyPropertyCode()
        {
            var property = PrimaryProperty;
            if (property == null || property.IsDiscard)
                return null;//"\n没有设置主键字段，生成的代码是错误的";
            return $@"

        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey({property.LastCsType} {property.Name.ToLWord()})
        {{
            {PropertyName(property)} = {property.Name.ToLWord()};
        }}
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public {property.LastCsType} {PropertyName(property)};

        {PropertyHeader(property)}
        public {property.LastCsType} {property.Name}
        {{
            get => this.{PropertyName(property)};
            set
            {{
                if(this.{PropertyName(property)} == value)
                    return;
                this.{PropertyName(property)} = value;
                this.OnSeted(nameof({property.Name}));
            }}
        }}";
        }

        private void PropertyCode(IFieldConfig property, StringBuilder code)
        {
            bool isInterface = property.IsInterfaceField && Model.InterfaceInner;

            var access = isInterface ? "" : $"{property.AccessType} ";
            var name = isInterface ? $"{property.Entity.EntityName}.{property.Name}" : property.Name;
            var type = property.IsEnum && property.CsType == "string" ? "string" : property.LastCsType;
            var file = PropertyName(property);
            if (Model.IsQuery)

                code.Append($@"

        {PropertyHeader(property, isInterface, property.DataType != "ByteArray")}
        {access}{type} {name}
        {{
            get;
            set;
        }}");
            else
                code.Append($@"
        {FieldHeader(property, isInterface, property.DataType != "ByteArray")}
        public {type} {file};

        {PropertyHeader(property, isInterface, property.DataType != "ByteArray")}
        {access}{type} {name}
        {{
            get => this.{file};
            set
            {{
                if(this.{file} == value)
                    return;
                this.{file} = value;
                this.OnSeted(nameof({property.Name}));
            }}
        }}");

            //ContentProperty(property, code);
        }

        /// <summary>
        /// 计算列属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="code"></param>
        private void ComputePropertyCode(IFieldConfig property, StringBuilder code)
        {
            var access = /*isInterface ? "" :*/ $"{property.AccessType} ";
            var name = /*isInterface ? $"{property.Parent.EntityName}.{property.Name}" :*/ property.Name;

            code.Append($@"
        {PropertyHeader(property)}
        {access} {property.LastCsType} {name}
        {{");

            if (!string.IsNullOrWhiteSpace(property.ComputeGetCode))
            {
                code.Append($@"
            get
            {{
                {property.ComputeGetCode}
            }}");
            }
            if (!string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                code.Append($@"
            set
            {{
                {property.ComputeSetCode}
            }}");
            }
            code.Append(@"
        }");
            ContentProperty(property, code);
        }

        /// <summary>
        /// 别名属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="code"></param>
        private void AliasPropertyCode(IFieldConfig property, StringBuilder code)
        {
            foreach (var alias in property.GetAliasPropertys())
            {
                if (ExistProperties.ContainsKey(alias))
                    continue;
                ExistProperties.Add(alias, alias);
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}的别名
        /// </summary>
        public {property.LastCsType} {alias} => this.{property.Name};");
            }
        }

        private void DbInnerProperty(IFieldConfig property, StringBuilder code)
        {
            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption)}(仅限用于查询的Lambda表达式使用)
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        [IgnoreDataMember , JsonIgnore]
        public {property.LastCsType} {property.Name} => throw new Exception(""{ToRemString(property.Caption)}属性仅限用于查询的Lambda表达式使用"");");

        }

        /// <summary>
        /// 数据访问字段,有BUG小心
        /// </summary>
        /// <returns></returns>
        private void AccessProperties(IFieldConfig property, StringBuilder code)
        {
            if (!string.IsNullOrWhiteSpace(property.StorageProperty))
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}的存储值读写字段
        /// </summary>
        /// <remarks>
        /// 仅存储使用
        /// </remarks>
        [IgnoreDataMember,JsonIgnore]
        public string {property.StorageProperty}
        {{
            get => this.{property.Name} == null ? null : Newtonsoft.Json.JsonConvert.SerializeObject(this.{property.Name});
            set
            {{
                this.{property.Name} = value == null 
                    ? null 
                    : Newtonsoft.Json.JsonConvert.DeserializeObject<{property.LastCsType}>(value);
                this.OnSeted(nameof({property.StorageProperty}));
            }}
        }}");
        }

        private void RelationPropertyCode(ReleationConfig releation, StringBuilder code)
        {
            var model = GlobalConfig.GetEntity(releation.ForeignTable);
            var type = releation.ModelType == ReleationModelType.Children
                ? $"List<{model.EntityName}>"
                : model.EntityName;
            var cs = releation.Name.ToLWord();
            code.Append($@"

        [JsonProperty(""{cs}"", NullValueHandling = NullValueHandling.Ignore)]
        private {type} _{cs};

        [JsonIgnore]
        public {type} {releation.Name}
        {{
            get => _{cs};
            set
            {{
                if(_{cs} == value)
                    return;
                _{cs} = value;
                this.OnSeted(nameof({releation.Name}));
            }}
        }}");
        }


        #endregion


    }
}