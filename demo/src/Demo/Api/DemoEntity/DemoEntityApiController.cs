/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/5 13:54:14*/
#region


using Agebull.MicroZero.Demo;
using Agebull.MicroZero.Demo.BusinessLogic;
using Agebull.MicroZero.Demo.DataAccess;

using Agebull.MicroZero.ZeroApis;
#endregion

namespace Agebull.MicroZero.Entity
{
    /// <summary>
    ///  演示实体
    /// </summary>
    [RoutePrefix("ProjectDemo/de/v1")]
    public partial class DemoEntityApiController : ApiController<DemoEntityData, DemoEntityDataAccess, ProjectDemoDb, DemoEntityBusinessLogic>
    {
        #region 基本扩展

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<DemoEntityData> GetListData()
        {
            return DefaultGetListData();
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected override void ReadFormData(DemoEntityData data, FormConvert convert)
        {
            DefaultReadFormData(data,convert);
        }

        #endregion
    }
}