using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.Funds
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class CppMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("C++", "数据通知转发代码(C++)", "cpp", CommandCode);
            MomentCoder.RegisteCoder("C++", "Form(WPF)", "cpp", WpfForm);
            MomentCoder.RegisteCoder("C++", "DataGrid(WPF)", "cpp", WpfDataGrid);
            MomentCoder.RegisteCoder("C++", "价格变化属性(WPF)", "xml", WpfColor);
            MomentCoder.RegisteCoder("C++", "C++Json声明", "cpp", ToCppJsonDef);
            MomentCoder.RegisteCoder("C++", "C++Json实现", "cpp", ToCppJson);
            MomentCoder.RegisteCoder("C++", "C++Cout声明", "cpp", ToCppCoutDef);
            MomentCoder.RegisteCoder("C++", "C++Cout实现", "cpp", ToCppCout);
            MomentCoder.RegisteCoder("C++", "Redis读写(C++)", "cpp", Redis);
            MomentCoder.RegisteCoder("C++", "C++Redis实现", "cpp", ToCppRedis);
            MomentCoder.RegisteCoder("C++", "C++到C#声明", "cpp", CppFieldToCsEntityDef);
            MomentCoder.RegisteCoder("C++", "C++到C#实现", "cpp", CppFieldToCsEntity);
            MomentCoder.RegisteCoder("C++", "C++序列化", "cpp", CppFieldSaveDef);
            MomentCoder.RegisteCoder("C++", "C++反序列化", "cpp", CppFieldSave);
            MomentCoder.RegisteCoder("C++", "数据类型(C++)", "cpp", DataType);


            MomentCoder.RegisteCoder("C++", "C++日志声明", "cpp", ToCppLogDef);
            MomentCoder.RegisteCoder("C++", "C++日志实现", "cpp", ToCppLog);
            MomentCoder.RegisteCoder("C++", "C++枚举日志", "cpp", EnumLog);


            MomentCoder.RegisteCoder<ProjectConfig>("C++", "文件包含(C++)", "cpp", IncludeFunc);
            MomentCoder.RegisteCoder<ProjectConfig>("C++", "工程文件包含(C++)", "cpp", ProjectFile);
            MomentCoder.RegisteCoder<ProjectConfig>("C++", "工程文件分组(C++)", "cpp", ProjectFilter);

        }
        #endregion

        #region 数据通知代码

        private static string CommandCode(EntityConfig entity)
        {
            return ($@"
                case {entity.Parent.NameSpace.Replace(".", "::")}::TYPE_INDEX_{entity.Name.ToUpper()}:
				{{
					{entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name} field;
					memset(&field, 0 , sizeof({entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name}));
					Deserialize(reader, &field);
					auto token = get_token_by_cusid(field.CustomerId);
					if (token.empty())
						return;
                    PNetCommand cmd;
					field >> cmd;
					memcpy(cmd, cmd_arg, NETCOMMAND_HEAD_LEN);
					strcpy_s(cmd->user_token, token.c_str());
					server_message_send(cmd);
                    return;
				}}");
        }

        #endregion

        #region WPF

        private static string WpfColor(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            foreach (var field in entityConfig.LastProperties.Where(p => p.CsType == "decimal"))
            {
                code.Append($@"
        /// <summary>
        /// {field.Caption}变化时同步颜色
        /// </summary>
        partial void On{field.Name}Set(ref decimal value)
        {{
            if (value == QOpeningPrice)
                {field.Name}Brush = Brushes.White;
            else if (value > QOpeningPrice)
                {field.Name}Brush = Brushes.Red;
            else
                {field.Name}Brush = Brushes.Green;
        }}
        /// <summary>
        /// {field.Caption}颜色
        /// </summary>
        private Brush _{field.Name}Brush;
        /// <summary>
        /// {field.Caption}颜色
        /// </summary>
        public Brush {field.Name}Brush
        {{
            get {{ return _{field.Name}Brush; }}
            set
            {{
                if (Equals(_{field.Name}Brush, value))
                    return;
                _{field.Name}Brush = value;
                OnPropertyChanged(nameof({field.Name}Brush));
            }}
        }}");
            }
            return code.ToString();

        }

        private static string WpfDataGrid(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            foreach (var field in entityConfig.LastProperties)
            {
                if (field.CsType == "bool")
                {
                    code.Append($@"
<DataGridCheckBoxColumn Header =""{field.Caption}"" Binding=""{{Binding {field.Name}, Mode=TwoWay , UpdateSourceTrigger=PropertyChanged}}""/>");
                }
                else if (field.CustomType != null)
                {
                    code.Append($@"
<DataGridTextColumn Header =""{field.Caption}"" Binding=""{{Binding {field.Name}, Mode=TwoWay , UpdateSourceTrigger=PropertyChanged}}""/>");
                }
                else if (field.CsType == "string")
                {
                    code.Append($@"
<DataGridTextColumn Header =""{field.Caption}"" Binding=""{{Binding {field.Name}, Mode=TwoWay , UpdateSourceTrigger=PropertyChanged}}""/>");
                }
                else
                {
                    code.Append($@"
<DataGridTextColumn Header =""{field.Caption}"" Binding=""{{Binding {field.Name}, Mode=TwoWay,UpdateSourceTrigger=PropertyChanged,StringFormat=F2}}""/>");
                }
            }
            return code.ToString();

        }

        private static string WpfInputForm(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            foreach (var field in entityConfig.LastProperties)
            {
                if (CppTypeHelper.ToCppLastType(field.CppLastType) is TypedefItem typedef && typedef.Items.Count > 0)
                {
                    code.AppendFormat(@"
                            <StackPanel VerticalAlignment=""Center"" Orientation =""Horizontal"" Width=""200px"">
                                <TextBlock VerticalAlignment = ""Center""  Width =""80px"">{0}:</TextBlock>
                                <ComboBox VerticalAlignment = ""Center"" Width =""120px"" 
                                          SelectedValue = ""{{Binding Order.{1}}}""
                                          ItemsSource = ""{{Binding {2}Dictionary}}""
                                          DisplayMemberPath = ""name"" SelectedValuePath = ""value"" />
                            </StackPanel>", field.Caption, field.Name, typedef.Name);
                }
                else
                {
                    code.AppendFormat(@"
                            <StackPanel VerticalAlignment=""Center"" Orientation =""Horizontal"" Width=""200px"">
                                <TextBlock VerticalAlignment = ""Center""  Width =""80px"">{0}:</TextBlock>
                                <TextBox VerticalAlignment = ""Center""  Width =""120px"" Text=""{{Binding Order.{1}}}""/>
                            </StackPanel>", field.Caption, field.Name);
                }
            }
            return code.ToString();

        }

        private static string WpfForm(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            foreach (var field in entityConfig.LastProperties)
            {
                //var typedef = CppTypeHelper.ToCppLastType(field.CppType) as TypedefItem;
                //if (typedef == null || (typedef.KeyWork == "char" && typedef.ArrayLen != null))
                //    code.AppendFormat(@"
                //++begin;
                //strcpy_s(field.{0},begin->c_str());//{1}", field.Name, field.Caption);
                //else if (typedef.KeyWork == "char")
                //    code.AppendFormat(@"
                //++begin;
                //field.{0} = begin->c_str()[0];//{1}", field.Name, field.Caption);
                //else
                //    code.AppendFormat(@"
                //++begin;
                //field.{0}= lexical_cast<{1}>(begin->c_str());//{1}", field.Name, typedef.KeyWork);
                code.AppendFormat(@"
                            <StackPanel VerticalAlignment=""Center"" Orientation =""Horizontal"" Width=""200px"">
                                <TextBlock VerticalAlignment = ""Center"" Width =""80px"">{0}:</TextBlock>
                                <Border VerticalAlignment = ""Center""  BorderThickness=""0,0,0,1"">
                                    <TextBlock Text="" {{Binding {1}}}""></TextBlock>
                                </Border>
                            </StackPanel>", field.Caption, field.Name);
            }

            return code.ToString();

        }
        #endregion

        #region 数据类型


        public static string DataType(EntityConfig entity)
        {
            return ($@"
    case 0x{entity.Index:X}:
        return ""{entity.Caption}"";");
        }

        #endregion

        #region redis读写

        private static string Redis(EntityConfig entity)
        {
            var code = new StringBuilder();
            if (entity.IsReference)
                code.Append(@"
#ifdef PROXY");
            code.Append(RedisRW(entity));
            if (entity.IsReference)
                code.Append(@"
#endif");
            return code.ToString();
        }

        private static string RedisRW(EntityConfig entity)
        {
            var name = ToWords(entity.Name).LinkToString('_');
            var point = $"{entity.Parent.Abbreviation}:{entity.Abbreviation}";
            if (entity.RedisKey != null)
            {
                var field = entity.LastProperties.FirstOrDefault(p => p.Name == entity.RedisKey);
                if (field != null)
                    return RedisKey(entity, name, point);
            }

            var code = new StringBuilder();
            if (entity.PrimaryColumn == null)
            {
                return $@"
/**
* @brief {entity.Name}保存到redis
* @param {{{entity.Name}}} field {entity.Caption}
* @return ID
*/
inline int save_to_redis(const {entity.Name}* field)
{{
    size_t len = 0;
    char* buffer = Serialize(field, len);
    int id = static_cast<int>(incr_redis(""i:{point}""));
    write_to_redis(buffer, len, ""e:{point}:%d"", id);
    return id;
}}";
            }
            code.Append($@"
/**
* @brief 生成一个新的{entity.PrimaryColumn.Caption}
* @return 新的{entity.PrimaryColumn.Caption}
*/
inline int generate_{name}_id()
{{
    return static_cast<int>(incr_redis(""i:{point}""));
}}");
            var ufield = entity.LastProperties.FirstOrDefault(p => p.IsUserId);
            if (ufield == null)
            {
                code.Append($@"
/**
* @brief 从redis读取{entity.Name}
* @param {{int}} id {entity.PrimaryColumn.Caption}
* @return {entity.Name}
*/
inline shared_ptr<{entity.Name}> load_{name}(int id)
{{
    acl::string vl = read_from_redis(""e:{point}:%d"", id);
    char* buf = static_cast<char*>(vl.buf());
    size_t len = static_cast<char*>(vl.buf_end()) - buf;
    if (len <= 0)
        return nullptr;
    {entity.Name}* field = new {entity.Name}();
    Deserialize(buf, len, field);
    return shared_ptr<{entity.Name}>(field);
}}
/**
* @brief {entity.Name}保存到redis
* @param {{{entity.Name}}} field {entity.Caption}
* @return ID
*/
inline int save_to_redis(const {entity.Name}* field)
{{
    size_t len = 0;
    char* buffer = Serialize(field, len);
    int id = field->{entity.PrimaryColumn.Name} == 0 ? generate_{name}_id() : field->{entity.PrimaryColumn.Name};
    write_to_redis(buffer, len, ""e:{point}:%d"", id);
    return id;
}}");
            }
            else
            {
                code.Append($@"
/**
* @brief 从redis读取{entity.Name}
* @param {{int}} id {entity.PrimaryColumn.Caption}
* @param {{int}} uid 用户ID
* @return {entity.Name}
*/
inline shared_ptr<{entity.Name}> load_{name}(int id, int uid)
{{
    acl::string vl = read_from_redis(""e:{point}:%d:%d"", uid, id);
    char* buf = static_cast<char*>(vl.buf());
    size_t len = static_cast<char*>(vl.buf_end()) - buf;
    if (len <= 0)
        return nullptr;
    {entity.Name}* field = new {entity.Name}();
    Deserialize(buf, len, field);
    return shared_ptr<{entity.Name}>(field);
}}
/**
* @brief {entity.Name}保存到redis
* @param {{{entity.Name}}} field {entity.Caption}
* @return ID
*/
inline int save_to_redis({entity.Name}* field)
{{
    size_t len = 0;
    char* buffer = Serialize(field, len);
    if(field->{entity.PrimaryColumn.Name} <= 0)
        field->{entity.PrimaryColumn.Name} = generate_{name}_id();
    write_to_redis(buffer, len, ""e:{point}:%d:%d"", field->{ufield.Name}, field->{entity.PrimaryColumn.Name});
    return field->{entity.PrimaryColumn.Name};
}}");
            }
            return code.ToString();
        }

        private static string RedisKey(EntityConfig entity, string name, string point)
        {
            var field = entity.LastProperties.FirstOrDefault(p => p.Name == entity.RedisKey);
            if (field == null)
                return null;
            var code = new StringBuilder();
            var ufield = entity.LastProperties.FirstOrDefault(p => p.IsUserId);
            if (ufield == null)
            {
                code.Append($@"
/**
* @brief 从redis读取{entity.Name}
* @param {{int}} id {field.Caption}
* @return {entity.Name}
*/
inline shared_ptr<{entity.Name}> load_{name}({field.CppLastType} {field.Name.ToLWord()})
{{
    acl::string vl = read_from_redis(""e:{point}:%s"", {field.Name.ToLWord()});
    char* buf = static_cast<char*>(vl.buf());
    size_t len = static_cast<char*>(vl.buf_end()) - buf;
    if (len <= 0)
        return nullptr;
    {entity.Name}* field = new {entity.Name}();
    Deserialize(buf, len, field);
    return shared_ptr<{entity.Name}>(field);
}}
/**
* @brief {entity.Name}保存到redis
* @param {{{entity.Name}}} field {entity.Caption}
* @return ID
*/
inline void save_to_redis(const {entity.Name}* field)
{{
    size_t len = 0;
    char* buffer = Serialize(field, len);
    write_to_redis(buffer, len, ""e:{point}:%s"", field->{field.Name});
}}");
            }
            else
            {
                code.Append($@"
/**
* @brief 从redis读取{entity.Name}
* @param {{int}} id {field.Caption}
* @param {{int}} uid 用户ID
* @return {entity.Name}
*/
inline shared_ptr<{entity.Name}> load_{name}(int uid,{field.CppLastType} {field.Name.ToLWord()})
{{
    acl::string vl = read_from_redis(""e:{point}:%d:%s"", uid, {field.Name.ToLWord()});
    char* buf = static_cast<char*>(vl.buf());
    size_t len = static_cast<char*>(vl.buf_end()) - buf;
    if (len <= 0)
        return nullptr;
    {entity.Name}* field = new {entity.Name}();
    Deserialize(buf, len, field);
    return shared_ptr<{entity.Name}>(field);
}}
/**
* @brief {entity.Name}保存到redis
* @param {{{entity.Name}}} field {entity.Caption}
* @return ID
*/
inline int save_to_redis({entity.Name}* field)
{{
    size_t len = 0;
    char* buffer = Serialize(field, len);
    write_to_redis(buffer, len, ""e:{point}:%d:%s"", field->{ufield.Name}, field->{field.Name});
    return field->{field.Name};
}}");
            }
            return code.ToString();
        }
        #endregion
        #region 二进制序列化
        /*
         
void Serialize(Serializer& io, TEsAddressField* field)
{
	io.CreateBuffer(sizeof(TEsAddressField) * 2);
	io.Begin(1, 1);
	//IP地址
	io.Write('\1');//索引
	io.Write(field->Ip);//索引
	//端口号
	io.Write('\2');//索引
	io.Write(field->Port);//索引
	io.End();
}

void Deserialize(Deserializer& io, TEsAddressField* field)
{
	io.Begin();
	strcpy(field->Ip, io.ReadString());
	field->Port = io.ReadUInt16();
	io.End();
}
*/

        public static string CppFieldSaveDef(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.AppendFormat(@"

/*******************{0}字段序列化顺序定义(不可更改)*******************/"
                , entityConfig.Caption);
            foreach (var field in entityConfig.LastProperties)
            {
                code.AppendFormat(@"
const FIELD_INDEX IDX_{1}_{2} = {3};// {0}", field.Caption, entityConfig.ReadTableName, field.Name, field.Index);
            }
            code.AppendFormat(@"
// {0} 保存到类型二进制
void Serialize(Serializer& wtiter, {1}* field);
// {0} 从类型二进制中还原
void Deserialize(Deserializer& reader, {1}* field);", entityConfig.Caption, entityConfig.ReadTableName);
            return code.ToString();
        }

        public static string CppFieldSave(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.AppendFormat(@"

///{0} 以TSON方式序列化
void Serialize(Serializer& wtiter, {1}* field)
{{
	wtiter.CreateBuffer(sizeof({1}) * 2);
	wtiter.Begin(1, 1);
    if(field == nullptr)
    {{
        wtiter.End();
        return;
    }}"
                , entityConfig.Caption, entityConfig.ReadTableName);
            foreach (var field in entityConfig.LastProperties)
            {
                code.AppendFormat(@"
    wtiter.WriteIndex(IDX_{0}_{1});//{2}", entityConfig.ReadTableName, field.Name, field.Caption);
                ToCppWriteCode(code, field);
            }
            code.AppendFormat(@"
    wtiter.End();
}}

///{0} 以TSON方式反序列化
void Deserialize(Deserializer& reader, {1}* field)
{{
	reader.Begin();
	
	while(!reader.IsEof())
	{{
		FIELD_INDEX idx = reader.ReadByte();
        //OBJ_TYPE type = reader.ReadByte();
		switch(idx)
		{{", entityConfig.Caption, entityConfig.ReadTableName);
            foreach (var t in entityConfig.LastProperties)
            {
                ToCppReadCode(code, t);
            }
            code.Append(@"
        }
    }
    reader.End();
}");

            return code.ToString();
        }

        private static void ToCppWriteCode(StringBuilder code, IFieldConfig field)
        {
            var type = CppTypeHelper.ToCppLastType(field.CppLastType);
            if (type is EntityConfig stru)
            {
                if (field.Datalen == 1)
                {
                    code.Append($@"
    {{
        Serializer serializer;
        Serialize(serializer,&field->{field.Name}[0]);
        wtiter.WriteObject(serializer);
    }}");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
		case IDX_{field.Entity.Name.ToUpper()}_{field.Name.ToUpper()}://{field.Caption}
        {{
            wtiter.WriteType(OBJ_TYPE_OBJECT_ARRAY);
            wtiter.Write({field.Datalen});
            for(int idx = 0;i < {field.Datalen};i++)
            {{
                Serializer serializer;
                Serialize(serializer,&field->{field.Name}[idx]);
                wtiter.Write(serializer);
            }}
        }}
            break;");
                }
                else
                {
                    code.Append($@"
    {{
        Serializer serializer;
        Serialize(serializer,&field->{field.Name});
        wtiter.WriteObject(serializer);
    }}");
                }
                return;
            }

            var typedef = type as TypedefItem;
            var keyword = typedef == null ? type.ToString() : typedef.KeyWork;
            var isArray = typedef == null ? field.Datalen > 0 : typedef.ArrayLen != null;
            if (keyword == "char" && isArray)
            {
                if (field.Datalen == 1 || typedef?.ArrayLen == "1")
                    code.Append($@"
    wtiter.Write(field->{field.Name}[0]);");
                else
                    code.Append($@"
    wtiter.Write(field->{field.Name});");
            }
            else if (isArray)
            {
                code.Append($@"
    wtiter.Write(field->{field.Name},{(typedef == null ? field.Datalen.ToString() : typedef.ArrayLen)});");
            }
            else
            {
                code.Append($@"
    wtiter.Write(field->{field.Name});");
            }
        }

        private static void ToCppReadCode(StringBuilder code, IFieldConfig field)
        {
            var type = CppTypeHelper.ToCppLastType(field.CppLastType);
            if (type is EntityConfig stru)
            {
                if (field.Datalen == 1)
                {
                    code.Append($@"
		case IDX_{field.Entity.ReadTableName}_{field.Name}://{field.Caption}
        {{
            int len = reader.ReadInt32();
            char* buffer = reader.ReadBinrary2(len);
            Deserializer deserializer(buffer,len,true);
            Deserialize(deserializer,&field->{field.Name}[0]);
        }}
            break;");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
		case IDX_{field.Entity.ReadTableName}_{field.Name}://{field.Caption}
        {{
            int cnt = reader.ReadInt32();
            for(int idx = 0;i < cnt;i++)
            {{
                char* buffer = reader.ReadBinrary(len);
                Deserializer deserializer(buffer,len,true);
                Deserialize(deserializer,&field->{field.Name}[idx]);
            }}
        }}
            break;");
                }
                else
                {
                    code.Append($@"
		case IDX_{field.Entity.ReadTableName}_{field.Name}://{field.Caption}
        {{
            int len = reader.ReadInt32();
            char* buffer = reader.ReadBinrary2(len);
            Deserializer deserializer(buffer,len,true);
            Deserialize(deserializer,&field->{field.Name});
        }}
            break;");
                }
                return;
            }
            var typedef = type as TypedefItem;
            var keyword = typedef == null ? type.ToString() : typedef.KeyWork;
            var isArray = typedef == null ? field.Datalen > 0 : typedef.ArrayLen != null;
            if (keyword == "char" && isArray)
            {
                code.Append($@"
		case IDX_{field.Entity.ReadTableName}_{field.Name}://{field.Caption}
            reader.ReadString(field->{field.Name});
            break;");
            }
            else if (isArray)
            {
                code.Append($@"
		case IDX_{field.Entity.ReadTableName}_{field.Name}://{field.Caption}
            reader.ReadArray<{keyword}>(&field->{field.Name});
            break;");
            }
            else
            {
                code.Append($@"
		case IDX_{field.Entity.ReadTableName}_{field.Name}://{field.Caption}
            reader.Read<{keyword}>(&field->{field.Name});
            break;");
            }
        }
        #endregion
        #region CppToCs

        public static string CppFieldToCsEntityDef(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.Append($@"
///{entityConfig.Caption} 结构复制到对应的.Net实体对象
{entityConfig.Name}^ CreateCsEntity({entityConfig.ReadTableName}* field);

///{entityConfig.Caption} 结构复制到对应的.Net实体对象
void CopyToCache({entityConfig.ReadTableName}* field);");

            return code.ToString();
        }

        public static string CppFieldToCsEntity(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.Append($@"

///{entityConfig.Caption} 结构复制到对应的.Net实体对象的缓存中
void CopyToCache({entityConfig.ReadTableName}* field)
{{
    if(field == nullptr)
        return;
    {entityConfig.Name}^ item = CreateCsEntity(field);
    {entityConfig.Name}::AddToCache(item);
}}

///{entityConfig.Caption} 结构复制构造一个对应的.Net实体对象
{entityConfig.Name}^ CreateCsEntity({entityConfig.ReadTableName}* field)
{{
	char buf[2];
	buf[1] = '\0';
    {entityConfig.Name}^ item = gcnew {entityConfig.Name}();");
            foreach (var t in entityConfig.LastProperties)
            {
                ToCppCopyCode(code, t);
            }
            code.Append(@"
    return item;
}");

            return code.ToString();
        }

        private static void ToCppCopyCode(StringBuilder code, IFieldConfig field)
        {
            var type = CppTypeHelper.ToCppLastType(field.CppLastType);
            if (type is EntityConfig)
            {
                code.Append($@"
    item->{field.Name} = CreateCsEntity(&field->{field.Name});//{field.Caption}");
                return;
            }
            if (!(type is TypedefItem typedef))
            {
                if (type.ToString() == "char")
                {
                    if (field.Datalen > 0)
                        code.Append($@"
    item->{field.Name} =  marshal_as<String^>(&field->{field.Name});//{field.Caption}");
                    else
                        code.Append($@"
    buf[0] = field->{field.Name};//{field.Caption}
    item->{field.Name} =  marshal_as<String^>(buf);");
                }
                else if (field.Datalen > 0)
                {
                    if (field.Datalen > 0)
                        code.Append($@"
    item->{field.Name} =  gcnew {type}();//{field.Caption}
    for each(auto vl in field->{field.Name})
        item->{field.Name}->Add(vl);");
                }
                else
                {
                    code.Append($@"
    item->{field.Name} =  field->{field.Name};//{field.Caption}");
                }
                return;
            }

            if (typedef.KeyWork == "char")
            {
                if (field.Datalen > 0 || typedef.ArrayLen != null)
                    code.Append($@"
    item->{field.Name} =  marshal_as<String^>(&field->{field.Name});//{field.Caption}");
                else
                    code.Append($@"
    buf[0] = field->{field.Name};//{field.Caption}
    item->{field.Name} =  marshal_as<String^>(buf);");
            }
            else if (field.Datalen > 0)
            {
                if (field.Datalen > 0)
                    code.Append($@"
    item->{field.Name} =  gcnew {field.CsType}();//{field.Caption}
    for each(auto vl in field->{field.Name})
        item->{field.Name}->Add(vl);");
            }
            else if (string.IsNullOrWhiteSpace(typedef.ArrayLen))
            {
                code.Append($@"
    item->{field.Name} =  field->{field.Name};//{field.Caption}");
            }
            else
            {
                if (field.Datalen > 0)
                    code.Append($@"
    item->{field.Name} =  gcnew {type}();//{field.Caption}
    for each(auto vl in field->{field.Name})
        quote->{field.Name}->Add(field.{field.Name}[i]);");
            }
        }

        #endregion
        #region CppJson
        public static string ToCppRedis(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.Append($@"

            ///{entityConfig.Caption} 保存到REDIS(JSON格式)
            void saveToRedis(const char* head,const {entityConfig.Name}* value)
            {{
                if(value == nullptr)
                    return;

            	char key[256];
            	sprintf_s(key,256, ""%s:{entityConfig.Name}:%s"", head, value->___);
                writeToRedis(key, toJson(value).c_str());
            }}");
            return code.ToString();
        }


        public static string ToCppJsonDef(EntityConfig entityConfig)
        {
            return ($@"

///{entityConfig.Caption} 格式化为JSON格式
string toJson(const {entityConfig.Name}* value);");
        }

        public static string ToCppJson(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.Append($@"

///{entityConfig.Caption} 格式化为JSON格式
string toJson(const {entityConfig.Name}* value)
{{
    if(value == nullptr)
        return """";
    int idx,len;
    ostringstream ostr;
    ostr << ""{{\""__i__\"":0"";");
            foreach (var t in entityConfig.LastProperties)
            {
                ToJsonCode(code, t, false);
            }
            code.Append(@"
    ostr.put('}');
    return ostr.str();
}");


            //            code.AppendFormat(@"

            /////{1} 保存到REDIS(JSON格式)
            //void saveToRedis(const char* head,const {0}* value)
            //{{
            //    if(value == nullptr)
            //        return;

            //	char key[256];
            //	sprintf_s(key,256, ""%s:{0}:%s"", head, value->___);
            //    writeToRedis(key, toJson(value).c_str());
            //}}", entityConfig.Name, entityConfig.Caption);
            return code.ToString();
        }


        private static void ToJsonCode(StringBuilder code, IFieldConfig field, bool isFirst)
        {
            var type = CppTypeHelper.ToCppLastType(field.CppLastType);
            if (type is EntityConfig stru)
            {
                FriendJson(code, field, isFirst);
                return;
            }
            if (!(type is TypedefItem typedef))
            {
                if (type.ToString() == "char")
                {
                    StringJson(code, field, field.Datalen.ToString(), isFirst);
                }
                else if (field.Datalen > 0)
                {
                    ArrayJson(code, field, field.Datalen.ToString(), isFirst);
                }
                else
                {
                    NumberJson(code, field, isFirst);
                }
                return;
            }

            if (typedef.KeyWork == "char")
            {
                StringJson(code, field, typedef.ArrayLen, isFirst);
            }
            else if (field.Datalen > 0)
            {
                ArrayJson(code, field, field.Datalen.ToString(), isFirst);
            }
            else if (string.IsNullOrWhiteSpace(typedef.ArrayLen))
            {
                NumberJson(code, field, isFirst);
            }
            else
            {
                ArrayJson(code, field, typedef.ArrayLen, isFirst);
            }
        }

        private static void FriendJson(StringBuilder code, IFieldConfig field, bool isFirst)
        {
            code.AppendFormat(@"
    //{0}
    ostr << ""{2}\""{1}\"":"" << toJson(&value->{1});", field.Caption, field.Name, isFirst ? null : ",");
        }

        private static void NumberJson(StringBuilder code, IFieldConfig field, bool isFirst)
        {
            code.AppendFormat(@"
    //{0}
    if(value->{1} != 0)
        ostr << ""{2}\""{1}\"":\"""" << value->{1} << '\""';", field.Caption, field.Name, isFirst ? null : ",");
        }

        private static void StringJson(StringBuilder code, IFieldConfig field, string len, bool isFirst)
        {
            if (!string.IsNullOrWhiteSpace(len))
                code.AppendFormat(@"
    //{0}
    if(strlen(value->{1}) > 0)
        ostr << ""{2}\""{1}\"":\"""" << value->{1} << '\""';", field.Caption, field.Name, isFirst ? null : ",");
            else
                code.AppendFormat(@"
    //{0}
    if(value->{1})
        ostr << ""{2}\""{1}\"":\"""" << value->{1} << '\""';", field.Caption, field.Name, isFirst ? null : ",");
        }
        private static void ArrayJson(StringBuilder code, IFieldConfig field, string len, bool isFirst)
        {
            code.AppendFormat(@"
    //{0}
    ostr << ""{2}\""{1}\"":["";", field.Caption, field.Name, isFirst ? null : ",");

            code.AppendFormat(@"
    len = {0} - 1;
    for(idx = 0;idx < len;idx++)
    {{
       ostr << value->{1}[idx] << ',';
    }}
    ostr << value->{1}[idx] << ']';", len, field.Name);
        }

        #endregion

        #region CppCout

        public static string ToCppCoutDef(EntityConfig entityConfig)
        {
            return string.Format(@"

///{1} 显示到Cout
void print_screen(const {0}* value);", entityConfig.Name, entityConfig.Caption);
        }

        public static string ToCppCout(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.AppendFormat(@"

///{1} 显示到Cout
void print_screen(const {0}* value)
{{
    if(value == nullptr)
        return;
    int idx,len; ", entityConfig.Name, entityConfig.Caption);
            foreach (var field in entityConfig.LastProperties)
            {
                ToCppCoutCode(code, field);
            }
            code.Append(@"
}");
            return code.ToString();
        }


        private static void ToCppCoutCode(StringBuilder code, IFieldConfig field)
        {
            if (!string.IsNullOrWhiteSpace(field.Caption))
                code.AppendFormat(@"
    cout << ""{0}:", field.Caption);
            else
                code.AppendFormat(@"
    cout << ""{0}:", field.Name);
            var type = CppTypeHelper.ToCppLastType(field.CppLastType);
            if (type is EntityConfig stru)
            {
                code.AppendFormat(@""" << endl;
    print_screen(&value->{0});", field.Name);
                return;
            }
            if (!(type is TypedefItem typedef))
            {
                if (type.ToString() == "char")
                {
                    code.AppendFormat(@""" << value->{0} << endl;", field.Name);
                }
                else if (field.Datalen > 0)
                {
                    ArrayOut(code, field, field.Datalen.ToString());
                }
                else
                {
                    code.AppendFormat(@""" << value->{0} << endl;", field.Name);
                }
                return;
            }
            if (typedef.Items?.Count > 0)
            {
                EnumOut(code, field, typedef);
            }
            else if (typedef.KeyWork == "char")
            {
                code.AppendFormat(@""" << value->{0} << endl;", field.Name);
            }
            else if (field.Datalen > 0)
            {
                ArrayOut(code, field, field.Datalen.ToString());
            }
            else if (string.IsNullOrWhiteSpace(typedef.ArrayLen))
            {
                code.AppendFormat(@""" << value->{0} << endl;", field.Name);
            }
            else
            {
                ArrayOut(code, field, typedef.ArrayLen);
            }
        }

        private static void EnumOut(StringBuilder code, IFieldConfig field, TypedefItem typedef)
        {
            code.AppendFormat(@""";");
            code.AppendFormat(@"
    switch(value->{0})
    {{", field.Name);
            foreach (var kv in typedef.Items.Values)
            {
                code.AppendFormat(@"
    case {0}:
            cout << ""{1}"" << endl;
            break;", kv.Name, kv.Value);
            }
            code.Append(@"
    }");
        }

        private static void ArrayOut(StringBuilder code, IFieldConfig field, string len)
        {
            code.Append(@""";");

            code.AppendFormat(@"
    cout.put('[');
    len = {0} - 1;
    for(idx = 0;idx < len;idx++)
    {{
       cout << value->{1}[idx] << ',';
    }}
    cout << value->{1}[idx] << ']';
    cout << endl;", len, field.Name);
        }

        #endregion

        #region 常量选择代码

        public static string IncludeFunc(ProjectConfig project)
        {
            var code = new StringBuilder();
            if (project.IsReference)
                code.AppendFormat(@"
#ifdef PROXY");
            foreach (var entity in project.Entities)
                code.AppendFormat($@"
#include ""{project.Name}\{entity.Name}.h""");
            if (project.IsReference)
                code.AppendFormat(@"
#endif");
            return code.ToString();
        }
        #endregion

        #region 常量名称字典

        public static string ProjectFile(ProjectConfig project)
        {
            var code = new StringBuilder();
            code.AppendFormat(@"
  <ItemGroup>");

            foreach (var entity in project.Entities)
            {
                code.Append($@"     
    <ClInclude Include=""..\SharpCode\{project.Name}\{entity.Name}.h""/>");
            }
            code.AppendFormat(@"
  </ItemGroup>");
            foreach (var entity in project.Entities)
            {
                code.Append($@"     
    <ClCompile Include=""..\SharpCode\{project.Name}\{entity.Name}.cpp""/>");
            }
            return code.ToString();
        }

        public static string ProjectFilter(ProjectConfig project)
        {
            var code = new StringBuilder();
            //        foreach (var project in SolutionConfig.Current.Projects)
            //        {
            //            code.Append($@"
            //<Filter Include=""business\{project.Name}"">
            //  <UniqueIdentifier>{{{Guid.NewGuid()}}}</UniqueIdentifier>
            //</Filter>");
            //        }
            //          code.AppendFormat(@"
            code.AppendFormat(@"
  <ItemGroup>");
            foreach (var entity in project.Entities)
            {
                code.Append($@"
    <ClInclude Include=""..\SharpCode\{project.Name}\{entity.Name}.h"">
      <Filter>business\{project.Name}</Filter>
    </ClInclude>");
            }
            foreach (var entity in project.Entities)
            {
                code.Append($@"
    <ClCompile Include=""..\SharpCode\{project.Name}\{entity.Name}.cpp"">
      <Filter>business\{project.Name}</Filter>
    </ClCompile>");
            }
            return code.ToString();
        }
        #endregion
        #region 日志

        private static string EnumLog(EntityConfig entity)
        {
            var code = new StringBuilder();
            foreach (var em in SolutionConfig.Current.Enums)
            {
                code.Append($@"

///{em.Caption} 生成日志文本
string to_log_text(GBS::Futures::{em.Name}Classify& value)
{{
    switch(value)
    {{");
                foreach (var item in em.Items)
                {
                    code.Append($@"
    case GBS::Futures::{em.Name}Classify::{item.Name}:
        return string(""{item.Caption}"");");
                }
                code.Append(@"
    };
    return string();
}");
            }
            return code.ToString();
        }

        public static string ToCppLogDef(ConfigBase config)
        {
            if (config is EntityConfig entityConfig)
            {
                return ToCppLogDef(entityConfig);
            }
            var code = new StringBuilder();
            if (config is ProjectConfig projectConfig)
            {
                if (projectConfig.IsReference)
                {
                    code.Append($@"
#ifdef {projectConfig.Name}");
                }
                foreach (var entity in projectConfig.Entities)
                {
                    code.Append(ToCppLogDef(entity));
                }
                if (projectConfig.IsReference)
                {
                    code.Append($@"
#endif");
                }
            }
            else
            {
                foreach (var project in SolutionConfig.Current.Projects)
                {
                    if (project.IsReference)
                    {
                        code.Append($@"
#ifdef {project.Name}");
                    }
                    foreach (var entity in project.Entities)
                    {
                        code.Append(ToCppLogDef(entity));
                    }
                    if (project.IsReference)
                    {
                        code.Append(@"
#endif");
                    }
                }
            }
            return code.ToString();
        }

        public static string ToCppLog(ConfigBase config)
        {
            if (config is EntityConfig entityConfig)
            {
                return ToCppLog(entityConfig);
            }
            var code = new StringBuilder();
            if (config is ProjectConfig projectConfig)
            {

                if (projectConfig.IsReference)
                {
                    code.Append($@"
#ifdef {projectConfig.Name}");
                }
                foreach (var entity in projectConfig.Entities)
                {
                    code.Append(ToCppLog(entity));
                }
                if (projectConfig.IsReference)
                {
                    code.Append(@"
#endif");
                }
            }
            else
            {
                foreach (var project in SolutionConfig.Current.Projects)
                {
                    if (project.IsReference)
                    {
                        code.Append($@"
#ifdef {project.Name}");
                    }
                    foreach (var entity in project.Entities)
                    {
                        code.Append(ToCppLog(entity));
                    }
                    if (project.IsReference)
                    {
                        code.Append($@"
#endif");
                    }
                }
            }
            return code.ToString();
        }
        public static string ToCppLogDef(EntityConfig entityConfig)
        {
            return $@"
///{entityConfig.Caption} 生成日志文本
string to_log_text(const {entityConfig.Name}* value);

///{entityConfig.Caption} 记录到日志
void log(const {entityConfig.Name}* value, int type, int level);";
        }
        public static string ToCppLog(EntityConfig entityConfig)
        {
            var code = new StringBuilder();
            code.Append($@"

///{entityConfig.Caption} 生成日志文本
string to_log_text(const {entityConfig.Name}* value)
{{
    if(value == nullptr)
        return """";
    int idx,len;
    ostringstream ostr;
    ostr << ""{{\""__i__\"":0"";");
            foreach (var field in entityConfig.LastProperties)
            {
                ToLogCode(code, field);
            }
            code.Append($@"
    ostr.put('}}');
    return ostr.str();
}}
///{entityConfig.Caption} 记录到日志
void log(const {entityConfig.Name}* value,int type,int level)
{{
    log_debug(type,level,to_log_text(value).c_str());
}}");
            return code.ToString();
        }


        private static void ToLogCode(StringBuilder code, IFieldConfig field)
        {
            if (field.IsIntDecimal)
            {
                IntDecimalLog(code, field);
                return;
            }
            if (field.EnumConfig != null)
            {
                InlineLog(code, field);
                return;
            }
            var type = CppTypeHelper.ToCppLastType(field.CppLastType ?? field.CppType);
            if (type == null)
                return;
            if (type is EntityConfig stru)
            {
                FriendLog(code, field);
                return;
            }
            if (!(type is TypedefItem typedef))
            {
                string tp = type.ToString();
                if (tp == "char")
                {
                    if (field.Datalen > 1)
                        StringLog(code, field, field.Datalen.ToString());
                    else
                        CharLog(code, field);
                }
                else if (!string.IsNullOrWhiteSpace(field.ArrayLen))
                {
                    ArrayLog(code, field, field.ArrayLen);
                }
                else if (tp == "tm")
                {
                    InlineLog(code, field);
                }
                else
                {
                    NumberLog(code, field);
                }
                return;
            }
            if (typedef.Items.Count > 0)
            {
                TypeDefLog(code, field, typedef);
            }
            else if (typedef.KeyWork == "char")
            {
                if (field.Datalen > 1)
                    StringLog(code, field, typedef.ArrayLen);
                else
                    CharLog(code, field);
            }
            else if (string.IsNullOrWhiteSpace(typedef.ArrayLen))
            {
                NumberLog(code, field);
            }
            else
            {
                ArrayLog(code, field, typedef.ArrayLen);
            }
        }
        private static void TypeDefLog(StringBuilder code, IFieldConfig field, TypedefItem typedef)
        {
            code.AppendFormat(@"
    ostr << "",\""{0}({1})\"":"";
    switch(value->{1})
    {{", field.Caption, field.Name);
            foreach (var item in typedef.Items)
            {
                code.Append($@"
    case '{item.Value.Value}':
        ostr << ""{item.Value.Caption}\"""";
        break;");
            }
            code.Append(@"
    }");
        }

        private static void CharLog(StringBuilder code, IFieldConfig field)
        {
            code.AppendFormat(@"
    ostr << "",\""{0}({1})\"":"" << value->{1};", field.Caption, field.Name);
        }
        private static void InlineLog(StringBuilder code, IFieldConfig field)
        {
            code.AppendFormat(@"
    ostr << "",\""{0}({1})\"":"" << to_log_text(value->{1});", field.Caption, field.Name);
        }

        private static void FriendLog(StringBuilder code, IFieldConfig field)
        {
            code.AppendFormat(@"
    ostr << "",\""{0}({1})\"":"" << to_log_text(&value->{1});", field.Caption, field.Name);
        }
        private static void IntDecimalLog(StringBuilder code, IFieldConfig field)
        {
            code.AppendFormat(@"
    if(value->{1} != 0)
        ostr << "",\""{0}({1})\"":\"""" << Int64ToDouble(value->{1}) << '\""';", field.Caption, field.Name);
        }

        private static void NumberLog(StringBuilder code, IFieldConfig field)
        {
            code.AppendFormat(@"
    if(value->{1} != 0)
        ostr << "",\""{0}({1})\"":\"""" << value->{1} << '\""';", field.Caption, field.Name);
        }

        private static void StringLog(StringBuilder code, IFieldConfig field, string len)
        {
            if (!string.IsNullOrWhiteSpace(len))
                code.AppendFormat(@"
    if(strlen(value->{1}) > 0)
        ostr << "",\""{0}({1})\"":\"""" << value->{1} << '\""';", field.Caption, field.Name);
            else
                code.AppendFormat(@"
    if(value->{1})
        ostr << "",\""{0}({1})\"":\"""" << value->{1} << '\""';", field.Caption, field.Name);
        }
        private static void ArrayLog(StringBuilder code, IFieldConfig field, string len)
        {
            code.AppendFormat(@"
    ostr << "",\""{0}({1})\"":["";", field.Caption, field.Name);

            code.AppendFormat(@"
    len = {0} - 1;
    for(idx = 0;idx < len;idx++)
    {{
       ostr << value->{1}[idx] << ',';
    }}
    ostr << value->{1}[idx] << ']';", len, field.Name);
        }
        #endregion
    }
}