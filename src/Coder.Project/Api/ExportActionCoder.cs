using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ExportActionCoder : CoderWithEntity, IAutoRegister
    {
        #region 代码片断

        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-Api", "Export.cs", "cs", BaseCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BaseCode(EntityConfig entity)
        {
            Entity = entity;
            using var scope= CodeGeneratorScope.CreateScope(entity);
            return BaseCode();
        }

        #endregion

        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Export_CS";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var file = ConfigPath(Entity, "File_Web_Api_cs", path, Entity.Name, Entity.Name);
            WriteFile(file + "Export.cs", BaseCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
        }

        #endregion

        private string BaseCode()
        {
            return $@"
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
using Agebull.Common.DataModel;
using Agebull.Common.WebApi;
using Gboxt.Common.DataModel.MySql;
using Gboxt.Common.WebUI;
using Gboxt.Common.DataModel;

{Project.UsingNameSpaces}

using {NameSpace}.DataAccess;

namespace {NameSpace}.{Entity.Name}Page
{{
    /// <summary>
    /// {Entity.Caption}
    /// </summary>
    public class ExportAction : ExportPageBase<{Entity.EntityName}, {Entity.Name}DataAccess>
    {{
        /// <summary>
        /// 导出表名称
        /// </summary>
        protected override string Name => ""{Entity.Caption}"";

        /// <summary>
        /// 当前数据筛选器
        /// </summary>
        /// <returns></returns>
        protected override LambdaItem<{Entity.EntityName}> GetFilter()
        {{
            var filter = new LambdaItem<{Entity.EntityName}>
            {{
                {(
                Entity.Interfaces?.Contains("IStateData") ?? false 
                    ? "Root = p => p.DataState <= DataStateType.Discard" 
                    : null
                )}
            }};{new ProjectApiActionCoder { Entity = Entity }.QueryCode()}
            return filter;
        }}
    }}
}}";
        }
    }
}