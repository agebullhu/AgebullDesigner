using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class ClrCoder : CoderWithModel
    {
        #region ClrHelper

        private string ClrHelperDef()
        {
            if (Model.IsInternal || Model.IsReference)
                return null;
            return $@"
#ifndef CLIENT
/**
* @brief {Model.Name}�й�ת��������
*/
public ref class {Model.Name}Helper
{{

public:
	//��������ָ��
	unsigned char* m_buffer;
	//�������ݳ���
	size_t m_buffer_len;
public:
	/**
	* @brief {Model.Name}���л��������ڴ�
	* @param {{{Model.EntityName}^}} cs_field {Model.Caption}�й���
	*/
	void WriteTo({Model.EntityName}^ cs_field);

	/**
	* @brief ���챾���ڴ����ڽ����������ݸ���
	* @param {{size_t}} len {Model.Caption}�й���
	* @return ��
	*/
	void CreateBuffer(size_t len);

	/**
	* @brief ����׼���õı�������ָ�빹��{Model.Name}�йܶ���
	* @return {{{Model.EntityName}^}} {Model.Caption}�й���
	*/
	{Model.EntityName}^ ReadFrom();

#ifdef WEB
public:
	/**
	* @brief �������޸ķ��͵����׷�����
	* @param {{{Model.EntityName}^}} cs_field {Model.Caption}�й���
	*/
	static void SendChanged(Manage::{Model.EntityName}^ cs_field);
#endif

	/**
	* @brief  ����
	*/
	{Model.Name}Helper()
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
	~{Model.Name}Helper()
	{{
        Disponse();
	}}
}};";
        }

        private string ClrHelper()
        {
            if (Model.IsInternal || Model.IsReference)
                return null;
            return $@"
/**
* @brief {Model.Name}���л��������ڴ�
* @param {{{Model.EntityName} ^}} cs_field {Model.Caption}�й���
*/
void {Model.Name}Helper::WriteTo({Model.EntityName}^ cs_field)
{{
	if (m_buffer != nullptr)
		delete[] m_buffer;
	{Model.Name} field;
	CopyFromClr(cs_field, field);
	Serializer writer;
	writer.CreateBuffer(TSON_BUFFER_LEN_{Model.Name.ToUpper()}, false);
	Serialize(writer, &field);
	m_buffer = (unsigned char*)writer.GetBuffer();
	m_buffer_len = writer.GetDataLen() + 2;
}}

/**
* @brief ���챾���ڴ����ڽ����������ݸ���
* @param {{size_t}} len {Model.Caption}�й���
* @return ��
*/
void {Model.Name}Helper::CreateBuffer(size_t len)
{{
	if (m_buffer != nullptr)
		delete[] m_buffer;
	m_buffer = new unsigned char[len];
	m_buffer_len = len;
}}

/**
* @brief ����׼���õı�������ָ�빹��{Model.Name}�йܶ���
* @return {{{Model.EntityName} ^}} {Model.Caption}�й���
*/
{Model.EntityName}^ {Model.Name}Helper::ReadFrom()
{{
    assert(m_buffer != nullptr);
	{Model.Name} field;
	Deserialize((char*)m_buffer, m_buffer_len, &field);
	return CopyToClr(field);
}}

#ifdef WEB
/**
* @brief �������޸ķ��͵����׷�����
* @param {{{Model.EntityName}^}} cs_field {Model.Caption}�й���
*/
void {Model.Name}Helper::SendChanged({Model.EntityName}^ cs_field)
{{
    {Model.Name} field;
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
* @brief {Model.Name}���й��ิ��
* @param {{{Model.EntityName}^}} cs_field {Model.Caption}�й���
* @param {{{Model.Name}&}} field {Model.Caption}������
* @return ��
*/
void CopyFromClr({Model.EntityName}^ cs_field, {Model.Name}& field);

/**
* @brief {Model.Name}���Ƶ��й���
* @param {{{Model.Name}&}} field {Model.Caption}������
* @return {{{Model.EntityName}^}}{Model.Caption}�й���
*/
{Model.EntityName}^ CopyToClr({Model.Name}& field);");
            return code.ToString();
        }

        private string Clr()
        {
            if (Model.IsInternal || Model.IsReference)
                return "";
            var ns = "";
            if (!string.IsNullOrWhiteSpace(Project.NameSpace))
                ns = $"using namespace {Project.NameSpace.Replace(".", "::")};";
            var code = new StringBuilder();
            code.Append($@"
{ns}

/**
* @brief {Model.Name}���й��ิ��
* @param {{{Model.EntityName}^}} cs_field {Model.Caption}�й���
* @param {{{Model.Name}&}} field {Model.Caption}������
* @return ��
*/
void CopyFromClr({Model.EntityName}^ cs_field, {Model.Name}& field)
{{
    memset(&field, 0, sizeof({Model.Name}));
    if(cs_field == nullptr)
        return;");


            foreach (var property in Model.UserProperty.Where(p => p.CanGet))
                CopyFromClr(code, property);

            code.Append($@"
}}

/**
* @brief {Model.Name}���Ƶ��й���
* @param {{{Model.Name}&}} field {Model.Caption}������
* @return {{{Model.EntityName}^}}{Model.Caption}�й���
*/
{Model.EntityName}^ CopyToClr({Model.Name}& field)
{{
    {Model.EntityName}^ cs_field = gcnew {Model.EntityName}();");

            foreach (var property in Model.UserProperty.Where(p => p.CanSet))
                CopyToClrCode(code, property);


            code.Append(@"
	return cs_field;
}");
            return code.ToString();
        }

        private void CopyFromClr(StringBuilder code, PropertyConfig property)
        {
            var field = property;
            if (field.IsIntDecimal)
            {
                if (!string.IsNullOrWhiteSpace(field.ArrayLen))
                    code.AppendFormat(@"
    if(cs_field->{0} != nullptr)
    {{
        for(size_t idx = 0;idx < cs_field->{0}->Count;idx++)//{1}
        {{
            field.{0}[idx] = cs_field->FromDecimal(cs_field->{0}[idx]);
        }}
    }}", property.Name, property.Caption);
                else
                    code.Append($@"
    field.{property.Name} = cs_field->FromDecimal(cs_field->{property.Name});//{property.Caption}");
                return;
            }
            var stru = GetLcEntity(field);
            if (stru != null)
            {
                code.AppendFormat(@"
    
    CopyFromClr(cs_field->{0},field.{0});//{1}-{2}", property.Name, property.Caption, field.CsType);
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
    }}", property.Name, property.Caption);
                else
                    code.AppendFormat(@"
    if(cs_field->{0} != nullptr)
    {{
        const char* c_{0} = (const char*)(Marshal::StringToHGlobalAnsi(cs_field->{0})).ToPointer();//{1}
	    field.{0} = c_{0}[0];
	    Marshal::FreeHGlobal(IntPtr((void*)c_{0}));
    }}", property.Name, property.Caption);
            else if (!string.IsNullOrWhiteSpace(field.ArrayLen))
                code.AppendFormat(@"
    if(cs_field->{0} != nullptr)
    {{
        for(size_t idx = 0;idx < cs_field->{0}->Count;idx++)//{1}
        {{
            field.{0}[idx] = cs_field->{0}[idx];
        }}
    }}", property.Name, property.Caption);
            else if (field.CppLastType == "tm")
                code.AppendFormat(@"
    FromClr(field.{0},cs_field->{0});//{1}-{2}", property.Name, property.Caption, field.CsType);
            else if (field.CustomType != null)
                code.Append($@"
    field.{property.Name} = ({field.CustomType}Classify)(int)cs_field->{property.Name};//{property.Caption}");
            else
                code.Append($@"
    field.{property.Name} = cs_field->{property.Name};//{property.Caption}");
        }

        private void CopyToClrCode(StringBuilder code, PropertyConfig property)
        {
            var field = property;
            if (field.IsIntDecimal)
            {
                if (!string.IsNullOrWhiteSpace(field.ArrayLen))
                    code.AppendFormat(@"
    cs_field->{0} =  gcnew {1}();//{2}-{3}
    for(auto vl : field.{0})
        cs_field->{0}->Add(cs_field->ToDecimal(vl));", property.Name, field.CppLastType, property.Caption, field.CsType);
                else
                    code.Append($@"
    cs_field->{property.Name} = cs_field->ToDecimal(field.{property.Name});//{property.Caption}-{field.CsType}");
                return;
            }
            var stru = GetLcEntity(field);
            if (stru != null)
            {
                code.AppendFormat(@"
    cs_field->{0} = CopyToClr(field.{0});//{1}-{2}", property.Name, property.Caption, field.CsType);
                return;
            }
            if (field.CppLastType == "char")
                if (field.Datalen <= 1)
                    code.AppendFormat(@"
    buf[0] = field.{0};//{1}-{2}
    cs_field->{0} =  marshal_as<String^>(buf);", property.Name, property.Caption, field.CsType);
                else
                    code.AppendFormat(@"
    if(strlen(field.{0}) > 0)//{1}-{2}
        cs_field->{0} =  marshal_as<String^>(field.{0});", property.Name, property.Caption, field.CsType);
            else if (!string.IsNullOrWhiteSpace(field.ArrayLen))
                code.AppendFormat(@"
    cs_field->{0} =  gcnew {1}();//{2}-{3}
    for(auto vl : field.{0})
        cs_field->{0}->Add(vl);", property.Name, field.CppLastType, property.Caption, field.CsType);
            else if (field.CppLastType == "tm")
                code.AppendFormat(@"
    cs_field->{0} = ToClr(field.{0});//{1}-{2}", property.Name, property.Caption, field.CsType);
            else if (field.CustomType != null)
                code.Append($@"
    cs_field->{property.Name} = (Manage::{field.CustomType})static_cast<int>(field.{property.Name});//{property.Caption}-{
                        field.CsType
                    }");
            else
                code.Append($@"
    cs_field->{property.Name} = field.{property.Name};//{property.Caption}-{field.CsType}");
        }

        #endregion


        #region ����

        private static string FriendInc(ModelConfig entity)
        {
            var code = new StringBuilder();
            foreach (var property in entity.UserProperty)
            {
                var field = property;
                if (CppTypeHelper.ToCppLastType(field.CppLastType ?? field.CppType) is ModelConfig friend)
                    code.Append($@"
#include <{friend.Parent.Name}/{friend.Name}.h>
#include <{friend.Parent.Name}/{friend.Name}_clr.h>");
            }
            return code.ToString();
        }

        private static ModelConfig GetLcEntity(FieldConfig field)
        {
            return CppTypeHelper.ToCppLastType(field.CppLastType ?? field.CppType) as ModelConfig;
        }

        private static ModelConfig GetLcEntity(PropertyConfig field)
        {
            return CppTypeHelper.ToCppLastType(field.CppLastType ?? field.CppType) as ModelConfig;
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
        protected override void CreateDesignerCode(string path)
        {
            if (Model.IsReference || Model.IsInternal)
                return;
            var h = Project.NameSpace?.Replace('.', '_').ToUpper() ?? "";
            var code = new StringBuilder();
            code.Append($@"#pragma once
#ifdef CLR
#ifndef _{h}_{Model.Name.ToUpper()}_CLR_H
#define _{h}_{Model.Name.ToUpper()}_CLR_H
#pragma unmanaged
#include <stdafx.h>
#include ""{Model.EntityName}.h""");
            if (!Model.NoDataBase)
                code.Append(@"
#include <DataModel/ModelBase.h>
#include <NetCommand/command_def.h>");

            code.Append($@"
{FriendInc(Model)}
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
struct {Model.Name};
#ifndef CLIENT
{ClrHelperDef()}
#endif");
            }

            code.Append(@"
#pragma unmanaged
/*-------------------------------CLR�������-----------------------------------*/
#endif
#endif");
            SaveCode(Path.Combine(path, Model.EntityName + "_clr.h"), code.ToString());
        }


        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            if (Model.IsReference || Model.IsInternal)
                return;
            var code = new StringBuilder();
            code.Append($@"#ifdef CLR
#include <stdafx.h>
#include ""{Model.EntityName}_clr.h""
{FriendInc(Model)}
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
            var file = Path.Combine(path, Model.EntityName + "_clr.cpp");
            SaveCode(file, code.ToString());
        }

        #endregion
    }
}