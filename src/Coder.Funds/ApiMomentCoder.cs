using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Funds
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ApiMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("�ڻ����", "API��־����(C++)", (ApiLog));
            MomentCoder.RegisteCoder("�ڻ����", "APIѡ��ִ�д���(C++)", (ApiSwitch));
            
            MomentCoder.RegisteCoder("�ڻ����", "API��������(C++)", (ApiCommandIdCode));
            
            MomentCoder.RegisteCoder("�ڻ����", "API���ö������(C++)-Proxy", (ApiCallCodeByProxyDef));
            MomentCoder.RegisteCoder("�ڻ����", "API����ʵ�ִ���(C++)-Proxy", (ApiCallCodeByProxy));
            
            MomentCoder.RegisteCoder("�ڻ����", "API���ö������(C++)-Server", (CmdCallCodeByServerDef));
            MomentCoder.RegisteCoder("�ڻ����", "API����ʵ�ִ���(C++)-Server", (CmdCallCodeByServer));
            MomentCoder.RegisteCoder("�ڻ����", "API�����߼�ģ��(C++)-Server", (BusinessCodeByServer));
            
            MomentCoder.RegisteCoder("�ڻ����", "API���ö������(C++)-Client", (CmdCallCodeByClientDef));
            MomentCoder.RegisteCoder("�ڻ����", "API����ʵ�ִ���(C++)-Client", (CmdCallCodeByClient));
            
            MomentCoder.RegisteCoder("�ڻ����", "API���ö������(C++)-Clr", (CmdCallCodeByClrDef));
            MomentCoder.RegisteCoder("�ڻ����", "API����ʵ�ִ���(C++)-Clr", (CmdCallCodeByClr));
            
            MomentCoder.RegisteCoder("�ڻ����", "API���ô���(C#)", (CmdCallCodeByCs));
        }
        #endregion

        #region ����

        public static string ApiLog(ConfigBase config)
        {
            StringBuilder code = new StringBuilder();
            code.Append(@"
		switch (cmd_call->cmd_id)
		{");
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
            case NET_COMMAND_ES_{item.Name.ToUpper()}://{item.Caption}
                 log_debug(""{item.Caption}:�û�[%s]"",cmd_call->user_token);
                 break;");
            }
            code.Append(@"
        }");
            return code.ToString();

        }

        public static string ApiSwitch(ConfigBase config)
        {
            StringBuilder code = new StringBuilder();
            code.Append(@"
//�������Ϣ��
void server_message_pump()
{
	while (get_net_state() == NET_STATE_RUNING)
	{
		if (server_cmd_queue.empty())
		{
			boost::this_thread::sleep(boost::posix_time::milliseconds(1));
			continue;
		}
		PNetCommand cmd_call;
		{
			boost::lock_guard<boost::mutex> guard(server_cmd_mutex);
			cmd_call = server_cmd_queue.front();
			server_cmd_queue.pop();
		}
		switch (cmd_call->cmd_id)
		{");
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
                case NET_COMMAND_ES_{item.Name.ToUpper()}://{item.Caption}
                    proxy.Do{item.Name}(cmd_call);
                    break;");
            }
            code.Append(@"
        }
    }
}");
            return code.ToString();

        }
        public static string ApiCommandIdCode(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
//{item.Caption}
const NET_COMMAND NET_COMMAND_ES_{item.Name.ToUpper()} = 0x{item.Index.ToString("X")};");

            }
            return code.ToString();
        }
        #endregion
        #region �ͻ��˵���

        public static string CmdCallCodeByClientDef(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                var entity = GlobalConfig.GetEntity(item.ResultArg);
                if (entity != null)
                    code.Append($@"
#include ""{entity.Parent.Name}/{entity.Name}.h""");
            }
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                if (string.IsNullOrWhiteSpace(item.ResultArg))
                {
                    code.Append($@"
/** 
* @brief {item.Caption}
* @return ��
*/
COMMAND_STATE {item.Name}();
");
                }
                else
                {
                    code.Append($@"
/** 
* @brief {item.Caption}
* @param {{{item.ResultArg}}} arg ���ò���
* @return ��
*/
COMMAND_STATE {item.Name}({item.ResultArg}& arg);
");

                }

            }
            return code.ToString();
        }
        public static string CmdCallCodeByClient(ConfigBase config)
        {
            var code = new StringBuilder();


            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                var type = item.ResultArg;
                if (item.ResultArg == "char")
                {
                    type = "const char* arg";
                }
                else if (!string.IsNullOrWhiteSpace(type))
                {
                    type = item.ResultArg + "& arg";
                }
                code.Append($@"
/** 
* @brief {item.Caption}
* @return ��
*/
COMMAND_STATE {item.Name}({type})
{{
    COMMAND_STATE state = NET_COMMAND_STATE_SUCCEED;
	try
	{{{ClientApiCall(item)}
		set_command_head(cmd_call, NET_COMMAND_ES_{item.Name.ToUpper()});
		request_net_cmmmand(cmd_call, nullptr);
	}}
	catch(...)
	{{
         state = NET_COMMAND_STATE_SERVER_UNKNOW;
	}}
	return state;
}}");
            }
            return code.ToString();
        }

        private static string ClientApiCall(ApiItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ResultArg))
            {
                return @"
		PNetCommand cmd_call = new NetCommand();
		cmd_call->data_len = 0;";
            }
            if (SolutionConfig.Current.Entities.Any(p => p.Name == item.ResultArg))
                return @"
        auto cmd_call = SerializeToCommand(&arg);";
            switch (item.ResultArg)
            {
                case "string":
                    return @"
        GBS::Futures::Manage::StringArgument str_arg;
		strcpy_s(str_arg.Argument, arg);
        auto cmd_call = SerializeToCommand(&str_arg);";
                case "short":
                case "long":
                case "int":
                case "long long":
                case "unsigned short":
                case "unsigned long":
                case "unsigned int":
                case "unsigned long long":
                    return @"
        GBS::Futures::Manage::StringArgument str_arg;
		sprintf_s(str_arg.Argument,""%d"", arg);
		auto cmd_call = SerializeToCommand(&str_arg);";
                case "float":
                case "double":
                    return @"
        GBS::Futures::Manage::StringArgument str_arg;
		sprintf_s(str_arg.Argument,""%f"", arg);
		auto cmd_call = SerializeToCommand(&str_arg);";
            }
            return @"
        GBS::Futures::Manage::StringArgument str_arg;
		strcpy_s(str_arg.Argument, arg);
        auto cmd_call = SerializeToCommand(&str_arg);";
        }
        #endregion
        #region ����˵���

        public static string CmdCallCodeByServerDef(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd �������
* @return ��
*/
void Do{item.Name}(const PNetCommand cmd);");

            }
            return code.ToString();
        }
        public static string CmdCallCodeByServer(ConfigBase config)
        {
            var code = new StringBuilder();
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
#include ""GbsTrade/{item.Name}Business.h""");
            }
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append(CmdCallCodeByServer(item, "Do"));

            }
            return code.ToString();
        }

        public static string CmdCallCodeByServer(ConfigBase item, string head)
        {
            return $@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd �������
* @return ��
*/
void GbsTradeCommand::{head}{item.Name}(const PNetCommand cmd_arg)
{{
	m_command_map.insert(make_pair(cmd_arg->cmd_identity, cmd_arg));
    assert(m_state == TRADECOMMAND_STATUS_SUCCEED);
	COMMAND_STATE state = NET_COMMAND_STATE_SUCCEED;
	try
	{{
        {item.Name}Business business;
		state = business.initialization(cmd_arg);
		if (state != NET_COMMAND_STATE_SUCCEED)
		{{
            publish_command_state(cmd_arg, state);
			return;
		}}
		state = business.verify();
		if (state != NET_COMMAND_STATE_SUCCEED)
		{{
            publish_command_state(cmd_arg, state);
			return;
		}}
		state = business.prepare();
		if (state != NET_COMMAND_STATE_SUCCEED)
		{{
            publish_command_state(cmd_arg, state);
			return;
		}}
		state = business.doit();
		business.doit();
		if (state != NET_COMMAND_STATE_SUCCEED)
		{{
            publish_command_state(cmd_arg, state);
			return;
		}}
	}}
	catch (...)
	{{
        state = NET_COMMAND_STATE_SERVER_UNKNOW;
	}}
	publish_command_state(cmd_arg, state);
}}";
        }

        #endregion
        #region ԭʼ���ö�

        public static string ApiCallCodeByProxyDef(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd_arg �������{(item.Argument == null ? "(δʹ�ã���֧��ͳһ��ʽ)" : null)}
* @return ��
*/
void Do{item.Name}(const PNetCommand cmd_arg);");
            }
            return code.ToString();
        }

        public static string ApiCallCodeByProxy(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                CallEsApi(code, item);
            }
            return code.ToString();
        }

        private static void CallEsApi(StringBuilder code, ApiItem item)
        {
            var entity = item.Argument;
            if (entity == null)
            {
                code.Append($@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd_arg �������(δʹ�ã���֧��ͳһ��ʽ)
* @return ��
*/
void EsTradeCommand::Do{item.Name}(const PNetCommand cmd_arg)
{{
    assert(m_last_trade_state == TRADECOMMAND_STATUS_SUCCEED);
	COMMAND_STATE state = NET_COMMAND_STATE_SUCCEED;
	try
	{{
		{item.CallArg} field;
		memset(&field, 0, sizeof(field));
        int reqId = 0;
		int re = m_trader->m_esTrader->{item.Name}(field, reqId);
		m_last_trade_state = state = m_trader->checkIsError(re, ""{item.Caption}"");
		if (state == 0)
		{{
			save_cmd2req(cmd_arg, reqId);
		}}
	}}
	catch (...)
	{{
        state = NET_COMMAND_STATE_SERVER_UNKNOW;
	}}
	publish_command_state(cmd_arg, state);
}}");
                return;
            }

            code.Append($@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd_arg �������
* @return ��
*/
void EsTradeCommand::Do{item.Name}(const PNetCommand cmd_arg)
{{
	COMMAND_STATE state = NET_COMMAND_STATE_SUCCEED;
	try
	{{
        {entity.Name} arg;
        cmd_arg >> arg;
		{item.CallArg} field;
		memset(&field, 0, sizeof(field));
        CopyToEs(&arg,&field);");
            var user = entity.Properties.FirstOrDefault(p => p.Name == "ClientNo");
            if (user != null)
            {
                code.Append($@"
		strcpy_s(field.ClientNo,m_user.c_str());//{user.Caption}");
            }
            code.Append($@"
        int reqId = 0;
		int re = m_trader->m_esTrader->{item.Name}(field, reqId);
		m_last_trade_state = state = m_trader->checkIsError(re, ""{item.Caption}"");
		if (state == 0)
		{{
			save_cmd2req(cmd_arg, reqId);
		}}
	}}
	catch (...)
	{{
        state = NET_COMMAND_STATE_SERVER_UNKNOW;
	}}
	publish_command_state(cmd_arg, state);
}}");
        }

        #endregion
        #region CLR�˵���

        public static string CmdCallCodeByClrDef(ConfigBase config)
        {
            var project = config as ProjectConfig;
            if (project == null)
                return null;
            var code = new StringBuilder();


            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                var entity = GlobalConfig.GetEntity(item.ResultArg);
                if (entity != null)
                    code.Append($@"
#include ""{entity.Parent.Name}/{entity.Name}.h""");
            }
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {/*static void Login(LoginArgData^ cs_field);*/
                if (string.IsNullOrWhiteSpace(item.ResultArg))
                {
                    code.Append($@"
/** 
* @brief {item.Caption}
* @return ��
*/
static void {item.Name}();
");
                }
                else
                {
                    string type = item.ResultArg;
                    var entity = GlobalConfig.GetEntity(item.ResultArg);
                    if (entity != null)
                        type = $"{item.ResultArg}Data^";
                    else if (type == "char")
                        type = "String^";

                    code.Append($@"
/** 
* @brief {item.Caption}
* @param {{{type}}} arg ���ò���
* @return ��
*/
static void {item.Name}({type} arg);
");

                }

            }
            return code.ToString();
        }
        public static string CmdCallCodeByCs(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                string type = item.ResultArg;
                string init = null;
                var entity = GlobalConfig.GetEntity(item.ResultArg);
                if (entity != null)
                {
                    type = $"{item.ResultArg}Data arg";
                    init = UnitTestMomentCoder.CreateCsEntityCode(entity, "arg");
                }
                else if (type == "char")
                {
                    type = "string arg";
                    init = @"string arg = ""���������""";
                }
                else if (!string.IsNullOrWhiteSpace(type))
                {
                    type = $"{item.ResultArg} arg";
                    init = $"{item.ResultArg} arg = 0";
                }
                code.Append($@"

/// <summary>
/// {item.Caption}
/// </summary>
/// <param name=""arg"">����</param>
/// <returns></returns>
public void {item.Name}({type})
{{
    {(string.IsNullOrWhiteSpace(item.ResultArg)
                ? $@"CommandProxy.{item.Name}();"
                : $@"CommandProxy.{item.Name}(arg);")}
}}");
                if (!string.IsNullOrEmpty(type))
                    code.Append($@"

/// <summary>
/// {item.Caption}
/// </summary>
/// <returns></returns>
public void {item.Name}()
{{
    {init}
    {(string.IsNullOrWhiteSpace(item.ResultArg)
                ? $@"CommandProxy.{item.Name}();"
                : $@"CommandProxy.{item.Name}(arg);")}
}}");

            }
            return code.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string CmdCallCodeByClr(ConfigBase config)
        {
            var project = config as ProjectConfig;
            if (project == null)
                return null;
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                string type = item.ResultArg;
                var entity = GlobalConfig.GetEntity(item.ResultArg);
                if (entity != null)
                    type = $"{item.ResultArg}Data^ arg";
                else if (type == "char")
                    type = "String^ arg";
                else if (!string.IsNullOrWhiteSpace(type))
                    type = $"{item.ResultArg} arg";
                code.Append($@"
/** 
* @brief {item.Caption}
* @return ��
*/
void CommandProxy::{item.Name}({type})
{{{ClrApiCall(item)}
}}");
            }
            return code.ToString();
        }

        private static string ClrApiCall(ApiItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ResultArg))
            {
                return $@"
		GBS::Futures::TradeCommand::Client::{item.Name}();";
            }
            if (SolutionConfig.Current.Entities.Any(p => p.Name == item.ResultArg))
                return $@"
        {item.ResultArg} field;
		CopyFromClr(arg, field);
		GBS::Futures::TradeCommand::Client::{item.Name}(field);";
            switch (item.ResultArg)
            {
                case "short":
                case "long":
                case "int":
                case "long long":
                case "unsigned short":
                case "unsigned long":
                case "unsigned int":
                case "unsigned long long":
                case "float":
                case "double":
                    return $@"
        GBS::Futures::TradeCommand::Client::{item.Name}(arg);";
            }
            return $@"
		const char* c_arg = (const char*)(Marshal::StringToHGlobalAnsi(arg)).ToPointer();
		GBS::Futures::TradeCommand::Client::{item.Name}(c_arg);
		Marshal::FreeHGlobal(IntPtr((void*)c_arg));";
        }
        #endregion
        #region �����ҵ���߼�ģ��

        public static string BusinessCodeByServer(ConfigBase config)
        {
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                ApiBusinessHCode(item);
                ApiBusinessCppCode(item);
            }
            return "����ļ��鿴";
        }

        private static void ApiBusinessHCode(ApiItem item)
        {
            var entity = item.Argument;
            var code = new StringBuilder();

            code.Append($@"#ifndef _{item.Name.ToUpper()}_BUSINESS_H
#define _{item.Name.ToUpper()}_BUSINESS_H
#pragma once
#include <stdafx.h>
#include <GbsBusiness/entity_redis.h>");

            if (entity != null)
            {
                code.Append($@"
#include <{entity.Parent.Name}/{entity.Name}.h>
using namespace {entity.Parent.NameSpace.Replace(".", "::")};");
            }

            code.Append($@"
namespace GBS
{{
	namespace Futures
	{{
		namespace TradeManagement
		{{
            /** 
            * @brief {item.Caption}�߼�������
            * @return ��
            */
			class {item.Name}Business
			{{");
            if (entity != null)
            {
                code.Append($@"
                //������Ĳ���
				{entity.Name} m_field;");
            }
            code.Append($@"
                //ԭʼ���ò���
				PNetCommand m_call_arg;
			public:
                /** 
                * @brief ����
                */
				{item.Name}Business();
                /** 
                * @brief ����
                */
				~{item.Name}Business();
                /** 
                * @brief ��ʼ��
                * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
                */
				COMMAND_STATE initialization(PNetCommand cmd_arg);
                /** 
                * @brief ����У��
                * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
                */
				COMMAND_STATE verify();
                /** 
                * @brief ׼��ִ��
                * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
                */
				COMMAND_STATE prepare();
                /** 
                * @brief ִ�ж���
                * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
                */
				COMMAND_STATE doit();
			}};
		}}
	}}
}}
#endif");
            FileCoder.WriteFile($@"c:\work\SharpCode\GbsTrade\{item.Name}Business.h", code.ToString());
        }

        public static void ApiBusinessCppCode(ApiItem item)
        {
            var entity = item.Argument;
            StringBuilder code = new StringBuilder();
            code.Append($@"#include <stdafx.h>
#include ""{item.Name}Business.h""
namespace GBS
{{
    namespace Futures
    {{
        namespace TradeManagement
        {{
            /** 
            * @brief ����
            */
		    {item.Name}Business::{item.Name}Business()
			:m_call_arg(nullptr)
            {{
            }}

            /** 
            * @brief ����
            */
            {item.Name}Business::~{item.Name}Business()
            {{
            }}

            /** 
            * @brief ��ʼ��
            * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
            */
            COMMAND_STATE {item.Name}Business::initialization(PNetCommand cmd_arg)
            {{
                m_call_arg = cmd_arg;");
            if (entity != null)
            {
                code.Append(@"
                cmd_arg >> m_field;
                save_to_redis(&m_field);");
            }
            code.Append($@"
                return NET_COMMAND_STATE_SUCCEED;
            }}

            /** 
            * @brief ����У��
            * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
            */
            COMMAND_STATE {item.Name}Business::verify()
            {{
                return NET_COMMAND_STATE_SUCCEED;
            }}

            /** 
            * @brief ׼��ִ��
            * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
            */
            COMMAND_STATE {item.Name}Business::prepare()
            {{
                return NET_COMMAND_STATE_SUCCEED;
            }}

            /** 
            * @brief ִ�ж���(Ĭ��ʵ��ԭ��������ʢ������)
            * @return �������,NET_COMMAND_STATE_SUCCEED��ʾ�ɹ�
            */
            COMMAND_STATE {item.Name}Business::doit()
            {{
                PNetCommand es_cmd_arg = new NetCommand();");
            if (entity != null)
            {
                code.Append(@"
                es_cmd_arg << m_field;");
            }
            code.Append(@"
                memcpy(es_cmd_arg, m_call_arg, NETCOMMAND_HEAD_LEN);
                request_net_cmmmand(es_cmd_arg, nullptr);
                return NET_COMMAND_STATE_SUCCEED;
            }
        }
    }
}");
            FileCoder.WriteFile($@"c:\work\SharpCode\GbsTrade\{item.Name}Business.cpp", code.ToString());
        }

        #endregion
    }
}