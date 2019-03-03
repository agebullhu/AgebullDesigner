/*design by:agebull designer date:2019/3/2 23:49:06*/
#region
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
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.Logging;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Agebull.ZeroNet.ZeroApi;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;



using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;
#endregion

namespace Agebull.Common.OAuth.WebApi.Entity
{
    /// <summary>
    ///  行政区域
    /// </summary>
    [RoutePrefix("sys/area/v1")]
    public partial class GovernmentAreaApiController : ApiControllerForDataState<GovernmentAreaData, GovernmentAreaDataAccess, UserCenterDb, GovernmentAreaBusinessLogic>
    {
        #region 基本扩展

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(GovernmentAreaData data, FormConvert convert)
        {
            DefaultReadFormData(data,convert);
        }

        #endregion

        /// <summary>
        ///     用在界面上的当前用户可以访问的按钮集合
        /// </summary>

        [Route("edit/tree")]
        public ApiResult Tree()
        {
            //var tree = OrganizationBusinessLogic.LoadTreeForUi(GlobalContext.Current.User.OrganizationId);
            return new ApiArrayResult<EasyUiTreeNode>
            {
                ResultData = Business.LoadComboTreeForUi()
            };
        }

        /// <summary>
        ///     导入
        /// </summary>

        [Route("edit/import")]
        public ApiResult Import()
        {
            var texts = GetArg("reg");
            if (string.IsNullOrWhiteSpace(texts))
            {
                return ApiResult.ArgumentError;
            }

            Business.Import(texts);
            return ApiResult.Succees(new GovernmentAreaData());
        }

        /// <summary>
        ///     新增一条带默认值的数据
        /// </summary>
        protected override GovernmentAreaData CreateData()
        {
            var pid = GetIntArg("pid", 0);
            if (pid < 0)
            {
                //SetFailed("请选择一个有效的上级");
                return null;
            }
            var data = new GovernmentAreaData
            {
                ParentId = pid,
                OrgLevel = 1
            };
            if (pid == 0)
                return data;
            var parent = Business.Access.LoadByPrimaryKey(pid);
            if (parent == null)
            {
                SetFailed("请选择一个有效的上级");
                return null;
            }
            data.OrgLevel = parent.OrgLevel + 1;
            return data;
        }

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<GovernmentAreaData> GetListData()
        {
            try
            {
                var id = GetIntArg("id", 0);
                return new ApiPageData<GovernmentAreaData>
                {
                    Rows = id == 0 ? Business.LoadEditTree() : Business.LoadTree(id)
                };
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                return null;
            }
        }

    }
}