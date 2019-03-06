/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/5 13:54:14*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Agebull.ZeroNet.ZeroApi;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;



using Agebull.EntityModel.Demo;
using Agebull.EntityModel.Demo.BusinessLogic;
using Agebull.EntityModel.Demo.DataAccess;
#endregion

namespace Agebull.EntityModel.Demo.WebApi.Entity
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