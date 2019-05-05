using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ProjectApiActionCoder : CoderWithEntity, IAutoRegister
    {

        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_CS";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var file = CheckOldFile(path);
            WriteFile(file + ".Designer.cs", BaseCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var file = CheckOldFile(path);
            WriteFile(file + ".cs", ExtendCode());
            //ExportCsCode(path);
        }

        string CheckOldFile(string path)
        {
            var fileNew = ConfigPath(Entity, "File_Edit_Api_d_cs", path, Entity.Classify, $"{Entity.Name}ApiController");
            return fileNew;
            var folderNew = Path.GetDirectoryName(fileNew);
            var fileOld = ConfigPath(Entity, "File_Web_Api_cs", path, Entity.Name, $"{Entity.Name}ApiController");
            var folderOld = Path.GetDirectoryName(fileOld);

            if (folderNew == folderOld || !File.Exists(fileOld + ".Designer.cs"))
                return fileNew;
            try
            {
                IOHelper.CheckPath(path, Entity.Classify);
                File.Move(fileOld + ".Designer.cs", fileNew + ".Designer.cs");
                File.Move(fileOld + ".cs", fileNew + ".cs");
                Directory.Delete(folderOld);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            return fileNew;
        }
        #endregion

        #region Export
        /*
        private void ExportCsCode(string path)
        {
            var file = ConfigPath(Entity, "File_Web_Export_cs", path, $"Page\\{Entity.Parent.Name}\\{Entity.Name}", "Export.cs");
            var coder = new ExportActionCoder
            {
                Entity = Entity,
                Project = Project
            };
            WriteFile(file, coder.Code());
        }

        private string ExportAspxCode()
        {
            return $@"<%@ Page Language='C#' AutoEventWireup='true'  Inherits='{NameSpace}.{Entity.Name}Page.ExportAction' %>";
        }
        */
        #endregion

        #region 代码片断

        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-Api", "表单保存", "cs", ApiHelperCoder.InputConvert4);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs", ExtendCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ExtendCode(EntityConfig entity)
        {
            Entity = entity;
            return ExtendCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BaseCode(EntityConfig entity)
        {
            Entity = entity;
            return BaseCode();
        }

        #endregion

        #region 代码

        private string BaseCode()
        {
            var coder = new ApiHelperCoder();
            return
                $@"#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Agebull.Common.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Agebull.MicroZero;
using Agebull.MicroZero.ZeroApis;

{Project.UsingNameSpaces}

using {NameSpace};
using {NameSpace}.BusinessLogic;
using {NameSpace}.DataAccess;
#endregion

namespace {NameSpace}.WebApi.Entity
{{
    partial class {Entity.Name}ApiController
    {{
        #region 设计器命令
{CommandCsCode()}

        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<{Entity.EntityName}> DefaultGetListData()
        {{
            var filter = new LambdaItem<{Entity.EntityName}>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }}

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name=""filter"">筛选器</param>
        public void SetKeywordFilter(LambdaItem<{Entity.EntityName}> filter)
        {{{QueryCode()}
        }}

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected void DefaultReadFormData({Entity.EntityName} data, FormConvert convert)
        {{{coder.InputConvert(Entity)}
        }}

        #endregion
    }}
}}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string QueryCode()
        {
            var code = new StringBuilder();
            var fields = Entity.ClientProperty.Where(p => !p.NoStorage && p.CanUserInput && p.CsType == "string" && !p.IsBlob).ToArray();
            if (fields.Length > 0)
            {
                code.Append(@"
            var keyWord = GetArg(""keyWord"");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>");
                bool first = true;
                foreach (var field in fields)
                {
                    if (first)
                        first = false;
                    else
                        code.Append(@" || 
                                   ");
                    code.Append($@"p.{field.Name}.Contains(keyWord)");
                }
                code.Append(@");
            }");
            }

            fields = Entity.ClientProperty.Where(p => !p.IsDiscard && p.CanUserInput).ToArray();
            foreach (var field in fields)
            {
                code.Append($@"
            if(ContainsArgument(""{field.JsonName}""))
            {{
                var {field.JsonName} =");

                switch (field.CsType.ToLower())
                {
                    case "short":
                    case "int16":
                    case "int":
                    case "int32":
                        code.Append($@"GetIntArg(""{field.JsonName}"");");
                        break;
                    case "bigint":
                    case "long":
                    case "int64":
                        code.Append($@"GetLongArg(""{field.JsonName}"");");
                        break;
                    case "decimal":
                        code.Append($@"GetDecimalArg(""{field.JsonName}"");");
                        break;
                    case "double":
                        code.Append($@"GetDoubleArg(""{field.JsonName}"");
                if (!double.IsNaN({field.JsonName}))");
                        break;
                    case "float":
                        code.Append($@"GetSingleArg(""{field.JsonName}"");
                if (!float.IsNaN({field.JsonName}))");
                        break;
                    case "datetime":
                        code.Append($@"GetDateArg(""{field.JsonName}"");
                if ({field.JsonName} > DateTime.MinValue)");
                        break;
                    case "bool":
                    case "boolean":
                        code.Append($@"GetBoolArg(""{field.JsonName}"");");
                        break;
                    case "byte":
                    case "sbyte":
                        code.Append($@"GetByteArg(""{field.JsonName}"");");
                        break;
                    case "guid":
                        code.Append($@"GetGuidArg(""{field.JsonName}"");");
                        break;
                    default:
                        code.Append($@"GetArg(""{field.JsonName}"");
                if (!string.IsNullOrEmpty({field.JsonName}))
                {{
                    if({field.JsonName} == ""*null*"")
                        filter.AddAnd(p => p.{field.Name} == null); 
                    else if({field.JsonName}[0] == '%')
                        filter.AddAnd(p => p.{field.Name}.Contains({field.JsonName}.Trim('%'))); 
                    else
                        filter.AddAnd(p => p.{field.Name} == {field.JsonName}); 
                }}
            }}");
                        continue;
                }
                if (field.IsEnum && !string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
                    filter.AddAnd(p => p.{field.Name} == ({field.CustomType}){field.JsonName});");
                }
                else
                    code.Append($@"
                    filter.AddAnd(p => p.{field.Name} == {field.JsonName});
            }}");
            }
            return code.ToString();
        }

        private string ExtendCode()
        {
            var folder = !string.IsNullOrWhiteSpace(Entity.PageFolder)
                ? Entity.PageFolder.Replace('\\', '/')
                    : string.IsNullOrEmpty(Entity.Classify)
                        ? Entity.Name
                        : $"{Entity.Classify}/{Entity.Name}";

            var page = $"/{Project.PageRoot}/{folder}/index.htm";

            var baseClass = "ApiController";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "ApiControllerForDataState";
                if (Entity.Interfaces.Contains("IAuditData"))
                    baseClass = "ApiControllerForAudit";
            }
            return $@"#region
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
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Agebull.MicroZero;
using Agebull.MicroZero.ZeroApis;

{Project.UsingNameSpaces}

using {NameSpace};
using {NameSpace}.BusinessLogic;

#endregion

namespace {NameSpace}.WebApi.Entity
{{
    /// <summary>
    ///  {ToRemString(Entity.Caption)}
    /// </summary>
    [RoutePrefix(""{Project.Abbreviation}/{Entity.Abbreviation}/v1"")]
    [ApiPage(""{page}"")]
    public partial class {Entity.Name}ApiController 
         : {baseClass}<{Entity.EntityName},{Entity.Name}BusinessLogic>
    {{
        #region 基本扩展

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<{Entity.EntityName}> GetListData()
        {{
            return DefaultGetListData();
        }}

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected override void ReadFormData({Entity.EntityName} data, FormConvert convert)
        {{
            DefaultReadFormData(data,convert);
        }}
        #endregion
    }}
}}";
        }


        private string CommandCsCode()
        {
            var code = new StringBuilder();
            if (Entity.TreeUi)
            {
                code.Append(@"

        /// <summary>
        ///     载入树节点
        /// </summary>
        [Route(""edit/tree"")]
        public ApiArrayResult<EasyUiTreeNode> OnLoadTree()
        {
            var nodes = Business.LoadTree(this.GetIntArg(""id""));
            return new ApiArrayResult<EasyUiTreeNode>
            {{
               Success = true,
               ResultData = nodes
            }};
        }");
            }
            var cap = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (cap != null)
            {
                code.Append($@"
        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        [Route(""edit/combo"")]
        public ApiArrayResult<EasyComboValues> ComboValues()
        {{");
                code.Append(Project.DbType == DataBaseType.MySql
                    ?$@"
            var fields = $""`{{Business.Access.FieldMap[nameof({Entity.EntityName}.{Entity.PrimaryField})]}}`,`{{Business.Access.FieldMap[nameof({cap.Name})]}}`"";"
                    : $@"
            var fields = $""[{{Business.Access.FieldMap[nameof({Entity.EntityName}.{Entity.PrimaryField})]}}],[{{Business.Access.FieldMap[nameof({cap.Name})]}}]"";");

                code.Append($@"
            List<{Entity.EntityName}> datas;
            using (DbReaderScope<{Entity.EntityName}>.CreateScope(Business.Access, fields, (reader, data) =>
            {{
                data.{Entity.PrimaryField} = reader.GetInt64(0);
                data.{cap.Name} = reader.IsDBNull(1) ? null : reader.GetString(1);
            }}))
            {{
                datas = Business.All(");
                if (Entity.Interfaces.Contains("IStateData"))
                    code.Append("p => p.DataState <= EntityModel.Common.DataStateType.Enable");
                code.Append($@");
            }}
            return ApiArrayResult<EasyComboValues>.Succees(datas.Count == 0
                ? new System.Collections.Generic.List<EasyComboValues>()
                : datas.OrderBy(p => p.{cap.Name}).Select(p => new EasyComboValues
                {{
                    Key = p.{Entity.PrimaryField},
                    Value = p.{cap.Name}
                }}).ToList());
        }}");
            }
            foreach (var cmd in Entity.Commands.Where(p => !p.IsLocalAction))
            {
                code.Append($@"
        /// <summary>
        ///     {ToRemString(cmd.Caption)}
        /// </summary>
        /// <remark>
        ///     {ToRemString(cmd.Description)}
        /// </remark>
        [Route(""edit/{cmd.Name.ToLWord()}"")]
        public ApiResult On{cmd.Name}()
        {{
            InitForm();");
                code.Append(cmd.IsSingleObject
                    ? $@"
            return !this.Business.{cmd.Name}(this.GetIntArg(""id""))"
                    : $@"
            return !this.Business.Do{cmd.Name}(this.GetIntArrayArg(""selects""))");
                code.Append(@"
            return IsFailed
                ? ApiResult.Error(State, Message)
                : ApiResult.Succees();
        }");
            }

            return code.ToString();
        }

        #endregion

    }
}