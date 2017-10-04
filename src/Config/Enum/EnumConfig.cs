// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // ����:
// // ����:CodeRefactor
// // ����:2016-06-06
// // �޸�:2016-06-22
// // *****************************************************/

#region ����

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     ö��ֵ����
    /// </summary>
    public sealed partial class EnumConfig
    {
        #region *���

        /// <summary>
        /// �Ƿ�λ��
        /// </summary>
        [DataMember, JsonProperty("IsFlagEnum", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isFlagEnum;

        /// <summary>
        /// �Ƿ�λ��
        /// </summary>
        /// <remark>
        /// �Ƿ�λ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*���"), DisplayName("�Ƿ�λ��"), Description("�Ƿ�λ��")]
        public bool IsFlagEnum
        {
            get
            {
                return _isFlagEnum;
            }
            set
            {
                if (_isFlagEnum == value)
                    return;
                BeforePropertyChanged(nameof(IsFlagEnum), _isFlagEnum, value);
                _isFlagEnum = value;
                OnPropertyChanged(nameof(IsFlagEnum));
            }
        }

        /// <summary>
        /// ���Ӷ�Ӧ���ֶ�
        /// </summary>
        [DataMember, JsonProperty("LinkField", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _linkField;

        /// <summary>
        /// ���Ӷ�Ӧ���ֶ�
        /// </summary>
        /// <remark>
        /// �Ƿ�λ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*���"), DisplayName("���Ӷ�Ӧ���ֶ�"), Description("�Ƿ�λ��")]
        public Guid LinkField
        {
            get
            {
                return _linkField;
            }
            set
            {
                if (_linkField == value)
                    return;
                BeforePropertyChanged(nameof(LinkField), _linkField, value);
                _linkField = value;
                OnPropertyChanged(nameof(LinkField));
            }
        }
        #endregion *��� 
        #region 

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public override IEnumerable<ConfigBase> MyChilds => _items;

        /// <summary>
        /// �Ӽ�
        /// </summary>
        [DataMember, JsonProperty("_items", NullValueHandling = NullValueHandling.Ignore)]
        internal ObservableCollection<EnumItem> _items;

        /// <summary>
        /// �Ӽ��ڵ�
        /// </summary>
        /// <remark>
        /// �Ӽ��ڵ�
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("�Ӽ��ڵ�"), Description("�Ӽ��ڵ�")]
        public ObservableCollection<EnumItem> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new ObservableCollection<EnumItem>();
                BeforePropertyChanged(nameof(Items), null, _items);
                return _items;
            }
            set
            {
                if (_items == value)
                    return;
                BeforePropertyChanged(nameof(Items), _items, value);
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        #endregion
        
    }
}