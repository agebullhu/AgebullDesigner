using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityPropertyBuilder : EntityBuilderBase
    {

        #region 主体代码

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
using Gboxt.Common.DataModel;
using Agebull.Common.WebApi;
{Project.UsingNameSpaces}

//using {NameSpace}.DataAccess;
//using Gboxt.Common.DataModel.SqlServer;
using Newtonsoft.Json;
";
        protected override string ClassHead => $@"/// <summary>
    /// {ToRemString(Entity.Description)}
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public ";
        protected override string ClassExtend => ExtendInterface();

        protected override string Folder => "Base";

        public override string BaseCode => $@"
        #region 属性字义
{Properties()}
{FullCode()}
        #endregion";

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
            if (Entity.NoDataBase || Entity.PrimaryColumn?.CsType != "long")
                return null;
            return $@"
        #region IIdentityData接口
{ ExtendProperty()}
        #endregion
        #region 属性扩展
{AccessProperties()}
        #endregion";
        }

        #endregion


        #region 扩展

        private string ExtendInterface()
        {
            List<string> list = new List<string>();
            if (Entity.Interfaces != null)
            {
                list.AddRange(Entity.Interfaces.Split(NoneLanguageChar, StringSplitOptions.RemoveEmptyEntries));
            }
            //code.Append("IEntityPoolSetting");
            if (!Entity.NoDataBase && Entity.PrimaryColumn?.CsType == "long")
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
                    code.AppendFormat(@"

        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public {0} Id
        {{
            get
            {{
                return this.{1};
            }}
            set
            {{
                this.{1} = value;
            }}
        }}", Entity.PrimaryColumn.LastCsType, Entity.PrimaryColumn.Name);
                }
                if(Entity.PrimaryColumn.CsType == "int")
                code.Append($@"

        /// <summary>
        /// Id键
        /// </summary>
        long IIdentityData.Id
        {{
            get
            {{
                return (long)this.{Entity.PrimaryColumn.Name};
            }}
            set
            {{
                this.{Entity.PrimaryColumn.Name} = value;
            }}
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
        //        throw new Exception(""不支持"");//BUG:ID不是INT类型
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
            IEnumerable<PropertyConfig> idp = Columns.Where(p => p.UniqueIndex > 0);
            var columnSchemata = idp as PropertyConfig[] ?? idp.ToArray();
            int cnt = columnSchemata.Length;
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

            for (int idx = 0; idx < cnt; idx++)
            {
                if (idx > 0)
                    code.Append(':');
                code.AppendFormat("{{{0}}}", idx);
            }
            code.AppendFormat("\" ");
            foreach (PropertyConfig property in columnSchemata.OrderBy(p => p.UniqueIndex))
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
            _{property.Name.ToLWord()} = {property.Name.ToLWord()};
        }}
        
        /// <summary>
        /// {ToRemString(property.Caption)}的实时记录顺序
        /// </summary>
        public const int Real_{property.Name} = 0;

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [DataMember,JsonIgnore]
        public {property.LastCsType} _{property.Name.ToLWord()};

        partial void On{property.Name}Get();

        partial void On{property.Name}Set(ref {property.LastCsType} value);

        partial void On{property.Name}Load(ref {property.LastCsType} value);

        partial void On{property.Name}Seted();

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        public {property.LastCsType} {property.Name}
        {{
            get
            {{
                On{property.Name}Get();
                return this._{property.Name.ToLWord()};
            }}
            set
            {{
                if(this._{property.Name.ToLWord()} == value)
                    return;
                //if(this._{property.Name.ToLWord()} > 0)
                //    throw new Exception(""主键一旦设置就不可以修改"");
                On{property.Name}Set(ref value);
                this._{property.Name.ToLWord()} = value;
                this.OnPropertyChanged(Real_{property.Name});
                On{property.Name}Seted();
            }}
        }}";
        }

        private void PropertyCode(PropertyConfig property, int index, StringBuilder code)
        {
            code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}的实时记录顺序
        /// </summary>
        public const int Real_{property.Name} = {index};

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [DataMember,JsonIgnore]
        public {property.LastCsType} _{property.Name.ToLWord()};

        partial void On{property.Name}Get();

        partial void On{property.Name}Set(ref {property.LastCsType} value);

        partial void On{property.Name}Seted();

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            get
            {{
                On{property.Name}Get();
                return this._{property.Name.ToLWord()};
            }}
            set
            {{
                if(this._{property.Name.ToLWord()} == value)
                    return;
                On{property.Name}Set(ref value);
                this._{property.Name.ToLWord()} = value;
                On{property.Name}Seted();
                this.OnPropertyChanged(Real_{property.Name});
            }}
        }}"
