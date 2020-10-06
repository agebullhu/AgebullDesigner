using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 业务逻辑对象生成器
    /// </summary>
    public sealed class BusinessBuilder<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Business_cs";
        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            if (Model.IsInterface || Model.IsQuery)
                return;
            var fileName = "BusinessLogic.cs";
            var file = Path.Combine(path, Model.EntityName + fileName);
            if (Model.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                else if (!string.IsNullOrWhiteSpace(Model.Alias))
                {
                    var oldFile = Path.Combine(path, Model.Alias + fileName);
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Model.Alias + fileName);
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }
            var code = CreateExtendCode();
            SaveCode(file, code);
        }

        private string CreateExtendCode()
        {
            //string dbNameSpace = Project.DbType switch
            //{
            //    DataBaseType.SqlServer => "System.Data.Sql",
            //    DataBaseType.Sqlite => "Microsoft.Data.Sqlite",
            //    _ => "MySqlConnector",//case DataBaseType.MySql:
            //};
            string baseClass = "BusinessLogicBase";
            if (!Model.IsQuery && Model.Interfaces != null)
            {
                if (Model.Interfaces.Contains("IStateData"))
                    baseClass = "BusinessLogicByStateData";
                if (Model.Interfaces.Contains("IAuditData"))
                    baseClass = "BusinessLogicByAudit";
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
using Agebull.EntityModel.Common;
using Agebull.EntityModel.{Project.DbType};
using Agebull.EntityModel.BusinessLogic;

{Project.UsingNameSpaces}
using {NameSpace}.DataAccess;
#endregion

namespace {NameSpace}
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    public partial class {Model.EntityName}BusinessLogic : {baseClass}<{Model.EntityName},{Model.PrimaryColumn.CsType}>
    {{
        /// <summary>
        ///  生成数据访问对象
        /// </summary>
        protected sealed override DataAccess<{Model.EntityName}> CreateAccess()
        {{
            return ServiceProvider.CreateDataAccess<{Model.EntityName}>();
        }}
        #region 基础继承

        /*// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving({Model.EntityName} data, bool isAdd)
        {{
             return base.OnSaving(data, isAdd);
        }}

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved({Model.EntityName} data, bool isAdd)
        {{
             return base.OnSaved(data, isAdd);
        }}

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool LastSavedByUser({Model.EntityName} data, bool isAdd)
        {{
            return base.LastSavedByUser(data, isAdd);
        }}

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser({Model.EntityName} data, bool isAdd)
        {{
            return base.PrepareSaveByUser(data, isAdd);
        }}*/
        #endregion

{CommandExCode()}{InterfaceExtendCode()}
    }}
}}
";
        }

        private string InterfaceExtendCode()
        {
            if (!(Model.Interfaces.Contains("IInnerTree")))
                return null;
            var code = new StringBuilder();
            code.Append(@"

        #region 树形数据
    ");
            if (Model.Interfaces.Contains("IInnerTree"))
            {
                code.Append(@"

        /// <summary>删除对象操作</summary>
        protected override bool DoDelete(long id)
        {
            DeleteChild(id);
            return base.DoDelete(id);
        }

        /// <summary>级联删除</summary>
        void DeleteChild(long id)
        {
            var childs = Access.LoadValues(p => p.Id, Convert.ToInt64, p => p.ParentId == id);
            foreach (var ch in childs.Distinct())
            {
                if (ch <= 0 || ch == id)
                    continue;
                DeleteChild(ch);
                Access.DeletePrimaryKey(ch);
            }
        }");
                code.Append($@"

        /// <summary>
        ///     载入树节点
        /// </summary>
        public List<Agebull.EntityModel.EasyUI.EasyUiTreeNode> LoadTree(long pid)
        {{
            if (pid <= 0)
                pid = 0;
            var node = new List<Agebull.EntityModel.EasyUI.EasyUiTreeNode>();
            LoadTree(pid, node);
            return node;
        }}

        /// <summary>
        ///     载入树节点
        /// </summary>
        public void LoadTree(long pid, List<Agebull.EntityModel.EasyUI.EasyUiTreeNode> node)
        {{
            var childs = Access.All(p => p.ParentId == pid);
            foreach (var ch in childs)
            {{
                var cnode = new Agebull.EntityModel.EasyUI.EasyUiTreeNode
                {{
                    ID = ch.{Model.PrimaryField},
                    Text = ch.{Model.LastProperties.FirstOrDefault()?.Name ?? Model.LastProperties[1].Name}.ToString(),
                    IsFolder = true,
                    Tag = pid.ToString()
                }};
                LoadTree(ch.Id, cnode.Children);
                node.Add(cnode);
            }}
        }}");
            }
            code.Append(@"
        #endregion
");
            return code.ToString();
        }
        private string CommandExCode()
        {
            var code = new StringBuilder();
            code.Append(@"

        #region 设计器命令
    ");
            bool hase = false;
            var cap = Model.LastProperties.FirstOrDefault(p => p.IsCaption);
            if (cap != null)
            {
                hase = true;
                code.Append(@"
        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        public List<Agebull.EntityModel.EasyUI.EasyComboValues> ComboValues()
        {");
                code.Append(Project.DbType == DataBaseType.MySql
                    ? $@"
            var fields = $""`{{Access.FieldMap[nameof({Model.EntityName}.{Model.PrimaryField})]}}`,`{{Access.FieldMap[nameof({Model.EntityName}.{cap.Name})]}}`"";"
                    : $@"
            var fields = $""[{{Access.FieldMap[nameof({Model.EntityName}.{Model.PrimaryField})]}}],[{{Access.FieldMap[nameof({Model.EntityName}.{cap.Name})]}}]"";");

                code.Append($@"
            List<{Model.EntityName}> datas;
            using (DbReaderScope<{Model.EntityName}>.CreateScope(Access, fields, (reader, data) =>
            {{
                data.{Model.PrimaryField} = ({Model.PrimaryColumn.LastCsType})reader.GetInt64(0);
                data.{cap.Name} = reader.IsDBNull(1) ? null : reader.GetString(1);
            }}))
            {{
                datas = Access.All(");
                if (Model.Interfaces.Contains("IStateData"))
                    code.Append("p => p.DataState <= DataStateType.Enable");
                code.Append($@");
            }}
            return datas.Count == 0
                ? new System.Collections.Generic.List<Agebull.EntityModel.EasyUI.EasyComboValues>()
                : datas.OrderBy(p => p.{cap.Name}).Select(p => new Agebull.EntityModel.EasyUI.EasyComboValues
                {{
                    Key = p.{Model.PrimaryField},
                    Value = p.{cap.Name}
                }}).ToList();
        }}");
            }
            if (Model is ModelConfig model)
                foreach (var cmd in model.Commands.Where(p => !p.IsLocalAction))
                {
                    hase = true;
                    code.Append($@"

        /// <summary>
        ///     {ToRemString(cmd.Caption)}
        /// </summary>
        /// <remark>
        ///     {ToRemString(cmd.Description)}
        /// </remark>
        public bool {cmd.Name}(int id)
        {{
            //Access.SetValue(p => p.Id, 0, id);
            return true;
        }}");
                }
            if (!hase)
                return null;
            code.Append(@"
        #endregion
");
            return code.ToString();
        }


    }
}
/*
 
        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            return;
            var fileName = "BusinessLogic.Designer.cs";
            var file = Path.Combine(path, Model.Name + fileName);
            if (Model.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                else if (!string.IsNullOrWhiteSpace(Model.Alias))
                {
                    var oldFile = Path.Combine(path, Model.Alias + fileName);
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Model.Alias + fileName);
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }
            var code = CreateBaseCode();
            SaveCode(file, code);
        }

        private string CreateBaseCode()
        {
            string dbNameSpace = Project.DbType switch
            {
                DataBaseType.SqlServer => "System.Data.Sql",
                DataBaseType.Sqlite => "Microsoft.Data.Sqlite",
                _ => "MySqlConnector",//case DataBaseType.MySql:
            };
            StringBuilder alias = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                alias.Append($@"
        /// <summary>
        /// {Model.Description} 别名
        /// </summary>
        {(Model.IsInternal ? "internal" : "public")} sealed class {Model.Alias}BusinessLogic : {Model.Name}BusinessLogic
        {{
        }}");
            }
            var code = $@"#region
using System;
using System.Collections.Generic;
using System.Data;

using {dbNameSpace};

using Agebull.Common;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.{Project.DbType};
using Agebull.EntityModel.BusinessLogic;

{Project.UsingNameSpaces}

using {NameSpace}.DataAccess;

#endregion

namespace {NameSpace}.BusinessLogic
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    partial class {Model.Name}BusinessLogic
    {{
{SyncCode()}{CommandCode()}
    }}{alias}
}}";
            return code;
        }

        private string SyncCode()
        {
            return SolutionConfig.Current.IsWeb ? "" : $@"
        /// <summary>
        /// 将修改发送到交易服务器
        /// </summary>
        /// <param name=""data"">修改的数据</param>
        public void SendDataChanged({Model.EntityName} data)
        {{
#if LinkServer
            using (var client = new DataNoticeServiceClient())
            {{
                client.Endpoint.Behaviors.Add(new ProxyEndpointBehavior());
                client.Open();
                client.On{Model.Name}Chanaged(data, data.Id);
                client.Close();
            }}
#endif
        }}";
        }
        private string CommandCode()
        {
            var code = new StringBuilder();
            if (Model is ModelConfig model)
                foreach (var cmd in model.Commands.Where(p => !p.IsLocalAction && !p.IsSingleObject))
                {
                    code.Append($@"

        /// <summary>
        ///     {ToRemString(cmd.Caption)}
        /// </summary>
        /// <remark>
        ///     {ToRemString(cmd.Description)}
        /// </remark>
        public bool Do{cmd.Name}(int[] ids)
        {{
            foreach (var id in ids)
            {{
                if(!{cmd.Name}(id))
                    return false;
            }}
            return true;
        }}");
                }

            return code.ToString();
        }
 */