using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class SimpleConfig : NotificationObject
    {
        /// <summary>
        ///     ����
        /// </summary>
        [DataMember, JsonProperty("_name", NullValueHandling = NullValueHandling.Ignore)] private string _name;

        /// <summary>
        ///     ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public string Name
        {
            get { return _name; }
            set
            {
                var now = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                if (_name == now)
                {
                    return;
                }
                BeforePropertyChanged(nameof(Name), _name, now);
                _name = now;
                RaisePropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        ///     ����
        /// </summary>
        [DataMember, JsonProperty("_caption", NullValueHandling = NullValueHandling.Ignore)]
        protected string _caption;

        /// <summary>
        ///     ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public string Caption
        {
            get { return _caption ?? _name; }
            set
            {
                var now = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                if (_caption == now)
                {
                    return;
                }
                BeforePropertyChanged(nameof(Caption), _caption, now);
                _caption = now;
                RaisePropertyChanged(nameof(Caption));
            }
        }

        /// <summary>
        ///     ˵��
        /// </summary>
        [DataMember, JsonProperty("_description", NullValueHandling = NullValueHandling.Ignore)]
        protected string _description;

        /// <summary>
        ///     ˵��
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"˵��")]
        public string Description
        {
            get { return _description ?? _caption ?? _name; }
            set
            {
                var now = !string.IsNullOrWhiteSpace(value) ? value.Trim() : null;
                if (_description == now)
                {
                    return;
                }
                BeforePropertyChanged(nameof(Description), _description, now);
                _description = now;
                RaisePropertyChanged(nameof(Description));
            }
        }
        /// <summary>
        /// ��ʾ�ı�
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}({Caption})";
        }
    }
}