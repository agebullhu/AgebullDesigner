/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/10/9 14:00:33*/
using Agebull.ZeroNet.ZeroApi;
using Agebull.Common.WebApi;
using Gboxt.Common.DataModel;

using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;

namespace Agebull.Common.UserCenter.WebApi.Entity
{
    [RoutePrefix("UserCenter/Person/v1")]
    public partial class PersonApiController : ApiControllerForDataState<PersonData, PersonDataAccess, UserCenterDb, PersonBusinessLogic>
    {
        #region 基本扩展

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<PersonData> GetListData()
        {
            return DefaultGetListData();
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(PersonData data, FormConvert convert)
        {
            DefaultReadFormData(data,convert);
        }

        #endregion
    }
}