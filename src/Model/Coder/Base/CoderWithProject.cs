using Agebull.EntityModel.Config;
using System.Collections.Generic;

namespace Agebull.EntityModel.RobotCoder
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
        protected override bool CanWrite => Project != null && !Project.IsFreeze && !Project.IsDiscard;

        /// <summary>
        ///     ���ݿ����ö���
        /// </summary>
        public virtual ProjectConfig Project { get; set; }

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