using System.Linq;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// ���������
    /// </summary>
    public abstract class CoderWithEntity : CoderWithProject
    {
        /// <summary>
        /// ȡ������������������
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetBaseCode<TBuilder>()
            where TBuilder : EntityBuilderBase, new()
        {
            var builder = new TBuilder
            {
                Entity = Entity,
                Project = Project
            };
            return builder.BaseCode;
        }
        /// <summary>
        /// ȡ������������������
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetExtendCode<TBuilder>()
            where TBuilder : EntityBuilderBase, new()
        {
            var builder = new TBuilder
            {
                Entity = Entity,
                Project = Project
            };
            return builder.ExtendCode;
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public sealed override ConfigBase CurrentConfig => Entity;

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public EntityConfig Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                if (value == null)
                    return;
                foreach (var property in value.Properties.Where(p => !p.Discard))
                {
                    if (property.IsReference)
                    {
                        var pp = GlobalConfig.GetConfig<PropertyConfig>(property.ReferenceKey);
                        if (pp != null)
                        {
                            property.CopyConfig(pp);
                        }
                    }
                }
            }
        }
        
        private EntityConfig _entity;
        
        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => base.CanWrite && Entity != null && !Entity.IsFreeze && !Entity.Discard;

        /// <summary>
        /// ȡ����չ���õ�·��
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        protected string ConfigPath(string path, string key, string def, string ext = "")
        {
            return SetPath(path, Entity.TryGetExtendConfig(key, def).Trim('\\'), ext);
        }
    }
}