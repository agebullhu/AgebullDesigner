using Agebull.Common;
using Agebull.EntityModel.Config;
using System.IO;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{

    /// <summary>
    /// 枚举代码生成器
    /// </summary>
    public sealed class CommonBuilder : CoderWithProject
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_Enum_Base_cs";
        

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            var folder = IOHelper.CheckPath(path, "Common");

            //var dbFile =  Path.Combine(GlobalConfig.RootPath, "Templates", "GlobalDataInterfaces.cs");
            //dbFile.FileCopyTo(Path.Combine(folder, "GlobalDataInterfaces.cs"));
            var file = Path.Combine(folder,"Enums.cs");
            var code = new StringBuilder();
            
            foreach(var enumConfig in Project.Enums)
            {
                if(!enumConfig.IsOut)
                    EnumMomentCoder.EnumCode(code, enumConfig);
            }
            SaveCode(file, $@"using System;

namespace {NameSpace}
{{
    {code}
}}");
        }
        
    }

}

