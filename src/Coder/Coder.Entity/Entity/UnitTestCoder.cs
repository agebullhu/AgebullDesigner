using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UnitTestCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("单元测试", "数据读取单元测试(C#)", "cs", EntityUt);
            CoderManager.RegisteCoder("单元测试", "数据操作单元测试(C#)", "cs", UpdateUt);
            CoderManager.RegisteCoder<SolutionConfig>("单元测试", "API单元测试(C#)", "cs", ApiUt);
        }
        #endregion

        #region 数据读取单元测试

        private static string EntityUt(EntityConfig config)
        {
            return ($@"
    try{{
        var access = new {config.Name}DataAccess();
        var data = access.First();
        Assert.IsNotNull(data,""{config.Caption}*数据为空"");
    }}catch(Exception ex){{
        Assert.Fail(""{config.Caption}:""+ex.Message);
    }}");
        }


        #endregion

        #region 数据读取单元测试

        public static string UpdateUt(EntityConfig config)
        {
            return ($@"
    try
    {{
        var access = new {config.Name}DataAccess();
        {NewCsCode(config)}
        access.Insert(data);
        Assert.IsTrue(data.{config.PrimaryField} > 0,""{config.Caption}*主键新增失败数据为空"");
        access.Update(data);
        Assert.IsTrue(data.{config.PrimaryField} > 0,""{config.Caption}*主键新增失败数据为空"");
        access.PhysicalDelete(data.{config.PrimaryField});
    }}
    catch (AssertFailedException)
    {{
    }}
    catch(Exception ex)
    {{
        Trace.WriteLine(ex);
        Assert.Fail(""{config.Caption}:""+ex.Message);
    }}");
        }

        private static string NewCsCode(EntityConfig config)
        {
            var builder = new StringBuilder();
            builder.Append($@"
            var data = new {config.EntityName}
            {{");
            bool first = true;
            foreach (var field in config.LastProperties.Where(p => !p.NoProperty))
            {
                if (first)
                    first = false;
                else builder.Append(',');
                builder.Append($@"
                {field.Name} = {GetTestValue(field.LastCsType)}");
            }
            builder.Append(@"
            };");
            return builder.ToString();
        }

        #region 测试片断

        private static readonly Random random = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

        private static string GetTestValue(string type)
        {
            switch (type)
            {
                case "bool":
                    return DateTime.Now.Ticks % 2 == 1 ? "true" : "false";
                case "DateTime":
                    return "DateTime.Now";
                case "double":
                case "float":
                    return (random.NextDouble() * 10000.0).ToString("F");
                case "byte":
                    return $"(byte)'{(char)('A' + random.Next(25))}'";
                case "char":
                    return $"'{(char)('A' + random.Next(25))}'";
                case "int":
                case "long":
                case "uint":
                case "ulong":
                case "short":
                case "ushort":
                    return random.Next(int.MaxValue).ToString();
                case "string":
                    return $"\"{TestLine[random.Next(TestLine.Length)]}\"";
            }
            return "0";
        }
        public static string[] TestLine =
        {
            "追踪着鹿",
            "谁不向前",
            "有志始知",
            "雄心壮志",
            "志之所趋",
            "不怕路远","就怕志短",
            "志高山峰矮","路从脚下伸",
            "有志者自有千方百计","无志者只感千难万难",
            "有志登山顶","无志站山脚",
            "有志的人战天斗地","无志的人怨天恨地",
            "天才是由于对事业的热爱感而发展起来的","简直可以说天才",
            "人生志气立","所贵功业昌",
            "人若有志","万事可为",
            "并非神仙才能烧陶器","有志的人总可以学得精手艺",
            "有志者能使石头长出青草来",
            "雄鹰必须比鸟飞得高","因为它的猎物就是鸟",
            "无所求则无所获",
            "壮志与毅力是事业的双翼",
            "志不真则心不热","心不热则功不贤",
            "把意念沉潜得下","何理不可得","把志气奋发得起","何事不可做",
            "立志是事业的大门","工作是登门入室的旅程",
            "壮志与毅力是事业的双翼",
            "在年轻人的颈项上","没有什么东西能比事业心这颗灿烂的宝珠",
            "不为穷变节","不为贱易志",
            "褴褛衣内可藏志",
            "志气和贫困是患难兄弟","世人常见他们伴在一起",
            "死犹未肯输心去","贫亦其能奈我何！",
            "困","你是人类艺术的源泉","你将伟大的灵感赐予诗人",
            "贫穷是一切艺术职业的母亲",
            "无钱之人脚杆硬","有钱之人骨头酥",
            "鸭仔无娘也长大","几多白手也成家",
            "贫困能造就男子气概",
            "穷人的孩子早当家",
            "贫困教会贫困者一切",
            "对没志气的人","路程显得远；对没有银钱的人","城镇显得远",
            "有志者","事竟成",
            "人惟患无志","有志无有不成者",
            "志不立","天下无可成之事",
            "志正则众邪不生",
            "鹰爱高飞","鸦栖一枝",
            "鸟贵有翼","人贵有志",
            "器大者声必闳","志高者意必远",
            "燕雀安知鸿鹄之志哉",
            "三军可夺帅也","匹夫不可夺志也",
            "志","气之帅也",
            "石看纹理山看脉","人看志气树看材",
            "志之所向","金石为开","谁能御之？",
            "志坚者","功名之柱也登山不以艰险而止","则必臻乎峻岭",
            "心志要坚","意趣要乐",
            "一人立志","万夫莫敌"
        };
        #endregion
        #endregion
        #region 数据读取单元测试

        public static string ApiUt(SolutionConfig config)
        {
            var code = new StringBuilder();
            code.Append(@"
                var caller = new WebApiCaller
                {
                    Host = ""http://localhost:20639/""
                };
                var result = caller.Get<ValiadateCodeResponse>(""v1/user/login"", new UserInfoLoginRequest { });
                if (!result.Result && result.Status.ErrorCode == ErrorCode.Auth_Device_Unknow)
                {
                    caller.Bearer = result.Status.Message;
                    result = caller.Get<ValiadateCodeResponse>(""v1/verification/getcode"");
                }
                Console.WriteLine(JsonConvert.SerializeObject(result));");
            foreach (var item in config.ApiItems)
                ApiUt(code, item);
            return code.ToString();
        }
        private static void ApiUt(StringBuilder code, ApiItem config)
        {
            code.Append($@"
        {{//{config.Project.Name}.{config.Name}
                var result = caller.{config.Method.ToString().ToLower().ToUWord()}<{config.Result?.Name}>(""{config.RoutePath}""");
            if (config.Argument != null)
            {
                code.Append($@", new Dictionary<string, string>
                {{");
                foreach (var field in config.Argument.LastProperties)
                {
                    code.Append($@"
                        {{""{field.Name}"",""{GetTestValue(field.CsType)}""}},");
                }
                code.Append(@"
                }");
            }
            code.Append(@");
                Console.WriteLine(JsonConvert.SerializeObject(result));
        }");
        }

        #endregion
    }
}