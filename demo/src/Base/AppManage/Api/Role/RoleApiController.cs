/*design by:agebull designer date:2017/5/26 19:43:33*/
using System.Linq;
using Agebull.Common.AppManage;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;
using Agebull.EntityModel.Common;


using Agebull.MicroZero.ZeroApis;
using Agebull.EntityModel.EasyUI;

namespace Agebull.Common.Organizations.WebApi.Entity
{
    [RoutePrefix("sys/role/v1")]
    public partial class RoleApiController : ApiControllerForDataState<RoleData, RoleDataAccess, AppManageDb, RoleBusinessLogic>
    {
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<RoleData> GetListData()
        {
            var filter = new LambdaItem<RoleData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(RoleData data, FormConvert convert)
        {
            DefaultReadFormData(data, convert);
        }

        /// <summary>
        /// 载入全部
        /// </summary>
        
        [Route("edit/roles")]
        public ApiResult Roles()
        {
            var roles = Business.All();
            roles.Insert(0, new RoleData
            {
                Name = "-",
                Caption = "-"
            });
            return new ApiArrayResult<EasyComboValues>
            {
                ResultData = roles.Select(p => new EasyComboValues
                {
                    Key = p.Id,
                    Value = p.Caption
                }).ToList()
            };
        }

        /// <summary>
        /// 载入当前角色权限
        /// </summary>
        
        [Route("edit/powers")]
        public ApiResult LoadPowers()
        {
            
            var rid = GetIntArg("rid", -1);
            var id = GetIntArg("id", -1);
            if (rid <= 0 || id >= 0)
                return ApiResult.Succees();
            return new ApiArrayResult<EasyUiTreeNode>
            {
                ResultData = RolePowerBusinessLogic.LoadPowers(rid)
            };
        }

        /// <summary>
        /// 保存权限分配
        /// </summary>
        /// <summary>
        /// 载入当前角色权限
        /// </summary>
        
        [Route("edit/savepowers")]
        public ApiResult SavePowers()
        {
            
            RolePowerBusinessLogic.SaveRolePagePower(ContextDataId, GetArg("selects"));
            return ApiResult.Succees();
        }

    }
}