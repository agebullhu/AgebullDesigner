/*design by:agebull designer date:2017/5/26 19:43:33*/

using Agebull.Common.Organizations.DataAccess;
using Agebull.EntityModel.Common;

using Agebull.MicroZero.ZeroApis;
using Agebull.EntityModel.EasyUI;

using Agebull.Common.Context;
using Agebull.Common.Organizations.BusinessLogic;

namespace Agebull.Common.Organizations.WebApi.Entity
{
    [RoutePrefix("sys/post/v1")]
    public partial class OrganizePositionApiController : ApiControllerForDataState<OrganizePositionData, OrganizePositionDataAccess, UserCenterDb, OrganizePositionBusinessLogic>
    {
        /// <summary>
        ///     新增一条带默认值的数据
        /// </summary>
        protected override OrganizePositionData CreateData()
        {
            return new OrganizePositionData
            {
                DepartmentId = GetIntArg("oid")
            };
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(OrganizePositionData data, FormConvert convert)
        {
            DefaultReadFormData(data, convert);

        }

        /// <summary>
        ///     用在界面上的当前用户可以访问的按钮集合
        /// </summary>

        [Route("edit/tree")]
        public ApiResult Tree()
        {
            var tree = OrganizationBusinessLogic.LoadTreeForUi(GlobalContext.Current.User.OrganizationId);
            return new ApiArrayResult<EasyUiTreeNode>
            {
                ResultData = tree
            };
        }

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<OrganizePositionData> GetListData()
        {
            var filter = new LambdaItem<OrganizePositionData>();
            var oid = GetLongArg("oid");
            if (oid > 0)
            {
                var max = ((oid >> 48) + 1) << 48;
                filter.Root = p => p.Id > oid && p.Id < max;
            }
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }
    }
}