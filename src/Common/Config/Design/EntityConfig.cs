using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    public sealed partial class EntityConfig
    {
        #region �Ӽ�
        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(FieldConfig propertyConfig)
        {
            if (Properties.Any(p => p.Name == propertyConfig.Name) || !Properties.TryAdd(propertyConfig))
                return;
            propertyConfig.Entity = this;
            if (WorkContext.InLoding || WorkContext.InSaving || WorkContext.InRepair)
                return;
            propertyConfig.Identity = ++MaxIdentity;
            propertyConfig.Index = Properties.Count == 0 ? 1 : Properties.Max(p => p.Index) + 1;
            MaxIdentity = Properties.Max(p => p.Identity);
        }

        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(FieldConfig propertyConfig)
        {
            Properties.Remove(propertyConfig);
        }

        #endregion

        #region �Ӽ�

        /// <summary>
        /// ������Ч������
        /// </summary>
        public List<IPropertyConfig> LastProperties { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        /// <remark>
        /// ����������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"����������"), Description("����������")]
        public IEnumerable<IPropertyConfig> PublishProperty => LastProperties?.Where(p => !p.NoProperty);

        /// <summary>
        /// �û�����
        /// </summary>
        /// <remark>
        /// �û�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"�û�����"), Description("�û�����")]
        public IEnumerable<IPropertyConfig> UserProperty => LastProperties?.Where(p => !p.NoProperty && !p.IsMiddleField && !p.IsSystemField);

        /// <summary>
        /// C++������
        /// </summary>
        /// <remark>
        /// C++������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"C++������"), Description("C++������")]
        public IEnumerable<IPropertyConfig> CppProperty => LastProperties?.Where(p => !p.NoProperty && !p.IsMiddleField && !p.IsSystemField);

        /// <summary>
        /// �ͻ��˿ɷ��ʵ�����
        /// </summary>
        /// <remark>
        /// �ͻ��˿ɷ��ʵ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"�ͻ��˿ɷ��ʵ�����"), Description("�ͻ��˿ɷ��ʵ�����")]
        public IEnumerable<IPropertyConfig> ClientProperty => LastProperties?.Where(p => p.UserSee);

        #endregion
    }
}