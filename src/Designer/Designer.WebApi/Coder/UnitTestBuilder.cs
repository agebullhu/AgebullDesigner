using System.IO;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class UnitTestBuilder<TModel> : ModelCoderBase<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_UnitTest_cs";

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {

            string code = $@"using System;
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
using {NameSpace}.WebApi.EntityApi;

namespace {NameSpace}
{{
    /// <summary>
    /// 身份验证单元测试
    /// </summary>
    [TestFixture]
    public class {Model.Name}UnitTest
    {{
        [SetUp]
        public void Initialize()
        {{
            MySqlDataBase.CreateDefaultFunc = () => new {Project.DataBaseObjectName}();
            IocHelper.Regist<IOAuthBusiness, OAuthServerProxy>();
        }}
        [TearDown]
        public void Cleanup()
        {{
            Thread.Sleep(2000);
        }}

        /// <summary>
        ///     测试
        /// </summary>
        [Test]
        public void {Model.Name}Test()
        {{
            var logical = new {Model.Name}ApiLogical()  as I{Model.Name}Api;
            // 新增
            var arg = {HelloCode(Model)};
            var iResult = logical.AddNew(arg);
            Assert.IsTrue(iResult.Result, ""{Model.Caption}新增成功"");
            // 修改
            {HelloCode(Model, "arg")}
            var uResult = logical.Update(arg);
            Assert.IsTrue(uResult.Result, ""{Model.Caption}修改成功"");
            // 分页
            var filter = new PageArgument
            {{
                Page = 0,PageSize = 10,
                Desc = false,
                Order = ""Id""
            }};
            var page = logical.Query(filter);
            Assert.IsTrue(page.Result, ""分页成功"");
            // 删除
            var dResult = logical.Delete(new Argument<long>
            {{
                Value = arg.UserId
            }});
            Assert.IsTrue(dResult.Result, ""登录账户删除成功"");
        }}
    }}
}}";
            var file = ConfigPath(Model, FileSaveConfigName, path, "Entity", $"{Model.Name}UnitTest.cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
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
using {NameSpace}.WebApi;

namespace {NameSpace}
{{
    /// <summary>
    /// 身份验证单元测试
    /// </summary>
    [TestFixture]
    public class {Project.Name}UnitTest
    {{
        private {Project.ApiName}Logical logical;
        [SetUp]
        public void Initialize()
        {{
            MySqlDataBase.CreateDefaultFunc = () => new YizuanDataBase();
            IocHelper.Regist<IOAuthBusiness, OAuthServerProxy>();
            logical = new {Project.ApiName}Logical();
        }}
        [TearDown]
        public void Cleanup()
        {{
            Thread.Sleep(2000);
        }}");

            foreach (var item in Project.ApiItems)
            {
                code.Append($@"
        /// <summary>
        ///     {item.Caption}单元测试
        /// </summary>
        [Test]
        public void {item.Name}Test()
        {{");
                if (item.Argument != null)
                {
                    code.Append($@"
            var arg = {HelloCode(item.Argument)};
            var result = logical.{item.Name}(arg);
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.Result, ""{item.Caption}成功"");
        }}");
                }
                else
                {
                    code.Append($@"
            var result = logical.{item.Name}();
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.Result, ""{item.Caption}成功"");
        }}");
                }
            }

            code.Append(@"
    }
}
");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Project", $"{Project.Name}UnitTest.cs");
            WriteFile(file, code.ToString());
        }
    }

}
