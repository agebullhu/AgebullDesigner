using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class CppStructCoder : CoderWithEntity
    {
        #region �ṹ����

        private string StructCode()
        {
            var code = new StringBuilder();
            if (!Entity.IsReference)
            {
                code.Append($@"

/**
* @brief {Entity.Name}�Ľṹ����
*/
struct {Entity.Name}");
                if (!Entity.NoDataBase && !Entity.IsInternal)
                    code.Append(@"
/*#ifdef SERVER
 : DataModel::IData
#endif*/");
                code.Append(@"
{");
                foreach (PropertyConfig property in Entity.CppProperty)
                {
                    string type = property.CppLastType;
                    if (!Entity.IsReference)
                    {
                        if (property.EnumConfig != null)
                        {
                            type = $"{property.EnumConfig.Name}Classify";
                        }
                        else
                        {
                            EntityConfig friend = GetLcEntity(property);
                            if (friend != null)
                                type = friend.Name;
                        }
                    }
                    code.Append($@"
    //{property.Caption}
    {type}  {property.Name}");

                    if (property.CppLastType == "char")
                    {
                        if (property.Datalen > 0)
                            code.Append($@"[{property.Datalen}]");
                    }
                    else if (!string.IsNullOrWhiteSpace(property.ArrayLen))
                        code.Append($@"[{property.ArrayLen}]");

                    code.Append(';');
                }
                code.Append(
                    $@"
    /**
    * @brief ������������������л�����{Entity.Caption}��
    * @param {{PNetCommand}} command �����������
    * @return {Entity.Caption}����ָ��
    */
    inline {Entity.Name}& operator = (const PNetCommand cmd)
	{{
        Deserialize(cmd, this);
		return *this;
	}}");
                if (!Entity.NoDataBase && !Entity.IsInternal)
                    code.Append(@"
#ifdef DATAACCESS
	/**
	* @brief ͨ������д���ı�ֵ
	* @param {{char *}} field �ֶ�
	* @param {{char *}} value �ı�ֵ
	*/
	void SetValue(const char* field, const char* value);
#endif");
                code.Append(@"
};");
            }
            code.AppendLine($@"
//{Entity.Caption}�ĺ������л�����
const size_t TSON_BUFFER_LEN_{Entity.Name.ToUpper()} = {GetEntitySerializeLen(Entity)}; ");
            return code.ToString();
        }


        #endregion

        #region ��������

        private static string GetEntitySerializeLen(EntityConfig entity)
        {
            var code = new StringBuilder();
            code.Append($"SERIALIZE_BASE_LEN + sizeof({entity.Name})");
            int len = entity.LastProperties.Count;
            foreach (var property in entity.CppProperty)
            {
                len += GetFieldSerializeLen(code, property);
            }
            code.Append($" + {len}");
            return code.ToString();
        }

        private static int GetFieldSerializeLen(StringBuilder code, PropertyConfig field)
        {
            int flen = 0;
            Trace.WriteLine(field.Caption);
            CppTypeHelper2.DoByCppType(field.Parent, field,
                (pro, len) =>
                {
                    flen = 4;
                },
                (pro, type, len) =>
                {
                    if (len > 1)
                        flen = 4;
                },
                (pro, type, len) =>
                {
                    flen = 4;
                    code.Append(" + " + GetEntitySerializeLen(type));
                },
                (pro, type, len) =>
                {
                    Debug.Assert(len < 1);
                },
                (pro, type, len) =>
                {
                    Debug.Assert(len < 1);
                }
            );
            return flen;
        }
        private string FunctionDef()
        {
            var code = new StringBuilder();
            code.Append($@"
/**
* @brief {Entity.Name}���л����ֽ�
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @param {{size_t}} len ���س���
* @return �ֽ�����
*/
char* Serialize(const {Entity.Name}* field,size_t& len);

/**
* @brief {Entity.Name}���л����������
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return �������
*/
PNetCommand SerializeToCommand(const {Entity.Name}* field);


/**
* @brief {Entity.Name}���л����������
* @param {{PNetCommand}} command �������
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Serialize(PNetCommand command, const {Entity.Name}* field);

/**
* @brief {Entity.Name}���л����ֽ�
* @param {{char*}} buffer �ֽ�����
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Serialize(char* buffer, size_t len, const {Entity.Name}* field);


/**
* @brief {Entity.Name}���л������л���
* @param {{Serializer&}} writer ���л���
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Serialize(Serializer& writer, const {Entity.Name}* field);

/**
* @brief ������������������л���{Entity.Name}
* @param {{PNetCommand}} command �����������
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Deserialize(PNetCommand command, {Entity.Name}* field);

/**
* @brief ���ֽڷ����л���{Entity.Name}
* @param {{char*}} buffer �ֽ�����ָ��
* @param {{size_t}} len �ֽ����鳤��
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Deserialize(char* buffer, size_t len, {Entity.Name}* field);

/**
* @brief ������������������л���{Entity.Name}
* @param {{PNetCommand}} command �����������
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Deserialize(PNetCommand command, {Entity.Name}* field);

/**
* @brief �ӷ����л��������л���{Entity.Name}
* @param {{Deserializer&}} reader �����л���
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Deserialize(Deserializer& reader, {Entity.Name}* field);");


            return code.ToString();
        }
        #endregion
        #region ����ʵ��


        private string Function()
        {
            var code = new StringBuilder();
            code.Append($@"
/**
* @brief {Entity.Name}���л����ֽ�
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @param {{size_t}} len ���س���
* @return �ֽ�����
*/
char* Serialize(const {Entity.Name}* field,size_t& len)
{{
	Serializer writer;
	writer.CreateBuffer(TSON_BUFFER_LEN_{Entity.Name.ToUpper()}, false);
	Serialize(writer, field);
	len = writer.GetDataLen();
	return writer.GetBuffer();
}}


/**
* @brief {Entity.Name}���л����������
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return �������
*/
PNetCommand SerializeToCommand(const {Entity.Name}* field)
{{
	size_t len = TSON_BUFFER_LEN_{Entity.Name.ToUpper()} + NETCOMMAND_HEAD_LEN;
	char* buffer = new char[len];
	memset(buffer, 0, NETCOMMAND_HEAD_LEN);
	Serializer writer(get_cmd_buffer(buffer), static_cast<size_t>(len - NETCOMMAND_HEAD_LEN));
	Serialize(writer, field);
	return reinterpret_cast<PNetCommand>(buffer);
}}

/**
* @brief {Entity.Name}���л����������
* @param {{PNetCommand}} command �������
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Serialize(PNetCommand command, const {Entity.Name}* field)
{{
	Serializer writer(get_cmd_buffer(command), static_cast<size_t>(get_cmd_len(command)));
	Serialize(writer, field);
}}

/**
* @brief {Entity.Name}���л����ֽ�
* @param {{char*}} buffer �ֽ�����
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Serialize(char* buffer, size_t len, const {Entity.Name}* field)
{{
	Serializer writer(buffer, len);
	Serialize(writer, field);
}}

/**
* @brief ������������������л���{Entity.Name}
* @param {{PNetCommand}} command �����������
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Deserialize(PNetCommand command, {Entity.Name}* field)
{{
	Deserializer reader(get_cmd_buffer(command), static_cast<size_t>(get_cmd_len(command)));
	Deserialize(reader, field);
}}

/**
* @brief ���ֽڷ����л���{Entity.Name}
* @param {{char*}} buffer �ֽ�����ָ��
* @param {{size_t}} len �ֽ����鳤��
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Deserialize(char* buffer, size_t len, {Entity.Name}* field)
{{
	Deserializer reader(buffer, len);
	Deserialize(reader, field);
}}

");
            code.Append($@"
/**
* @brief {Entity.Name}���л������л���
* @param {{Serializer&}} writer ���л���
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Serialize(Serializer& writer,const {Entity.Name}* field)
{{
	writer.Begin(TYPE_INDEX_{Entity.Name.ToUpper()}, static_cast<char>(1));
    if(field == nullptr)
    {{
        writer.End();
        return;
    }}");
            foreach (PropertyConfig field in Entity.CppProperty)
            {
                code.Append(ToSerialize(field));
            }
            code.Append($@"
    writer.End();
}}


/**
* @brief �ӷ����л��������л���{Entity.Name}
* @param {{Deserializer&}} reader �����л���
* @param {{{Entity.Name}*}} field {Entity.Caption}����ָ��
* @return ��
*/
void Deserialize(Deserializer& reader, {Entity.Name}* field)
{{
    memset(field, 0 , sizeof({Entity.Name}));");
            if (Entity.LastProperties.Count > 0)
            {
                code.Append(@"
    reader.Begin();	
	while(!reader.IsEof())
	{
		FIELD_INDEX idx = reader.ReadByte();
        //OBJ_TYPE type = reader.ReadByte();
		switch(idx)
		{");
                foreach (PropertyConfig field in Entity.CppProperty)
                {
                    code.Append($@"
        case FIELD_INDEX_{Entity.Name.ToUpper()}_{field.Name.ToUpper()}://{field.Caption}
        {{{DeserializeCode(field)}
            break;
        }}");
                }
                code.Append(@"
        }
    }
    reader.End();");
            }

            code.Append(@"
}");

            return code.ToString();
        }

        private string ToSerialize(PropertyConfig field)
        {
            EntityConfig stru = GetLcEntity(field);
            if (stru == null)
            {
                if (field.EnumConfig != null)
                    return $@"
    if(!writer.is_empty(field->{field.Name}))//{field.Caption}
    {{
        writer.WriteIndex(FIELD_INDEX_{Entity.Name.ToUpper()}_{field.Name.ToUpper()});
        writer.WriteValue(field->{field.Name});
    }}";
                if (field.CppLastType == "char" && field.Datalen > 1)
                {
                    return $@"
    if(!writer.str_is_empty(field->{field.Name}))//{field.Caption}
    {{
        writer.WriteIndex(FIELD_INDEX_{Entity.Name.ToUpper()}_{field.Name.ToUpper()});
        writer.WriteStr(field->{field.Name});
    }}";
                }
                return $@"
    if(!writer.is_empty(field->{field.Name}))//{field.Caption}
    {{
        writer.WriteIndex(FIELD_INDEX_{Entity.Name.ToUpper()}_{field.Name.ToUpper()});
        writer.Write(field->{field.Name});
    }}";
            }
            if (string.IsNullOrWhiteSpace(field.ArrayLen))
                return $@"
    {{
        writer.WriteIndex(FIELD_INDEX_{Entity.Name.ToUpper()}_{field.Name.ToUpper()
                    });
        Serializer serializer;
		serializer.CreateBuffer(TSON_BUFFER_LEN_{stru.Name.ToUpper()}, false);
        Serialize(serializer,&field->{field.Name});
        writer.WriteObject(serializer);
    }}";
            return $@"
    {{
        writer.WriteIndex(FIELD_INDEX_{Entity.Name.ToUpper()}_{field.Name.ToUpper()});
        writer.Write({field.ArrayLen});
        for(size_t idx = 0;i < {field.ArrayLen};i++)
        {{
            Serializer serializer;
            Serialize(serializer,&field->{field.Name}[idx]);
            writer.Write(serializer);
        }}
    }}";
        }

        private string DeserializeCode(PropertyConfig field)
        {
            EntityConfig stru = GetLcEntity(field);
            if (stru == null)
            {
                if (field.CppLastType == "char" && field.Datalen > 1)
                    return $@"
            reader.ReadStr(field->{field.Name});";
                return $@"
            reader.Read(field->{field.Name});";
            }
            return string.IsNullOrEmpty(field.ArrayLen)
                ? $@"
            size_t len;
			reader.Read(len);
            char* buffer = reader.ReadBinrary(len);
            Deserializer deserializer(buffer,len,true);
            Deserialize(deserializer,&field->{field.Name});"
                : $@"
            size_t cnt;
			reader.Read(cnt);
            for(size_t idx = 0;i < cnt;i++)
            {{
                size_t len;
			    reader.Read(len);
                char* buffer = reader.ReadBinrary(len);
                Deserializer deserializer(buffer,len,true);
                Deserialize(deserializer,&field->{field.Name}[idx]);
            }}";
        }

        #endregion
        #region ���ݿ��д

        private string SetValueFromDb()
        {
            if (Entity.IsReference || Entity.NoDataBase)
                return null;
            StringBuilder code = new StringBuilder();
            code.Append($@"
#ifdef DATAACCESS
/**
* @brief �����ֶ�ֵ
* @param {{const char*}} field �ֶ�����
* @param {{const char*}} value �ֶ��ı�ֵ
*/
void {Entity.Name}::SetValue(const char* field, const char* value)
{{
    if(value == nullptr || strlen(value) == 0)
        return;");

            foreach (PropertyConfig field in Entity.CppProperty)
            {
                if (field.IsPrimaryKey)
                    code.Append($@"
    if(field == nullptr || strcmp(field,""{field.Name}"") == 0)//{field.Caption}-����");
                else
                    code.Append($@"
    if(strcmp(field,""{field.Name}"") == 0)//{field.Caption}");
                SetValue2(code, field);
            }
            code.Append(@"
}
#endif");
            return code.ToString();
        }

        #endregion

        #region ��ʢ����

        private static string FriendInc(EntityConfig entity)
        {

            var code = new StringBuilder();
            foreach (var pro in entity.CppProperty)
            {
                var friend = CppTypeHelper.ToCppLastType(pro.CppLastType ?? pro.CppType) as EntityConfig;
                if (friend != null)
                {
                    code.Append($@"
#include <{friend.Parent.Name}/{friend.Name}.h>");
                }
            }
            if (!entity.IsReference)
                return code.ToString();
            var lc_entity = GlobalConfig.GetEntity(p => !p.IsReference && p.Option.ReferenceTag != null && p.Option.ReferenceTag.Contains(entity.Option.ReferenceTag));
            if (lc_entity == null)
                return code.ToString();
            code.Append($@"
#include <{lc_entity.Parent.Name}/{lc_entity.Name}.h>");

            if (!string.IsNullOrWhiteSpace(entity.Parent.NameSpace))
                code.Append($@"
using namespace {entity.Parent.NameSpace.Replace(".", "::")};");
            if (!string.IsNullOrWhiteSpace(lc_entity.Parent.NameSpace))
                code.Append($@"
using namespace {lc_entity.Parent.NameSpace.Replace(".", "::")};");

            return code.ToString();
        }

        private static string FriendDef(EntityConfig es_entity)
        {
            if (!es_entity.IsReference)
                return null;
            var lc_entity = GlobalConfig.GetEntity(p => !p.IsReference && p.Option.ReferenceTag != null && p.Option.ReferenceTag.Contains(es_entity.Option.ReferenceTag));
            if (lc_entity == null)
                return null;
            var code = new StringBuilder();
            code.Append($@"
/**
* @brief ��{es_entity.Caption}������ʢ�ṹ�ӱ��ؽṹ����
* @param {{{lc_entity.Name}}} lc_field {es_entity.Caption}-���ؽṹ
* @param {{{es_entity.Name}}} es_field {es_entity.Caption}-��ʢ�ṹ
* @return ��
*/
void CopyFromEs(const {es_entity.Name}* es_field , {lc_entity.Name}* lc_field);

/**
* @brief ��{es_entity.Caption}������ʢ�ṹ���Ƶ����ؽṹ
* @param {{{lc_entity.Name}}} lc_field {es_entity.Caption}-���ؽṹ
* @param {{{es_entity.Name}}} es_field {es_entity.Caption}-��ʢ�ṹ
* @return ��
*/
void CopyToEs(const {lc_entity.Name}* lc_field, {es_entity.Name}* es_field);");
            return code.ToString();
        }

        private static string Friend(EntityConfig es_entity)
        {
            if (!es_entity.IsReference)
                return null;
            var lc_entity = GlobalConfig.GetEntity(p => !p.IsReference && p.Option.ReferenceTag != null && p.Option.ReferenceTag.Contains(es_entity.Option.ReferenceTag));
            if (lc_entity == null)
                return null;
            Dictionary<PropertyConfig, PropertyConfig> maps = new Dictionary<PropertyConfig, PropertyConfig>();
            foreach (PropertyConfig property in es_entity.CppProperty.Where(p => p.Option.ReferenceTag != null))
            {
                PropertyConfig link = lc_entity.LastProperties.FirstOrDefault(p => p.Option.ReferenceTag != null && p.Option.ReferenceTag.Contains(property.Option.ReferenceTag));
                if (link == null)
                {
                    string tag = $"{es_entity.Option.ReferenceTag},{property.CppType},{property.Name}";
                    link = lc_entity.LastProperties.FirstOrDefault(p => p.Option.ReferenceTag != null && p.Option.ReferenceTag.Contains(tag));
                }
                if (link == null)
                {
                    link = lc_entity.LastProperties.FirstOrDefault(p => p.Option.ReferenceTag != null && p.Option.ReferenceTag.Contains(property.Name));
                }
                if (link != null)
                {
                    maps.Add(property, link);
                }
            }

            var code = new StringBuilder();
            code.Append($@"
/**
* @brief ��{es_entity.Caption}������ʢ�ṹ�ӱ��ؽṹ����
* @param {{{lc_entity.Name}}} lc_field {lc_entity.Caption}-���ؽṹ
* @param {{{es_entity.Name}}} es_field {es_entity.Caption}-��ʢ�ṹ
* @return ��
*/
void CopyFromEs(const {es_entity.Name}* es_field , {lc_entity.Name}* lc_field)
{{
    memset(lc_field, 0, sizeof({lc_entity.Name}));
    if(es_field == nullptr)
        return;");


            foreach (var item in maps)
            {
                if (IsEnumCode(item.Value))
                {
                    EnumFieldCopyFromTypedef(code, item.Value, $"es_field->{item.Key.Name}", $"lc_field->{item.Value.Name}");
                }
                else if (item.Key.CppLastType == "char" && item.Value.CppLastType == "tm")
                {
                    code.Append(FriendFieldCopy_Tm_FromEs(item.Value, $"es_field->{item.Key.Name}", $"lc_field->{item.Value.Name}"));
                }
                else if (item.Value.IsIntDecimal)
                {
                    code.Append(FriendFieldCopy_IntDecimal_FromEs(item.Value, $"es_field->{item.Key.Name}", $"lc_field->{item.Value.Name}"));
                }
                else
                {
                    FriendFieldCopy(code, item.Key, item.Value, "es_field", "lc_field", "CopyFromEs");
                }
            }

            code.Append($@"
}}

/**
* @brief ��{es_entity.Parent.Caption}������ʢ�ṹ���Ƶ����ؽṹ
* @param {{{lc_entity.Name}}} lc_field {lc_entity.Caption}-���ؽṹ
* @param {{{es_entity.Name}}} es_field {es_entity.Parent.Caption}-��ʢ�ṹ
* @return ��
*/
void CopyToEs(const {lc_entity.Name}* lc_field,{es_entity.Name}* es_field)
{{
    memset(es_field, 0, sizeof({es_entity.Name}));
    if(lc_field == nullptr)
        return;");

            foreach (var item in maps)
            {
                if (IsEnumCode(item.Value))
                    EnumFieldCopyToTypedef(code, item.Value, $"lc_field->{item.Value.Name}", $"es_field->{item.Key.Name}");
                else if (item.Value.IsIntDecimal)
                {
                    code.Append(FriendFieldCopy_IntDecimal_ToEs(item.Value, $"lc_field->{item.Key.Name}", $"es_field->{item.Value.Name}"));
                }
                else if (item.Key.CppLastType == "char" && item.Value.CppLastType == "tm")
                {
                    code.Append(FriendFieldCopy_Tm_ToEs(item.Value, $"es_field->{item.Key.Name}", $"lc_field->{item.Value.Name}"));
                }
                else
                    FriendFieldCopy(code, item.Value, item.Key, "lc_field", "es_field", "CopyToEs");
            }
            code.Append(@"
}");
            return code.ToString();
        }

        #region ���ʹ���

        private static bool IsEnumCode(PropertyConfig srcField)
        {
            if (srcField.EnumConfig == null)
                return false;
            var type = CppProject.Instance.GetTypedefByTag(srcField.EnumConfig.Option.ReferenceTag);
            return type != null;
        }
        public static bool EnumFieldCopyToTypedef(StringBuilder code, PropertyConfig srcField, string src_field_name,
            string dest_field_name)
        {
            var type = CppProject.Instance.GetTypedefByTag(srcField.EnumConfig.Option.ReferenceTag);
            code.Append($@"
    switch({src_field_name})//{srcField.Caption}
    {{");
            foreach (var item in srcField.EnumConfig.Items)
            {
                if (string.IsNullOrWhiteSpace(item.Option.ReferenceTag))
                {
                    continue;
                }
                var tag = item.Option.ReferenceTag.Split(',').Last();
                if (!type.Items.TryGetValue(tag, out EnumItem titem))
                {
                    code.Append($@"
    case {srcField.EnumConfig.Name}Classify::{item.Name}://{item.Caption}
        {dest_field_name} = '����';break;");
                }
                else
                {
                    var vl = titem.Value.Trim('\'');
                    if (string.IsNullOrWhiteSpace(vl))
                        continue;
                    code.Append($@"
    case {srcField.EnumConfig.Name}Classify::{item.Name}://{item.Caption}
        {dest_field_name} = '{vl}';break;");
                }
            }
            code.Append($@"
    default:
        {dest_field_name} = '\0';break;
    }}");
            return true;
        }

        public static bool EnumFieldCopyFromTypedef(StringBuilder code, PropertyConfig srcField, string src_field_name,
            string dest_field_name)
        {
            if (srcField.EnumConfig == null)
                return false;
            var type = CppProject.Instance.GetTypedefByTag(srcField.EnumConfig.Option.ReferenceTag);
            if (type == null)
                return false;
            code.Append($@"
    switch({src_field_name})//{srcField.Caption}
    {{");
            foreach (var item in type.Items.Values)
            {
                var eitem = srcField.EnumConfig.Items.FirstOrDefault(p => p.Option.ReferenceTag != null && p.Option.ReferenceTag.Contains(item.Name));
                var vl = item.Value.Trim('\'');
                if (string.IsNullOrWhiteSpace(vl))
                    continue;
                if (eitem != null)
                {
                    code.Append($@"
    case '{vl}'://{item.Caption}
        {dest_field_name} = {srcField.EnumConfig.Name}Classify::{eitem.Name};break;");
                }
                else
                {
                    code.Append(
                        $@"
    case '{item.Value.Trim(NoneLanguageChar)}':
        {dest_field_name} = err;break;");
                }
            }
            code.Append($@"
    default:
        {dest_field_name} = {srcField.EnumConfig.Name}Classify::None;break;
    }}");
            return true;
        }

        public static void FriendFieldCopy(StringBuilder code, PropertyConfig srcField, PropertyConfig destField,
            string src, string dest, string cpName)
        {
            code.Append(FriendFieldCopy(srcField, destField, $"{src}->{srcField.Name}", $"{dest}->{destField.Name}",
                cpName));
        }
        public static string FriendFieldCopy_Tm_FromEs(PropertyConfig srcField, string src_field_name, string dest_field_name)
        {
            if (!string.IsNullOrEmpty(srcField.ArrayLen))
            {
                return $@"
    for(int idx = 0;idx < {srcField.ArrayLen};idx++)//{srcField.Caption}
        {dest_field_name}[idx] = string2time({src_field_name}[idx]);";
            }
            return ($@"
    {dest_field_name} = string2time({src_field_name});//{srcField.Caption}");
        }
        public static string FriendFieldCopy_Tm_ToEs(PropertyConfig srcField, string src_field_name, string dest_field_name)
        {
            if (!string.IsNullOrEmpty(srcField.ArrayLen))
            {
                return $@"
    for(int idx = 0;idx < {srcField.ArrayLen};idx++)//{srcField.Caption}
        time2string({dest_field_name}[idx] ,{src_field_name}[idx]);";
            }
            return ($@"
    time2string({src_field_name},{dest_field_name});//{srcField.Caption}");
        }
        public static string FriendFieldCopy_IntDecimal_FromEs(PropertyConfig srcField, string src_field_name, string dest_field_name)
        {
            if (!string.IsNullOrEmpty(srcField.ArrayLen))
            {
                return $@"
    for(int idx = 0;idx < {srcField.ArrayLen};idx++)//{srcField.Caption}
        {dest_field_name}[idx] = DoubleToInt64({src_field_name}[idx]);";
            }
            return ($@"
    {dest_field_name} = DoubleToInt64({src_field_name});//{srcField.Caption}");
        }
        public static string FriendFieldCopy_IntDecimal_ToEs(PropertyConfig srcField, string src_field_name, string dest_field_name)
        {
            if (!string.IsNullOrEmpty(srcField.ArrayLen))
            {
                return $@"
    for(int idx = 0;idx < {srcField.ArrayLen};idx++)//{srcField.Caption}
        {dest_field_name}[idx] = Int64ToDouble({src_field_name}[idx]);";
            }
            return ($@"
    {dest_field_name} = Int64ToDouble({src_field_name});//{srcField.Caption}");
        }

        public static string FriendFieldCopy(PropertyConfig srcField, PropertyConfig destField, string src_field_name,
            string dest_field_name, string cpName)
        {
            var type = CppTypeHelper.ToCppLastType(srcField.CppLastType ?? srcField.CppType);
            var stru = type as EntityConfig;
            if (stru != null)
            {
                if (srcField.CppLastType == destField.CppLastType)
                    return
                        $@"
    memcpy(&{dest_field_name},&{src_field_name},sizeof({destField.CppLastType}));//{
                            srcField.Caption}";
                return $@"
    {cpName}(&{src_field_name},&{dest_field_name});//{srcField.Caption}";

            }
            if (srcField.CppLastType == "char")
            {
                if (srcField.Datalen > 1)
                    return $@"
	strcpy_s({dest_field_name},{src_field_name});//{srcField.Caption}";
                return ($@"
    {dest_field_name} = {src_field_name};//{srcField.Caption}");
            }
            if (!CppTypeHelper.IsCppBaseType(srcField.CppLastType))
            {
                if (!string.IsNullOrEmpty(srcField.ArrayLen))
                {
                    return
                        ($@"
    if({src_field_name} != nullptr)//{srcField.Caption
                            }
    {{
        for(int idx = 0;idx < {srcField.ArrayLen};idx++)
            memcpy({
                            dest_field_name}[idx],{src_field_name}[idx], sizeof({srcField.CppLastType}));
    }}");
                }
                return
                    ($@"
	memcpy({dest_field_name},{src_field_name}, sizeof({srcField.CppLastType}));//{
                        srcField.Caption}");
            }
            if (!string.IsNullOrEmpty(srcField.ArrayLen))
            {
                return
                    ($@"
    if({src_field_name} != nullptr)//{srcField.Caption
                        }
    {{
        for(int idx = 0;idx < {srcField.ArrayLen};idx++)
            {
                        dest_field_name}[idx] = {src_field_name}[idx];
    }}");
            }
            return ($@"
    {dest_field_name} = {src_field_name};//{srcField.Caption}");
        }

        public static void FriendFieldCopyString(StringBuilder code, PropertyConfig field,
            string src_field_name, string dest_field_name)
        {
            if (field.CppLastType == "char")
            {
                if (field.Datalen > 1)
                {
                    code.Append($@"
	strcpy_s({dest_field_name},{src_field_name});//{field.Caption}");
                    return;
                }
            }
            else if (!string.IsNullOrEmpty(field.ArrayLen))
            {
                code.Append(
                    $@"
    if({dest_field_name} != nullptr)//{field.Caption
                        }
    {{
        std::vector<std::string> words;
        boost::split(words, arg.Argument, boost::is_any_of(_T("",��"")));
        int idx = 0;
		for(auto word : words)
            {
                        dest_field_name}[idx++] = boost::lexical_cast<{field.CppLastType}>(word);
    }}");
                return;
            }
            code.Append(
                $@"
    {dest_field_name} =  boost::lexical_cast<{field.CppLastType}>({src_field_name});//{field.Caption}");
        }

        #endregion
        #endregion
        

        #region �ֶ���Ŷ���

        private string Index()
        {
            var code = new StringBuilder();
            code.Append($@"

// {Entity.Name}���ʹ���
const TYPE_INDEX TYPE_INDEX_{Entity.Name.ToUpper()} = 0x{Entity.Identity:X};");
            foreach (PropertyConfig property in Entity.CppProperty)
            {
                code.Append($@"
//��{Entity.Caption}-{property.Caption}���ֶ�����
const FIELD_INDEX FIELD_INDEX_{Entity.Name.ToUpper()}_{property.Name.ToUpper()} = {property.Identity};");
            }
            return code.ToString();
        }

        #endregion

        #region ȡ�÷����ö���

        private EntityConfig GetLcEntity(PropertyConfig field)
        {
            return CppTypeHelper.ToCppLastType(field.CppLastType ?? field.CppType) as EntityConfig;
        }
        #endregion
        #region �ı�дֵ����

        private void SetValue2(StringBuilder code, PropertyConfig field)
        {
            EntityConfig stru = GetLcEntity(field);
            if (stru != null)
            {
                code.Append($@"
        /*memcpy({field.Name},value,sizeof({stru.Name}));�޷�����*/");
                return;
            }
            if (field.CppLastType == "char")
            {
                if (field.Datalen <= 1)
                    code.Append($@"
    {{
        {field.Name} = value[0];
        return;
    }}");
                else
                    code.Append($@"
    {{
        strcpy_s({field.Name},value);
        return;
    }}");
            }
            else if (!string.IsNullOrEmpty(field.ArrayLen))
            {
                code.Append($@"
    {{
        vector<string> vStr;
        boost::split(vStr,value, boost::is_any_of("",/""), boost::token_compress_on);
        size_t idx=0;
        for (string vl : vStr)
            {field.Name}[idx] = boost::lexical_cast<{field.CppLastType}>(vl);
        return;
    }}");
            }
            else
            {
                code.Append($@"
    {{
        {field.Name} = boost::lexical_cast<{field.CppLastType}>(value);
        return;
    }}");
            }
        }
        #endregion

        #region ����������

        private string Operator()
        {
            var code = new StringBuilder();
            code.Append($@"
/**
* @brief {Entity.Name}���л����������
* @param {{{Entity.Name}&}} field {Entity.Caption}����
* @param {{PNetCommand}} cmd �������
* @return �������
*/
inline void operator  << (PNetCommand& cmd, {Entity.Name}& field)
{{
    cmd = SerializeToCommand(&field);
}}

/**
* @brief {Entity.Name} �������л����������
* @param {{{Entity.Name}&}} field {Entity.Caption}����
* @param {{PNetCommand}} cmd �������
* @return �������
*/
inline void operator >> ({Entity.Name}& field, PNetCommand& cmd)
{{
    cmd = SerializeToCommand(&field);
}}

/**
* @brief ����������ٷ����л���{Entity.Name}
* @param {{PNetCommand}} command �������
* @param {{{Entity.Name}&}} field {Entity.Caption}����
* @return ��
*/
inline void operator >> (PNetCommand cmd, {Entity.Name}& field)
{{
    Deserialize(cmd, &field);
}}

/**
* @brief �ӷ����л��������л���{Entity.Name}
* @param {{PNetCommand}} cmd �������
* @param {{{Entity.Name}&}} field {Entity.Caption}����
* @return ��
*/
inline void operator << ({Entity.Name}& field, PNetCommand cmd)
{{
    Deserialize(cmd, &field);
}}");

            return code.ToString();
        }

        #endregion

        #region �������

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Struct_cpp";

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var h = Project.NameSpace?.Replace('.', '_').ToUpper() ?? "";
            var code = new StringBuilder();
            code.Append("#pragma once");
            if (Entity.IsReference)
            {
                code.Append($@"
#ifdef {Entity.Parent.Name}");
            }
            code.Append($@"
#ifndef _{h}_{Entity.Name.ToUpper()}_H
#define _{h}_{Entity.Name.ToUpper()}_H
#pragma unmanaged

#include <stdafx.h>");
            if (!Entity.IsReference && !Entity.NoDataBase)
            {
                code.Append(@"
#include <DataModel/ModelBase.h>
#include <NetCommand/command_def.h>");
            }
            if (Entity.IsReference)
            {
                code.Append(@"
#include ""Es/EsForeignApiStruct.h""
#include ""Es/TapQuoteAPIDataType.h""
using namespace ESForeign;");
            }
            code.Append($@"
{FriendInc(Entity)}
using namespace std;
using namespace Agebull::Tson;
");
            using (var scope = CppNameSpaceScope.CreateScope(code, Project.NameSpace))
            {
                scope.Append(Index());

                if (!Entity.IsReference)
                {
                    scope.Append($@"
struct {Entity.Name};");
                }
                scope.Append(FunctionDef());
                scope.Append(StructCode());
                scope.Append(Operator());
                //if (!Entity.IsInternal)
                //    scope.Append(ClrHelperDef());
            }
            if (Entity.IsReference)
                code.Append(FriendDef(Entity));
            //if (!Entity.IsInternal)
            //    code.Append(ClrDef());

            code.Append(@"
#endif");
            if (Entity.IsReference)
            {
                code.Append(@"
#endif");
            }
            SaveCode(Path.Combine(path, Entity.Name + ".h"), code.ToString());
        }


        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string file = Path.Combine(path, Entity.Name + ".cpp");
            var code = new StringBuilder();
            code.Append($@"#include <stdafx.h>
#include ""{Entity.Name}.h""
{FriendInc(Entity)}
using namespace std;
using namespace Agebull::Tson;

#pragma unmanaged
");

            using (var scope = CppNameSpaceScope.CreateScope(code, Project.NameSpace))
            {
                scope.Append(Function());
                scope.Append(SetValueFromDb());
                //scope.Append(ClrHelper());
            }
            code.Append(Friend(Entity));

            //code.Append(Clr());

            SaveCode(file, code.ToString());
        }

        #endregion

    }
}

