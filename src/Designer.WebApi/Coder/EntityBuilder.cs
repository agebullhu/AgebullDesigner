using System.Linq;
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
        protected override void CreateDesignerCode(string path)
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

namespace {NameSpace}
{{
    {EntityRemHeader}
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class {Model.Name}
    {{
        {Properties()}
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, Model.Classify, Model.Name);
            WriteFile(file + ".cs", code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            string code = $@"using System;
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

using Agebull.EntityModel.Common;

{Project.UsingNameSpaces}

namespace {NameSpace}
{{
    sealed partial class {Model.Name} : IApiResultData , IApiArgument
    {{
        {ToData()}
        {ValidateCode()}
    }}
}}";
            var file = ConfigPath(Project, FileSaveConfigName, path, Model.Classify, Model.Name);
            WriteFile(file + ".api.cs", code);
        }


        #endregion

        #region 属性定义


        private string Properties()
        {
            var code = new StringBuilder();
            foreach (var property in Columns)//.Where(p => !p.NoneApiArgument)
            {
                string type = property.DataType == "ByteArray" ? "string" : property.LastCsType;
                code.Append(PropertyHeader(property));
                code.Append($@"
        public {type} {property.Name}
        {{
            get;
            set;
        }}");
            }

            return code.ToString();
        }

        #endregion

        #region 转转


        private string ToData()
        {
            if (Model.NoDataBase)
                return "";
            var code = new StringBuilder();
            int len = Columns.Max(p => p.Name.Length) + 2;
            code.Append($@"
        #region 与内部数据库对象互相转换

        /// <summary>转为内部数据库对象</summary>
        /// <returns>内部数据库对象</returns>
        public {Model.EntityName} ToData => new {Model.EntityName}
        {{");
            bool isFirst = true;
            foreach (var property in Columns.Where(p => !p.NoneApiArgument))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(",");
                code.Append($@"
            {property.Name}");
                code.Append(' ', len - property.Name.Length);
                code.Append("= ");
                code.Append(property.DataType == "ByteArray"
                    ? $@"{property.Name}==null || {property.Name}.Length == 0 ? null : Convert.FromBase64String({property.Name})"
                    : property.Name);
            }
            code.Append($@"
        }};

        /// <summary>转为参数对象</summary>
        /// <returns>参数对象</returns>
        public void FromData ({Model.EntityName} data) 
        {{");
            foreach (var property in Columns.Where(p => !p.NoneApiArgument))
            {
                code.Append($@"
            {property.Name}");
                code.Append(' ', len - property.Name.Length);
                code.Append("= ");
                code.Append(property.DataType == "ByteArray"
                    ? $@"data.{property.Name}==null || data.{property.Name}.Length == 0 ? null : Convert.ToBase64String(data.{property.Name})"
                    : property.Name);
                code.Append(';');
            }
            code.Append($@"
        }}

        /// <summary>转为参数对象</summary>
        /// <returns>参数对象</returns>
        public static {Model.Name} ToArgument ({Model.EntityName} data) => new {Model.Name}
        {{");
            isFirst = true;
            foreach (var property in Columns.Where(p => !p.NoneApiArgument))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(",");

                code.Append($@"
            {property.Name}");
                code.Append(' ', len - property.Name.Length);
                code.Append("= ");
                code.Append(property.DataType == "ByteArray"
                    ? $@"data.{property.Name}==null || data.{property.Name}.Length == 0 ? null : Convert.ToBase64String(data.{property.Name})"
                    : $@"data.{property.Name}");
            }
            code.Append(@"
        };

        #endregion");

            return code.ToString();
        }

        #endregion

        #region 数据校验

        public string ValidateCode()
        {
            var coder = new EntityValidateCoder { Model = Model };
            var code = coder.Code(Columns.Where(p => !p.NoneApiArgument));
            return $@"
        #region 数据校验
        /// <summary>数据校验</summary>
        /// <param name=""message"">返回的消息</param>
        /// <returns>成功则返回真</returns>
        public bool Validate(out string message)
        {{
            var result = Validate();
            message = result.succeed ? null : result.Items.Where(p => !p.succeed).Select(p => $""{{p.Caption}}:{{ p.Message}}"").LinkToString(';');
            return true;//result.succeed;
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
            var result = new ValidateResult();{code}
            ValidateEx(result);
            return result;
        }}
        #endregion";
        }

        #endregion
    }

}

