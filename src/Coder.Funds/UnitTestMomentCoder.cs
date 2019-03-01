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
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("�ڻ����", "ʵ�����л���Ԫ����(C++)", EntityUt);
            MomentCoder.RegisteCoder("�ڻ����", "API�������(C++)", ApiUt);
            MomentCoder.RegisteCoder("�ڻ����", "��ֵ����(C++)", CreateEntityCode);
        }
        #endregion
        

        #region ʵ�����л���Ԫ����

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
//����
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
//{config.Caption}���л���Ԫ����
bool ut_{config.Name}()
{{
    //��ʼ��{CreateEntityCode(config, "field_org")}
    //���л�
    PNetCommand cmd;
    cmd << field_org;
    //�����л�
    {config.Name} field_dec;
    cmd >> field_dec;
    //У����
    bool succeed = true;
    cout << ""{config.Caption}���Խ��:"" << endl;");
            foreach (var field in config.Properties)
            {
                FieldCmp(code, field);
            }
            code.Append(@"
    if(succeed)
        cout << ""    �ɹ�"" <<endl;
    delete cmd;
    return succeed;
}");
        }


        #endregion

        #region API�������

        #region ʵ�����л���Ԫ����

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
//{api.Caption}API�������
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
//{api.Caption}API�������
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
//{api.Caption}API�������
bool api_ut_{api.Name}(EsTradeCommand& command)
{{
    //��ʼ��{CreateEntityCode(api.Argument, "field_org")}
    //���л�
    PNetCommand cmd_arg;
    cmd_arg << field_org;
    //��������
    command.Do{api.Name}(cmd_arg);
	if (command.get_last_trade_state() == 0)
		command.m_trader->WaitEvent();
    cout << ""����{api.Caption}����:"" << command.m_trader->m_last_error << endl;
    return command.m_trader->m_last_error == 0;
}}");
            else
                code.Append($@"
//{api.Caption}API�������
bool api_ut_{api.Name}(EsTradeCommand& command)
{{
    //��ʼ��
    NetCommand cmd_arg;
    memset(&cmd_arg,0,sizeof(NetCommand));
    //��������
    cout << ""����{api.Caption}����"" << endl;
    command.Do{api.Name}(&cmd_arg);
	if (command.get_last_trade_state() == 0)
		command.m_trader->WaitEvent();
    cout << ""����{api.Caption}����:"" << command.m_trader->m_last_error << endl;
    return command.m_trader->m_last_error == 0;
}}");
        }


        #endregion
        #endregion

        #region ����Ƭ��

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
        cout << ""    {field.Caption}ʧ��:"" << field_org.{field.Name} << ""***""<<field_dec.{field.Name}<<endl;
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
        cout << ""    {field.Caption}ʧ��:"" <<endl;
    }}");
                     }
                     else
                     {
                         code.Append($@"
    if( field_org.{field.Name} != field_dec.{field.Name})
    {{
        succeed = false;
        cout << ""    {field.Caption}ʧ��:"" << field_org.{field.Name} << ""***""<< field_dec.{field.Name}<<endl;
    }}");
                     }
                 },
                 (pro, en, len) =>
                 {
                     code.Append($@"
    if(memcmp(&field_org.{field.Name},&field_dec.{field.Name},sizeof({en.Name})) != 0)
    {{
        succeed = false;
        cout << ""    {field.Caption}ʧ��:"" <<endl;
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
        cout << ""    {field.Caption}ʧ��:"" <<endl;
    }}");
                     }
                     else
                         code.Append($@"
    if( field_org.{field.Name} != field_dec.{field.Name})
    {{
        succeed = false;
        cout << ""    {field.Caption}ʧ��:"" << field_org.{field.Name} << ""***""<< field_dec.{field.Name}<<endl;
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
        cout << ""    {field.Caption}ʧ��:"" <<endl;
    }}");
                     }
                     else
                         code.Append($@"
    if( field_org.{field.Name} != field_dec.{field.Name})
    {{
        succeed = false;
        cout << ""    {field.Caption}ʧ��:"" << field_org.{field.Name} << ""***""<< field_dec.{field.Name}<<endl;
    }}");
                 });
        }

        #region �����ز�

        public static string[] TestLine =
        {
            "׷����¹�������ǿ�����ɽ��",
"˭����ǰ����˭�ͻ������������",
"��־ʼ֪����������Ϊ�ܾ����Զ",
"����׳־��ãã��ҹ�еı�����",
"־֮��������Զ��죬��ɽ����������Ҳ��־֮�����޼᲻��",
"����·Զ������־��",
"־��ɽ�尫��·�ӽ�����",
"��־������ǧ���ټƣ���־��ֻ��ǧ������",
"��־��ɽ������־վɽ��",
"��־����ս�춷�أ���־����Թ��޵�",
"��������ڶ���ҵ���Ȱ��ж���չ�����ģ���ֱ����˵���",
"����־����������ҵ��",
"������־�����¿�Ϊ",
"�������ɲ�������������־�����ܿ���ѧ�þ�����",
"��־����ʹʯͷ���������",
"��ӥ�������ɵøߣ���Ϊ�������������",
"��������������",
"׳־����������ҵ��˫��",
"־�������Ĳ��ȣ��Ĳ����򹦲���",
"�������Ǳ���£������ɵã���־���ܷ����𣬺��²�����",
"��־����ҵ�Ĵ��ţ������ǵ������ҵ��ó�",
"׳־����������ҵ��˫��",
"�������˵ľ����ϣ�û��ʲô�����ܱ���ҵ����Ų��õı���",
"��Ϊ���ڣ���Ϊ����־",
"�������ڿɲ�־",
"־����ƶ���ǻ����ֵܣ����˳������ǰ���һ��",
"����δ������ȥ��ƶ���������ҺΣ�",
"������������������ԴȪ���㽫ΰ�����д���ʫ��",
"ƶ����һ������ְҵ��ĸ��",
"��Ǯ֮�˽Ÿ�Ӳ����Ǯ֮�˹�ͷ��",
"Ѽ������Ҳ���󣬼������Ҳ�ɼ�",
"ƶ���������������",
"���˵ĺ����統��",
"ƶ���̻�ƶ����һ��",
"��û־�����ˣ�·���Ե�Զ����û����Ǯ���ˣ������Ե�Զ",
"��־�ߣ��¾���",
"��Ω����־����־���в�����",
"־�����������޿ɳ�֮��",
"־������а����",
"ӥ���߷ɣ�ѻ��һ֦",
"��������˹���־",
"�����������ȣ�־�������Զ",
"��ȸ��֪����֮־��",
"�����ɶ�˧Ҳ��ƥ�򲻿ɶ�־Ҳ",
"־����֮˧Ҳ",
"ʯ������ɽ�������˿�־��������",
"־֮���򣬽�ʯΪ����˭����֮��",
"־���ߣ�����֮��Ҳ��ɽ���Լ��ն�ֹ������������",
"��־Ҫ�ᣬ��ȤҪ��",
"һ����־�����Ī��"
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