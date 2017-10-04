using System.IO;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    public sealed class CppModelCoder : CoderWithEntity
    {
        #region 类实现代码

        private string ClassCode(int space)
        {

            var code = new StringBuilder();
            code.Append(
                $@"
/**
* @brief 引发修改事件,进行逻辑处理
*/
void {Entity.Name}Model::OnModify()
{{
    /*");

            foreach (PropertyConfig property in Entity.CppProperty)
            {
                code.Append($@"
    if(is_modified[FIELD_INDEX_{Entity.Name.ToUpper()}_{property.Name.ToUpper()}])
    {{
        //TO DO:{property.Caption}修改逻辑
    }}");
            }

            code.Append($@"
    */
    //TO DO:整体修改事件逻辑处理
}}");
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', space);
            return code.Replace("\n", "\n" + sb).ToString();
        }

        #endregion

        #region 类声明代码头

        private string ClassDef(int space)
        {

            var code = new StringBuilder();
            code.Append($@"
/**
* @brief {Entity.Name}数据模型封装类
*/
class {Entity.Name}Model : public ModelBase<{Entity.Name},{Entity.CppProperty.Count()}>
{{
public:
	/**
	* @brief 默认构造
	* @param {{{Entity.Name}}} state 数据状态,默认为新增
	*/
	{Entity.Name}Model(DATA_STATE state = DATA_STATE_NEW)
		: ModelBase(state)
	{{
	}}
	/**
	* @brief 复制构造
	* @param {{{Entity.Name}}} data 数据对象
	* @param {{DATA_STATE}} state 数据状态,默认为已存在
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
    * @brief 取得{property.Caption}
    * @return {{{type}}} {Entity.Caption}值
    */
    {type} get_{property.Name}() const
    {{
        return m_data.{property.Name};
    }}

    /**
    * @brief 设置{property.Caption}
    * @param {{{type}}} value 值
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
	* @brief 引发修改事件,进行逻辑处理
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

        #region 主体代码


        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Model_cpp";

        /// <summary>
        ///     生成实体代码
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
        ///     生成扩展代码
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

