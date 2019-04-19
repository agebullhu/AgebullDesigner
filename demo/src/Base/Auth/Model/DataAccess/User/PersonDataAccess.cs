#region



#endregion

using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;

namespace Agebull.Common.Organizations.DataAccess
{
    /// <summary>
    /// 用户的个人信息
    /// </summary>
    sealed partial class PersonDataAccess : DataStateTable<PersonData,UserCenterDb>
    {
        /// <summary>保存完成后期处理</summary>
        /// <param name="entity">保存的对象</param>
        /// <param name="operatorType">操作类型</param>
        protected override void OnDataSaved(DataOperatorType operatorType, PersonData entity)
        {
            UserHelper.CacheUserInfo(entity);
            base.OnDataSaved(operatorType, entity);
        }
    }
}