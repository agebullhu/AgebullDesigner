using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��������
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// ��ʶ
        /// </summary>
        string Key
        {
            get;
            set;
        }
    }

    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class SimpleConfig : NotificationObject, IKey
    {
        /// <summary>
        /// ��ʶ
        /// </summary>
        public virtual string Key
        {
            get;set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        [DataMember, JsonProperty("_name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _name;

        /// <summary>
        ///     ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public virtual string Name
        {
            get => _name;
            set
            {
                var now = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                if (_name == now)
                {
                    return;
                }
                BeforePropertyChanged(nameof(Name), _name, now);
                _name = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                RaisePropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        ///     ����
        /// </summary>
        [DataMember, JsonProperty("_caption", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _caption;

        /// <summary>
        ///     ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public virtual string Caption
        {
            get => WorkContext.InCoderGenerating ? _caption ?? _name : _caption;
            set
            {
                if (_caption == value)
                {
                    return;
                }
                if (value == _name)
                    value = null;
                BeforePropertyChanged(nameof(Caption), _caption, value);
                _caption = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                RaisePropertyChanged(nameof(Caption));
            }
        }

        /// <summary>
        ///     ˵��
        /// </summary>
        [DataMember, JsonProperty("_description", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _description;

        /// <summary>
        ///     ˵��
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"˵��")]
        public virtual string Description
        {
            get => WorkContext.InCoderGenerating ? _description ?? Caption : _description;
            set
            {
                var now = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                if (_description == now)
                {
                    return;
                }
                if (value == _caption)
                    value = null;
                BeforePropertyChanged(nameof(Description), _description, now);
                _description = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                RaisePropertyChanged(nameof(Description));
            }
        }

        [DataMember, JsonProperty("_remark", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _remark;

        /// <summary>
        /// �μ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"�μ�")]
        public virtual string Remark
        {
            get => _remark;
            set
            {
                var now = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                if (_remark == now)
                {
                    return;
                }
                BeforePropertyChanged(nameof(Description), _description, now);
                _remark = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                RaisePropertyChanged(nameof(Description));
            }
        }
        /// <summary>
        /// ��ʾ�ı�
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Caption}({Name})[{GetType().Name}]";
        }

        /// <summary>
        /// �ֶθ���
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        public void CopyConfig(SimpleConfig dest)
        {
            using (WorkModelScope.CreateScope(WorkModel.Loding))
            {
                CopyFrom(dest);
            }
        }

        /// <summary>
        /// �ֶθ���
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        public void Copy(SimpleConfig dest)
        {
            using (WorkModelScope.CreateScope(WorkModel.Loding))
            {
                CopyFrom(dest);
            }
        }

        /// <summary>
        /// �ֶθ���
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected virtual void CopyFrom(SimpleConfig dest)
        {
            Name = dest.Name;
            Caption = dest.Caption;
            Description = dest.Description;
            Remark = dest.Remark;
        }
    }
}