using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityPropertyBuilder : ModelBuilderBase
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
    /// {Model.Description.ToRemString()}
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
                    code.AppendLine($@"
        /// <summary>
        /// Key键
        /// </summary>
        [JsonIgnore]
        Guid IsGlobalKey.Key 
        {{
            get => this.{Model.PrimaryColumn.Name};
            set => this.{Model.PrimaryColumn.Name} = value;
        }}");
                }
                else
                {
                    code.AppendLine($@"
        /// <summary>
        /// 对象标识
        /// </summary>
        [JsonIgnore]
        {Model.PrimaryColumn.LastCsType} IIdentityData<{Model.PrimaryColumn.CsType}>.Id
        {{
            get => this.{Model.PrimaryColumn.Name};
            set => this.{Model.PrimaryColumn.Name} = value;
        }}");
                }
            }
            var uniques = Columns.Where(p => p.UniqueIndex).ToArray();
            if (uniques.Length != 0)
            {
                code.Append(@"
        /// <summary>
        /// 组合后的唯一值
        /// </summary>
        [JsonIgnore]
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
                code.AppendLine(@""";");
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
            if (Model.IsInterface || Model.IsQuery || !Model.UpdateByModified)
                return "";
            return Model.IsQuery
                ? null
                : @"
        #region 修改记录

        [JsonIgnore]
        EntityEditStatus _editStatusRecorder;

        /// <summary>
        /// 修改状态
        /// </summary>
        [JsonIgnore]
        EntityEditStatus IEditStatus.EditStatusRecorder { get => _editStatusRecorder; set=>_editStatusRecorder = value; }

        /// <summary>
        /// 发出标准属性修改事件
        /// </summary>
        [Conditional(""StandardPropertyChanged"")]
        partial void InitEntityEditStatus()
        {
            _editStatusRecorder = new EntityEditStatus();
        }

        /// <summary>
        /// 发出标准属性修改事件
        /// </summary>
        [Conditional(""StandardPropertyChanged"")]
        void OnSeted(string name)
        {
            _editStatusRecorder.SetModified(name);
        }
        #endregion";
        }
        #endregion

        #region 属性


        private Dictionary<string, string> ExistProperties;

        /// <summary>
        /// 属性代码
        /// </summary>
        /// <returns></returns>
        public string InterfaceProperties()
        {
            var code = new StringBuilder();
            code.Append($@"

        #region 属性");
            foreach (var property in Columns.Where(p => !p.IsInterfaceField).OrderBy(p => p.Index))
            {
                var type = property.IsEnum && property.CsType == "string" ? "string" : property.LastCsType;
                code.Append($@"

        /// <summary>
        /// {property.Caption.ToRemString()}
        /// </summary>
        {type} {property.Name} {{ get; set; }}");
            }

            code.Append($@"
        #endregion

        #region 复制简化

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>TModel对象</returns>
        TModel Clone<TModel>() where TModel : class,I{Model.Name},new()
        {{
            var dest = new TModel();
            CopyTo(dest);
            return dest;
        }}

        /// <summary>
        /// 复制到
        /// </summary>
        /// <param name=""dest"">复制的目标</param>
        void CopyTo(I{Model.Name} dest)
        {{");
            foreach (var property in Columns.Where(p => !p.IsInterfaceField).OrderBy(p => p.Index))
            {
                var type = property.IsEnum && property.CsType == "string" ? "string" : property.LastCsType;
                code.Append($@"
            dest.{property.Name} = {property.Name};");
            }
            code.Append(@"
        }
        #endregion");
            return code.ToString();
        }

        /// <summary>
        /// 属性代码
        /// </summary>
        /// <returns></returns>
        public string NoExtendProperties(bool filter)
        {
            var code = new StringBuilder();
            foreach (var property in Columns)
            {
                string ov = "";
                if(filter)
                {
                    switch (property.Name.ToLower())
                    {
                        case "id":
                            continue;
                        case "createduserid":
                        case "createddate":
                        case "latestupdateduserid":
                        case "latestupdateddate":
                        case "isdeleted":
                            ov = "override ";
                            break;
                    }
                }
                PropertyCode(property, code, ov);
            }
            return code.ToString();
        }
        
        /// <summary>
        /// 属性代码
        /// </summary>
        /// <returns></returns>
        public string Properties()
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
            {
                var array = model.Releations.Where(p => p.ModelType != ReleationModelType.ExtensionProperty
                            && p.ModelType != ReleationModelType.Custom).OrderBy(p => p.Index).ToArray();
                foreach (var relation in array)
                {
                    RelationPropertyCode(relation, code);
                }
            }
            return code.ToString();
        }

        private string PrimaryKeyPropertyCode()
        {
            var property = PrimaryProperty;
            if (property == null || property.IsDiscard)
                return null;//"\n没有设置主键字段，生成的代码是错误的";

            var propertyName = property.Name;

            if (Model.IsQuery || !Model.UpdateByModified)
                return $@"
        {FieldHeader(property,false)}
        public {property.LastCsType} {property.Name} {{ get; set; }}";

            var fieldName = FieldName(property);

            return $@"
        {FieldHeader(property,  false)}
        private {property.LastCsType} {fieldName};
        {PropertyHeader(property)}
        public {property.LastCsType} {propertyName}
        {{
            get => this.{fieldName};
            set
            {{
                if(this.{fieldName} == value)
                    return;
                this.{fieldName} = value;
                this.OnSeted(nameof({propertyName}));
            }}
        }}";
        }

        private void PropertyCode(IFieldConfig property, StringBuilder code,string ov="")
        {
            var fieldName = FieldName(property);
            var propertyName = PropertyName(property);

            var type = property.IsEnum && property.CsType == "string" ? "string" : property.LastCsType;

            if (Model.IsQuery || !Model.UpdateByModified)

                code.Append($@"
        {FieldHeader(property, property.DataType == "ByteArray")}
        public {type} {property.Name} {{ get; set; }}");
            else
                code.Append($@"
        {FieldHeader(property, property.DataType == "ByteArray")}
        private {type} {fieldName};
        {PropertyHeader(property)}
        public {ov}{type} {propertyName}
        {{
            get => this.{fieldName};
            set
            {{
                if(this.{fieldName} == value)
                    return;
                this.{fieldName} = value;
                this.OnSeted(nameof({propertyName}));
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
            var propertyName = PropertyName(property);

            code.Append($@"
        {FieldHeader(property, property.DataType == "ByteArray")}
        public {property.LastCsType} {propertyName}
        {{");

            if (string.IsNullOrWhiteSpace(property.ComputeGetCode) && string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                code.Append($@"get;set;");
            }
            else
            {
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
            foreach (var name in property.GetAliasPropertys())
            {
                if (ExistProperties.ContainsKey(name))
                    continue;
                ExistProperties.Add(name, name);
                code.Append($@"
        /// <summary>
        /// {property.Caption.ToRemString()}的别名
        /// </summary>
        [JsonIgnore]
        public {property.LastCsType} {name} => this.{property.Name};");
            }
        }

        private void DbInnerProperty(IFieldConfig property, StringBuilder code)
        {
            code.Append($@"
        {PropertyHeader(property)}
        public {property.LastCsType} {property.Name} => throw new Exception(@""{property.Caption}属性仅限用于查询的Lambda表达式使用"");");

        }

        /// <summary>
        /// 数据访问字段,有BUG小心
        /// </summary>
        /// <returns></returns>
        private void AccessProperties(IFieldConfig property, StringBuilder code)
        {
            if (string.IsNullOrWhiteSpace(property.StorageProperty))
                return;
            code.Append($@"

        /// <summary>
        /// {property.Caption.ToRemString()}的存储值读写字段
        /// </summary>
        /// <remarks>
        /// 仅存储使用
        /// </remarks>
        [JsonIgnore]
        public string {property.StorageProperty}
        {{
            get => this.{property.Name} == null ? null : Newtonsoft.Json.JsonConvert.SerializeObject(this.{property.Name});");
            if (!Model.IsQuery  && Model.UpdateByModified)
            {
                code.Append($@"
            set
            {{
                this.{property.Name} = value == null 
                    ? null 
                    : Newtonsoft.Json.JsonConvert.DeserializeObject<{property.LastCsType}>(value);
                this.OnSeted(nameof({property.StorageProperty}));
            }}");
            }
            code.Append(@"
        }");
        }

        private void RelationPropertyCode(ReleationConfig releation, StringBuilder code)
        {
            var model = GlobalConfig.GetEntity(releation.ForeignTable);
            var type = releation.ModelType == ReleationModelType.Children
                ? $"List<{model.EntityName}>"
                : model.EntityName;
            var cs = releation.Name.ToLWord();

            if (Model.IsQuery || !Model.UpdateByModified)
                code.Append($@"

        /// <summary>
        /// {releation.Caption.ToRemString()}
        /// </summary>
        [JsonProperty(""{cs}"" , NullValueHandling = NullValueHandling.Include)]
        public {type} {releation.Name} {{ get; set; }}");
            else
            code.Append($@"

        [JsonProperty(""{cs}"", NullValueHandling = NullValueHandling.Include)]
        private {type} _{cs};

        /// <summary>
        /// {releation.Caption.ToRemString()}
        /// </summary>
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