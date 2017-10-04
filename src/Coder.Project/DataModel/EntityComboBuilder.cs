using System.Linq;

namespace Agebull.Common.SimpleDesign.Coder.Cs
{
    public sealed class EntityComboBuilder : EntityBuilderBase
    {
        /// <summary>
        /// �����ļ�������
        /// </summary>
        protected override string Folder => "ComboApi";

        /// <summary>
        /// ��������
        /// </summary>
        public override string BaseCode => ComboCode();
        /// <summary>
        ///     ���ɴ���
        /// </summary>
        string ComboCode()
        {
            var title = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (title == null)
                return "";
            string filter = "";
            if (Entity.Interfaces?.Contains("IStateData") ?? false)
                filter = "p => p.DataState == DataStateType.Enable";
            return  $@"using System.Collections.Generic;
using System.Linq;
using Agebull.Common.DataModel.Redis;
using Agebull.ProjectDeveloper.WebDomain.Models;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;
using Gboxt.Common.WebUI;

namespace {NameSpace}.DataAccess
{{
    partial class {Entity.Name}DataAccess
    {{
        /// <summary>
        /// �����б��
        /// </summary>
        private const string comboKey = ""ui:combo:{Entity.Name}"";
        /// <summary>
        /// ��������
        /// </summary>
        private const string treeKey = ""ui:tree:{Entity.Name}"";

        /// <summary>
        /// ȡ�������б�ֵ
        /// </summary>
        /// <returns></returns>
        public static List<EasyComboValues> GetComboValues()
        {{
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {{
                var result = proxy.Client.Get<List<EasyComboValues>>(comboKey);
                if (result == null)
                {{
                    var access = new {Entity.Name}DataAccess();
                    var datas = access.All({filter});
                    result = new List<EasyComboValues>{{EasyComboValues.Empty}};
                    result.AddRange(datas.Select(p => new EasyComboValues(p.{Entity.PrimaryField}, p.{title.PropertyName})));
                    proxy.Client.Set(comboKey, result);
                }}
                return result;
            }}
        }}

        /// <summary>
        /// ȡ��������ֵ
        /// </summary>
        /// <returns></returns>
        public static List<EasyUiTreeNode> GetTreeValues()
        {{
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {{
                var result = proxy.Client.Get<List<EasyUiTreeNode>>(treeKey);
                if (result == null)
                {{
                    var access = new {Entity.Name}DataAccess();
                    var datas = access.All({filter});
                    result = new List<EasyUiTreeNode>{{EasyUiTreeNode.EmptyNode}};
                    result.AddRange(datas.Select(p => new EasyUiTreeNode
                    {{
                        ID = p.{Entity.PrimaryField},
                        Text = p.{title.PropertyName},
                        Title = p.{title.PropertyName},
                        IsOpen = true
                    }}));
                    proxy.Client.Set(treeKey, result);
                }}
                return result;
            }}
        }}

        /// <summary>
        ///     ������ɺ��ڴ���(Insert��Update)
        /// </summary>
        /// <param name=""entity""></param>
        protected sealed override void OnDataSaved(DataOperatorType operatorType,{Entity.EntityName} entity)
        {{
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {{
                proxy.RemoveKey(treeKey);
                proxy.RemoveKey(comboKey);
            }}
        }}

        /// <summary>
        ///     ����������(����ʵ�����������)
        /// </summary>
        /// <param name=""condition"">ִ������</param>
        /// <param name=""args"">����ֵ</param>
        /// <param name=""operatorType"">��������</param>
        protected sealed override void OnOperatorExecutd(DataOperatorType operatorType, string condition, IEnumerable<MySqlParameter> args)
        {{
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {{
                proxy.RemoveKey(treeKey);
                proxy.RemoveKey(comboKey);
            }}
        }}
    }}
}}";
            
        }
        

    }
}