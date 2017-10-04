using System.IO;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class ClrCoder : CoderWithEntity
    {
        #region ClrHelper

        private string ClrHelperDef()
        {
            if (Entity.IsInternal || Entity.IsReference)
                return null;
            return $@"
#ifndef CLIENT
/**
* @brief {Entity.Name}�й�ת��������
*/
public ref class {Entity.Name}Helper
{{

public:
	//��������ָ��
	unsigned char* m_buffer;
	//�������ݳ���
	size_t m_buffer_len;
public:
	/**
	* @brief {Entity.Name}���л��������ڴ�
	* @param {{{Entity.EntityName}^}} cs_field {Entity.Caption}�й���
	*/
	void WriteTo({Entity.EntityName}^ cs_field);

	/**
	* @brief ���챾���ڴ����ڽ����������ݸ���
	* @param {{size_t}} len {Entity.Caption}�й���
	* @return ��
	*/
	void CreateBuffer(size_t len);

	/**
	* @brief ����׼���õı�������ָ�빹��{Entity.Name}�йܶ���
	* @return {{{Entity.EntityName}^}} {Entity.Caption}�й���
	*/
	{Entity.EntityName}^ ReadFrom();

#ifdef WEB
public:
	/**
	* @brief �������޸ķ��͵����׷�����
	* @param {{{Entity.EntityName}^}} cs_field {Entity.Caption}�й���
	*/
	static void SendChanged(Manage::{Entity.EntityName}^ cs_field);
#endif

	/**
	* @brief  ����
	*/
	{Entity.Name}Helper()
		: m_buffer(nullptr)
		, m_buffer_len(0)
	{{

    }}

	/**
	* @brief  ����
	*/
	void Disponse()
	{{
		if (m_buffer != nullptr)
			delete[] m_buffer;
		m_buffer = nullptr;
	}}

	/**
	* @brief  ����
	*/
	~{Entity.Name}Helper()
	{{
        Disponse();
	}}
}};";
        }

        private string ClrHelper()
        {
            if (Entity.IsInternal || Entity.IsReference)
                return null;
            return $@"
/**
* @brief {Entity.Name}���л��������ڴ�
* @param {{{Entity.EntityName} ^}} cs_field {Entity.Caption}�й���
*/
void {Entity.Name}Helper::WriteTo({Entity.EntityName}^ cs_field)
{{
	if (m_buffer != nullptr)
		delete[] m_buffer;
	{Entity.Name} field;
	CopyFromClr(cs_field, field);
	Serializer writer;
	writer.CreateBuffer(TSON_BUFFER_LEN_{Entity.Name.ToUpper()}, false);
	Serialize(writer, &field);
	m_buffer = (unsigned char*)writer.GetBuffer();
	m_buffer_len = writer.GetDataLen() + 2;
}}

/**
* @brief ���챾���ڴ����ڽ����������ݸ���
* @param {{size_t}} len {Entity.Caption}�й���
* @return ��
*/
void {Entity.Name}Helper::CreateBuffer(size_t len)
{{
	if (m_buffer != nullptr)
		delete[] m_buffer;
	m_buffer = new unsigned char[len];
	m_buffer_len = len;
}}

/**
* @brief ����׼���õı�������ָ�빹��{Entity.Name}�йܶ���
* @return {{{Entity.EntityName} ^}} {Entity.Caption}�й���
*/
{Entity.EntityName}^ {Entity.Name}Helper::ReadFrom()
{{
    assert(m_buffer != nullptr);
	{Entity.Name} field;
	Deserialize((char*)m_buffer, m_buffer_len, &field);
	return CopyToClr(field);
}}

#ifdef WEB
/**
* @brief �������޸ķ��͵����׷�����
* @param {{{Entity.EntityName}^}} cs_field {Entity.Caption}�й���
*/
void {Entity.Name}Helper::SendChanged({Entity.EntityName}^ cs_field)
{{
    {Entity.Name} field;
	CopyFromClr(cs_field, field);
	auto cmd_arg = SerializeToCommand(&field);
	cmd_arg->cmd_id = NET_COMMAND_DATA_CHANGED;
	request_net_cmmmand(cmd_arg, nullptr);
}}
#endif";
        }

        #endregion

        #region �������йܻ���

        private string ClrDef()
        {
            var ns = "";
            if (!string.IsNullOrWhiteSpace(Project.NameSpace))
                ns = $"using namespace {Project.NameSpace.Replace(".", "::")};";
            var code = new StringBuilder();
            code.Append($@"
//CLR ȫ�ַ�������ʹ�������ռ�
{ns}

/**
* @brief {Entity.Name}���й��ิ��
* @param {{{Entity.EntityName}^}} cs_field {Entity.Caption}�й���
* @param {{{Entity.Name}&}} field {Entity.Caption}������
* @return ��
*/
void CopyFromClr({Entity.EntityName}^ cs_field, {Entity.Name}& field);

/**
* @brief {Entity.Name}���Ƶ��й���
* @param {{{Entity.Name}&}} field {Entity.Caption}������
* @return {{{Entity.EntityName}^}}{Entity.Caption}�й���
*/
{Entity.EntityName}^ CopyToClr({Entity.Name}& field);");
            return code.ToString();
        }

        private string Clr()
        {
            if (Entity.IsInternal || Entity.IsReference)
                return "";
            var ns = "";
            if (!string.IsNullOrWhiteSpace(Project.NameSpace))
                ns = $"using namespace {Project.NameSpace.Replace(".", "::")};";
            var code = new StringBuilder();
            code.Append($@"
{ns}

/**
* @brief {Entity.Name}���й��ิ��
* @param {{{Entity.EntityName}^}} cs_field {Entity.Caption}�й���
* @param {{{Entity.Name}&}} field {Entity.Caption}������
* @return ��
*/
void CopyFromClr({Entity.EntityName}^ cs_field, {Entity.Name}& field)
{{
    memset(&field, 0, sizeof({Entity.Name}));
    if(cs_field == nullptr)
        return;");


            foreach (var property in Entity.CppProperty.Where(p => p.CanGet))
                CopyFromClr(code, property);

            code.Append($@"
}}

/**
* @brief {Entity.Name}���Ƶ��й���
* @param {{{Entity.Name}&}} field {Entity.Caption}������
* @return {{{Entity.EntityName}^}}{Entity.Caption}�й���
*/
{Entity.EntityName}^ CopyToClr({Entity.Name}& field)
{{
    {Entity.EntityName}^ cs_field = gcnew {Entity.EntityName}();");

            foreach (var property in Entity.CppProperty.Where(p => p.CanSet))
                CopyToClrCode(code, property);


            code.Append(@"
	return cs_field;
}");
            return code.ToString();
        }

        private void CopyFromClr(StringBuilder code, PropertyConfig field)
        {
            if (field.IsIntDecimal)
            {
                if (!string.IsNullOrEmpty(field.ArrayLen))
                    code.AppendFormat(@"
    if(cs_field->{0} != nullptr)
    {{
        for(size_t idx = 0;idx < cs_field->{0}->Count;idx++)//{1}
        {{
            field.{0}[idx] = cs_field->FromDecimal(cs_field->{0}[idx]);
        }}
    }}", field.Name, field.Caption);
                else
                    code.Append($@"
    field.{field.Name} = cs_field->FromDecimal(cs_field->{field.Name});//{field.Caption}");
                return;
            }
            var stru = GetLcEntity(field);
            if (stru != null)
            {
                code.AppendFormat(@"
    
    CopyFromClr(cs_field->{0},field.{0});//{1}-{2}", field.Name, field.Caption, field.CsType);
                return;
            }
            if (field.CppLastType == "char")
                if (field.Datalen > 1)
                    code.AppendFormat(@"
    if(cs_field->{0} != nullptr)
    {{
        const char* c_{0} = (const char*)(Marshal::StringToHGlobalAnsi(cs_field->{0})).ToPointer();//{1}
	    strcpy_s(field.{0}, c_{0});
	    Marshal::FreeHGlobal(IntPtr((void*)c_{0}));
    }}", field.Name, field.Caption);
                else
                    code.AppendFormat(@"
    if(cs_field->{0} != nullptr)
    {{
        const char* c_{0} = (const char*)(Marshal::StringToHGlobalAnsi(cs_field->{0})).ToPointer();//{1}
	    field.{0} = c_{0}[0];
	    Marshal::FreeHGlobal(IntPtr((void*)c_{0}));
    }}", field.Name, field.Caption);
            else if (!string.IsNullOrEmpty(field.ArrayLen))
                code.AppendFormat(@"
    if(cs_field->{0} != nullptr)
    {{
        for(size_t idx = 0;idx < cs_field->{0}->Count;idx++)//{1}
        {{
            field.{0}[idx] = cs_field->{0}[idx];
        }}
    }}", field.Name, field.Caption);
            else if (field.CppLastType == "tm")
                code.AppendFormat(@"
    FromClr(field.{0},cs_field->{0});//{1}-{2}", field.Name, field.Caption, field.CsType);
            else if (field.CustomType != null)
                code.Append($@"
    field.{field.Name} = ({field.CustomType}Classify)(int)cs_field->{field.Name};//{field.Caption}");
            else
                code.Append($@"
    field.{field.Name} = cs_field->{field.Name};//{field.Caption}");
        }

        private void CopyToClrCode(StringBuilder code, PropertyConfig field)
        {
            if (field.IsIntDecimal)
            {
                if (!string.IsNullOrEmpty(field.ArrayLen))
                    code.AppendFormat(@"
    cs_field->{0} =  gcnew {1}();//{2}-{3}
    for(auto vl : field.{0})
        cs_field->{0}->Add(cs_field->ToDecimal(vl));", field.Name, field.CppLastType, field.Caption, field.CsType);
                else
                    code.Append($@"
    cs_field->{field.Name} = cs_field->ToDecimal(field.{field.Name});//{field.Caption}-{field.CsType}");
                return;
            }
            var stru = GetLcEntity(field);
            if (stru != null)
            {
                code.AppendFormat(@"
    cs_field->{0} = CopyToClr(field.{0});//{1}-{2}", field.Name, field.Caption, field.CsType);
                return;
            }
            if (field.CppLastType == "char")
                if (field.Datalen <= 1)
                    code.AppendFormat(@"
    buf[0] = field.{0};//{1}-{2}
    cs_field->{0} =  marshal_as<String^>(buf);", field.Name, field.Caption, field.CsType);
                else
                    code.AppendFormat(@"
    if(strlen(field.{0}) > 0)//{1}-{2}
        cs_field->{0} =  marshal_as<String^>(field.{0});", field.Name, field.Caption, field.CsType);
            else if (!string.IsNullOrEmpty(field.ArrayLen))
                code.AppendFormat(@"
    cs_field->{0} =  gcnew {1}();//{2}-{3}
    for(auto vl : field.{0})
        cs_field->{0}->Add(vl);", field.Name, field.CppLastType, field.Caption, field.CsType);
            else if (field.CppLastType == "tm")
                code.AppendFormat(@"
    cs_field->{0} = ToClr(field.{0});//{1}-{2}", field.Name, field.Caption, field.CsType);
            else if (field.CustomType != null)
                code.Append($@"
    cs_field->{field.Name} = (Manage::{field.CustomType})static_cast<int>(field.{field.Name});//{field.Caption}-{
                        field.CsType
                    }");
            else
                code.Append($@"
    cs_field->{field.Name} = field.{field.Name};//{field.Caption}-{field.CsType}");
        }

        #endregion


        #region ����

        private static string FriendInc(EntityConfig entity)
        {
            var code = new StringBuilder();
            foreach (var pro in entity.CppProperty)
            {
                var friend = CppTypeHelper.ToCppLastType(pro.CppLastType ?? pro.CppType) as EntityConfig;
                if (friend != null)
                    code.Append($@"
#include <{friend.Parent.Name}/{friend.Name}.h>
#include <{friend.Parent.Name}/{friend.Name}_clr.h>");
            }
            return code.ToString();
        }

        private static EntityConfig GetLcEntity(PropertyConfig field)
        {
            return CppTypeHelper.ToCppLastType(field.CppLastType ?? field.CppType) as EntityConfig;
        }

        #endregion

        #region �������


        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Clr_cpp";

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.IsReference || Entity.IsInternal)
                return;
            var h = Project.NameSpace?.Replace('.', '_').ToUpper() ?? "";
            var code = new StringBuilder();
            code.Append($@"#pragma once
#ifdef CLR
#ifndef _{h}_{Entity.Name.ToUpper()}_CLR_H
#define _{h}_{Entity.Name.ToUpper()}_CLR_H
#pragma unmanaged
#include <stdafx.h>
#include ""{Entity.EntityName}.h""");
            if (!Entity.IsClass)
                code.Append(@"
#include <DataModel/ModelBase.h>
#include <NetCommand/command_def.h>");

            code.Append($@"
{FriendInc(Entity)}
using namespace std;
using namespace Agebull::Tson;

#pragma managed
/*-------------------------------CLR���뿪ʼ-----------------------------------*/
#include <msclr\marshal.h>

using namespace System;
using namespace msclr::interop;
using namespace Runtime::InteropServices;
");
            code.Append(ClrDef());
            using (var scope = CppNameSpaceScope.CreateScope(code, Project.NameSpace))
            {
                scope.Append($@"
struct {Entity.Name};
#ifndef CLIENT
{ClrHelperDef()}
#endif");
            }

            code.Append(@"
#pragma unmanaged
/*-------------------------------CLR�������-----------------------------------*/
#endif
#endif");
            SaveCode(Path.Combine(path, Entity.EntityName + "_clr.h"), code.ToString());
        }


        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            if (Entity.IsReference || Entity.IsInternal)
                return;
            var code = new StringBuilder();
            code.Append($@"#ifdef CLR
#include <stdafx.h>
#include ""{Entity.EntityName}_clr.h""
{FriendInc(Entity)}
using namespace std;
using namespace Agebull::Tson;

#pragma managed
/*-------------------------------CLR���뿪ʼ-----------------------------------*/
#include <msclr\marshal.h>

using namespace System;
using namespace msclr::interop;
using namespace Runtime::InteropServices;");
            code.Append(Clr());
            using (var scope = CppNameSpaceScope.CreateScope(code, Project.NameSpace))
            {
                scope.Append($@"
#ifndef CLIENT
{ClrHelper()}
#endif");
            }
            code.Append(@"
#pragma unmanaged
/*-------------------------------CLR�������-----------------------------------*/
#endif");
            var file = Path.Combine(path, Entity.EntityName + "_clr.cpp");
            SaveCode(file, code.ToString());
        }

        #endregion
    }
}