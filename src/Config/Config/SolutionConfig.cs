// /***********************************************************************************************************************
// 工程：Agebull.EntityModel.Designer
// 项目：CodeRefactor
// 文件：DataBaseSchema.cs
// 作者：Administrator/
// 建立：2015－07－08 16:51
// ****************************************************文件说明**********************************************************
// 对应文档：
// 说明摘要：
// 作者备注：
// ****************************************************修改记录**********************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ***********************************************************************************************************************/

#region 命名空间引用


#endregion

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 解决方案配置
    /// </summary>
    public sealed partial class SolutionConfig
    {
        /// <summary>
        /// 设置当前解决方案
        /// </summary>
        /// <param name="solution"></param>
        public static void SetCurrentSolution(SolutionConfig solution)
        {
            Current = solution;
        }

        /// <summary>
        ///     当前实例
        /// </summary>
        public static SolutionConfig Current { get; private set; }

        #region 设计器支持
        /// <summary>
        /// 试图加入
        /// </summary>
        /// <param name="project"></param>
        public void Add(ProjectConfig project)
        {
            if (!ProjectList.Contains(project))
                ProjectList.Add(project);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="entity"></param>
        internal void Add(EntityConfig entity)
        {
            if (!EntityList.Contains(entity))
                EntityList.Add(entity);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Add(EnumConfig enumConfig)
        {
            if (!EnumList.Contains(enumConfig))
                EnumList.Add(enumConfig);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="api"></param>
        internal void Add(ApiItem api)
        {
            if (!ApiList.Contains(api))
                ApiList.Add(api);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        internal void Remove(EntityConfig entity)
        {
            if (!EntityList.Contains(entity))
                EntityList.Remove(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Remove(EnumConfig enumConfig)
        {
                EnumList.Remove(enumConfig);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="api"></param>
        internal void Remove(ApiItem api)
        {
                ApiList.Remove(api);
        }
        #endregion
    }
}