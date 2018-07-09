using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ClassifyItem<TConfig> : ParentConfigBase
        where TConfig : ClassifyConfig, new()
    {
        /// <summary>
        /// ����
        /// </summary>
        public ClassifyItem()
        {

        }
        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public override string Type => typeof(ParentConfigBase).Name;

        /// <summary>
        ///     �����޸Ĵ���
        /// </summary>
        /// <param name="propertyName">����</param>
        protected override void OnPropertyChangedInner(string propertyName)
        {
            base.OnPropertyChangedInner(propertyName);
            if (_items == null || _items.Count == 0)
                return;
            foreach (var item in _items)
                item.OnClassifyChanged(this);
        }

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public override IEnumerable<ConfigBase> MyChilds => _items;

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigCollection<TConfig> _items = new ConfigCollection<TConfig>();

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigCollection<TConfig> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                {
                    return;
                }
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="updateAction"></param>
        public ClassifyItem(Action<string, TConfig> updateAction)
        {
            UpdateAction = updateAction;
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        public readonly Action<string, TConfig> UpdateAction;

        /// <summary>
        ///     ��¼�����޸�
        /// </summary>
        /// <param name="propertyName">����</param>
        protected override void RecordModifiedInner(string propertyName)
        {
            base.RecordModifiedInner(propertyName);
            if (UpdateAction == null || propertyName != nameof(Name))
                return;
            foreach (var item in Items)
                UpdateAction(Name, item);
        }
    }
}