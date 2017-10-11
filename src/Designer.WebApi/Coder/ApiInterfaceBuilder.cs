using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class ApiInterfaceBuilder : CoderWithEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Interface_cs";
        
        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using Gboxt.Common.DataModel;
using Yizuan.Service.Api;


namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// {Entity.Description}的实体数据操作接口
    /// </summary>
    public interface I{Entity.Name}Api
    {{
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResult<{Entity.Name}> AddNew({Entity.Name} data);

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResult<{Entity.Name}> Update({Entity.Name} data);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        ApiResult Delete(Argument<{Entity.PrimaryColumn.LastCsType}> dataKey);

        /// <summary>
        ///     分页
        /// </summary>
        ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg);

    }}
}}";
            var file = ConfigPath(path, FileSaveConfigName, $"I{Entity.Name}EntityApi", ".cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using Gboxt.Common.DataModel;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;


namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// {Entity.Description}的实体数据操作接口代理类
    /// </summary>
    public class {Entity.Name}ApiProxy : I{Entity.Name}Api
    {{
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        public ApiResult<{Entity.Name}> AddNew({Entity.Name} data)
        {{
            return InternalApiCaller.Post<{Entity.Name}>(""entity/{Entity.Name}/AddNew"", data);
        }}

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        public ApiResult<{Entity.Name}> Update({Entity.Name} data)
        {{
            return InternalApiCaller.Post<{Entity.Name}>(""entity/{Entity.Name}/Update"", data);
        }}

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        public ApiResult Delete(Argument<{Entity.PrimaryColumn.LastCsType}> dataKey)
        {{
            return InternalApiCaller.Post(""entity/{Entity.Name}/Delete"", dataKey);
        }}

        /// <summary>
        ///     分页
        /// </summary>
        public ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg)
        {{
            return InternalApiCaller.Post<ApiPageData<{Entity.Name}>>(""entity/{Entity.Name}/Query"", arg);
        }}

    }}
}}";
            var file = ConfigPath(path, FileSaveConfigName, $"{Entity.Name}EntityApi", ".cs");
            SaveCode(file, code);
        }

    }
}