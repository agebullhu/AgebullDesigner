using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class CppDataFactoryCode : FileCoder
    {

        public override ConfigBase CurrentConfig => SolutionConfig.Current;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_DataFactory_cpp";
        #region 服务器代码

        private string IncludeCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsReference))
                {
                    code.Append($@"
#include ""{entity.Parent.Name}/{entity.Name}.h""");
                }
            }
            return code.ToString();
        }
        private string CreateCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsReference))
                {
                    code.Append($@"
                case {entity.Parent.NameSpace.Replace(".", "::")}::TYPE_INDEX_{entity.Name.ToUpper()}:
				{{
					auto field = new {entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name}();
					memset(field, 0 , sizeof({entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name}));
					return field;
				}}");
                }
            }
            return code.ToString();
        }
        private string CommandCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsReference))
                {
                    code.Append($@"
                case {entity.Parent.NameSpace.Replace(".", "::")}::TYPE_INDEX_{entity.Name.ToUpper()}:
				{{
					auto field = new {entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name}();
					memset(field, 0 , sizeof({entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name}));
					Deserialize(reader, field);
					return field;
				}}");
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var code = $@"#include <stdafx.h>
#include ""ModelBase.h""
#include ""DataFactory.h""
{IncludeCode()}

using namespace std;
using namespace Agebull::Tson;

namespace GBS
{{
	namespace Futures
	{{
		namespace DataModel
		{{
			DataFactory* DataFactory::m_data_factory = nullptr;

			DataFactory* DataFactory::get_factory()
			{{
				if (m_data_factory == nullptr)
					m_data_factory = new DataFactory();
				return m_data_factory;
			}}
			void* DataFactory::create_data(int type_id)
			{{
				switch(type_id)
				{{{CreateCode()}
                }}
				return nullptr;
			}}
			void* DataFactory::get_command_data(PNetCommand command)
			{{
	            if (command->data_len < NET_COMMAND_STATE_END)
		            return nullptr;
				Deserializer reader(get_cmd_buffer(command), static_cast<size_t>(get_cmd_len(command)));
				switch (reader.GetDataType())
				{{{CommandCode()}
				}}
				return nullptr;
			}}
		}}
	}}
}}";
            SaveCode(@"C:\work\SharpCode\DataModel\DataFactory.cpp", code);
        }
        #endregion
        #region 客户端代码

        private string ClrIncludeCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsReference && !p.NoDataBase && !p.IsInternal))
                {
                    code.Append($@"
#include ""{entity.Parent.Name}/{entity.Name}.h""");
                }
            }
            return code.ToString();
        }

        private string ClrCode()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
            {
                foreach (var entity in project.Entities.Where(p => !p.IsReference && !p.NoDataBase && !p.IsInternal))
                {
                    code.Append($@"
        case {entity.Parent.NameSpace.Replace(".", "::")}::TYPE_INDEX_{entity.Name.ToUpper()}:
	    {{
		    {entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name} field;
		    memset(&field, 0 , sizeof({entity.Parent.NameSpace.Replace(".", "::")}::{entity.Name}));
		    Deserialize(reader, &field);
		    auto cs_field = CopyToClr(field);
#ifdef CLIENT_CACHE
            {entity.Parent.NameSpace.Replace(".", "::")}::CommodityData::AddToCache(cs_field);
#endif
            return cs_field;
	    }}");
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var code = $@"#include <stdafx.h>
#include ""ModelBase.h""
#include ""DataFactory.h""
{ClrIncludeCode()}

using namespace std;
using namespace Agebull::Tson;
#pragma managed
System::Object^ get_command_clr_data(PNetCommand command, int& type)
{{
	if (command->data_len < NET_COMMAND_STATE_END)
		return nullptr;
	Deserializer reader(get_cmd_buffer(command), static_cast<size_t>(get_cmd_len(command)));
	switch (type=reader.GetDataType())
	{{{ClrCode()}
	}}
	return nullptr;
}}";
            SaveCode(@"C:\work\SharpCode\DataModel\DataFactory_CLR.cpp", code);
        }
        #endregion
    }
}

