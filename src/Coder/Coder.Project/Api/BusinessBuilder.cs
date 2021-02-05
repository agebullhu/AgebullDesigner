using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 业务逻辑对象生成器
    /// </summary>
    public sealed class BusinessBuilder : CoderWithModel
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
            if (Model.IsInterface)
                return;
            var fileName = "BusinessLogic.cs";
            var file = Path.Combine(path, Model.EntityName + fileName);
            if (!Model.EnableDataBase)
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Agebull.Common;
using Agebull.Common.Ioc;
using ZeroTeam.MessageMVC;
using ZeroTeam.MessageMVC.ZeroApis;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.BusinessLogic;
using Agebull.EntityModel.Vue;

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
        #region 基础继承

        /// <summary>
        /// 构造
        /// </summary>
        public {Model.EntityName}BusinessLogic()
        {{
            ServiceProvider = DependencyHelper.ServiceProvider;
        }}

        /// <summary>
        ///  生成数据访问对象
        /// </summary>
        protected sealed override DataAccess<{Model.EntityName}> CreateAccess()
        {{
            return {Project.DataBaseObjectName}Ex.CreateDataAccess<{Model.EntityName}>(ServiceProvider);
        }}

        /// <summary>
        /// 构造数据访问对象
        /// </summary>
        static DataAccess<TEntity> CreateDataAccess<TEntity>() where TEntity : class, new()
        {{
            return {Project.DataBaseObjectName}Ex.CreateDataAccess<TEntity>(DependencyHelper.ServiceProvider);
        }}
{ModelDeleteCode()}
        #endregion
{CommandExCode()}{IInnerTreeCode()}
    }}
}}
";
        }

        private string IInnerTreeCode()
        {
            if (!(Model.Interfaces.Contains("IInnerTree")))
                return null;
            var cap = Model.CaptionColumn;

            return $@"
        #region 树形数据

        /// <summary>删除对象操作</summary>
        protected override async Task<bool> DoDelete({Model.PrimaryColumn.CsType} id)
        {{
            using var scope = Access.DynamicOption();
            Access.Option.DynamicOption.InjectionLevel = InjectionLevel.UpdateField;
            await DeleteChild(id);
            return await base.DoDelete(id);
        }}

        /// <summary>级联删除</summary>
        async Task DeleteChild({Model.PrimaryColumn.CsType} id)
        {{
            var childs = await Access.LoadIdsAsync<{Model.PrimaryColumn.CsType}>(p => p.ParentId == id);
            foreach (var ch in childs.Distinct())
            {{
                if (ch <= 0 || ch == id)
                    continue;
                await DeleteChild(ch);
                await Access.DeletePrimaryKeyAsync(ch);
            }}
        }}

        /// <summary>
        ///     载入树节点
        /// </summary>
        public async Task<List<TreeNode>> LoadTree(long pid)
        {{
            if (pid <= 0)
                pid = 0;
            var node = new List<TreeNode>();
            using var scope = Access.Select(nameof({Model.EntityName}.{Model.PrimaryField}),nameof({Model.EntityName}.{cap.Name}));
            await LoadTree(pid, node);
            return node;
        }}

        /// <summary>
        ///     载入树节点
        /// </summary>
        public async Task LoadTree(long pid, List<TreeNode> parent)
        {{
            var childs = await Access.AllAsync(p => p.ParentId == pid);
            foreach (var ch in childs)
            {{
                var node = new TreeNode
                {{
                    Id = ch.{Model.PrimaryField}.ToString(),
                    Label = ch.{cap.Name},
                    Tag = pid.ToString(),
                    Children = new List<TreeNode>()
                }};
                await LoadTree(ch.Id, node.Children);
                parent.Add(node);
            }}
        }}
        #endregion";
        }
        private string CommandExCode()
        {
            var code = new StringBuilder();
            if (Model.TreeUi)
            {
                TreeCode(code);
            }
            var cap = Model.CaptionColumn;
            if (cap != null)
            {
                ComboValues(code, cap);
            }
            CommandCode(code);
            if (code.Length == 0)
                return null;
            code.Append(@"
");
            return $@"
        #region 设计器命令{code}
        #endregion";
        }

        private void CommandCode(StringBuilder code)
        {
            foreach (var cmd in Model.Commands.Where(p => !p.IsLocalAction))
            {
                if (cmd.IsSingleObject)
                    code.Append($@"

        /// <summary>
        ///     {cmd.Caption.ToRemString()}
        /// </summary>
        /// <param name=""id"" >主键</param>
        /// <remark>
        ///     {cmd.Description.ToRemString()}
        /// </remark>
        public async Task<bool> {cmd.Name}({PrimaryProperty.CsType} id)
        {{
            var data = await Access.LoadByPrimaryKeyAsync(id);
            if (data == null)
                return false;
            return true;
        }}");
                else if (cmd.IsMulitOperator)
                    code.Append($@"

        /// <summary>
        ///     {cmd.Caption.ToRemString()}
        /// </summary>
        /// <param name=""ids"" >选择的主键</param>
        /// <remark>
        ///     {cmd.Description.ToRemString()}
        /// </remark>
        public async Task<bool> {cmd.Name}(List<{PrimaryProperty.CsType}> ids)
        {{
            return true;
        }}");
                else
                    code.Append($@"

        /// <summary>
        ///     {cmd.Caption.ToRemString()}
        /// </summary>
        /// <remark>
        ///     {cmd.Description.ToRemString()}
        /// </remark>
        public async Task<bool> {cmd.Name}()
        {{
            return true;
        }}");

            }
        }

        private void TreeCode(StringBuilder code)
        {
            if (Model.Interfaces.Contains("IInnerTree"))
                code.Append($@"
        /// <summary>
        ///     载入树节点
        /// </summary>
        public async Task<List<TreeNode>> LoadTree()
        {{
            var nodes = await LoadTree(0);
            return new List<TreeNode>
            {{
                new TreeNode
                {{
                    Id =""0"",
                    Label = ""{Model.Caption}"",
                    Tag = ""0"",
                    Children = nodes
                }}
            }};
        }}");
            else
                code.Append(@"
        /// <summary>
        ///     载入树节点
        /// </summary>
        public Task<List<TreeNode>> LoadTree()
        {
            return Task.FromResult(new List<TreeNode>());
        }");
        }

        private void ComboValues(StringBuilder code, IFieldConfig cap)
        {
            var parent = Model.ParentColumn;
            if (parent == null)
            {
                code.Append($@"
        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        public async Task<List<DataItem<{Model.PrimaryColumn.LastCsType}>>> ComboValues()
        {{
            using var scope = Access.Select(nameof({Model.EntityName}.{Model.PrimaryField}),nameof({Model.EntityName}.{cap.Name}));
            var datas =await Access.AllAsync();
            return datas.Count == 0
                ? new List<DataItem<{Model.PrimaryColumn.LastCsType}>>()
                : datas.OrderBy(p => p.{cap.Name}).Select(p => new DataItem<{Model.PrimaryColumn.LastCsType}>
                {{
                    Id = p.{Model.PrimaryField},
                    Text = p.{cap.Name}
                }}).ToList();
        }}");
            }
            else
            {
                code.Append($@"
        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        public async Task<List<DataItem<{Model.PrimaryColumn.LastCsType}>>> ComboValues()
        {{
            using var scope = Access.Select(nameof({Model.EntityName}.{Model.PrimaryField}),nameof({Model.EntityName}.{parent.Name}),nameof({Model.EntityName}.{cap.Name}));
            var datas =await Access.AllAsync();
            return datas.Count == 0
                ? new List<DataItem<{Model.PrimaryColumn.LastCsType}>>()
                : datas.OrderBy(p => p.{cap.Name}).Select(p => new DataItem<{Model.PrimaryColumn.LastCsType}>
                {{
                    Id = p.{Model.PrimaryField},
                    Text = p.{cap.Name},
                    ParentId = p.{parent.Name}
                }}).ToList();
        }}");
            }
        }


        private string ModelDeleteCode()
        {
            if (!(Model is ModelConfig model))
                return null;
            StringBuilder code = new StringBuilder();
            code.Append(@"
        ///<inheritdoc/>
        protected override async Task<bool> DoDelete(long id)
        {
            if (!await base.DoDelete(id))
                return false;");
            var firends = model.Releations.Where(p => p.CanDelete).ToArray();
            if (firends.Length == 0)
                return null;
            foreach (var firend in firends)
            {
                var entity = firend.ForeignEntity == model.Entity ? firend.PrimaryEntity : firend.ForeignEntity;
                var field = firend.ForeignEntity == model.Entity ? firend.PrimaryField : firend.ForeignField;
                code.Append($@"
            //{entity.Caption}
            await CreateDataAccess<{entity.EntityName}>().DeleteAsync(p => p.{field.Name} == id);");
            }
            code.Append(@"
            return true;
        }");
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
            if (Model.EnableDataBase)
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