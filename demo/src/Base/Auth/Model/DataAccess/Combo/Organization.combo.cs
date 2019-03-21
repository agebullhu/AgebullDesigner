/*design by:agebull designer date:2017/6/27 8:45:14*/
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Agebull.EntityModel.Redis;
using System.Collections.Generic;


namespace Agebull.Common.OAuth.DataAccess
{
    partial class OrganizationDataAccess
    {
        /// <summary>
        /// 下拉列表键
        /// </summary>
        private const string comboKey = "ui:combo:Area";

        /// <summary>
        /// 取得下拉列表值
        /// </summary>
        /// <returns></returns>
        public static List<EasyComboValues> GetComboValues()
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                var result = proxy.Get<List<EasyComboValues>>(comboKey);
                return result;
            }
        }

        /// <summary>
        ///     保存完成后期处理(Insert或Update)
        /// </summary>
        /// <param name="operatorType"></param>
        /// <param name="entity"></param>
        protected sealed override void OnDataSaved(DataOperatorType operatorType, OrganizationData entity)
        {
            using (var proxy = new RedisProxy(RedisProxy.DbComboCache))
            {
                proxy.RemoveKey(comboKey);
            }
        }
    }
}