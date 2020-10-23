// /***********************************************************************************************************************
// ���̣�Agebull.EntityModel.Designer
// ��Ŀ��CodeRefactor
// �ļ���DataBaseSchema.cs
// ���ߣ�Administrator/
// ������2015��07��08 16:51
// ****************************************************�ļ�˵��**********************************************************
// ��Ӧ�ĵ���
// ˵��ժҪ��
// ���߱�ע��
// ****************************************************�޸ļ�¼**********************************************************
// ���ڣ�
// ��Ա��
// ˵����
// ************************************************************************************************************************
// ���ڣ�
// ��Ա��
// ˵����
// ************************************************************************************************************************
// ���ڣ�
// ��Ա��
// ˵����
// ***********************************************************************************************************************/

#region �����ռ�����


#endregion

using System;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// �����������
    /// </summary>
    public sealed partial class SolutionConfig
    {
        /// <summary>
        /// ���õ�ǰ�������
        /// </summary>
        /// <param name="solution"></param>
        public static void SetCurrentSolution(SolutionConfig solution)
        {
            Current = solution;
        }

        /// <summary>
        ///     ��ǰʵ��
        /// </summary>
        public static SolutionConfig Current { get; private set; }

        #region �����֧��

        /// <summary>
        /// �����Ӽ�
        /// </summary>
        public override void ForeachChild(Action<ConfigBase> action)
        {
            if (_projects == null) return;
            foreach (var item in _projects)
                action(item);
        }
        /// <summary>
        /// ��ͼ����
        /// </summary>
        /// <param name="project"></param>
        public void Add(ProjectConfig project)
        {
            ProjectList.TryAdd(project);
            GlobalConfig.Add(project);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity"></param>
        internal void Add(EntityConfig entity)
        {
            EntityList.TryAdd(entity);
            GlobalConfig.Add(entity);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="model"></param>
        internal void Add(ModelConfig model)
        {
            ModelList.TryAdd(model);
            GlobalConfig.Add(model);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Add(EnumConfig enumConfig)
        {
            EnumList.TryAdd(enumConfig);
            GlobalConfig.Add(enumConfig);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="api"></param>
        internal void Add(ApiItem api)
        {
            ApiList.TryAdd(api);
            GlobalConfig.Add(api);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="entity"></param>
        internal void Remove(EntityConfig entity)
        {
            EntityList.Remove(entity);
            GlobalConfig.Remove(entity);
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="entity"></param>
        internal void Remove(ModelConfig model)
        {
            ModelList.Remove(model);
            GlobalConfig.Remove(model);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Remove(EnumConfig enumConfig)
        {
            EnumList.Remove(enumConfig);
            GlobalConfig.Remove(enumConfig);
        }

        /// <summary>
        /// ɾ��
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