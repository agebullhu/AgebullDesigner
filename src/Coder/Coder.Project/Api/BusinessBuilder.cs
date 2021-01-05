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
        static DataAccess<TEntity> CreateDataAccess<TEntity>()
            where TEntity : class, new()
        {{
            return {Project.DataBaseObjectName}Ex.CreateDataAccess<TEntity>(DependencyHelper.ServiceProvider);
        }}

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
            var cap = Model.LastProperties.FirstOrDefault(p => p.IsCaption);
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
            var childs = await Access.LoadIdssAsync<{Model.PrimaryColumn.CsType}>(p => p.Id,p => p.ParentId == id);
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
        public async Task<List<Agebull.EntityModel.Vue.TreeNode>> LoadTree(long pid)
        {{
            if (pid <= 0)
                pid = 0;
            var node = new List<Agebull.EntityModel.Vue.TreeNode>();
            using var scope = Access.Select(nameof({Model.EntityName}.{Model.PrimaryField}),nameof({Model.EntityName}.{cap.Name}));
            await LoadTree(pid, node);
            return node;
        }}

        /// <summary>
        ///     载入树节点
        /// </summary>
        public async Task LoadTree(long pid, List<Agebull.EntityModel.Vue.TreeNode> parent)
        {{
            var childs = await Access.AllAsync(p => p.ParentId == pid);
            foreach (var ch in childs)
            {{
                var node = new Agebull.EntityModel.Vue.TreeNode
                {{
                    Id = ch.{Model.PrimaryField}.ToString(),
                    Label = ch.{cap.Name},
                    Tag = pid.ToString(),
                    Children = new List<Agebull.EntityModel.Vue.TreeNode>()
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
            code.Append(@"
        #region 设计器命令
    ");
            bool hase = false;

            if (Model.TreeUi)
            {
                hase = true;
                code.Append(@"
        /// <summary>
        ///     载入树节点
        /// </summary>
        public Task<List<Agebull.EntityModel.Vue.TreeNode>> LoadTree()
        {
            return Task.FromResult(new List<Agebull.EntityModel.Vue.TreeNode>());
        }");
            }
            var cap = Model.LastProperties.FirstOrDefault(p => p.IsCaption);
            if (cap != null)
            {
                hase = true;
                code.Append($@"
        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        public async Task<List<Agebull.EntityModel.Vue.DataItem>> ComboValues()
        {{
            using var scope = Access.Select(nameof({Model.EntityName}.{Model.PrimaryField}),nameof({Model.EntityName}.{cap.Name}));
            var datas =await Access.AllAsync();
            return datas.Count == 0
                ? new List<Agebull.EntityModel.Vue.DataItem>()
                : datas.OrderBy(p => p.{cap.Name}).Select(p => new Agebull.EntityModel.Vue.DataItem
                {{
                    Id = p.{Model.PrimaryField}.ToString(),
                    Text = p.{cap.Name}
                }}).ToList();
        }}");
            }
            if (Model is ModelConfig model)
                foreach (var cmd in model.Commands.Where(p => !p.IsLocalAction))
                {
                    hase = true;
                    code.Append($@"

        /// <summary>
        ///     {cmd.Caption.ToRemString()}
        /// </summary>
        /// <remark>
        ///     {cmd.Description.ToRemString()}
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