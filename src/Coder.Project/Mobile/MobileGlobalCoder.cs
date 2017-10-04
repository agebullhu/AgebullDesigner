using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class MobileGlobalCoder : CoderWithProject
    {

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_MobileGlobal_CS";

        #region �������

        /// <summary>
        ///     ����ʵ�����
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
    ///     {SolutionConfig.Current.Caption}��ȫ������
    /// </summary>
    public class {SolutionConfig.Current.Name}Option
    {{
        #region ע������
        
        /// <summary>
        /// ע������
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
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {

        }

        #endregion

        #region ����

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