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
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Interface_cs";
        
        /// <summary>
        ///     ���ɻ�������
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
    /// {Entity.Description}��ʵ�����ݲ����ӿ�
    /// </summary>
    public interface I{Entity.Name}Api
    {{
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResult<{Entity.Name}> AddNew({Entity.Name} data);

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResult<{Entity.Name}> Update({Entity.Name} data);

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        ApiResult Delete(Argument<{Entity.PrimaryColumn.LastCsType}> dataKey);

        /// <summary>
        ///     ��ҳ
        /// </summary>
        ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg);

    }}
}}";
            var file = ConfigPath(path, FileSaveConfigName, $"I{Entity.Name}EntityApi", ".cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     ���ɻ�������
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
    /// {Entity.Description}��ʵ�����ݲ����ӿڴ�����
    /// </summary>
    public class {Entity.Name}ApiProxy : I{Entity.Name}Api
    {{
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        public ApiResult<{Entity.Name}> AddNew({Entity.Name} data)
        {{
            return InternalApiCaller.Post<{Entity.Name}>(""entity/{Entity.Name}/AddNew"", data);
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        public ApiResult<{Entity.Name}> Update({Entity.Name} data)
        {{
            return InternalApiCaller.Post<{Entity.Name}>(""entity/{Entity.Name}/Update"", data);
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        public ApiResult Delete(Argument<{Entity.PrimaryColumn.LastCsType}> dataKey)
        {{
            return InternalApiCaller.Post(""entity/{Entity.Name}/Delete"", dataKey);
        }}

        /// <summary>
        ///     ��ҳ
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