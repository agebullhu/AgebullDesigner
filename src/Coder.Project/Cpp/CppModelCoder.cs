using System.IO;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class CppModelCoder : CoderWithEntity
    {
        #region ��ʵ�ִ���

        private string ClassCode(int space)
        {

            var code = new StringBuilder();
            code.Append(
                $@"
/**
* @brief �����޸��¼�,�����߼�����
*/
void {Entity.Name}Model::OnModify()
{{
    /*");

            foreach (PropertyConfig property in Entity.CppProperty)
            {
                code.Append($@"
    if(is_modified[FIELD_INDEX_{Entity.Name.ToUpper()}_{property.Name.ToUpper()}])
    {{
        //TO DO:{property.Caption}�޸��߼�
    }}");
            }

            code.Append($@"
    */
    //TO DO:�����޸��¼��߼�����
}}");
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', space);
            return code.Replace("\n", "\n" + sb).ToString();
        }

        #endregion

        #region ����������ͷ

        private string ClassDef(int space)
        {

            var code = new StringBuilder();
            code.Append($@"
/**
* @brief {Entity.Name}����ģ�ͷ�װ��
*/
class {Entity.Name}Model : public ModelBase<{Entity.Name},{Entity.CppProperty.Count()}>
{{
public:
	/**
	* @brief Ĭ�Ϲ���
	* @param {{{Entity.Name}}} state ����״̬,Ĭ��Ϊ����
	*/
	{Entity.Name}Model(DATA_STATE state = DATA_STATE_NEW)
		: ModelBase(state)
	{{
	}}
	/**
	* @brief ���ƹ���
	* @param {{{Entity.Name}}} data ���ݶ���
	* @param {{DATA_STATE}} state ����״̬,Ĭ��Ϊ�Ѵ���
	*/
	{Entity.Name}Model({Entity.Name} data, DATA_STATE state = DATA_STATE_EXIST)
		: ModelBase(data, state)
	{{
	}}");

            foreach (PropertyConfig property in Entity.CppProperty)
            {
                var type = property.CppLastType == "char" && property.Datalen > 0 ? "const char*" : property.CppLastType;
                code.Append($@"
    /**
    * @brief ȡ��{property.Caption}
    * @return {{{type}}} {Entity.Caption}ֵ
    */
    {type} get_{property.Name}() const
    {{
        return m_data.{property.Name};
    }}

    /**
    * @brief ����{property.Caption}
    * @param {{{type}}} value ֵ
    */
    void set_{property.Name}({type} value)
    {{
		 set_modified(FIELD_INDEX_{Entity.Name.ToUpper()}_{property.Name.ToUpper()});");
                SetValue(code, property);
                code.Append(@"
    }");
            }
            code.Append($@"
	/**
	* @brief �����޸��¼�,�����߼�����
	*/
	virtual void OnModify() override;
}};");
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', space);
            return code.Replace("\n", "\n" + sb).ToString();
        }

        private static void SetValue(StringBuilder code, PropertyConfig field)
        {
            var type = CppTypeHelper.ToCppLastType(field.CppLastType);
            var stru = type as EntityConfig;
            if (stru != null)
            {
                code.Append($@"
        memcpy(m_data.{field.Name},value,sizeof({stru.Name}));");
                return;
            }
            var typedef = type as TypedefItem;
            var keyword = typedef == null ? type.ToString() : typedef.KeyWork;
            var isArray = typedef == null ? field.Datalen > 0 : typedef.ArrayLen != null;
            if (keyword == "char" && isArray)
            {
                if (field.Datalen == 1 || typedef?.ArrayLen == "1")
                    code.Append($@"
        m_data.{field.Name} = value;");
                else
                    code.Append($@"
        strcpy_s(m_data.{field.Name},value);");
            }
            else if (isArray)
            {
                code.Append($@"
        memcpy(m_data.{field.Name},value,sizeof({field.Name}));");
            }
            else
            {
                code.Append($@"
        m_data.{field.Name} = value;");
            }
        }
        #endregion

        #region �������


        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Model_cpp";

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var code = new StringBuilder();
            code.Append($@"
#ifndef _{Entity.Name.ToUpper()}_MODEL_H
#define _{Entity.Name.ToUpper()}_MODEL_H
#pragma once

#include ""{Entity.Name}.h""
#include ""../DataModel/ModelBase.h""
using namespace GBS::Futures::DataModel;
");
            int sapce = 0;
            var ns = Project.NameSpace.Split('.');
            foreach (var name in ns)
            {
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append($@"namespace {name}");
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('{');
                sapce += 4;
            }

            code.Append(ClassDef(sapce));

            for (int index = 0; index < ns.Length; index++)
            {
                sapce -= 4;
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('}');
            }

            code.Append($@"
#endif");


            SaveCode(Path.Combine(path, Entity.Name + "Model.h"), code.ToString());
        }


        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            string file = Path.Combine(path, Entity.Name + "Model.cpp");

            var code = new StringBuilder();
            code.Append($@"#include <stdafx.h>
#include ""{Entity.Name}Model.h""
using namespace GBS::Futures::DataModel;

");
            int sapce = 0;
            var ns = Project.NameSpace.Split('.');
            foreach (var name in ns)
            {
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append($@"namespace {name}");
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('{');
                sapce += 4;
            }

            code.Append(ClassCode(sapce));

            for (int index = 0; index < ns.Length; index++)
            {
                sapce -= 4;
                code.AppendLine();
                code.Append(' ', sapce);
                code.Append('}');
            }


            SaveCode(file, code.ToString());
        }

        #endregion

    }
}

