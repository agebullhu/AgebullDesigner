using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ExportActionCoder : EasyUiCoderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileName => "Export.cs";

        protected override string LangName => "aspx";


        protected override string BaseCode()
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
            }};
            var keyWord = GetArg(""keyWord"");
            if (!string.IsNullOrEmpty(keyWord))
            {{
                filter.AddAnd(p => {new ApiActionCoder { Entity = Entity }.QueryCode()});
            }}
            return filter;
        }}
    }}
}}";
        }
    }
}