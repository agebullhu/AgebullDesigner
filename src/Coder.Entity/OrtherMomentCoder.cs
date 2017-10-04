using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class OrtherMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("数据类型处理代码(C#)_服务端", (DataSwitch));
            MomentCoder.RegisteCoder("-", null);
            MomentCoder.RegisteCoder("方法实现(C++)_服务端", (ConstFunc));
            MomentCoder.RegisteCoder("方法声明(C++)_服务端", (ConstFuncDef));
            MomentCoder.RegisteCoder("方法实现(C++)_客户端", (ConstFunc_C));
            MomentCoder.RegisteCoder("方法声明(C++)_客户端", (ConstFuncDef_C));
            MomentCoder.RegisteCoder("-", null);
            MomentCoder.RegisteCoder("常量选择代码(C++)", (ConstSwitch));
            MomentCoder.RegisteCoder("常量名称字典(C#)", (TypedefDictionary));
            MomentCoder.RegisteCoder("事件处理方法(C#)", (ConstFunc_Cs));
            MomentCoder.RegisteCoder("常量到文本(C++)", (ConstToStringSwitch));
        }


        #endregion

        #region 常量选择代码

        public static string DataSwitch(ConfigBase config)
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var enttiy in project.Entities.Where(p => !p.IsClass))
                {
                    code.Append($@"
                case 0x{enttiy.Index:X}://{enttiy.Caption}
                    On{enttiy.Name}Sended(({enttiy.EntityName})data);
                    break;");
                }
            }
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var enttiy in project.Entities.Where(p => !p.IsClass))
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
            }
            return code.ToString();
        }
        public static string ConstToStringSwitch(ConfigBase config)
        {
            var code = new StringBuilder();

            var item = config as TypedefItem;
            if (item != null)
            {
                foreach (var c in item.Items.Values)
                {
                    code.Append($@"
                case {c.Value}:
                    return ""{c.Caption}"";");
                }
            }
            return code.ToString();
        }

        public static string ConstSwitch(ConfigBase config)
        {
            var code = new StringBuilder();

            var item = config as TypedefItem;
            if (item != null)
            {
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
                foreach (var typedef in SolutionConfig.Current.TypedefItems.Where(p => p.Items.Count > 0))
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
    }
}