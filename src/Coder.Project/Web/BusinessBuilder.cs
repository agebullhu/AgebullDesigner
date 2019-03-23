using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
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
            string baseClass = "UiBusinessLogicBase";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "BusinessLogicByStateData";
                if (Entity.Interfaces.Contains("IAuditData"))
                    baseClass = "BusinessLogicByAudit";
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
using Agebull.EntityModel.BusinessLogic.{(Project.DbType == DataBaseType.MySql ? "MySql" : "SqlServer")};

{Project.UsingNameSpaces}

using {NameSpace}.DataAccess;
#endregion

namespace {NameSpace}.BusinessLogic
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    public sealed partial class {Entity.Name}BusinessLogic : {baseClass}<{Entity.EntityName},{Entity.Name}DataAccess,{Entity.Parent.DataBaseObjectName}>
    {{
{CommandExCode()}

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
    }}
}}
";
            var file = ConfigPath(Entity, FileSaveConfigName, path, null, Entity.Name + "BusinessLogic") ;
            SaveCode(file + ".cs", code);
        }
        private string CommandExCode()
        {
            var code = new StringBuilder();
            if (Entity.TreeUi)
            {
                code.Append($@"

        /// <summary>
        ///     载入树节点
        /// </summary>
        public List<EasyUiTreeNode> LoadTree(int pid)
        {{
            EasyUiTreeNode node;
            using (var proxy = new RedisProxy(RedisProxy.DbWebCache))
            {{
                node = proxy.Client.Get<EasyUiTreeNode>($""ui:{Entity.Name.ToLower()}:tree:{{pid}}"");
            }}
            return new List<EasyUiTreeNode> {{ node }};
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

        private string usingCode => SolutionConfig.Current.IsWeb
            ? ""
            : @"
using Fuctures.Manage.BusinessLogical.WCF;
using Agebull.Common.Mvvm.ServiceAccess;
";

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
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
using Agebull.EntityModel.BusinessLogic.{(Project.DbType == DataBaseType.MySql ? "MySql" : "SqlServer")};

{Project.UsingNameSpaces}

using {NameSpace}.DataAccess;

{usingCode}
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
        public override int EntityType => { Entity.EntityName}._DataStruct_.EntityIdentity;

{SyncCode()}
{CommandCode()}
    }}
}}";
            var file = ConfigPath(Entity, FileSaveConfigName, path, null, Entity.Name + "BusinessLogic");
            SaveCode(file + ".Designer.cs", code);
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