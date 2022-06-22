using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    public sealed partial class EntityConfig
    {
        #region �Ӽ�
        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(PropertyConfig propertyConfig)
        {
            if (Properties.Any(p=>p.Name== propertyConfig.Name) ||!Properties.TryAdd(propertyConfig))
                return;
            propertyConfig.Parent = this;
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
        public void Add(EntityReleationConfig propertyConfig)
        {
            propertyConfig.Parent = this;
            Releations.TryAdd(propertyConfig);
        }
        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(UserCommandConfig propertyConfig)
        {
            propertyConfig.Parent = this;
            Commands.TryAdd(propertyConfig);
        }

        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(PropertyConfig propertyConfig)
        {
            Properties.Remove(propertyConfig);
        }
        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(UserCommandConfig propertyConfig)
        {
            Commands.Remove(propertyConfig);
        }

        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(EntityReleationConfig propertyConfig)
        {
            Releations.Remove(propertyConfig);
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
                    action(item);
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
        public List<PropertyConfig> LastProperties { get; set; }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public void CreateLast()
        {
            LastProperties = new List<PropertyConfig>();
            int idx = 0;
            foreach (var pro in Properties.OrderBy(p => p.Index))
            {
                if (pro.IsDelete || pro.IsDiscard)
                    continue;
                pro.Option.Index = ++idx;
                LastProperties.Add(pro);
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <remark>
        /// ����������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"����������"), Description("����������")]
        public IEnumerable<PropertyConfig> PublishProperty => LastProperties?.Where(p => !p.DbInnerField);

        /// <summary>
        /// C++������
        /// </summary>
        /// <remark>
        /// C++������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"C++������"), Description("C++������")]
        public IEnumerable<PropertyConfig> CppProperty => LastProperties?.Where(p => !p.IsMiddleField && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// �ͻ��˿ɷ��ʵ�����
        /// </summary>
        /// <remark>
        /// �ͻ��˿ɷ��ʵ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"�ͻ��˿ɷ��ʵ�����"), Description("�ͻ��˿ɷ��ʵ�����")]
        public IEnumerable<PropertyConfig> ClientProperty => LastProperties?.Where(p => !p.DenyClient && !p.DbInnerField);

        /// <summary>
        /// �ͻ��˿ɷ��ʵ�����
        /// </summary>
        /// <remark>
        /// �ͻ��˿ɷ��ʵ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"�ͻ��˿ɷ��ʵ�����"), Description("�ͻ��˿ɷ��ʵ�����")]
        public IEnumerable<PropertyConfig> UserProperty => LastProperties?.Where(p => !p.DenyClient && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// ���ݿ��ֶ�
        /// </summary>
        /// <remark>
        /// ���ݿ��ֶ�
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"���ݿ��ֶ�"), Description("���ݿ��ֶ�")]
        public IEnumerable<PropertyConfig> DbFields => LastProperties?.Where(p => !p.NoStorage);


        #endregion
    }
}