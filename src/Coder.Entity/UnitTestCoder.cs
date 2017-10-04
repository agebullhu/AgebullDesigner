using System;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public class UnitTestCoder : MomentCoderBase, IAutoRegister
    {
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("��Ԫ����", "���ݶ�ȡ��Ԫ����(C#)", EntityUt);
            MomentCoder.RegisteCoder("��Ԫ����", "���ݲ�����Ԫ����(C#)", UpdateUt);
            MomentCoder.RegisteCoder("��Ԫ����", "API��Ԫ����(C#)", ApiUt);
        }
        #endregion

        #region ���ݶ�ȡ��Ԫ����

        public static string EntityUt(ConfigBase config)
        {
            var code = new StringBuilder();
            var project = config as ProjectConfig;
            if (project != null)
            {
                foreach (var em in project.Entities)
                    EntityUt(code, em);
            }
            else
            {
                var entity = config as EntityConfig;
                if (entity != null)
                {
                    EntityUt(code, entity);
                    return code.ToString();
                }
                foreach (var em in SolutionConfig.Current.Entities)
                    EntityUt(code, em);
            }
            return code.ToString();
        }

        private static void EntityUt(StringBuilder code, EntityConfig config)
        {
            if (config == null || config.Discard)
                return;

            code.Append($@"
    try{{
        var access = new {config.Name}DataAccess();
        var data = access.First();
        Assert.IsNotNull(data,""{config.Caption}*����Ϊ��"");
    }}catch(Exception ex){{
        Assert.Fail(""{config.Caption}:""+ex.Message);
    }}");
        }


        #endregion

        #region ���ݶ�ȡ��Ԫ����

        public static string UpdateUt(ConfigBase config)
        {
            var code = new StringBuilder();
            var project = config as ProjectConfig;
            if (project != null)
            {
                foreach (var em in project.Entities)
                    UpdateUt(code, em);
            }
            else
            {
                var entity = config as EntityConfig;
                if (entity != null)
                {
                    UpdateUt(code, entity);
                    return code.ToString();
                }
                foreach (var em in SolutionConfig.Current.Entities)
                    UpdateUt(code, em);
            }
            return code.ToString();
        }

        private static void UpdateUt(StringBuilder code, EntityConfig config)
        {
            if (config == null || config.Discard || config.IsInternal)
                return;

            code.Append($@"
    try
    {{
        var access = new {config.Name}DataAccess();
        {NewCsCode(config)}
        access.Insert(data);
        Assert.IsTrue(data.{config.PrimaryField} > 0,""{config.Caption}*��������ʧ������Ϊ��"");
        access.Update(data);
        Assert.IsTrue(data.{config.PrimaryField} > 0,""{config.Caption}*��������ʧ������Ϊ��"");
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
            foreach (PropertyConfig field in config.Properties.Where( p=>!p.IsDelete && !p.Discard && !p.DbInnerField && !p.InnerField ))
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

        #region ����Ƭ��

        private static readonly Random random = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

        private static string GetTestValue(string type)
        {
            switch (type)
            {
                case "bool":
                    return (DateTime.Now.Ticks % 2) == 1 ? "true" : "false";
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
            "׷����¹",
            "˭����ǰ",
            "��־ʼ֪",
            "����׳־",
            "־֮����",
            "����·Զ","����־��",
            "־��ɽ�尫","·�ӽ�����",
            "��־������ǧ���ټ�","��־��ֻ��ǧ������",
            "��־��ɽ��","��־վɽ��",
            "��־����ս�춷��","��־����Թ��޵�",
            "��������ڶ���ҵ���Ȱ��ж���չ������","��ֱ����˵���",
            "����־����","����ҵ��",
            "������־","���¿�Ϊ",
            "�������ɲ���������","��־�����ܿ���ѧ�þ�����",
            "��־����ʹʯͷ���������",
            "��ӥ�������ɵø�","��Ϊ�������������",
            "��������������",
            "׳־����������ҵ��˫��",
            "־�������Ĳ���","�Ĳ����򹦲���",
            "�������Ǳ����","�����ɵ�","��־���ܷ�����","���²�����",
            "��־����ҵ�Ĵ���","�����ǵ������ҵ��ó�",
            "׳־����������ҵ��˫��",
            "�������˵ľ�����","û��ʲô�����ܱ���ҵ����Ų��õı���",
            "��Ϊ����","��Ϊ����־",
            "�������ڿɲ�־",
            "־����ƶ���ǻ����ֵ�","���˳������ǰ���һ��",
            "����δ������ȥ","ƶ���������ҺΣ�",
            "��","��������������ԴȪ","�㽫ΰ�����д���ʫ��",
            "ƶ����һ������ְҵ��ĸ��",
            "��Ǯ֮�˽Ÿ�Ӳ","��Ǯ֮�˹�ͷ��",
            "Ѽ������Ҳ����","�������Ҳ�ɼ�",
            "ƶ���������������",
            "���˵ĺ����統��",
            "ƶ���̻�ƶ����һ��",
            "��û־������","·���Ե�Զ����û����Ǯ����","�����Ե�Զ",
            "��־��","�¾���",
            "��Ω����־","��־���в�����",
            "־����","�����޿ɳ�֮��",
            "־������а����",
            "ӥ���߷�","ѻ��һ֦",
            "�������","�˹���־",
            "������������","־�������Զ",
            "��ȸ��֪����֮־��",
            "�����ɶ�˧Ҳ","ƥ�򲻿ɶ�־Ҳ",
            "־","��֮˧Ҳ",
            "ʯ������ɽ����","�˿�־��������",
            "־֮����","��ʯΪ��","˭����֮��",
            "־����","����֮��Ҳ��ɽ���Լ��ն�ֹ","����������",
            "��־Ҫ��","��ȤҪ��",
            "һ����־","���Ī��"
        };
        #endregion
        #endregion
        #region ���ݶ�ȡ��Ԫ����

        public static string ApiUt(ConfigBase config)
        {
            var code = new StringBuilder();
            code.Append(@"
                var caller = new WebApiCaller
                {{
                    Host = ""http://localhost:20639/""
                }};
                var result = caller.Get<ValiadateCodeResponse>(""v1/user/login"", new UserInfoLoginRequest {{ }});
                if (!result.Result && result.Status.ErrorCode == ErrorCode.Auth_Device_Unknow)
                {{
                    caller.Bearer = result.Status.Message;
                    result = caller.Get<ValiadateCodeResponse>(""v1/verification/getcode"");
                }}
                Console.WriteLine(JsonConvert.SerializeObject(result));");
            foreach (var item in SolutionConfig.Current.ApiItems)
                ApiUt(code, item);
            return code.ToString();
        }
        private static void ApiUt(StringBuilder code, ApiItem config)
        {
            code.Append($@"
        {{//{config.Project}.{config.Name}
                var result = caller.{config.Method.ToString().ToLower().ToUWord()}/*<{config.ResultArg}>*/(""{config.Org}""");
            if (config.Argument != null)
            {
                code.Append($@", new Dictionary<string, string>
                {{");
                foreach (var field in config.Argument.Properties)
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