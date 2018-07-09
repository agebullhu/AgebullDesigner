using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class ClientEntityCoder : EntityCoderBase
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
        protected override void CreateBaCode(string path)
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
    /// {Entity.Description ?? Entity.Caption}
    /// </summary>
    public partial class {Entity.EntityName}
    {{
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public {Entity.EntityName}()
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

            SaveCode(Path.Combine(path, $"{Entity.Name}.Designer.cs"), code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var file = Path.Combine(path, Entity.EntityName + ".cs");
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
    /// {Entity.Description ?? Entity.Caption}
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    sealed partial class {Entity.EntityName} : EntityObjectBase
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
        public static EntityList<{Entity.EntityName}> Caches{{get;}} = new EntityList<{Entity.EntityName}>();
        
        /// <summary>
        /// 添加到缓存
        /// </summary>
        public static void AddToCache({Entity.EntityName} value)
        {{
            Caches.AddOrSwitch(value);
        }}
");

            if (Entity.PrimaryColumn != null)
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
            var data = obj as {Entity.EntityName};
            return data!= null && data.{Entity.PrimaryColumn.Name} == {Entity.PrimaryColumn.Name};
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
            code.Append(PrimaryKeyPropertyCode());
            var index = 1;
            foreach (var property in Columns.Where(p => !p.IsPrimaryKey))
            {
                if (property.IsCompute)
                    ComputePropertyCode(property, index++, code);
                else
                {
                    PropertyCode(property, index++, code);
                }
            }
            return code.ToString();
        }
        

        private string PrimaryKeyPropertyCode()
        {
            var property = Entity.PrimaryColumn;
            if (property == null)
                return "";
            return string.Format(@"

        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey({3} {1})
        {{
            _{1} = {1};
        }}
        
        /// <summary>
        /// {0}的实时记录顺序
        /// </summary>
        public const int Real_{2} = 0;

        /// <summary>
        /// {0}
        /// </summary>
        [DataMember,JsonIgnore]
        public {3} _{1};

        partial void On{2}Get();

        partial void On{2}Set(ref {3} value);

        partial void On{2}Load(ref {3} value);

        partial void On{2}Seted();

        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {5}
        /// </remarks>
        {4}
        public {3} {2}
        {{
            get
            {{
                On{2}Get();
                return this._{1};
            }}
            set
            {{
                if(this._{1} == value)
                    return;
                //if(this._{1} > 0)
                //    throw new Exception(""主键一旦设置就不可以修改"");
                On{2}Set(ref value);
                this._{1} = value;
                {6}this.OnPropertyChanged(nameof({2}));
                On{2}Seted();
            }}
        }}"
                , ToRemString(property.Caption + ":" + property.Description)
                , property.PropertyName.ToLower()
                , property.PropertyName
                , property.LastCsType
                , Attribute(property)
                , ToRemString(property.Description)
                , null /*Table.UpdateByModified ? "//" : property.IsIdentity ? "//" : ""*/);
        }

        private void PropertyCode(PropertyConfig property, int index, StringBuilder code)
        {
            code.AppendFormat(@"
        /// <summary>
        /// {0}的实时记录顺序
        /// </summary>
        public const int Real_{2} = {6};

        /// <summary>
        /// {0}
        /// </summary>
        [DataMember,JsonIgnore]
        public {3} _{1};

        partial void On{2}Get();

        partial void On{2}Set(ref {3} value);

        partial void On{2}Seted();

        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {5}
        /// </remarks>
        {4}
        {7} {3} {2}
        {{
            get
            {{
                On{2}Get();
                return this._{1};
            }}
            set
            {{
                if(this._{1} == value)
                    return;
                On{2}Set(ref value);
                this._{1} = value;
                On{2}Seted();
                OnPropertyChanged(nameof({2}));
                {8}
            }}
        }}"
                , ToRemString(property.Caption + ":" + property.Description)
                , property.PropertyName.ToLower()
                , property.PropertyName
                , property.LastCsType
                , Attribute(property)
                , ToRemString(property.Description)
                , index
                , property.AccessType
                , property.EnumConfig == null
                    ? null
                    : $@"OnPropertyChanged(""{property.PropertyName}_Content"");" /*Table.UpdateByModified ? "//" : ""*/);


            EnumContentProperty(property, code);
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
                code.AppendFormat(@"
        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {5}
        /// </remarks>
        {4}
        {6} {3} {2}
        {{
            get
            {{
                {1}
            }}
        }}"
                    , ToRemString(property.Caption + ":" + property.Description)
                    , property.ComputeGetCode
                    , property.PropertyName
                    , property.LastCsType
                    , Attribute(property)
                    , ToRemString(property.Description)
                    , property.AccessType);
            }
            else if (string.IsNullOrWhiteSpace(property.ComputeGetCode))
            {
                code.AppendFormat(@"
        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {5}
        /// </remarks>
        [{4}]
        [JsonProperty(""{2}"", NullValueHandling = NullValueHandling.Ignore)]
        {6} {3} {2}
        {{
            set
            {{
                {1}
            }}
        }}"
                    , ToRemString(property.Caption)
                    , property.ComputeSetCode
                    , property.PropertyName
                    , property.LastCsType
                    , Attribute(property)
                    , ToRemString(property.Description)
                    , property.AccessType);
            }
            else
            {
                code.AppendFormat(@"
        /// <summary>
        /// {0}
        /// </summary>
        /// <remarks>
        /// {5}
        /// </remarks>
        {4}
        [JsonProperty(""{2}"", NullValueHandling = NullValueHandling.Ignore)]
        {6} {3} {2}
        {{
            set
            {{
                {1}
            }}
            get
            {{
                {7}
            }}
        }}"
                    , ToRemString(property.Caption + ":" + property.Description)
                    , property.ComputeSetCode
                    , property.PropertyName
                    , property.LastCsType
                    , Attribute(property)
                    , ToRemString(property.Description)
                    , property.AccessType
                    , property.ComputeGetCode);
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
            foreach (var field in Entity.CppProperty)
            {
                ToStringCode(code, field);
            }
            code.Append(@""";
        }");
            return code.ToString();
        }

        private void ToStringCode(StringBuilder code, PropertyConfig field)
        {
            string caption = string.IsNullOrWhiteSpace(field.Caption) ? field.Name : field.Caption;
            if (!string.IsNullOrWhiteSpace(field.ArrayLen))
            {
                if (field.EnumConfig != null)
                {
                    code.Append($@"
[{caption}]:{{string.Join("","", {field.Name}_Content)}}");
                }
                else
                {
                    code.Append($@"
[{caption}]:{{string.Join("","", {field.Name})}}");
                }
            }
            else
            {
                if (field.EnumConfig != null)
                {
                    code.Append($@"
[{caption}]:{{{field.Name}_Content}}");
                }
                else
                {
                    code.Append($@"
[{caption}]:{{{field.Name}}}");
                }
            }
        }


        #endregion

        #region 复制
        
        private string Copy()
        {
            var code = new StringBuilder();
            var type = IsClient ? "EntityObjectBase" : "DataObjectBase";
            code.Append($@"

        partial void CopyExtendValue({Entity.EntityName} source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name=""source"">复制的源字段</param>
        protected override void CopyValueInner({type} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");

            code.Append($@"
            var sourceEntity = source as {Entity.EntityName};
            if(sourceEntity == null)
                return;");

            foreach (PropertyConfig property in Entity.CppProperty.Where(p=>p.CanGet && p.CanSet ))
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
        public void Copy({Entity.EntityName} source)
        {{");

            if (!string.IsNullOrWhiteSpace(Entity.ModelBase))
                code.AppendLine(@"
            base.CopyValueInner(source);");


            foreach (PropertyConfig property in Entity.CppProperty.Where(p => p.CanGet && p.CanSet))
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