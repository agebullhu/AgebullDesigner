using System.Linq;

namespace Agebull.Common.SimpleDesign.Coder.Cs
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
        /// 下拉列表键
        /// </summary>
        private const string comboKey = ""ui:combo:{Entity.Name}"";
        /// <summary>
        /// 下拉树键
        /// </summary>
        private const string treeKey = ""ui:tree:{Entity.Name}"";

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
        ///     保存完成后期处理(Insert或Update)
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