/*Entity.UpdateByModified ? "//" : ""*/);
            EnumContentProperty(property, code);
        }

        /// <summary>
        /// 计算列属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="index"></param>
        /// <param name="code"></param>
        private void ComputePropertyCode(PropertyConfig property, int index, StringBuilder code)
        {
            EnumContentProperty(property, code);
            code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}的实时记录顺序
        /// </summary>
        public const int Real_{property.Name} = {index};
");
            if (string.IsNullOrWhiteSpace(property.ComputeGetCode) && string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [DataMember,JsonIgnore]
        public {property.LastCsType} _{property.Name.ToLWord()};

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
        {property.AccessType} {property.LastCsType} {property.Name}
        {{
            get
            {{
                return this._{property.Name.ToLWord()};
            }}
            set
            {{
                this._{property.Name.ToLWord()} = value;
            }}
        }}");
            }
            else if (string.IsNullOrWhiteSpace(property.ComputeSetCode))
            {
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
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
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
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
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property)}
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
        /// <summary>
        /// 别名属性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="code"></param>
        private void AliasPropertyCode(PropertyConfig property, StringBuilder code)
        {
            if (property == null)
                return;
            var hase = new Dictionary<string, string> {
                {property.Name,property.Name }
            };
            foreach (var alias in property.GetAliasPropertys())
            {
                if (hase.ContainsKey(alias))
                    continue;
                hase.Add(alias, alias);
                code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {Attribute(property).Replace($"\"{property.JsonName}\"", $"\"{alias}\"")}
        public {property.LastCsType} {alias}
        {{
            get
            {{
                return this.{property.Name};
            }}
            set
            {{
                this.{property.Name} = value;
            }}
        }}");
            }
        }
        private string Properties()
        {
            var code = new StringBuilder();
            code.Append(PrimaryKeyPropertyCode());
            AliasPropertyCode(Entity.PrimaryColumn, code);
            int index = 1;
            foreach (PropertyConfig property in Columns.Where(p =>!p.DbInnerField && !p.IsPrimaryKey))
            {
                if (property.IsCompute)
                    ComputePropertyCode(property, index++, code);
                else
                    PropertyCode(property, index++, code);
                AliasPropertyCode(property, code);
            }
            foreach (PropertyConfig property in Entity.DbFields.Where(p => p.DbInnerField))
            {
                DbInnerProperty(property, code);
            }
            return code.ToString();
        }
        private void DbInnerProperty(PropertyConfig property, StringBuilder code)
        {
            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remarks>
        /// 仅限用于查询的Lambda表达式使用
        /// </remarks>
        [IgnoreDataMember , Browsable(false),JsonIgnore]
        public {property.LastCsType} {property.Name}
        {{
            get
            {{
                throw new Exception(""{ToRemString(property.Caption)}属性仅限用于查询的Lambda表达式使用"");
            }}
        }}");
        }

        private string AccessProperties()
        {
            var code = new StringBuilder();
            foreach (PropertyConfig property in Entity.DbFields.Where(p => !string.IsNullOrWhiteSpace(p.StorageProperty)))
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
        [DataMember , Browsable(false),JsonIgnore]
        public string {property.StorageProperty}
        {{
            get
            {{
                On{property.StorageProperty}Get();
                return this.{property.Name} == null ? null : IOHelper.XMLSerializer(this.{property.Name});
            }}
            set
            {{
                On{property.StorageProperty}Set(ref value);
                this.{property.Name} = IOHelper.XMLDeSerializer<{property.LastCsType}>(value);
                On{property.StorageProperty}Seted();
            }}
        }}");
            }
            return code.ToString();
        }


        #endregion


    }
}