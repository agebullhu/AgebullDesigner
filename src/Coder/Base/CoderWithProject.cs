using System.Collections.Generic;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{

    /// <summary>
    ///     �������ö����������
    /// </summary>
    public abstract class CoderWithProject : FileCoder
    {
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public override ConfigBase CurrentConfig => Project;

        /// <summary>
        ///     �Ƿ��д
        /// </summary>
        protected override bool CanWrite => Project != null && !Project.IsFreeze && !Project.Discard;

        /// <summary>
        ///     ���ݿ����ö���
        /// </summary>
        public ProjectConfig Project { get; set; }

        /// <summary>
        ///     �����ü���
        /// </summary>
        public IEnumerable<EntityConfig> Entities => Project.Entities;

        /// <summary>
        ///     �����ռ�
        /// </summary>
        public string NameSpace => Project.NameSpace;

    }
}