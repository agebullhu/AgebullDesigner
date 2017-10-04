using System;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.Upgrade
{
    /// <summary>
    /// 配置对象升级代码生成器
    /// </summary>
    public sealed class UpgradeModelBuilder : FileCoder
    {
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;
        /// <summary>
        /// 类配置
        /// </summary>
        public ClassUpgradeConfig Config { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "Upgrade_Config_Base_cs";

        /// <summary>
        /// 当前对象
        /// </summary>
        public override ConfigBase CurrentConfig => Config;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            string file = Path.Combine(path, Config.Name + ".cs");

            string code = $@"/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:{DateTime.Today:yyyy-MM-dd}
*****************************************************/

using System;
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

using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{{
    /// <summary>
    /// {Config.Description}
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class {Config.Name} : {Config.BaseType}
    {{
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public {Config.Name}()
        {{
        }}

        #endregion

{PropertyCode()}

    }}
}}";
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {

        }

        private string PropertyCode()
        {
            StringBuilder code = new StringBuilder();
            var properties = Config.Properties.Values;
            foreach (var group in properties.GroupBy(p => p.Category))
            {
                code.Append($@" 
        #region {group.Key}");
                foreach (var property in group.Where(p => !p.Discard))
                {
                    if (!string.IsNullOrWhiteSpace(property.Code))
                        PropertyByCode(property, code);
                    else if (!property.IsList)
                        PropertyCode(property, code);
                    else
                        CollctionPropertyCode(property, code);
                }
                code.Append(@" 
        #endregion");
            }
            return code.ToString();
        }

        private void PropertyByCode(PropertyUpgradeConfig property, StringBuilder code)
        {
            var di = DescriptionCode(property, code);

            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remark>
        /// {ToRemString(property.Description)}
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""{property.Category}""),DisplayName(@""{property.Caption}""),Description({di})]
        {property.Code}");
        }

        private void PropertyCode(PropertyUpgradeConfig property, StringBuilder code)
        {
            string fieldName = ToTfWordName(property.Name, "_");
            var di = DescriptionCode(property, code);
            FieldCode(property, code, fieldName);

            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remark>
        /// {ToRemString(property.Description)}
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""{property.Category}""),DisplayName(@""{property.Caption}""),Description({di})]
        public {property.TypeName} {property.Name}
        {{
            get
            {{
                return {fieldName};
            }}
            set
            {{
                if({fieldName} == value)
                    return;
                this.BeforePropertyChanged(nameof({property.Name}), {fieldName},value);
                this.{fieldName} = value;
                this.OnPropertyChanged(nameof({property.Name}));
            }}
        }}");
        }


        private void CollctionPropertyCode(PropertyUpgradeConfig property, StringBuilder code)
        {
            string fieldName = ToTfWordName(property.Name, "_");

            var di = DescriptionCode(property, code);
            FieldCode(property, code, fieldName);

            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        /// <remark>
        /// {ToRemString(property.Description)}
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""{property.Category}""),DisplayName(@""{property.Caption}""),Description({di})]
        public {property.TypeName} {property.Name}
        {{
            get
            {{
                if ({fieldName} != null)
                    return {fieldName};
                {fieldName} = new {property.TypeName}();
                BeforePropertyChanged(nameof({property.Name}), null, {fieldName});
                return {fieldName};
            }}
            set
            {{
                if({fieldName} == value)
                    return;
                this.BeforePropertyChanged(nameof({property.Name}), {fieldName},value);
                this.{fieldName} = value;
                this.OnPropertyChanged(nameof({property.Name}));
            }}
        }}");
        }
        private static string DescriptionCode(PropertyUpgradeConfig property, StringBuilder code)
        {
            string di;
            if (property.Description != null && (property.Description.Length > 20 || property.Description.Contains("\r")))
            {
                code.Append($@"

        /// <summary>
        /// {property.Caption}的说明文字
        /// </summary>
        const string {property.Name}_Description = @""{property.Description}"";");
                di = $"{property.Name}_Description";
            }
            else
            {
                di = $"\"{property.Description}\"";
            }
            return di;
        }

        private static void FieldCode(PropertyUpgradeConfig property, StringBuilder code, string fieldName)
        {
            code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>
        [");
            code.Append(property.IsDataAttribute ? "DataMember" : "IgnoreDataMember");
            code.Append(property.IsJsonAttribute
                ? $@",JsonProperty(""{property.JsonName}"", NullValueHandling = NullValueHandling.Ignore)"
                : ",JsonIgnore");

            code.Append($@"]
        internal {property.TypeName} {fieldName};");
        }
    }
}

