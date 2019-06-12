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
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API日志代码(C++)", "cpp", ApiLog);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API选择执行代码(C++)", "cpp", ApiSwitch);
            
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API命令定义代码(C++)", "cpp", ApiCommandIdCode);
            
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用定义代码(C++)-Proxy", "cpp", ApiCallCodeByProxyDef);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用实现代码(C++)-Proxy", "cpp", ApiCallCodeByProxy);
            
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用定义代码(C++)-Server", "cpp", CmdCallCodeByServerDef);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用实现代码(C++)-Server", "cpp", CmdCallCodeByServer);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用逻辑模板(C++)-Server", "cpp", BusinessCodeByServer);
            
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用定义代码(C++)-Client", "cpp", CmdCallCodeByClientDef);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用实现代码(C++)-Client", "cpp", CmdCallCodeByClient);
            
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用定义代码(C++)-Clr", "cpp", CmdCallCodeByClrDef);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用实现代码(C++)-Clr", "cpp", CmdCallCodeByClr);
            
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "API调用代码(C#)", "cs", CmdCallCodeByCs);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "常量名称字典(C#)", "cs", TypedefDictionary);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "数据类型处理代码(C#)_服务端", "cs", DataSwitch);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "方法实现(C++)_服务端", "cpp", ConstFunc);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "方法声明(C++)_服务端", "cpp", ConstFuncDef);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "方法实现(C++)_客户端", "cpp", ConstFunc_C);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "方法声明(C++)_客户端", "cpp", ConstFuncDef_C);
            MomentCoder.RegisteCoder<TypedefItem>("期货相关", "常量选择代码(C++)", "cpp", ConstSwitch);
            MomentCoder.RegisteCoder<ProjectConfig>("期货相关", "事件处理方法(C#)", "cs", ConstFunc_Cs);
            MomentCoder.RegisteCoder<TypedefItem>("期货相关", "常量到文本(C++)", "cpp", ConstToStringSwitch);
        }
        #endregion

        #region 定义

        public static string ApiLog(ProjectConfig config)
        {
            StringBuilder code = new StringBuilder();
            code.Append(@"
		switch (cmd_call->cmd_id)
		{");
            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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

        public static string ApiSwitch(ProjectConfig config)
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
            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
        public static string ApiCommandIdCode(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
            {
                code.Append($@"
//{item.Caption}
const NET_COMMAND NET_COMMAND_ES_{item.Name.ToUpper()} = 0x{item.Index.ToString("X")};");

            }
            return code.ToString();
        }
        #endregion
        #region 客户端调用

        public static string CmdCallCodeByClientDef(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
            {
                var entity = GlobalConfig.GetEntity(item.ResultArg);
                if (entity != null)
                    code.Append($@"
#include ""{entity.Parent.Name}/{entity.Name}.h""");
            }
            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
        public static string CmdCallCodeByClient(ProjectConfig config)
        {
            var code = new StringBuilder();


            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
	{{{ClientApiCall(config, item)}
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

        private static string ClientApiCall(ProjectConfig config,ApiItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ResultArg))
            {
                return @"
		PNetCommand cmd_call = new NetCommand();
		cmd_call->data_len = 0;";
            }
            if (config.Entities.Any(p => p.Name == item.ResultArg))
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

        public static string CmdCallCodeByServerDef(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
        public static string CmdCallCodeByServer(ProjectConfig config)
        {
            var code = new StringBuilder();
            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
            {
                code.Append($@"
#include ""GbsTrade/{item.Name}Business.h""");
            }
            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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

        public static string ApiCallCodeByProxyDef(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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

        public static string ApiCallCodeByProxy(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
            var user = entity.LastProperties.FirstOrDefault(p => p.Name == "ClientNo");
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

        public static string CmdCallCodeByClrDef(ProjectConfig config)
        {
            var project = config as ProjectConfig;
            if (project == null)
                return null;
            var code = new StringBuilder();


            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
            {
                var entity = GlobalConfig.GetEntity(item.ResultArg);
                if (entity != null)
                    code.Append($@"
#include ""{entity.Parent.Name}/{entity.Name}.h""");
            }
            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
        public static string CmdCallCodeByCs(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
                if (!string.IsNullOrWhiteSpace(type))
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
        public static string CmdCallCodeByClr(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
{{{ClrApiCall(config, item)}
}}");
            }
            return code.ToString();
        }

        private static string ClrApiCall(ProjectConfig config,ApiItem item)
        {
            if (string.IsNullOrWhiteSpace(item.ResultArg))
            {
                return $@"
		GBS::Futures::TradeCommand::Client::{item.Name}();";
            }
            if (config.Entities.Any(p => p.Name == item.ResultArg))
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

        public static string BusinessCodeByServer(ProjectConfig config)
        {
            var code = new StringBuilder();
            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
            {
                ApiBusinessHCode(item, code);
                ApiBusinessCppCode(item, code);
            }
            return code.ToString();
        }

        private static void ApiBusinessHCode(ApiItem item, StringBuilder code)
        {
            var entity = item.Argument;
            code.AppendLine($@"/*------------------------------------------------------
*** {item.Name}Business.h ***
------------------------------------------------------*/
");

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
        }

        public static void ApiBusinessCppCode(ApiItem item, StringBuilder code)
        {
            var entity = item.Argument;
            code.AppendLine($@"/*------------------------------------------------------
*** {item.Name}Business.cpp ***
------------------------------------------------------*/
");
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
        }

        #endregion

        #region 常量名称字典

        private static string TypedefDictionary(ConfigBase config)
        {
            var code = new StringBuilder();
            var item = config as TypedefItem;
            if (item != null)
            {
                TypedefToCombo(code, item);
            }
            else
            {
                foreach (var typedef in CppProject.Instance.TypedefItems.Where(p => p.Items.Count > 0))
                {
                    TypedefToCombo(code, typedef);
                }
            }
            return code.ToString();
        }

        private static void TypedefToCombo(StringBuilder code, TypedefItem typedef)
        {
            code.AppendFormat(@"
        
        /// <summary>
        /// {0}
        /// </summary>
        List<NameValue> {1}Dictionary = new List<NameValue>
        {{", typedef.Description, typedef.Name);

            bool first = true;
            foreach (var con in typedef.Items.Values)
            {
                if (first)
                    first = false;
                else
                    code.Append(',');
                code.AppendFormat(@"
                new NameValue{{value=""{0}"",name=""{1}""}}", con.Value.Trim('\''), con.Caption);
            }
            code.Append(@"
        };");
        }
        #endregion

        #region 常量选择代码

        public static string DataSwitch(ProjectConfig project)
        {
            var code = new StringBuilder();
            foreach (var enttiy in project.Entities.Where(p => !p.NoDataBase))
            {
                code.Append($@"
                case 0x{enttiy.Index:X}://{enttiy.Caption}
                    On{enttiy.Name}Sended(({enttiy.EntityName})data);
                    break;");
            }
            foreach (var enttiy in project.Entities.Where(p => !p.NoDataBase))
            {
                code.Append($@"
                void On{enttiy.Name}Sended({enttiy.EntityName} data)
                {{
                    var access = new {enttiy.Name}DataAccess();
                    using (MySqlDataBaseScope.CreateScope(access.DataBase))
                    {{
                        access.Insert(data);
                    }}
                }}");
            }
            return code.ToString();
        }
        public static string ConstToStringSwitch(TypedefItem item)
        {
            var code = new StringBuilder();

            foreach (var c in item.Items.Values)
            {
                code.Append($@"
                case {c.Value}:
                    return ""{c.Caption}"";");
            }
            return code.ToString();
        }

        public static string ConstSwitch(TypedefItem item)
        {
            var code = new StringBuilder();
            foreach (var c in item.Items.Values)
            {
                code.AppendFormat(@"
                case {1}://{0}
                    ", c.Caption, c.Name);

                var names = c.Name.Trim().Split('_');
                if (names.Length > 3)
                {
                    code.Append(@"//");
                    for (int idx = 2; idx < names.Length; idx++)
                    {
                        code.Append(names[idx].ToLower().ToUWord());
                    }
                    code.Append(@"();
                    ");
                }
                else
                {
                    code.AppendFormat(@"
                    value.{0} = data.MoneyValue;
                    ", c.Name.Trim().Replace("MONEY_CHG_", "").ToLower().ToUWord());
                }

                code.Append(@"break;");
            }
            return code.ToString();
        }

        public static string ConstFuncDef(ConfigBase config)
        {
            var code = new StringBuilder();

            var item = config as TypedefItem;
            if (item == null)
                return code.ToString();
            foreach (var c in item.Items.Values)
            {
                var names = c.Name.Trim().Split('_');
                if (names.Length <= 3)
                {
                    continue;
                }
                var c2 = new StringBuilder();
                for (int idx = 2; idx < names.Length; idx++)
                {
                    c2.Append(names[idx].ToLower().ToUWord());
                }
                code.Append($@"

/** 
* @brief {c.Caption}
* @param {{PNetCommand}} cmd 命令对象
* @return 无
*/
void {c2}(const PNetCommand cmd);");


            }
            return code.ToString();
        }

        public static string ConstFunc(ConfigBase config)
        {
            var code = new StringBuilder();

            var item = config as TypedefItem;
            if (item == null)
                return code.ToString();
            foreach (var c in item.Items.Values)
            {
                var names = c.Name.Trim().Split('_');
                if (names.Length <= 3)
                {
                    continue;
                }
                var c2 = new StringBuilder();
                for (int idx = 2; idx < names.Length; idx++)
                {
                    c2.Append(names[idx].ToLower().ToUWord());
                }
                code.Append($@"

/**
* @brief {c.Caption}完成的消息回发
* @param {{PNetCommand}} cmd 命令对象
* @param {{unsigned short}} state 执行状态
* @return 无
*/
void On{c2}(const PNetCommand cmd, unsigned short state)
{{
    PNetCommand result = new NetCommand();
	//memset(&result, 0, sizeof(NetCommandResult));
	memcpy(&result, cmd, sizeof(NetCommand));
	result->data_len = 0 - state;
	server_message_send(result);
}}

/** 
* @brief {c.Caption}
* @param {{PNetCommand}} cmd 命令对象
* @return 无
*/
void {c2}(const PNetCommand cmd)
{{
    unsigned short state = 0;
	try
	{{
		//deserialize_cmd_arg(cmd,DATA_TYPE,cmd_arg);//类型名称为cmd_arg
        //TO DO:处理方法
	}}
	catch (...)
	{{
        state = 65536;
	}}
	On{c2}(cmd, state);
}}");


            }
            return code.ToString();
        }

        public static string ConstFuncDef_C(ConfigBase config)
        {
            var code = new StringBuilder();

            var item = config as TypedefItem;
            if (item == null)
                return code.ToString();
            foreach (var c in item.Items.Values)
            {
                var names = c.Name.Trim().Split('_');
                if (names.Length <= 3)
                {
                    continue;
                }
                var c2 = new StringBuilder();
                for (int idx = 2; idx < names.Length; idx++)
                {
                    c2.Append(names[idx].ToLower().ToUWord());
                }
                code.Append($@"

/**
* @brief 发出〖{c.Caption}〗请求
* @param {{PNetCommand}} net_cmd 命令对象
* @return 无
*/
void {c2}(PNetCommand net_cmd);

/** 
* @brief {c.Caption}调用开始
* @param {{CommandArg}} arg 命令参数
* @return 无
*/
void {c2}(CommandArg& cmd);

/** 
* @brief {c.Caption}调用完成
* @param {{PNetCommand}} cmd 命令
* @return 无
*/
void On{c2}(const PNetCommand cmd);

/**
* @brief 〖{c.Caption}〗的状态处理
* @param {{COMMAND_STATE}} cmd_state 命令调用
* @return 无
*/
void On{c2}(COMMAND_STATE cmd_state);");


            }
            return code.ToString();
        }

        public static string ConstFunc_C(ConfigBase config)
        {
            var code = new StringBuilder();

            var item = config as TypedefItem;
            if (item == null)
                return code.ToString();
            foreach (var c in item.Items.Values)
            {
                var names = c.Name.Trim().Split('_');
                if (names.Length <= 3)
                {
                    continue;
                }
                var c2 = new StringBuilder();
                for (int idx = 2; idx < names.Length; idx++)
                {
                    c2.Append(names[idx].ToLower().ToUWord());
                }
                code.Append($@"

/**
* @brief 发出〖{c.Caption}〗请求
* @param {{PNetCommand}} net_cmd 命令对象
* @return 无
*/
void {c2}(PNetCommand net_cmd)
{{
	try
	{{
		request_net_cmmmand(net_cmd, On{c2});
		//TO DO:处理方法
	}}
	catch (...)
	{{
		net_cmd->cmd_state = NET_COMMAND_STATE_CLIENT_UNKNOW;
		On{c2}(net_cmd);
		delete[] net_cmd;
	}}
}}

/**
* @brief 发出〖{c.Caption}〗请求
* @param {{CommandArg}} args 命令对象
* @return 无
*/
void {c2}(CommandArg& args)
{{
    serialize_to_cmd(CommandArg, args, NET_COMMAND_USER_LOGIN);
	//参数检查
	/*if (strlen(args.user_name) == 0 || strlen(args.pass_word) == 0)
	{{
        net_cmd->cmd_state = NET_COMMAND_STATE_ARGUMENT_INVALID;
		delete[] net_cmd;
		return;
	}}*/
	try
	{{
		//TO DO:处理方法
		request_net_cmmmand(net_cmd, On{c2});
	}}
	catch (...)
	{{
		net_cmd->cmd_state = NET_COMMAND_STATE_CLIENT_UNKNOW;
		On{c2}(net_cmd);
		delete[] net_cmd;
	}}
}}

/**
* @brief 〖{c.Caption}〗的状态处理
* @param {{COMMAND_STATE}} cmd_state 命令调用
* @return 无
*/
void On{c2}(COMMAND_STATE cmd_state)
{{
	switch (cmd_state)
	{{
	case NET_COMMAND_STATE_NETERROR:
	case NET_COMMAND_STATE_SERVER_UNKNOW:
	case NET_COMMAND_STATE_CLIENT_UNKNOW:
	case NET_COMMAND_STATE_ARGUMENT_INVALID:
	{{
		//TO DO:错误处理方法
	}}
	break;
	case NET_COMMAND_STATE_SENDED:
	case NET_COMMAND_STATE_WAITING:
		//TO DO:已发送处理方法
		break;
	case NET_COMMAND_STATE_SUCCEED:
		//TO DO:成功结束处理方法
		break;
	}}
}}

/**
* @brief 〖{c.Caption}〗的回调
* @param {{PNetCommand}} cmd 命令对象
* @return 无
*/
void On{c2}(const PNetCommand cmd)
{{
	if (cmd->cmd_state <= NET_COMMAND_STATE_END)
	{{
        OnUserLogin(cmd->cmd_state);
		return;
	}}
	//TO DO:数据推送处理方法
	deserialize_cmd_arg(cmd, CommandArg, cmd_data);//类型名称为cmd_data
}}");


            }
            return code.ToString();
        }
        public static string ConstFunc_Cs(ConfigBase config)
        {
            var code = new StringBuilder();

            var item = config as TypedefItem;
            if (item == null)
                return code.ToString();
            foreach (var c in item.Items.Values)
            {
                var names = c.Name.Trim().Split('_');
                if (names.Length <= 3)
                {
                    continue;
                }
                var c2 = new StringBuilder();
                for (int idx = 2; idx < names.Length; idx++)
                {
                    c2.Append(names[idx].ToLower().ToUWord());
                }
                code.Append($@"

        /// <summary>
        /// 〖{c.Caption}〗的状态处理
        /// </summary>
        /// <param name=""e"">状态参数</param>
        void On{c2}(NetCommandEventArg e)
        {{
            if (e.get_State() > NET_COMMAND_STATE_END)
            {{
                //TO DO:数据推送处理方法
                //deserialize_cmd_arg(cmd, CommandArg, cmd_data);//类型名称为cmd_data
                return;
            }}
            switch (e.get_State())
            {{
                case NET_COMMAND_STATE_UNKNOW:
                case NET_COMMAND_STATE_NETERROR:
                case NET_COMMAND_STATE_SERVER_UNKNOW:
                case NET_COMMAND_STATE_CLIENT_UNKNOW:
                case NET_COMMAND_STATE_ARGUMENT_INVALID:
                    {{
                        //TO DO:错误处理方法
                    }}
                    break;
                case NET_COMMAND_STATE_SENDED:
                case NET_COMMAND_STATE_WAITING:
                    //TO DO:已发送处理方法
                    break;
                case NET_COMMAND_STATE_SUCCEED:
                    //TO DO:已成功处理方法
                    break;
            }}
        }}");


            }
            return code.ToString();
        }
        #endregion
    }
}