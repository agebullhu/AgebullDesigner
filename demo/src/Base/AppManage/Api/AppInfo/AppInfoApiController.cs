/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 17:27:14*/
#region


using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;

using Agebull.MicroZero.ZeroApis;
#endregion

namespace Agebull.Common.AppManage.WebApi.Entity
{
    /// <summary>
    ///  应用信息
    /// </summary>
    [RoutePrefix("app/app/v1")]
    public partial class AppInfoApiController : ApiControllerForDataState<AppInfoData, AppInfoDataAccess, AppManageDb, AppInfoBusinessLogic>
    {
        #region 基本扩展

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<AppInfoData> GetListData()
        {
            return DefaultGetListData();
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(AppInfoData data, FormConvert convert)
        {
            DefaultReadFormData(data,convert);
        }

        #endregion
    }
}