using System.Linq;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityComboBuilder : EntityBuilderBase
    {
        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        protected override string Folder => "ComboApi";

        /// <summary>
        /// 基本代码
        /// </summary>
        public override string BaseCode => ComboCode();

        /// <summary>
        ///     生成代码
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
        /// 下拉列表键
        /// </summary>
        private const string comboKey = ""ui:combo:{Model.Name}"";
        /// <summary>
        /// 下拉树键
        /// </summary>
        private const string treeKey = ""ui:tree:{Model.Name}"";

        /// <summary>
        /// 取得下拉列表值
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
        /// 取得下拉树值
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
        ///     保存完成后期处理(Insert或Update)
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
        ///     更新语句后处理(单个实体操作不引发)
        /// </summary>
        /// <param name=""condition"">执行条件</param>
        /// <param name=""args"">参数值</param>
        /// <param name=""operatorType"">操作类型</param>
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