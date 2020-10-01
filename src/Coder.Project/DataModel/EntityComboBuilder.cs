using System.Linq;

namespace Agebull.EntityModel.RobotCoder
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
            var title = Model.LastProperties.FirstOrDefault(p => p.IsCaption);
            if (title == null)
                return "";
            string filter = "";
            if (Model.Interfaces?.Contains("IStateData") ?? false)
                filter = "p => p.DataState == DataStateType.Enable";
            return  $@"
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
using Agebull.Common.Configuration;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
using Agebull.EntityModel.MySql;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.EasyUI;

{Project.UsingNameSpaces}


namespace {NameSpace}.DataAccess
{{
    partial class {Model.Name}DataAccess
    {{
        /// <summary>
        /// �����б��
        /// </summary>
        private const string comboKey = ""ui:combo:{Model.Name}"";
        /// <summary>
        /// ��������
        /// </summary>
        private const string treeKey = ""ui:tree:{Model.Name}"";

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
                    var access = new {Model.Name}DataAccess();
                    var datas = access.All({filter});
                    result = new List<EasyComboValues>{{EasyComboValues.Empty}};
                    result.AddRange(datas.Select(p => new EasyComboValues(p.{Model.PrimaryField}, p.{title.Name})));
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
                    var access = new {Model.Name}DataAccess();
                    var datas = access.All({filter});
                    result = new List<EasyUiTreeNode>{{EasyUiTreeNode.EmptyNode}};
                    result.AddRange(datas.Select(p => new EasyUiTreeNode
                    {{
                        ID = p.{Model.PrimaryField},
                        Text = p.{title.Name},
                        Title = p.{title.Name},
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
        protected sealed override void OnDataSaved(DataOperatorType operatorType,{Model.EntityName} entity)
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