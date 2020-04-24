// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-09-18
// // 修改:2016-09-18
// // *****************************************************/


namespace Agebull.EntityModel.Config
{
    internal class ProjectBusinessModel : ConfigModelBase
    {
        #region 编辑方法

        /// <summary>
        /// 项目对象
        /// </summary>
        public SolutionConfig Solution { get; set; }

        public static void ToModify(ProjectConfig project)
        {
            foreach (var entity in project.Entities)
            {
                entity.IsModify = true;
            }
        }

        public static void ToReference(ProjectConfig project)
        {
            foreach (var entity in project.Entities)
            {
                entity.IsReference = true;
            }
        }

        public static void ToClass(ProjectConfig project)
        {
            foreach (var entity in project.Entities)
            {
                entity.IsClass = true;
            }
        }

        public static void UnLock(ProjectConfig project)
        {
            project.IsFreeze = false;
            foreach (var entity in project.Entities)
            {
                entity.IsFreeze = false;
                foreach (var field in entity.Properties)
                {
                    field.IsFreeze = false;
                    if (field.EnumConfig != null)
                        field.EnumConfig.IsFreeze = false;
                }
            }
        }

        public static void Lock(ProjectConfig project)
        {
            project.IsFreeze = true;
            foreach (var entity in project.Entities)
            {
                entity.IsFreeze = true;
                foreach (var field in entity.Properties)
                {
                    field.IsFreeze = true;
                    if (field.EnumConfig != null)
                        field.EnumConfig.IsFreeze = true;
                }
            }
        }

        #endregion

    }
}