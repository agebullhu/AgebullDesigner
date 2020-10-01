using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class ClientEntityCoder : ModelCoderBase
    {

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Client_Entity_cs";
        protected override bool IsClient => true;

        #region 主体代码

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            //{(Entity.IsInternal ? "internal" : "public")}
            string code =
                $@"using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using Agebull.Common;
using Agebull.EntityModel;
using Agebull.Common.Entity.Generic;

using Newtonsoft.Json;

namespace {NameSpace}
{{
    /// <summary>
    /// {Model.Description ?? Model.Caption}
    /// </summary>
    public partial class {Model.EntityName}
    {{
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public {Model.EntityName}()
        {{
            Initialize();
        }}

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();

        #endregion


        #region 属性字义
{Properties()}
        #endregion


        #region 复制
{Copy()}
        #endregion
        #region 文本
{ToStringCode()}
        #endregion
    }}
}}";

            SaveCode(Path.Combine(path, $"{Model.Name}.Designer.cs"), code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            var file = Path.Combine(path, Model.EntityName + ".cs");
            string code =
                $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Agebull.EntityModel;
using Agebull.Common.Entity.Generic;
using Newtonsoft.Json;
namespace {NameSpace}
{{
    /// <summary>
    /// {Model.Description ?? Model.Caption}
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    sealed partial class {Model.EntityName} : EntityObjectBase
    {{
    }}
}}";
            /*
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {{
{ DefaultValueCode()}
        }}
             
        #region 缓存
{CacheCode()}
        #endregion
             */
            SaveCode(file, code);
        }

        #endregion
        #region 缓存

        private string CacheCode()
        {
            var code = new StringBuilder();
            code.Append(
                $@"

        /// <summary>
        /// 缓存数据
        /// </summary>
        public static EntityList<{Model.EntityName}> Caches{{get;}} = new EntityList<{Model.EntityName}>();
        
        /// <summary>
        /// 添加到缓存
        /// </summary>
        public static void AddToCache({Model.EntityName} value)
        {{
            Caches.AddOrSwitch(value);
        }}
");

            if (Model.PrimaryColumn != null)
            {
                code.Append(
                    $@"

#pragma warning disable 659
        /// <summary>
        /// 确定指定的对象是否等于当前对象。
        /// </summary>
        /// <returns>
        /// 如果指定的对象等于当前对象，则为 true；否则为 false。
        /// </returns>
        /// <param name=""obj"">要与当前对象进行比较的对象。</param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {{
            var data = obj as {Model.EntityName};
            return data!= null && data.{Model.PrimaryColumn.Name} == {Model.PrimaryColumn.Name};
        }}
#pragma warning restore 659
");
            }
            return code.ToString();
        }

        #endregion
        #region 属性

        private string Properties()
        {
            var code = new StringBuilder();
            var index = 0;
            if (PrimaryKeyPropertyCode(code))
                ++index;
            foreach (var property in Columns.Where(p => !p.IsPrimaryKey))
            {
                if (property.IsCompute)
                    ComputePropertyCode(property, index++, code);
                else
                    PropertyCode(property, index++, code);
            }
            return code.ToString();
        }


        private bool PrimaryKeyPropertyCode(StringBuilder builder)
        {
            var property = PrimaryProperty;
            if (property == null)
                return false;
            builder.Append($@"

        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey({property.LastCsType} {property.Name.ToLower()})
        {{
            {PropertyName(property)} = {property.Name.ToLower()};
        }}
        
        /// <summary>
        /// {ToRemString(property.Caption)}的实时记录顺序
        /// </summary>
        public const int Real_{property.Name} = 0;

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public {property.LastCsType} {PropertyName(property)};

        partial void On{property.Name}Get();

        partial void On{property.Name}Set(ref {property.LastCsType} value);

        partial void On{property.Name}Load(ref {property.LastCsType} value);

        partial void On{property.Name}Seted();

        {PropertyHeader(property)}
        public {property.LastCsType} {property.Name}
        {{
            get
            {{
                On{property.Name}Get();
                return this.{PropertyName(property)};
            }}
            set
            {{
                if(this.{PropertyName(property)} == value)
                    return;
                //if(this.{PropertyName(property)} > 0)
                //    throw new Exception(""主键一旦设置就不可以修改"");
                On{property.Name}Set(ref value);
                this.{PropertyName(property)} = value;
                this.OnPropertyChanged(nameof({property.Name}));
                On{property.Name}Seted();
            }}
        }}");
            return true;
        }

        private void PropertyCode(PropertyConfig property, int index, StringBuilder code)
        {
            code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public {property.LastCsType} {PropertyName(property)};

        {PropertyHeader(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            get
            {{
                return this.{PropertyName(property)};
            }}
            set
            {{
                if(this.{PropertyName(property)} == value)
                    return;
                this.{PropertyName(property)} = value;
                OnPropertyChanged(nameof({property.Name}));
            }}
        }}");
            ContentProperty(property, code);
        }

        /// <summary>
        ///     计算列属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="index"></param>
        /// <param name="code"></param>
        private void ComputePropertyCode(PropertyConfig property, int index, StringBuilder code)
        {
            
            if (string.IsNullOrWhiteSpace(property.ComputeGetCode) && string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                PropertyCode(property, index, code);
            }
            else if (string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                code.Append($@"
        {PropertyHeader(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            get
            {{
                {property.ComputeGetCode}
            }}
        }}");
            }
            else if (string.IsNullOrWhiteSpace(property.ComputeGetCode))
            {
                code.Append($@"
        [{PropertyHeader(property)}]
        [JsonProperty(""{property.Name}"",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            set
            {{
                {property.ComputeSetCode}
            }}
        }}");
            }
            else
            {
                code.Append($@"
        {PropertyHeader(property)}
        [JsonProperty(""{property.Name}"",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            set
            {{
                {property.ComputeSetCode}
            }}
            get
            {{
                {property.ComputeGetCode}
            }}
        }}");
            }
        }
        #endregion
        #region 文本

        private object ToStringCode()
        {
            var code = new StringBuilder();
            code.Append(@"

        /// <summary>
        /// 显示为文本
        /// </summary>
        /// <returns>文本</returns>
        public override string ToString()
        {
            return $@""");
            foreach (var property in Model.UserProperty)
            {
                ToStringCode(code, property);
            }
            code.Append(@""";
        }");
            return code.ToString();
        }

        private void ToStringCode(StringBuilder code, PropertyConfig property)
        {
            string caption = string.IsNullOrWhiteSpace(property.Caption) ? property.Name : property.Caption;
            if (!string.IsNullOrWhiteSpace(property.ArrayLen))
            {
                code.Append(property.EnumConfig != null
                    ? $@"
[{caption}]:{{string.Join("","", {property.Name}_Content)}}"
                    : $@"
[{caption}]:{{string.Join("","", {property.Name})}}");
            }
            else
            {
                code.Append(property.EnumConfig != null
                    ? $@"
[{caption}]:{{{property.Name}_Content}}"
                    : $@"
[{caption}]:{{{property.Name}}}");
            }
        }


        #endregion

        #region 复制

        private string Copy()
        {
            var code = new StringBuilder();
            var type = IsClient ? "EntityObjectBase" : "DataObjectBase";
            code.Append($@"

        partial void CopyExtendValue({Model.EntityName} source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name=""source"">复制的源字段</param>
        protected override void CopyValueInner({type} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Model.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");

            code.Append($@"
            var sourceEntity = source as {Model.EntityName};
            if(sourceEntity == null)
                return;");

            foreach (var property in Model.UserProperty.Where(p => p.CanGet && p.CanSet))
            {
                if (property.IsCompute && property.CanSet)
                {
                    code.AppendFormat(@"
            this.{0} = sourceEntity.{0};", property.Name);
                    continue;
                }
                code.AppendFormat(@"
            this._{0} = sourceEntity._{0};", property.Name.ToLower());
            }
            code.Append(@"
            CopyExtendValue(sourceEntity);");
            if (!IsClient)
                code.Append(@"
            this.__EntityStatus.SetModified();");
            code.Append(@"
        }");
            //return code.ToString();
            code.Append($@"

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name=""source"">复制的源字段</param>
        public void Copy({Model.EntityName} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Model.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");


            foreach (var property in Model.UserProperty.Where(p => p.CanGet && p.CanSet))
            {
                code.Append($@"
            this.{property.Name} = source.{property.Name};");
            }
            code.Append(@"
        }");


            return code.ToString();
        }
        #endregion
    }
}