/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 10:19:45*/
using System;
using NUnit.Framework;
using System.Diagnostics;
using System.Threading;
using Gboxt.Common.DataModel.MySql;
using GoodLin.Common.Ioc;
using Newtonsoft.Json;
using NServiceKit.ServiceHost;
using Yizuan.Common.UserCenter.DataAccess;
using Yizuan.Common.UserCenter.WebApi;
using Yizuan.Service.Api;
using Yizuan.Service.Api.OAuth;
using Yizuan.Service.Api.WebApi;
using Agebull.Common.AppManage.WebApi;

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 身份验证单元测试
    /// </summary>
    [TestFixture]
    public class AppManageUnitTest
    {
        private AppManageLogical logical;
        [SetUp]
        public void Initialize()
        {
            MySqlDataBase.CreateDefaultFunc = () => new YizuanDataBase();
            IocHelper.Regist<IOAuthBusiness, OAuthServerProxy>();
            logical = new AppManageLogical();
        }
        [TearDown]
        public void Cleanup()
        {
            Thread.Sleep(2000);
        }
    }
}