using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class BusinessBuilder : CoderWithEntity
    {

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Business_cs";
        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string baseClass = "UiBusinessLogicBase";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "BusinessLogicByStateData";
                if (Entity.Interfaces.Contains("IHistoryData"))
                    baseClass = "BusinessLogicByHistory";
                if (Entity.Interfaces.Contains("IAuditData"))
                    baseClass = "BusinessLogicByAudit";
            }
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using {(Project.DbType == DataBaseType.MySql ? "MySql.Data.MySqlClient" : "System.Data.Sql")};
using Gboxt.Common.DataModel.{(Project.DbType == DataBaseType.MySql ? "MySql" : "SqlServer")};

using Agebull.Common.DataModel.Redis;

using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.BusinessLogic;
using {NameSpace}.DataAccess;

namespace {NameSpace}.BusinessLogic
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    public sealed partial class {Entity.Name}BusinessLogic : {baseClass}<{Entity.EntityName},{Entity.Name}DataAccess>
    {{{CommandExCode()}
        /*// <summary>
        ///     ����ǰ�Ĳ���
        /// </summary>
        /// <param name=""data"">����</param>
        /// <param name=""isAdd"">�Ƿ�Ϊ����</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        protected override bool OnSaving({Entity.EntityName} data, bool isAdd)
        {{
             return base.OnSaving(data, isAdd);
        }}

        /// <summary>
        ///     ������ɺ�Ĳ���
        /// </summary>
        /// <param name=""data"">����</param>
        /// <param name=""isAdd"">�Ƿ�Ϊ����</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        protected override bool OnSaved({Entity.EntityName} data, bool isAdd)
        {{
             return base.OnSaved(data, isAdd);
        }}
        /// <summary>
        ///     ���û��༭�����ݵı���ǰ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <param name=""isAdd"">�Ƿ�Ϊ����</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        protected override bool LastSavedByUser(MeetingData data, bool isAdd)
        {{
            return base.LastSavedByUser(data, isAdd);
        }}

        /// <summary>
        ///     ���û��༭�����ݵı���ǰ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <param name=""isAdd"">�Ƿ�Ϊ����</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        protected override bool PrepareSaveByUser(MeetingData data, bool isAdd)
        {{
            return base.PrepareSaveByUser(data, isAdd);
        }}*/
    }}
}}
";
            var file = ConfigPath(path, "File_Model_Business", Entity.Name + "BusinessLogic", ".cs");
            SaveCode(file, code);
        }
        private string CommandExCode()
        {
            var code = new StringBuilder();
            if (Entity.TreeUi)
            {
                code.Append($@"

        /// <summary>
        ///     �������ڵ�
        /// </summary>
        internal List<EasyUiTreeNode> LoadTree(int pid)
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
        internal bool {cmd.Name}(int id)
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
using Agebull.Common.Client.ServiceAccess;
";

        private string EntityType => $"{Entity.Parent.DataBaseObjectName}.Table_{Entity.Name}";

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using {(Project.DbType == DataBaseType.MySql ? "MySql.Data.MySqlClient" : "System.Data.Sql")};
using Gboxt.Common.DataModel.{(Project.DbType == DataBaseType.MySql ? "MySql" : "SqlServer")};
using System.Linq;
using System.Text;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.BusinessLogic;
using {NameSpace}.DataAccess;

{usingCode}

namespace {NameSpace}.BusinessLogic
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    partial class {Entity.Name}BusinessLogic
    {{
        
        /// <summary>
        ///     ʵ������
        /// </summary>
        public override int EntityType => {EntityType};

{SyncCode()}
{CommandCode()}
    }}
}}";
            var file = ConfigPath(path, "File_Model_Business", Entity.Name + "BusinessLogic", ".Designer.cs");
            SaveCode(file, code);
        }

        private string SyncCode()
        {
            return SolutionConfig.Current.IsWeb ? "" : $@"
        /// <summary>
        /// ���޸ķ��͵����׷�����
        /// </summary>
        /// <param name=""data"">�޸ĵ�����</param>
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