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
        /// ��ͼ����
        /// </summary>
        /// <param name="project"></param>
        public void Add(ProjectConfig project)
        {
            if (!ProjectList.Contains(project))
                ProjectList.Add(project);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity"></param>
        internal void Add(EntityConfig entity)
        {
            if (!EntityList.Contains(entity))
                EntityList.Add(entity);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Add(EnumConfig enumConfig)
        {
            if (!EnumList.Contains(enumConfig))
                EnumList.Add(enumConfig);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="api"></param>
        internal void Add(ApiItem api)
        {
            if (!ApiList.Contains(api))
                ApiList.Add(api);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="entity"></param>
        internal void Remove(EntityConfig entity)
        {
            if (!EntityList.Contains(entity))
                EntityList.Remove(entity);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="enumConfig"></param>
        internal void Remove(EnumConfig enumConfig)
        {
                EnumList.Remove(enumConfig);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="api"></param>
        internal void Remove(ApiItem api)
        {
                ApiList.Remove(api);
        }
        #endregion
    }
}