using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class NotifyMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {

            MomentCoder.RegisteCoder("期货相关", "消息通知实现代码(C++)-Proxy", (NotifyCodeByProxy));
            MomentCoder.RegisteCoder("期货相关", "数据发布代码(C++)-Proxy", (EsPublishCode));
            MomentCoder.RegisteCoder("期货相关", "消息通知逻辑模板(C++)-Server", (BusinessCodeByServer));
            MomentCoder.RegisteCoder("期货相关", "消息通知调用定义代码(C++)-Server", (CmdCallCodeByServerDef));
            MomentCoder.RegisteCoder("期货相关", "消息通知调用实现代码(C++)-Server", (CmdCallCodeByServer));
        }
        #endregion

        #region 原始调用端

        public static string NotifyCodeByProxy(ConfigBase config)
        {
            var code = new StringBuilder();

            foreach (var item in SolutionConfig.Current.NotifyItems.Where(p => !p.Discard))
            {
                code.Append(item.Org
                    .TrimEnd('　', ' ', '\t', '\n', '\r', '{', '}', '=', '0', ';')
                    .Replace(item.Name, $"EsunnyTrader::{item.Name}")
                    .Replace("virtual", "")
                    .Replace("* rsp,", "* es_field,")
                    .Replace("rsp)", "es_field)"));
                if (!item.IsCommandResult)
                {
                    code.Append($@"
{{
    log_debug(DEBUG_REQUEST, 1,""{item.Caption}通知: %s"", to_log_text(&es_field).c_str());
    save_to_redis(&es_field);
    {item.ClientEntity} arg;
    CopyFromEs(&es_field,&arg);
    PNetCommand lc_cmd_arg;
    lc_cmd_arg << arg;
    memset(lc_cmd_arg, 0, NETCOMMAND_HEAD_LEN);
    server_message_send(lc_cmd_arg);
}}");
                    continue;
                }
                code.Append($@"
{{
    m_last_error = errCode;
	auto es_cmd_arg = get_req_cmd(iReqID);
    if (errCode != 0)
    {{
        std::string msg;
        es_cmd_arg->cmd_state = checkIsError(errCode,""{item.Caption}"",msg);
        log_debug(DEBUG_REQUEST, 1,""{item.Caption}返回出错:用户%s,命令%s错误号%d 错误消息%s""
            , es_cmd_arg->user_token
            , es_cmd_arg->cmd_identity
            , es_cmd_arg->cmd_state
            , msg);

        server_message_send(es_cmd_arg);
    }}");
                if (item.IsMulit)
                    code.Append(@"
    else if (islast)
    {
        log_debug(DEBUG_REQUEST, 1,""{item.Caption}返回结束:用户%s,命令%s""
            , es_cmd_arg->user_token
            , es_cmd_arg->cmd_identity);

        es_cmd_arg->cmd_state = 0;
        server_message_send(es_cmd_arg);
    }");

                code.Append($@"
    else
    {{
        log_debug(DEBUG_REQUEST, 1,""{item.Caption}返回数据:用户%s,命令%s数据%s""
            , es_cmd_arg->user_token
            , es_cmd_arg->cmd_identity
            , to_log_text(es_field).c_str());

		save_to_redis(es_field);
        {item.ClientEntity} arg;
        CopyFromEs(es_field,&arg);
        PNetCommand lc_cmd_arg = new NetCommand();
        lc_cmd_arg << arg;
        memcpy(lc_cmd_arg, es_cmd_arg, NETCOMMAND_HEAD_LEN);
        server_message_send(lc_cmd_arg);
    }}
}}

");
            }
            return code.ToString();
        }
        #endregion


        #region 数据发布代码

        private static string EsPublishCode(ConfigBase config)
        {
            Dictionary<EntityConfig, NotifyItem> dictionary = new Dictionary<EntityConfig, NotifyItem>();
            foreach (var item in SolutionConfig.Current.NotifyItems.Where(p => !p.Discard))
                if (!dictionary.ContainsKey(item.EsEntity))
                    dictionary.Add(item.EsEntity, item);

            var code = new StringBuilder();
            code.Append(@"#ifndef _PUBLISHESDATA_H
#define _PUBLISHESDATA_H
#pragma once
#include <NetCommand/command_serve.h>
#include ""EsPrint.h""
#include ""EsJson.h""
#include ""EsRedis.h""");
            foreach (var item in dictionary.Values)
            {
                code.Append($@"
#include ""{item.EsEntity.Parent.Name}/{item.EsEntity.Name}.h""
#include ""{item.LocalEntity.Parent.Name}/{item.LocalEntity.Name}.h""");
            }
            using (var scope = CppNameSpaceScope.CreateScope(code, "GBS::Futures::Manage"))
            {
                foreach (var item in dictionary.Values)
                {
                    scope.Append($@"

/**
* @brief 发布〖{item.EsEntity.Caption}〗数据通知
* @param {{{item.EsEntity.Name}*}} value {item.EsEntity.Caption}
* @return 无
*/
inline void publishEsData(const char* user,const {item.EsEntity.Name}* value)
{{
	if (value == nullptr)
		return;
    print_screen(value);
    char key[1024];
    sprintf(key, ""data:es:{item.EsEntity.Name}:%d"", incr(""data:es: {item.EsEntity.Name}""));
    saveToRedis(key , value);");
                    //if (friend == null)
                    //    scope.Append("/*");
                    scope.Append($@"
	{item.LocalEntity.Name} lc_field;
	CopyFromEs(value, &lc_field);
	PNetCommand cmd_arg;
	cmd_arg << lc_field;
	server_message_send(cmd_arg);");

                    //if (friend == null)
                    //    scope.Append("*/");
                    scope.Append(@"
}");
                }

            }
            code.AppendLine();
            code.Append("#endif");
            return code.ToString();
        }
        #endregion


        #region 服务端业务逻辑模板

        public static string BusinessCodeByServer(ConfigBase config)
        {
            foreach (var item in SolutionConfig.Current.NotifyItems.Where(p => !p.Discard))
            {
                NotifyBusinessHCode(item);
                NotifyBusinessCppCode(item);
            }
            return "请打开文件查看";
        }

        private static void NotifyBusinessHCode(NotifyItem item)
        {
            var entity = item.LocalEntity;
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
            FileCoder.WriteFile($@"c:\work\SharpCode\GbsNotify\{item.Name}Business.h", code.ToString());
        }

        private static void NotifyBusinessCppCode(NotifyItem item)
        {
            var entity = item.LocalEntity;
            StringBuilder code = new StringBuilder();
            code.Append($@"#include ""stdafx.h""
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
            * @brief 执行动作(默认实现原样发到交易端)
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
                server_message_send(es_cmd_arg);
                return NET_COMMAND_STATE_SUCCEED;
            }
        }
    }
}");
            FileCoder.WriteFile($@"c:\work\SharpCode\GbsNotify\{item.Name}Business.cpp", code.ToString());
        }

        #endregion


        #region 服务端调用

        public static string CmdCallCodeByServerDef(ConfigBase config)
        {
            Dictionary<EntityConfig, NotifyItem> dictionary = new Dictionary<EntityConfig, NotifyItem>();
            foreach (var item in SolutionConfig.Current.NotifyItems.Where(p => !p.Discard))
                if (!dictionary.ContainsKey(item.LocalEntity))
                    dictionary.Add(item.LocalEntity, item);
            var code = new StringBuilder();
            foreach (var item in dictionary.Values)
            {
                code.Append($@"
#include ""GbsNotify/{item.Name}Business.h""");
            }

            foreach (var item in dictionary.Values)
            {
                code.Append($@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd_arg 命令对象
* @return 无
*/
void {item.Name}(const PNetCommand cmd_arg);");

            }
            return code.ToString();
        }
        public static string CmdCallCodeByServer(ConfigBase config)
        {
            Dictionary<EntityConfig, NotifyItem> dictionary = new Dictionary<EntityConfig, NotifyItem>();
            foreach (var item in SolutionConfig.Current.NotifyItems.Where(p => !p.Discard))
                if (!dictionary.ContainsKey(item.LocalEntity))
                    dictionary.Add(item.LocalEntity, item);
            var code = new StringBuilder();
            code.AppendLine(CmdPumpCodeByServer(dictionary));
            foreach (var item in dictionary.Values)
            {
                code.Append(CmdCallCodeByServer(item));
            }
            return code.ToString();
        }
        public static string CmdPumpCodeByServer(Dictionary<EntityConfig, NotifyItem> dictionary)
        {
            var code = new StringBuilder();
            code.Append(@"/**
			* @brief 消息泵
			* @param {PNetCommand} cmd_call 命令对象
			*/
			void GbsNotify::message_pump(PNetCommand cmd_call)
			{
            	Deserializer reader(get_cmd_buffer(cmd_call), static_cast<size_t>(get_cmd_len(cmd_call)));
				switch (reader.GetDataType())
				{");
            foreach (var item in dictionary)
            {
                code.Append($@"
				case TYPE_INDEX_{item.Key.Name.ToUpper()}://{item.Key.Caption}
					{item.Value.Name}(cmd_call);
					break;");
            }
            code.Append(@"
				}
				delete cmd_call;
			}");
            return code.ToString();
        }

        public static string CmdCallCodeByServer(NotifyItem item)
        {
            return $@"
/** 
* @brief {item.Caption}
* @param {{PNetCommand}} cmd_arg 命令对象
* @return 无
*/
void GbsNotify::{item.Name}(const PNetCommand cmd_arg)
{{
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
    }
}