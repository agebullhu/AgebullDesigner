using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class MobileGlobalCoder : CoderWithProject
    {

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_MobileGlobal_CS";

        #region 主体代码

        /// <summary>
        ///     生成实体代码
        /// </summary>
        /// <remarks></remarks>
        protected override void CreateBaCode(string path)
        {
            var code = $@"using System;
using System.Collections.Generic;
using System.Text;
using Agebull.EntityModel;
using Agebull.ZmqCommand;

namespace {SolutionConfig.Current.NameSpace}
{{
    
    /// <summary>
    ///     {SolutionConfig.Current.Caption}的全局内容
    /// </summary>
    public class {SolutionConfig.Current.Name}Option
    {{
        #region 注册类型
        
        /// <summary>
        /// 注册类型
        /// </summary>
        public static void ReigsterEntityType()
        {{{ReigsterEntityType()}
        }}
        #endregion
    }}
}}";

            SaveCode(Path.Combine(Path.GetDirectoryName(path), SolutionConfig.Current.Name + "Option.cs"), code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {

        }

        #endregion

        #region 属性

        private string ReigsterEntityType()
        {
            var code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects.Where(p => !p.IsReference))
            {
                code.Append($@"

            //{project.Caption}");
                foreach (var entity in project.Entities)
                {
                    if (entity.IsReference)
                        continue;
                    code.Append($@"            
            TsonTypeRegister.RegisteType<{entity.EntityName}>({entity.EntityName}.EntityId,""{ entity.Caption}""); ");
                }
            }
            return code.ToString();
        }

        #endregion
    }
}