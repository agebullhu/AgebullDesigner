using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Funds
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UnitTestMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("期货相关", "实体序列化单元测试(C++)", EntityUt);
            MomentCoder.RegisteCoder("期货相关", "API请求测试(C++)", ApiUt);
            MomentCoder.RegisteCoder("期货相关", "赋值代码(C++)", CreateEntityCode);
        }
        #endregion
        

        #region 实体序列化单元测试

        public static string EntityUt(ConfigBase config)
        {
            var code = new StringBuilder();
            var project = config as ProjectConfig;
            if (project != null)
            {
                ProjectUt(project, code);
                return code.ToString();
            }
            var entity = config as EntityConfig;
            if (entity != null)
            {
                EntityUt(code, entity);
                return code.ToString();
            }
            SolutionUt(code, false);
            return code.ToString();
        }

        private static void SolutionUt(StringBuilder code, bool full)
        {
            if (full)
            {
                foreach (var pro in SolutionConfig.Current.Projects)
                {
                    if (pro.NameSpace != null)
                        code.Append($@"
using namespace {pro.NameSpace.Replace(".", "::")};");
                }
                foreach (var pro in SolutionConfig.Current.Projects)
                {
                    foreach (var em in pro.Entities)
                    {
                        code.Append($@"
//{em.Caption}
bool ut_{em.Name}();");
                    }
                }
                code.Append(@"
//测试
int main()
{");
                foreach (var pro in SolutionConfig.Current.Projects)
                {
                    foreach (var em in pro.Entities)
                    {
                        code.Append($@"
    ut_{em.Name}();");
                    }
                }
                code.Append(@"
    return 0;
}");
            }
            foreach (var pro in SolutionConfig.Current.Projects)
            {
                EntityUt(code, pro);
            }
        }

        private static void ProjectUt(ProjectConfig project, StringBuilder code)
        {
            if (project.NameSpace != null)
                code.Append($@"
using namespace {project.NameSpace.Replace(".", "::")};");
            foreach (var em in project.Entities)
            {
                code.Append($@"
//{em.Caption}
bool ut_{em.Name}();");
            }
            code.Append(@"
int main()
{");
            foreach (var em in project.Entities)
            {
                code.Append($@"
    ut_{em.Name}();");
            }
            code.Append(@"
    return 0;
}");
            EntityUt(code, project);
        }

        private static void EntityUt(StringBuilder code, ProjectConfig project)
        {
            foreach (var em in project.Entities)
                EntityUt(code, em);
        }

        private static void EntityUt(StringBuilder code, EntityConfig config)
        {
            if (config == null)
                return;
            code.Append($@"
#include ""{config.Parent.Name}/{config.Name}.h""
//{config.Caption}序列化单元测试
bool ut_{config.Name}()
{{
    //初始化{CreateEntityCode(config, "field_org")}
    //序列化
    PNetCommand cmd;
    cmd << field_org;
    //反序列化
    {config.Name} field_dec;
    cmd >> field_dec;
    //校验结果
    bool succeed = true;
    cout << ""{config.Caption}测试结果:"" << endl;");
            foreach (var field in config.Properties)
            {
                FieldCmp(code, field);
            }
            code.Append(@"
    if(succeed)
        cout << ""    成功"" <<endl;
    delete cmd;
    return succeed;
}");
        }


        #endregion

        #region API请求测试

        #region 实体序列化单元测试

        public static string ApiUt(ConfigBase config)
        {
            return ProjectApiUt();
        }

        private static string ProjectApiUt(bool full = false)
        {
            var code = new StringBuilder();
            var apis = SolutionConfig.Current.ApiItems.Where(p => !p.Discard).ToArray();
            if (full)
            {
                foreach (var api in apis)
                {
                    if (api.Argument != null)
                        code.Append($@"
#include ""{api.Argument.Parent.Name}/{api.Argument.Name}.h""");
                }
                foreach (var api in apis)
                {
                    code.Append($@"
//{api.Caption}API请求测试
bool api_ut_{api.Name}(ServerTradeCommand& command);");
                }
                code.Append(@"
void api_ut()
{
    ServerTradeCommand command;
    command.Initialize();
");
                foreach (var api in apis)
                {
                    code.Append($@"
//{api.Caption}API请求测试
api_ut_{api.Name}(command);");
                }
                code.Append(@"
}");
            }

            foreach (var api in apis)
            {
                ApiUt(code, api);
            }
            return code.ToString();
        }

        private static void ApiUt(StringBuilder code, ApiItem api)
        {
            if (api == null)
                return;
            if (api.Argument != null)
                code.Append($@"
//{api.Caption}API请求测试
bool api_ut_{api.Name}(EsTradeCommand& command)
{{
    //初始化{CreateEntityCode(api.Argument, "field_org")}
    //序列化
    PNetCommand cmd_arg;
    cmd_arg << field_org;
    //发出请求
    command.Do{api.Name}(cmd_arg);
	if (command.get_last_trade_state() == 0)
		command.m_trader->WaitEvent();
    cout << ""结束{api.Caption}请求:"" << command.m_trader->m_last_error << endl;
    return command.m_trader->m_last_error == 0;
}}");
            else
                code.Append($@"
//{api.Caption}API请求测试
bool api_ut_{api.Name}(EsTradeCommand& command)
{{
    //初始化
    NetCommand cmd_arg;
    memset(&cmd_arg,0,sizeof(NetCommand));
    //发出请求
    cout << ""发出{api.Caption}请求"" << endl;
    command.Do{api.Name}(&cmd_arg);
	if (command.get_last_trade_state() == 0)
		command.m_trader->WaitEvent();
    cout << ""结束{api.Caption}请求:"" << command.m_trader->m_last_error << endl;
    return command.m_trader->m_last_error == 0;
}}");
        }


        #endregion
        #endregion

        #region 测试片断

        private static string CreateEntityCode(ConfigBase config)
        {
            return CreateEntityCode((EntityConfig)config, "field", true);
        }
        public static string CreateCsEntityCode(EntityConfig config, string name)
        {
            return CreateCsEntityCode(config, name, true);
        }

        private static string CreateCsEntityCode(EntityConfig config, string name, bool def)
        {
            if (config == null)
                return null;
            StringBuilder code = new StringBuilder();
            if (def)
                code.Append($@"
    {config.EntityName} {name} = new {config.EntityName}();");
            foreach (var field in config.Properties)
            {
                GetCsRandomValue(code, field, name);
            };
            return code.ToString();
        }
        public static void GetCsRandomValue(StringBuilder code, PropertyConfig field, string entityName)
        {
            CppTypeHelper.DoByCppType(field.Parent, field,
                (pro, len) =>
                {
                    int len2 = (len - 1) / 2;
                    var value = TestLine[random.Next(TestLine.Length - 1)];
                    if (value.Length > len2)
                        value = value.Substring(0, len2);
                    code.Append($@"
    //{field.Caption}
    {entityName}.{field.Name}=""{value}"";");
                },
                (pro, type, len) =>
                {
                    if (type == "tm")
                    {
                        code.Append($@"
    //{field.Caption}
    {entityName}.{field.Name} = DateTime.Now;");
                    }
                    else if (len <= 0)
                    {
                        code.Append($@"
    //{field.Caption}
    {entityName}.{field.Name} = {GetBaseTypeValue(type)};");
                    }
                    else
                    {
                        code.Append($@"
    //{field.Caption}");
                        for (int idx = 0; idx < len; idx++)
                        {
                            code.Append($@"
    {entityName}.{field.Name}[{idx}] = {GetBaseTypeValue(type)};");
                        }
                    }
                },
                (pro, en, len) =>
                {
                    code.Append($@"
    //{field.Caption}
    {CreateCsEntityCode(en, $"{entityName}.{field.Name}", false)};");
                },
                (pro, type, len) =>
                {
                },
                (pro, enumcfg, len) =>
                {
                    var value = enumcfg.Items[random.Next(enumcfg.Items.Count - 1)].Name;
                    code.Append($@"
    //{field.Caption}
    {entityName}.{field.Name} ={enumcfg.Name}.{value};");
                });
        }

        private static string CreateEntityCode(EntityConfig config, string name)
        {
            return CreateEntityCode(config, name, true);
        }

        private static string CreateEntityCode(EntityConfig config, string name, bool def)
        {
            if (config == null)
                return null;
            StringBuilder code = new StringBuilder();
            if (def)
                code.Append($@"
    {config.Name} {name};
    memset(&{name},0,sizeof({config.Name}));");
            foreach (var field in config.Properties)
            {
                GetRandomValue(code, field, name);
            };
            return code.ToString();
        }

        private static readonly Random random = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
        public static void GetRandomValue(StringBuilder code, PropertyConfig field, string entityName)
        {
            CppTypeHelper.DoByCppType(field.Parent, field,
                (pro, len) =>
                {
                    int len2 = (len - 1) / 2;
                    var value = TestLine[random.Next(TestLine.Length - 1)];
                    if (value.Length > len2)
                        value = value.Substring(0, len2);
                    code.Append($@"
    //{field.Caption} -- char[{len}]
    strcpy_s({entityName}.{field.Name},""{value}"");");
                },
                (pro, type, len) =>
                {
                    if (type == "tm")
                    {
                        DateTime tm = DateTime.Now;
                        code.Append($@"
    //{field.Caption} -- {type}
    {entityName}.{field.Name}.tm_year = {tm.Year };
    {entityName}.{field.Name}.tm_mon = {tm.Month };
    {entityName}.{field.Name}.tm_mday = {tm.Day };
    {entityName}.{field.Name}.tm_hour = {tm.Hour};
    {entityName}.{field.Name}.tm_min = {tm.Minute};
    {entityName}.{field.Name}.tm_sec = {tm.Second};");
                    }
                    else if (len <= 0)
                    {
                        code.Append($@"
    //{field.Caption} -- {type}
    {entityName}.{field.Name} = {GetBaseTypeValue(type)};");
                    }
                    else
                    {
                        code.Append($@"
    //{field.Caption} -- {type}[{len}]");
                        for (int idx = 0; idx < len; idx++)
                        {
                            code.Append($@"
    {entityName}.{field.Name}[{idx}] = {GetBaseTypeValue(type)};");
                        }
                    }
                },
                (pro, en, len) =>
                {
                    code.Append($@"
    //{field.Caption} -- {en.Caption}
    {CreateEntityCode(en, $"{entityName}.{field.Name}", false)};");
                },
                (pro, type, len) =>
                {
                    var value = type.Items.Values.ToArray()[random.Next(type.Items.Count - 1)].Value;
                    if (value == "'")
                        value = "'\\0'";
                    code.Append($@"
    //{field.Caption} -- {type}
    {entityName}.{field.Name} = {value};");
                },
                (pro, enumcfg, len) =>
                {
                    var value = enumcfg.Items[random.Next(enumcfg.Items.Count - 1)].Name;
                    code.Append($@"
    //{field.Caption} -- {enumcfg}
    {entityName}.{field.Name} = GBS::Futures::{enumcfg.Name}Classify::{value};");
                });
        }


        public static void FieldCmp(StringBuilder code, PropertyConfig field)
        {
            CppTypeHelper.DoByCppType(field.Parent, field,
                 (pro, len) =>
                 {
                     code.Append($@"
    if(strcmp(field_org.{field.Name},field_dec.{field.Name}) != 0)
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" << field_org.{field.Name} << ""***""<<field_dec.{field.Name}<<endl;
    }}");
                 },
                 (pro, type, len) =>
                 {
                     if (type == "tm" || len > 1)
                     {
                         code.Append($@"
    if(memcmp(&field_org.{field.Name},&field_dec.{field.Name},sizeof(tm)) != 0)
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" <<endl;
    }}");
                     }
                     else
                     {
                         code.Append($@"
    if( field_org.{field.Name} != field_dec.{field.Name})
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" << field_org.{field.Name} << ""***""<< field_dec.{field.Name}<<endl;
    }}");
                     }
                 },
                 (pro, en, len) =>
                 {
                     code.Append($@"
    if(memcmp(&field_org.{field.Name},&field_dec.{field.Name},sizeof({en.Name})) != 0)
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" <<endl;
    }}");
                 },
                 (pro, ty, len) =>
                 {
                     if (len > 1)
                     {
                         code.Append($@"
    if(memcmp(&field_org.{field.Name},&field_dec.{field.Name},sizeof(tm)) != 0)
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" <<endl;
    }}");
                     }
                     else
                         code.Append($@"
    if( field_org.{field.Name} != field_dec.{field.Name})
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" << field_org.{field.Name} << ""***""<< field_dec.{field.Name}<<endl;
    }}");
                 },
                 (pro, em, len) =>
                 {
                     if (len > 1)
                     {
                         code.Append($@"
    if(memcmp(&field_org.{field.Name},&field_dec.{field.Name},sizeof(tm)) != 0)
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" <<endl;
    }}");
                     }
                     else
                         code.Append($@"
    if( field_org.{field.Name} != field_dec.{field.Name})
    {{
        succeed = false;
        cout << ""    {field.Caption}失败:"" << field_org.{field.Name} << ""***""<< field_dec.{field.Name}<<endl;
    }}");
                 });
        }

        #region 测试素材

        public static string[] TestLine =
        {
            "追踪着鹿的猎人是看不见山的",
"谁不向前看，谁就会面临许多困难",
"有志始知蓬莱近，无为总觉咫尺远",
"雄心壮志是茫茫黑夜中的北斗星",
"志之所趋，无远勿届，穷山复海不能限也；志之所向，无坚不摧",
"不怕路远，就怕志短",
"志高山峰矮，路从脚下伸",
"有志者自有千方百计，无志者只感千难万难",
"有志登山顶，无志站山脚",
"有志的人战天斗地，无志的人怨天恨地",
"天才是由于对事业的热爱感而发展起来的，简直可以说天才",
"人生志气立，所贵功业昌",
"人若有志，万事可为",
"并非神仙才能烧陶器，有志的人总可以学得精手艺",
"有志者能使石头长出青草来",
"雄鹰必须比鸟飞得高，因为它的猎物就是鸟",
"无所求则无所获",
"壮志与毅力是事业的双翼",
"志不真则心不热，心不热则功不贤",
"把意念沉潜得下，何理不可得，把志气奋发得起，何事不可做",
"立志是事业的大门，工作是登门入室的旅程",
"壮志与毅力是事业的双翼",
"在年轻人的颈项上，没有什么东西能比事业心这颗灿烂的宝珠",
"不为穷变节，不为贱易志",
"褴褛衣内可藏志",
"志气和贫困是患难兄弟，世人常见他们伴在一起",
"死犹未肯输心去，贫亦其能奈我何！",
"困，你是人类艺术的源泉，你将伟大的灵感赐予诗人",
"贫穷是一切艺术职业的母亲",
"无钱之人脚杆硬，有钱之人骨头酥",
"鸭仔无娘也长大，几多白手也成家",
"贫困能造就男子气概",
"穷人的孩子早当家",
"贫困教会贫困者一切",
"对没志气的人，路程显得远；对没有银钱的人，城镇显得远",
"有志者，事竟成",
"人惟患无志，有志无有不成者",
"志不立，天下无可成之事",
"志正则众邪不生",
"鹰爱高飞，鸦栖一枝",
"鸟贵有翼，人贵有志",
"器大者声必闳，志高者意必远",
"燕雀安知鸿鹄之志哉",
"三军可夺帅也，匹夫不可夺志也",
"志，气之帅也",
"石看纹理山看脉，人看志气树看材",
"志之所向，金石为开，谁能御之？",
"志坚者，功名之柱也登山不以艰险而止，则必臻乎峻岭",
"心志要坚，意趣要乐",
"一人立志，万夫莫敌"
        };
        #endregion

        private static string GetBaseTypeValue(string type)
        {
            switch (type.MulitReplace2("", " ", "\t").ToLower())
            {
                case "bool":
                    return (DateTime.Now.Ticks % 2) == 1 ? "true" : "false";
                case "double":
                case "float":
                    return (random.NextDouble() * 10000.0).ToString("F");
                case "char":
                case "unsignedchar":
                    return $"'{((char)('A' + random.Next(25)))}'";

                case "wchar":
                case "wchar_t":
                case "char16_t":
                case "char32_t":
                    return $"L'{('A' + random.Next(25))}'";
                case "short":
                    return random.Next(short.MaxValue).ToString();
                case "unsignedshort":
                case "ushort":
                    return random.Next(ushort.MaxValue).ToString();
                case "int":
                case "unsignedint":
                case "uint":
                case "longlong":
                case "unsignedlonglong":
                case "__int16":
                case "unsigned__int16":
                case "__int32":
                case "unsigned__int32":
                case "__int64":
                case "unsigned__int64":
                    return random.Next(int.MaxValue).ToString();
            }
            return "error";
        }
        #endregion
    }
}