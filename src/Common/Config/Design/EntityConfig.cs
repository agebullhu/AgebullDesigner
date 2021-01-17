using Newtonsoft.Json;
using System;
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
        /// �����Ӽ�
        /// </summary>
        public override void ForeachChild(Action<ConfigBase> action)
        {
            if (WorkContext.InCoderGenerating)
            {
                foreach (var item in LastProperties)
                    action(item as ConfigBase);
            }
            else
            {
                foreach (var item in Properties)
                    action(item);
            }
        }

        /// <summary>
        /// ������Ч������
        /// </summary>
        public List<IFieldConfig> LastProperties { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        /// <remark>
        /// ����������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"����������"), Description("����������")]
        public IEnumerable<IFieldConfig> PublishProperty => LastProperties?.Where(p => !p.NoProperty && !p.DbInnerField);

        /// <summary>
        /// �û�����
        /// </summary>
        /// <remark>
        /// �û�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"�û�����"), Description("�û�����")]
        public IEnumerable<IFieldConfig> UserProperty => LastProperties?.Where(p => !p.NoProperty && !p.IsMiddleField && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// C++������
        /// </summary>
        /// <remark>
        /// C++������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"C++������"), Description("C++������")]
        public IEnumerable<IFieldConfig> CppProperty => LastProperties?.Where(p => !p.NoProperty && !p.IsMiddleField && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// �ͻ��˿ɷ��ʵ�����
        /// </summary>
        /// <remark>
        /// �ͻ��˿ɷ��ʵ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"�ͻ��˿ɷ��ʵ�����"), Description("�ͻ��˿ɷ��ʵ�����")]
        public IEnumerable<IFieldConfig> ClientProperty => LastProperties?.Where(p => p.UserSee);

        /// <summary>
        /// ���ݿ��ֶ�
        /// </summary>
        /// <remark>
        /// ���ݿ��ֶ�
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"���ݿ��ֶ�"), Description("���ݿ��ֶ�")]
        public IEnumerable<IFieldConfig> DbFields => LastProperties?.Where(p => !p.NoStorage);


        #endregion
    }
}