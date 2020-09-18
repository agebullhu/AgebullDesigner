using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityPropertyBuilder : EntityBuilderBase
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
    /// {ToRemString(Entity.Description)}
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

        /// <summary>
        /// 发出标准属性修改事件
        /// </summary>
        [Conditional(""StandardPropertyChanged"")]
        void OnSeted(string name) => OnPropertyChanged(name);

{Properties()}
{FullCode()}
        #endregion";

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

        /// <summary>
        ///     生成实体代码
        /// </summary>
        private string FullCode()
        {
            if (Entity.NoDataBase/* || Entity.PrimaryColumn?.CsType != "long"*/)
                return null;
            return $@"
        #region 接口属性
{ ExtendProperty()}
        #endregion
        #region 扩展属性
{AccessProperties()}
        #endregion";
        }

        #endregion


        #region 扩展

        private string ExtendInterface()
        {
            var list = new List<string>();
            if (Entity.Interfaces != null)
            {
                list.AddRange(Entity.Interfaces.Split(NoneLanguageChar, StringSplitOptions.RemoveEmptyEntries));
            }
            //code.Append("IEntityPoolSetting");
            if (!Entity.NoDataBase && Entity.HasePrimaryKey && Entity.PrimaryColumn.CsType.IsNumberType())
                list.Add("IIdentityData");
            //if (!Entity.IsLog)
            //{
            //    code.Append(" , IFieldJson , IPropertyJson");
            //}
            if (Entity.PrimaryColumn != null && Entity.PrimaryColumn.IsGlobalKey)
            {
                list.Add("IKey");
            }
            if (Entity.IsUniqueUnion)
            {
                list.Add("IUnionUniqueEntity");
            }

            //if (Entity.LastColumns.Any(p => p.IsUserId))
            //{
            //    code.Append(" , IUserChildEntity");
            //}
            return Entity.NoDataBase ? " : NotificationObject" : " : EditDataObject" + (list.Count == 0 ? null : list.DistinctBy().LinkToString(" , ", " , "));
        }

        private string ExtendProperty()
        {
            var code = new StringBuilder();
            if (Entity.PrimaryColumn != null)
            {
                if (Entity.PrimaryColumn.Name != "Id" && Entity.LastProperties.All(p => p.Name != "Id"))
                {
                    code.Append($@"

        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public {Entity.PrimaryColumn.LastCsType} Id
        {{
            get
            {{
                return this.{Entity.PrimaryColumn.Name};
            }}
            set
            {{
                this.{Entity.PrimaryColumn.Name} = value;
            }}
        }}");
                }
                if (Entity.PrimaryColumn.CsType == "int")
                    code.Append($@"

        /// <summary>
        /// Id键
        /// </summary>
        long IIdentityData.Id
        {{
            get => this.{Entity.PrimaryColumn.Name};
            set => this.{Entity.PrimaryColumn.Name} = ({Entity.PrimaryColumn.LastCsType})value;
        }}");
                //        else
                //            code.AppendFormat(@"

                ///// <summary>
                ///// Id键
                ///// </summary>
                //int IIdentityData.Id
                //{{
                //    get
                //    {{
                //        throw new Exception(""不支持"");//B UG:ID不是INT类型
                //    }}
                //    set
                //    {{
                //    }}
                //}}");
                if (Entity.PrimaryColumn.IsGlobalKey)
                {
                    code.AppendFormat(@"

        /// <summary>
        /// Key键
        /// </summary>
        Guid IKey.Key
        {{
            get
            {{
                return this.{0};
            }}
        }}", Entity.PrimaryColumn.Name);
                }
            }
            var idp = Columns.Where(p => p.UniqueIndex > 0);
            var columnSchemata = idp as PropertyConfig[] ?? idp.ToArray();
            var cnt = columnSchemata.Length;
            if (cnt <= 1)
            {
                return code.ToString();
            }
            code.Append(@"

        /// <summary>
        /// 组合后的唯一值
        /// </summary>
        public string UniqueValue
        {
            get
            {
                return string.Format(""");

            for (var idx = 0; idx < cnt; idx++)
            {
                if (idx > 0)
                    code.Append(':');
                code.AppendFormat("{{{0}}}", idx);
            }
            code.AppendFormat("\" ");
            foreach (var property in columnSchemata.OrderBy(p => p.UniqueIndex))
            {
                code.AppendFormat(" , {0}", property.Name);
            }
            code.Append(@");
            }
        }");
            return code.ToString();
        }

        #endregion


        #region 属性

        private string PrimaryKeyPropertyCode()
        {
            var property = Entity.PrimaryColumn;
            if (property == null)
                return null;//"\n没有设置主键字段，生成的代码是错误的";
            return $@"

        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey({property.LastCsType} {property.Name.ToLWord()})
        {{
            {FieldName(property)} = {property.Name.ToLWord()};
        }}
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public {property.LastCsType} {FieldName(property)};

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
                return this.{FieldName(property)};
            }}
            set
            {{
                if(this.{FieldName(property)} == value)
                    return;
                //if(this.{FieldName(property)} > 0)
                //    throw new Exception(""主键一旦设置就不可以修改"");
                On{property.Name}Set(ref value);
                this.{FieldName(property)} = value;
                On{property.Name}Seted();
                this.OnPropertyChanged(_DataStruct_.Real_{property.Name});
                this.OnSeted(nameof({property.Name}));
            }}
        }}";
        }

        private void PropertyCode(PropertyConfig property, StringBuilder code)
        {
            bool isInterface = property.IsInterfaceField && Entity.InterfaceInner;

            var access = isInterface ? "" : $"{property.AccessType} ";
            var name = isInterface ? $"{property.Parent.EntityName}.{property.Name}" : property.Name;
            var type = property.IsEnum && property.CsType == "string" ? "string" :  property.LastCsType;
            var file = FieldName(property);

            code.Append($@"
        {FieldHeader(property, isInterface, property.DataType != "ByteArray")}
        public {type} {file};

        partial void On{property.Name}Get();

        partial void On{property.Name}Set(ref {type} value);

        partial void On{property.Name}Seted();

        {PropertyHeader(property,isInterface, property.DataType != "ByteArray")}
        {access}{type} {name}
        {{
            get
            {{
                On{property.Name}Get();
                return this.{file};
            }}
            set
            {{
                if(this.{file} == value)
                    return;
                On{property.Name}Set(ref value);
                this.{file} = value;
                On{property.Name}Seted();
                this.OnPropertyChanged(_DataStruct_.Real_{property.Name});
                this.OnSeted(nameof({property.Name}));
            }}
        }}");

            ContentProperty(property, code);
        }

        /// <summary>
        /// 计算列属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="code"></param>
        private void ComputePropertyCode(PropertyConfig property, StringBuilder code)
        {
            if (string.IsNullOrWhiteSpace(property.ComputeGetCode) && string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                PropertyCode(property, code);
                return;
            }
            //bool isInterface = property.IsInterfaceField && Entity.InterfaceInner;

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

        private Dictionary<string, string> ExistProperties;

        /// <summary>
        /// 别名属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="code"></param>
        private void AliasPropertyCode(PropertyConfig property, StringBuilder code)
        {
            if (property == null)
                return;
            foreach (var alias in property.GetAliasPropertys())
            {
                if (ExistProperties.ContainsKey(alias))
                    continue;
                ExistProperties.Add(alias, alias);
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}\n--{property.Name}的别名
        /// </summary>
        public {property.LastCsType} {alias}
        {{
            get => this.{property.Name};
            set => this.{property.Name} = value;
        }}");
            }
        }

        private string Properties()
        {
            ExistProperties = new Dictionary<string, string>();
            var code = new StringBuilder();
            code.Append(PrimaryKeyPropertyCode());
            ExistProperties.Add(Entity.PrimaryField, Entity.PrimaryField);
            foreach (var property in Columns.Where(p => !p.DbInnerField && !p.IsPrimaryKey))
            {
                if (property.IsCompute)
                    ComputePropertyCode(property, code);
                else
                    PropertyCode(property, code);
                ExistProperties.Add(property.Name, property.Name);
            }
            foreach (var property in Columns.Where(p => p.DbInnerField && !p.IsPrimaryKey))
            {
                DbInnerProperty(property, code);
                ExistProperties.Add(property.Name, property.Name);
            }
            foreach (var property in Columns)
            {
                AliasPropertyCode(property, code);
            }
            return code.ToString();
        }

        private void DbInnerProperty(PropertyConfig property, StringBuilder code)
        {
            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption)}(仅限用于查询的Lambda表达式使用)
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        [IgnoreDataMember , Browsable(false),JsonIgnore]
        public {property.LastCsType} {property.Name} => throw new Exception(""{ToRemString(property.Caption)}属性仅限用于查询的Lambda表达式使用"");");

        }

        /// <summary>
        /// 数据访问字段,有BUG小心
        /// </summary>
        /// <returns></returns>
        private string AccessProperties()
        {
            var code = new StringBuilder();
            foreach (var property in Entity.DbFields.Where(p => !string.IsNullOrWhiteSpace(p.StorageProperty)))
            {
                code.Append($@"

        partial void On{property.StorageProperty}Get();

        partial void On{property.StorageProperty}Set(ref string value);

        partial void On{property.StorageProperty}Seted();

        /// <summary>
        /// {ToRemString(property.Caption)}的存储值读写字段
        /// </summary>
        /// <remarks>
        /// 仅存储使用
        /// </remarks>
        [IgnoreDataMember,Browsable(false),JsonIgnore]
        public string {property.StorageProperty}
        {{
            get
            {{
                On{property.StorageProperty}Get();
                return this.{property.Name} == null ? null : Newtonsoft.Json.JsonConvert.SerializeObject(this.{property.Name});
            }}
            set
            {{
                On{property.StorageProperty}Set(ref value);
                this.{property.Name} = value == null 
                    ? null 
                    : Newtonsoft.Json.JsonConvert.DeserializeObject<{property.LastCsType}>(value);
                On{property.StorageProperty}Seted();
                this.OnPropertyChanged(_DataStruct_.Real_{property.StorageProperty});
                this.OnSeted(nameof({property.StorageProperty}));
            }}
        }}");
            }

            return code.ToString();
        }


        #endregion


    }
}