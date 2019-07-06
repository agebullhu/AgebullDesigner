using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 业务逻辑对象生成器
    /// </summary>
    public sealed class BusinessBuilder : CoderWithEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Business_cs";
        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var fileName = "BusinessLogic.cs";
            var file = Path.Combine(path, Entity.Name + fileName);
            if (Entity.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                else if (!string.IsNullOrWhiteSpace(Entity.Alias))
                {
                    var oldFile = Path.Combine(path, Entity.Alias + fileName);
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Entity.Alias + fileName);
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
            string baseClass = "UiBusinessLogicBase";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "BusinessLogicByStateData";
                if (Entity.Interfaces.Contains("IAuditData"))
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

using {(Project.DbType == DataBaseType.MySql ? "MySql.Data.MySqlClient" : "System.Data.Sql")};

using Agebull.Common;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;

using Agebull.EntityModel.{(Project.DbType == DataBaseType.MySql ? "MySql" : "SqlServer")};
using Agebull.EntityModel.BusinessLogic;

{Project.UsingNameSpaces}

using {NameSpace}.DataAccess;
using {SolutionConfig.Current.NameSpace}.DataAccess;
#endregion

namespace {NameSpace}.BusinessLogic
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    public partial class {Entity.Name}BusinessLogic : {baseClass}<{Entity.EntityName},{Entity.Name}DataAccess>
    {{
{CommandExCode()}

{InterfaceExtendCode()}
        #region CURD扩展
        /*// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving({Entity.EntityName} data, bool isAdd)
        {{
             return base.OnSaving(data, isAdd);
        }}

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved({Entity.EntityName} data, bool isAdd)
        {{
             return base.OnSaved(data, isAdd);
        }}
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool LastSavedByUser({Entity.EntityName} data, bool isAdd)
        {{
            return base.LastSavedByUser(data, isAdd);
        }}

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""isAdd"">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser({Entity.EntityName} data, bool isAdd)
        {{
            return base.PrepareSaveByUser(data, isAdd);
        }}*/
        #endregion
    }}
}}
";
        }

        private string InterfaceExtendCode()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region 树形数据
    ");
            if (Entity.Interfaces.Contains("IInnerTree"))
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
                    ID = ch.{Entity.PrimaryField},
                    Text = ch.{Entity.Properties.FirstOrDefault()?.Name ?? Entity.Properties[1].Name}.ToString(),
                    IsFolder = true,
                    Tag = pid.ToString()
                }};
                LoadTree(ch.Id, cnode.Children);
                node.Add(cnode);
            }}
        }}");
            }

            code.Append(@"
        #endregion");
            return code.ToString();
        }
        private string CommandExCode()
        {
            var code = new StringBuilder();
            code.Append(@"
        #region 设计器命令
    ");
            var cap = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (cap != null)
            {
                code.Append(@"
        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        public List<Agebull.EntityModel.EasyUI.EasyComboValues> ComboValues()
        {");
                code.Append(Project.DbType == DataBaseType.MySql
                    ? $@"
            var fields = $""`{{Access.FieldMap[nameof({Entity.EntityName}.{Entity.PrimaryField})]}}`,`{{Access.FieldMap[nameof({Entity.EntityName}.{cap.Name})]}}`"";"
                    : $@"
            var fields = $""[{{Access.FieldMap[nameof({Entity.EntityName}.{Entity.PrimaryField})]}}],[{{Access.FieldMap[nameof({Entity.EntityName}.{cap.Name})]}}]"";");

                code.Append($@"
            List<{Entity.EntityName}> datas;
            using (DbReaderScope<{Entity.EntityName}>.CreateScope(Access, fields, (reader, data) =>
            {{
                data.{Entity.PrimaryField} = reader.GetInt64(0);
                data.{cap.Name} = reader.IsDBNull(1) ? null : reader.GetString(1);
            }}))
            {{
                datas = Access.All(");
                if (Entity.Interfaces.Contains("IStateData"))
                    code.Append("p => p.DataState <= EntityModel.Common.DataStateType.Enable");
                code.Append($@");
            }}
            return datas.Count == 0
                ? new System.Collections.Generic.List<Agebull.EntityModel.EasyUI.EasyComboValues>()
                : datas.OrderBy(p => p.{cap.Name}).Select(p => new Agebull.EntityModel.EasyUI.EasyComboValues
                {{
                    Key = p.{Entity.PrimaryField},
                    Value = p.{cap.Name}
                }}).ToList();
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
        public bool {cmd.Name}(int id)
        {{
            //Access.SetValue(p => p.Id, 0, id);
            return true;
        }}");
            }

            code.Append(@"
        #endregion");
            return code.ToString();
        }

        private string CommandCode()
        {
            var code = new StringBuilder();
            foreach (var cmd in Entity.Commands.Where(p => !p.IsLocalAction && !p.IsSingleObject))
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

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var fileName = "BusinessLogic.Designer.cs";
            var file = Path.Combine(path, Entity.Name + fileName);
            if (Entity.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                else if (!string.IsNullOrWhiteSpace(Entity.Alias))
                {
                    var oldFile = Path.Combine(path, Entity.Alias + fileName);
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Entity.Alias + fileName);
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
            StringBuilder alias = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                alias.Append($@"
        /// <summary>
        /// {Entity.Description} 别名
        /// </summary>
        {(Entity.IsInternal ? "internal" : "public")} sealed class {Entity.Alias}BusinessLogic : {Entity.Name}BusinessLogic
        {{
        }}");
            }
            var code = $@"#region
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

using {(Project.DbType == DataBaseType.MySql ? "MySql.Data.MySqlClient" : "System.Data.Sql")};

using Agebull.Common;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.{(Project.DbType == DataBaseType.MySql ? "MySql" : "SqlServer")};
using Agebull.EntityModel.BusinessLogic;

{Project.UsingNameSpaces}

using {NameSpace}.DataAccess;
using {SolutionConfig.Current.NameSpace}.DataAccess;

#endregion

namespace {NameSpace}.BusinessLogic
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    partial class {Entity.Name}BusinessLogic
    {{
        
        /// <summary>
        ///     实体类型
        /// </summary>
        public override int EntityType => {Entity.EntityName}._DataStruct_.EntityIdentity;

{SyncCode()}
{CommandCode()}
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
        public void SendDataChanged({Entity.EntityName} data)
        {{
#if LinkServer
            using (var client = new DataNoticeServiceClient())
            {{
                client.Endpoint.Behaviors.Add(new ProxyEndpointBehavior());
                client.Open();
                client.On{Entity.Name}Chanaged(data, data.Id);
                client.Close();
            }}
#endif
        }}";
        }
    }
}