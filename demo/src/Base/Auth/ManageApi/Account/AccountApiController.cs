/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/10/5 23:06:23*/


using Agebull.Common.Organizations;
using Agebull.Common.Organizations.BusinessLogic;
using Agebull.Common.Organizations.DataAccess;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.Organizations.WebApi.Entity
{
    [RoutePrefix("user/Account/v1")]
    public partial class AccountApiController : ApiControllerForDataState<AccountData, AccountDataAccess, UserCenterDb, AccountBusinessLogic>
    {
        #region 基本扩展

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<AccountData> GetListData()
        {
            return DefaultGetListData();
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(AccountData data, FormConvert convert)
        {
            DefaultReadFormData(data,convert);
        }

        #endregion
    }
}