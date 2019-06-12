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
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API��־����(C++)", "cpp", ApiLog);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "APIѡ��ִ�д���(C++)", "cpp", ApiSwitch);
            
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API��������(C++)", "cpp", ApiCommandIdCode);
            
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API���ö������(C++)-Proxy", "cpp", ApiCallCodeByProxyDef);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API����ʵ�ִ���(C++)-Proxy", "cpp", ApiCallCodeByProxy);
            
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API���ö������(C++)-Server", "cpp", CmdCallCodeByServerDef);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API����ʵ�ִ���(C++)-Server", "cpp", CmdCallCodeByServer);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API�����߼�ģ��(C++)-Server", "cpp", BusinessCodeByServer);
            
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API���ö������(C++)-Client", "cpp", CmdCallCodeByClientDef);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API����ʵ�ִ���(C++)-Client", "cpp", CmdCallCodeByClient);
            
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API���ö������(C++)-Clr", "cpp", CmdCallCodeByClrDef);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API����ʵ�ִ���(C++)-Clr", "cpp", CmdCallCodeByClr);
            
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "API���ô���(C#)", "cs", CmdCallCodeByCs);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "���������ֵ�(C#)", "cs", TypedefDictionary);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "�������ʹ������(C#)_�����", "cs", DataSwitch);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "����ʵ��(C++)_�����", "cpp", ConstFunc);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "��������(C++)_�����", "cpp", ConstFuncDef);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "����ʵ��(C++)_�ͻ���", "cpp", ConstFunc_C);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "��������(C++)_�ͻ���", "cpp", ConstFuncDef_C);
            MomentCoder.RegisteCoder<TypedefItem>("�ڻ����", "����ѡ�����(C++)", "cpp", ConstSwitch);
            MomentCoder.RegisteCoder<ProjectConfig>("�ڻ����", "�¼�������(C#)", "cs", ConstFunc_Cs);
            MomentCoder.RegisteCoder<TypedefItem>("�ڻ����", "�������ı�(C++)", "cpp", ConstToStringSwitch);
        }
        #endregion

        #region ����

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
                 log_debug(""{item.Caption}:�û�[%s]"",cmd_call->user_token);
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
        #region �ͻ��˵���

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
* @return ��
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
        #region ����˵���

        public static string CmdCallCodeByServerDef(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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

        public static string ApiCallCodeByProxyDef(ProjectConfig config)
        {
            var code = new StringBuilder();

            foreach (var item in config.ApiItems.Where(p => !p.IsDiscard))
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
        #region CLR�˵���

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
* @return ��
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
        #region �����ҵ���߼�ģ��

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
        }

        #endregion

        #region ���������ֵ�

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

        #region ����ѡ�����

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
* @param {{PNetCommand}} cmd �������
* @return ��
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
* @brief {c.Caption}��ɵ���Ϣ�ط�
* @param {{PNetCommand}} cmd �������
* @param {{unsigned short}} state ִ��״̬
* @return ��
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
* @param {{PNetCommand}} cmd �������
* @return ��
*/
void {c2}(const PNetCommand cmd)
{{
    unsigned short state = 0;
	try
	{{
		//deserialize_cmd_arg(cmd,DATA_TYPE,cmd_arg);//��������Ϊcmd_arg
        //TO DO:������
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
* @brief ������{c.Caption}������
* @param {{PNetCommand}} net_cmd �������
* @return ��
*/
void {c2}(PNetCommand net_cmd);

/** 
* @brief {c.Caption}���ÿ�ʼ
* @param {{CommandArg}} arg �������
* @return ��
*/
void {c2}(CommandArg& cmd);

/** 
* @brief {c.Caption}�������
* @param {{PNetCommand}} cmd ����
* @return ��
*/
void On{c2}(const PNetCommand cmd);

/**
* @brief ��{c.Caption}����״̬����
* @param {{COMMAND_STATE}} cmd_state �������
* @return ��
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
* @brief ������{c.Caption}������
* @param {{PNetCommand}} net_cmd �������
* @return ��
*/
void {c2}(PNetCommand net_cmd)
{{
	try
	{{
		request_net_cmmmand(net_cmd, On{c2});
		//TO DO:������
	}}
	catch (...)
	{{
		net_cmd->cmd_state = NET_COMMAND_STATE_CLIENT_UNKNOW;
		On{c2}(net_cmd);
		delete[] net_cmd;
	}}
}}

/**
* @brief ������{c.Caption}������
* @param {{CommandArg}} args �������
* @return ��
*/
void {c2}(CommandArg& args)
{{
    serialize_to_cmd(CommandArg, args, NET_COMMAND_USER_LOGIN);
	//�������
	/*if (strlen(args.user_name) == 0 || strlen(args.pass_word) == 0)
	{{
        net_cmd->cmd_state = NET_COMMAND_STATE_ARGUMENT_INVALID;
		delete[] net_cmd;
		return;
	}}*/
	try
	{{
		//TO DO:������
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
* @brief ��{c.Caption}����״̬����
* @param {{COMMAND_STATE}} cmd_state �������
* @return ��
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
		//TO DO:��������
	}}
	break;
	case NET_COMMAND_STATE_SENDED:
	case NET_COMMAND_STATE_WAITING:
		//TO DO:�ѷ��ʹ�����
		break;
	case NET_COMMAND_STATE_SUCCEED:
		//TO DO:�ɹ�����������
		break;
	}}
}}

/**
* @brief ��{c.Caption}���Ļص�
* @param {{PNetCommand}} cmd �������
* @return ��
*/
void On{c2}(const PNetCommand cmd)
{{
	if (cmd->cmd_state <= NET_COMMAND_STATE_END)
	{{
        OnUserLogin(cmd->cmd_state);
		return;
	}}
	//TO DO:�������ʹ�����
	deserialize_cmd_arg(cmd, CommandArg, cmd_data);//��������Ϊcmd_data
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
        /// ��{c.Caption}����״̬����
        /// </summary>
        /// <param name=""e"">״̬����</param>
        void On{c2}(NetCommandEventArg e)
        {{
            if (e.get_State() > NET_COMMAND_STATE_END)
            {{
                //TO DO:�������ʹ�����
                //deserialize_cmd_arg(cmd, CommandArg, cmd_data);//��������Ϊcmd_data
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
                        //TO DO:��������
                    }}
                    break;
                case NET_COMMAND_STATE_SENDED:
                case NET_COMMAND_STATE_WAITING:
                    //TO DO:�ѷ��ʹ�����
                    break;
                case NET_COMMAND_STATE_SUCCEED:
                    //TO DO:�ѳɹ�������
                    break;
            }}
        }}");


            }
            return code.ToString();
        }
        #endregion
    }
}