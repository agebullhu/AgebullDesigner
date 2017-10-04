using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class ClientEntityCoder : EntityCoderBase
    {

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Client_Entity_cs";
        protected override bool IsClient => true;

        #region �������

        /// <summary>
        ///     ����ʵ�����
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
        #region ����
        
        /// <summary>
        /// ����
        /// </summary>
        public {Entity.EntityName}()
        {{
            Initialize();
        }}

        /// <summary>
        /// ��ʼ��
        /// </summary>
        partial void Initialize();

        #endregion


        #region ��������
{Properties()}
        #endregion


        #region ����
{Copy()}
        #endregion
        #region �ı�
{ToStringCode()}
        #endregion
    }}
}}";

            SaveCode(Path.Combine(path, $"{Entity.Name}.Designer.cs"), code);
        }

        /// <summary>
        ///     ������չ����
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
        /// ��ʼ��
        /// </summary>
        partial void Initialize()
        {{
{ DefaultValueCode()}
        }}
             
        #region ����
{CacheCode()}
        #endregion
             */
            SaveCode(file, code);
        }

        #endregion
        #region ����

        private string CacheCode()
        {
            var code = new StringBuilder();
            code.Append(
                $@"

        /// <summary>
        /// ��������
        /// </summary>
        public static EntityList<{Entity.EntityName}> Caches{{get;}} = new EntityList<{Entity.EntityName}>();
        
        /// <summary>
        /// ��ӵ�����
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
        /// ȷ��ָ���Ķ����Ƿ���ڵ�ǰ����
        /// </summary>
        /// <returns>
        /// ���ָ���Ķ�����ڵ�ǰ������Ϊ true������Ϊ false��
        /// </returns>
        /// <param name=""obj"">Ҫ�뵱ǰ������бȽϵĶ���</param><filterpriority>2</filterpriority>
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
        #region ����

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
        /// �޸�����
        /// </summary>
        public void ChangePrimaryKey({3} {1})
        {{
            _{1} = {1};
        }}
        
        /// <summary>
        /// {0}��ʵʱ��¼˳��
        /// </summary>
        internal const int Real_{2} = 0;

        /// <summary>
        /// {0}
        /// </summary>
        [DataMember,JsonIgnore]
        internal {3} _{1};

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
                //    throw new Exception(""����һ�����þͲ������޸�"");
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
        /// {0}��ʵʱ��¼˳��
        /// </summary>
        internal const int Real_{2} = {6};

        /// <summary>
        /// {0}
        /// </summary>
        [DataMember,JsonIgnore]
        internal {3} _{1};

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
        ///     ����������
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
        #region �ı�

        private object ToStringCode()
        {
            var code = new StringBuilder();
            code.Append(@"

        /// <summary>
        /// ��ʾΪ�ı�
        /// </summary>
        /// <returns>�ı�</returns>
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

        #region ����
        
        private string Copy()
        {
            var code = new StringBuilder();
            var type = IsClient ? "EntityObjectBase" : "DataObjectBase";
            code.Append($@"

        partial void CopyExtendValue({Entity.EntityName} source);

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name=""source"">���Ƶ�Դ�ֶ�</param>
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
        /// ����
        /// </summary>
        /// <param name=""source"">���Ƶ�Դ�ֶ�</param>
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