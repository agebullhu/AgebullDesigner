using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// �������ɻ���
    /// </summary>
    public class CoderBase : NameHelper
    {
        public static void RepairConfigName(ConfigBase config, bool includeName)
        {
            if (includeName)
                config.Name = ToLanguageName(config.Name);
            config.Caption = ToLanguageName(config.Caption);
            config.Description = ToLanguageName(config.Description);

        }

        #region Ŀ¼��չ
        /// <summary>
        /// ��鲢��ɴ����ļ�·��
        /// </summary>
        /// <param name="project"></param>
        /// <param name="root"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string CheckPath(ProjectConfig project, string root, params string[] names)
        {
            if (names.Length == 0)
            {
                return string.IsNullOrWhiteSpace(project.BranchFolder) 
                    ? root
                    : GlobalConfig.CheckPath(root, project.BranchFolder);
            }
            if (string.IsNullOrWhiteSpace(project.BranchFolder))
                return GlobalConfig.CheckPath(root, names);
            var list = new List<string>();
            list.AddRange(names);
            list.Add(project.BranchFolder);
            return GlobalConfig.CheckPath(root, list.ToArray());
        }
        

        /// <summary>
        /// ��鲢����ĵ��ļ�·��
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static string GetDocumentPath(ProjectConfig project)
        {
            return GlobalConfig.CheckPath(SolutionConfig.Current.RootPath, SolutionConfig.Current.DocFolder);
            
        }
        #endregion
    }
}