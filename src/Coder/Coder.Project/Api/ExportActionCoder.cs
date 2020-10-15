using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ExportActionCoder<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        #region 代码片断

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BaseCode(TModel entity)
        {
            Model = entity;
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
        protected override void CreateDesignerCode(string path)
        {
            if (Model.IsInternal || Model.NoDataBase || Model.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var file = ConfigPath(Model, "File_Web_Api_cs", path, Model.Name, Model.Name);
            WriteFile(file + "Export.cs", BaseCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
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

namespace {NameSpace}.{Model.Name}Page
{{
    /// <summary>
    /// {Model.Caption}
    /// </summary>
    public class ExportAction : ExportPageBase<{Model.EntityName}, {Model.Name}DataAccess>
    {{
        /// <summary>
        /// 导出表名称
        /// </summary>
        protected override string Name => ""{Model.Caption}"";

        /// <summary>
        /// 当前数据筛选器
        /// </summary>
        /// <returns></returns>
        protected override LambdaItem<{Model.EntityName}> GetFilter()
        {{
            var filter = new LambdaItem<{Model.EntityName}>
            {{
                {(
                Model.Interfaces?.Contains("IStateData") ?? false 
                    ? "Root = p => p.DataState <= DataStateType.Discard" 
                    : null
                )}
            }};{new ProjectApiActionCoder<TModel> { Model = Model }.QueryCode()}
            return filter;
        }}
    }}
}}";
        }
    }
}