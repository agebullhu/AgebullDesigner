﻿/*design by:agebull designer date:2017/5/26 19:43:32*/

using Agebull.Common.Organizations.DataAccess;

using Agebull.Common.Logging;
using Agebull.Common.Organizations.BusinessLogic;
using Agebull.MicroZero.ZeroApis;

using Agebull.EntityModel.EasyUI;

namespace Agebull.Common.Organizations.WebApi.Entity
{
    [RoutePrefix("sys/org/v1")]
    public partial class OrganizationApiController : ApiControllerForDataState<OrganizationData, OrganizationDataAccess, UserCenterDb, OrganizationBusinessLogic>
    {

        /// <summary>
        ///     用在界面上的当前用户可以访问的按钮集合
        /// </summary>

        [Route("edit/tree")]
        public ApiResult Tree()
        {
            return new ApiArrayResult<EasyUiTreeNode>
            {
                ResultData = OrganizationBusinessLogic.LoadAreaTreeForUi(0)
            };
        }

        /// <summary>
        ///     导入
        /// </summary>

        [Route("edit/import")]
        public ApiResult Import()
        {
            var oid = GetLongArg("oid");
            var texts = GetArg("reg");
            if (string.IsNullOrWhiteSpace(texts))
            {
                return ApiResult.ArgumentError;
            }

            Business.Import(texts, oid);
            return ApiResult.Succees(new OrganizationData());
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(OrganizationData data, FormConvert convert)
        {
            DefaultReadFormData(data, convert);
        }
        /// <summary>
        ///     新增一条带默认值的数据
        /// </summary>
        protected override OrganizationData CreateData()
        {
            var oid = GetLongArg("oid");
            if (oid < 0)
            {
                SetFailed("请选择一个有效的行政区划");
                return null;
            }
            var pid = GetIntArg("pid", 0);
            if (pid < 0)
            {
                //SetFailed("请选择一个有效的上级");
                return null;
            }

            var area = Business.Access.LoadByPrimaryKey(oid);
            if (area == null)
            {
                SetFailed("请选择一个有效的行政区划");
                return null;
            }
            var data = new OrganizationData
            {
                ParentId = pid,
                Type = OrganizationType.Organization,
                OrgLevel = 1,
                CityCode = area.Code,
                DistrictCode = area.Code
            };
            if (pid == 0)
                return data;
            var parent = Business.Access.LoadByPrimaryKey(pid);
            if (parent == null)
            {
                SetFailed("请选择一个有效的上级");
                return null;
            }
            data.Type = OrganizationType.Department;
            data.OrgLevel = parent.OrgLevel + 1;
            return data;
        }

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<OrganizationData> GetListData()
        {
            try
            {
                var id = GetIntArg("oid", 0);
                return new ApiPageData<OrganizationData>
                {
                    Rows = id == 0 ? Business.LoadEditTree() : Business.LoadTree(id)
                };
            }
            catch (System.Exception ex )
            {
                LogRecorder.Exception(ex);
                return null;
                //return new ApiPageData<OrganizationData> {
                //    s

                //};
            }
           
        }

    }
}