/*design by:agebull designer date:2017/5/26 19:43:33*/

using Agebull.ZeroNet.ZeroApi;
using Agebull.Common.DataModel.WebUI;
using Gboxt.Common.DataModel;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Agebull.Common.OAuth;

namespace Agebull.Common.UserCenter.WebApi.Entity
{
    [RoutePrefix("sys/person/v1")]
    public partial class PositionPersonnelApiController : ApiControllerForAudit<PositionPersonnelData, PositionPersonnelDataAccess, UserCenterDb, PositionPersonnelBusinessLogic>
    {
        /// <summary>
        ///     用在界面上的当前用户可以访问的按钮集合
        /// </summary>
        
        [Route("edit/tree")]
        public ApiResult Tree()
        {
            return new ApiArrayResult<EasyUiTreeNode>
            {
                ResultData = OrganizationBusinessLogic.LoadPostTreeForUi(GlobalContext.Current.User.OrganizationId)
            };
        }

        /// <summary>
        ///     新增一条带默认值的数据
        /// </summary>
        protected override PositionPersonnelData CreateData()
        {
            return new PositionPersonnelData
            {
                DepartmentId = GetIntArg("oid", 0),
                OrganizePositionId = GetIntArg("pid", 0)
            };
        }
        protected override PositionPersonnelData DoDetails()
        {
            PositionPersonnelData data;
            if (ContextDataId == 0)
            {
                data = CreateData();
                OnDetailsLoaded(data, true);
            }
            else
            {
                data = Business.Details(ContextDataId);
                if (data == null)
                {
                    SetFailed("数据不存在");
                    return null;
                }
                OnDetailsLoaded(data, false);
            }

            return data;
        }
        /// <summary>
        ///     取得列表数据
        /// </summary>
        /// <remarks>安全检查有漏洞</remarks>
        protected override ApiPageData<PositionPersonnelData> GetListData()
        {
            var pid = GetIntArg("pid", 0);
            var oid = GetIntArg("oid", 0);
            var kw = GetArg("keyWord");
            var condition = new LambdaItem<PositionPersonnelData>();
            if (pid == -1)
            {
                condition.Root = p => p.RealName == kw || p.PhoneNumber == kw;
                return base.GetListData(condition);
            }
            if (!string.IsNullOrWhiteSpace(kw))
            {
                condition.AddAnd(p => p.RealName.Contains(kw) || p.PhoneNumber.Contains(kw) || p.Appellation.Contains(kw) || p.Position.Contains(kw));
            }
            if (pid > 0)
            {
                condition.Root = p => p.OrganizePositionId == pid;
                return base.GetListData(condition);
            }

            if (oid > 0)
            {
                using (ReadTableScope<PositionPersonnelData>.CreateScope(Business.Access, "view_org_position_personnel_master"))
                {
                    condition.Root = p => p.MasterId == oid;
                    return base.GetListData(condition);
                }
            }

            if (GlobalContext.Current.User.UserId == 1)
            {
                return base.GetListData(condition);
            }

            using (ReadTableScope<PositionPersonnelData>.CreateScope(Business.Access, "view_org_position_personnel_master"))
            {
                condition.Root = p => p.MasterId == GlobalContext.Current.User.OrganizationId;
                return base.GetListData(condition);
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(PositionPersonnelData data, FormConvert convert)
        {
            //数据
            data.Appellation = convert.ToString("Appellation");
            data.RealName = convert.ToString("RealName", false);
            data.Sex = (SexType)convert.ToInteger("Sex");
            data.Birthday = convert.ToDateTime("Birthday");
            data.PhoneNumber = convert.ToString("Mobile", false);

            data.OrganizePositionId = convert.ToInteger("OrganizePositionId", 0);
            //备注
            data.Memo = convert.ToString("Memo", true);
        }
    }
}