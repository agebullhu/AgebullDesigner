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
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder("��Ԫ����", "���ݶ�ȡ��Ԫ����(C#)", "cs", EntityUt);
            CoderManager.RegisteCoder("��Ԫ����", "���ݲ�����Ԫ����(C#)", "cs", UpdateUt);
            CoderManager.RegisteCoder<SolutionConfig>("��Ԫ����", "API��Ԫ����(C#)", "cs", ApiUt);
        }
        #endregion

        #region ���ݶ�ȡ��Ԫ����

        private static string EntityUt(EntityConfig config)
        {
            return ($@"
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

        public static string UpdateUt(EntityConfig config)
        {
            return ($@"
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

        #region ����Ƭ��

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