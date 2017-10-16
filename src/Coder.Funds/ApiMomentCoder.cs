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
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("期货相关", "API日志代码(C++)", (ApiLog));
            MomentCoder.RegisteCoder("期货相关", "API选择执行代码(C++)", (ApiSwitch));
            
            MomentCoder.RegisteCoder("期货相关", "API命令定义代码(C++)", (ApiCommandIdCode));
            
            MomentCoder.RegisteCoder("期货相关", "API调用定义代码(C++)-Proxy", (ApiCallCodeByProxyDef));
            MomentCoder.RegisteCoder("期货相关", "API调用实现代码(C++)-Proxy", (ApiCallCodeByProxy));
            
            MomentCoder.RegisteCoder("期货相关", "API调用定义代码(C++)-Server", (CmdCallCodeByServerDef));
            MomentCoder.RegisteCoder("期货相关", "API调用实现代码(C++)-Server", (CmdCallCodeByServer));
            MomentCoder.RegisteCoder("期货相关", "API调用逻辑模板(C++)-Server", (BusinessCodeByServer));
            
            MomentCoder.RegisteCoder("期货相关", "API调用定义代码(C++)-Client", (CmdCallCodeByClientDef));
            MomentCoder.RegisteCoder("期货相关", "API调用实现代码(C++)-Client", (CmdCallCodeByClient));
            
            MomentCoder.RegisteCoder("期货相关", "API调用定义代码(C++)-Clr", (CmdCallCodeByClrDef));
            MomentCoder.RegisteCoder("期货相关", "API调用实现代码(C++)-Clr", (CmdCallCodeByClr));
            
            MomentCoder.RegisteCoder("期货相关", "API调用代码(C#)", (CmdCallCodeByCs));
        }
        #endregion

        #region 定义

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
                 log_debug(""{item.Caption}:用户[%s]"",cmd_call->user_token);
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
//服务端消息泵
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
        #region 客户端调用

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
* @return 无
*/
COMMAND_STATE {item.Name}();
");
                }
                else
                {
                    code.Append($@"
/** 
* @brief {item.Caption}
* @param {{{item.ResultArg}}} arg 调用参数
* @return 无
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
* @return 无
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
        #region 服务端调用

        public static string CmdCallCodeByServerDef(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd 命令对象
* @return 无
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
* @param {{PNetCommand}} cmd 命令对象
* @return 无
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
        #region 原始调用端

        public static string ApiCallCodeByProxyDef(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                code.Append($@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd_arg 命令对象{(item.Argument == null ? "(未使用，仅支持统一格式)" : null)}
* @return 无
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
* @param {{PNetCommand}} cmd_arg 命令对象(未使用，仅支持统一格式)
* @return 无
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
* @param {{PNetCommand}} cmd_arg 命令对象
* @return 无
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
        #region CLR端调用

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
* @return 无
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
* @param {{{type}}} arg 调用参数
* @return 无
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
                    init = @"string arg = ""请输入参数""";
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
/// <param name=""arg"">参数</param>
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
* @return 无
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
        #region 服务端业务逻辑模板

        public static string BusinessCodeByServer(ConfigBase config)
        {
            foreach (var item in SolutionConfig.Current.ApiItems.Where(p => !p.Discard))
            {
                ApiBusinessHCode(item);
                ApiBusinessCppCode(item);
            }
            return "请打开文件查看";
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
            * @brief {item.Caption}逻辑处理类
            * @return 无
            */
			class {item.Name}Business
			{{");
            if (entity != null)
            {
                code.Append($@"
                //解析后的参数
				{entity.Name} m_field;");
            }
            code.Append($@"
                //原始调用参数
				PNetCommand m_call_arg;
			public:
                /** 
                * @brief 构造
                */
				{item.Name}Business();
                /** 
                * @brief 析构
                */
				~{item.Name}Business();
                /** 
                * @brief 初始化
                * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
                */
				COMMAND_STATE initialization(PNetCommand cmd_arg);
                /** 
                * @brief 数据校验
                * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
                */
				COMMAND_STATE verify();
                /** 
                * @brief 准备执行
                * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
                */
				COMMAND_STATE prepare();
                /** 
                * @brief 执行动作
                * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
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
            * @brief 构造
            */
		    {item.Name}Business::{item.Name}Business()
			:m_call_arg(nullptr)
            {{
            }}

            /** 
            * @brief 析构
            */
            {item.Name}Business::~{item.Name}Business()
            {{
            }}

            /** 
            * @brief 初始化
            * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
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
            * @brief 数据校验
            * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
            */
            COMMAND_STATE {item.Name}Business::verify()
            {{
                return NET_COMMAND_STATE_SUCCEED;
            }}

            /** 
            * @brief 准备执行
            * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
            */
            COMMAND_STATE {item.Name}Business::prepare()
            {{
                return NET_COMMAND_STATE_SUCCEED;
            }}

            /** 
            * @brief 执行动作(默认实现原样发到易盛服务器)
            * @return 错误代号,NET_COMMAND_STATE_SUCCEED表示成功
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