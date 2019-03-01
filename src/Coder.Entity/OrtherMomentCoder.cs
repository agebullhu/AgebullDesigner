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
        #region ע��

        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("�������ʹ������(C#)_�����", (DataSwitch));
            MomentCoder.RegisteCoder("-", null);
            MomentCoder.RegisteCoder("����ʵ��(C++)_�����", (ConstFunc));
            MomentCoder.RegisteCoder("��������(C++)_�����", (ConstFuncDef));
            MomentCoder.RegisteCoder("����ʵ��(C++)_�ͻ���", (ConstFunc_C));
            MomentCoder.RegisteCoder("��������(C++)_�ͻ���", (ConstFuncDef_C));
            MomentCoder.RegisteCoder("-", null);
            MomentCoder.RegisteCoder("����ѡ�����(C++)", (ConstSwitch));
            MomentCoder.RegisteCoder("���������ֵ�(C#)", (TypedefDictionary));
            MomentCoder.RegisteCoder("�¼�������(C#)", (ConstFunc_Cs));
            MomentCoder.RegisteCoder("�������ı�(C++)", (ConstToStringSwitch));
        }


        #endregion

        #region ����ѡ�����

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