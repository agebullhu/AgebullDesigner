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

using System;

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
        /// 遍历子级
        /// </summary>
        public override void ForeachChild(Action<ConfigBase> action)
        {
            if (_projects == null) return;
            foreach (var item in _projects)
                action(item);
        }
        /// <summary>
        /// 试图加入
        /// </summary>
        /// <param name="project"></param>
        public void Add(ProjectConfig project)
        {
            ProjectList.TryAdd(project);
            GlobalConfig.Add(project);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="entity"></param>
        internal void Add(EntityConfig entity)
        {
            EntityList.TryAdd(entity);
            GlobalConfig.Add(entity);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="model"></param>
        internal void Add(ModelConfig model)
        {
            ModelList.TryAdd(model);
            GlobalConfig.Add(model);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Add(EnumConfig enumConfig)
        {
            EnumList.TryAdd(enumConfig);
            GlobalConfig.Add(enumConfig);
        }

        /// <summary>
        /// 加入
        /// </summary>
        /// <param name="api"></param>
        internal void Add(ApiItem api)
        {
            ApiList.TryAdd(api);
            GlobalConfig.Add(api);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        internal void Remove(EntityConfig entity)
        {
            EntityList.Remove(entity);
            GlobalConfig.Remove(entity);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        internal void Remove(ModelConfig model)
        {
            ModelList.Remove(model);
            GlobalConfig.Remove(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Remove(EnumConfig enumConfig)
        {
            EnumList.Remove(enumConfig);
            GlobalConfig.Remove(enumConfig);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="api"></param>
        internal void Remove(ApiItem api)
        {
            ApiList.Remove(api);
            GlobalConfig.Remove(api);
        }
        #endregion
    }
}