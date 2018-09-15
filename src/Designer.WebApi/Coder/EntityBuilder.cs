using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class EntityBuilder : EntityCoderBase
    {

        #region 主体代码
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Entity_Base_cs";
        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            string code = $@"
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

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class {Entity.Name} : IApiResultData , IApiArgument
    {{
        {Properties()}
        {ToForm()}
        {ValidateCode()}
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, "Model", Entity.Name);
            WriteFile(file + ".Designer.cs", code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string code = $@"
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

namespace {NameSpace}.WebApi
{{
    sealed partial class {Entity.Name}
    {{
        /*// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name=""result"">结果存放处</param>
        partial void ValidateEx(ValidateResult result);*/
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, "Model", Entity.Name);
            WriteFile(file + ".cs", code);
        }


        #endregion

        #region 属性定义


        private string Properties()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region 属性");
            foreach (PropertyConfig property in Columns.Where(p => p.CanUserInput))
            {
                var attribute = new StringBuilder();
                attribute.Append("[DataMember");
                //if (!IsClient)
                {
                    attribute.Append(!property.NoneJson
                        ? $@" , JsonProperty(""{property.Name}"", NullValueHandling = NullValueHandling.Ignore)"
                        : " , JsonIgnore");
                }
                attribute.Append("]");
                code.Append($@"

        /// <summary>
        /// {ToRemString(property.Caption ?? property.Description)}
        /// </summary>
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>
        {attribute}
        public {property.LastCsType ?? "int"} {property.Name}
        {{
            get;
            set;
        }}");
            }
            code.Append(@"
        #endregion");

            return code.ToString();
        }

        #endregion

        #region 转Form


        private string ToForm()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region 转为符合HTTP协议的FORM的文本
        /// <summary>转为符合HTTP协议的FORM的文本</summary>
        /// <returns>符合HTTP协议的FORM的文本</returns>
        public string ToFormString()
        {
            return $@""");


            bool isFirst = true;
            foreach (PropertyConfig property in Columns.Where(p => p.CanUserInput))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append("&");
                code.Append(property.CsType == "string"
                    ? $"{property.Name}={{HttpUtility.UrlEncode({property.Name}, Encoding.UTF8)}}"
                    : $"{property.Name}={{{property.Name}}}");
            }
            code.Append(@""";
        }

        /// <summary>到文本</summary>
        /// <returns>文本</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return ToFormString();
        }

        #endregion");

            return code.ToString();
        }

        #endregion

        #region 数据校验

        public string ValidateCode()
        {
            if (!SolutionConfig.Current.HaseValidateCode)
                return "";
            var coder = new EntityValidateCoder { Entity = Entity };
            return $@"
        #region 数据校验
        /// <summary>数据校验</summary>
        /// <param name=""message"">返回的消息</param>
        /// <returns>成功则返回真</returns>
        bool IApiArgument.Validate(out string message)
        {{
            var result = Validate();
            message = result.Messages.LinkToString('；');
            return result.succeed;
        }}

        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name=""result"">结果存放处</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns>数据校验对象</returns>
        public ValidateResult Validate()
        {{
            var result = new ValidateResult();{coder.Code()}
            ValidateEx(result);
            return result;
        }}
        #endregion";
        }

        #endregion
    }

}

