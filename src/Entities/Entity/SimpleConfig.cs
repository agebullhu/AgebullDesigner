using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class SimpleConfig : NotificationObject
    {
        /// <summary>
        ///     名称
        /// </summary>
        [DataMember, JsonProperty("_name", NullValueHandling = NullValueHandling.Ignore)] private string _name;

        /// <summary>
        ///     名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
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
        ///     标题
        /// </summary>
        [DataMember, JsonProperty("_caption", NullValueHandling = NullValueHandling.Ignore)]
        protected string _caption;

        /// <summary>
        ///     标题
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
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
        ///     说明
        /// </summary>
        [DataMember, JsonProperty("_description", NullValueHandling = NullValueHandling.Ignore)]
        protected string _description;

        /// <summary>
        ///     说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
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
        /// 显示文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}({Caption})";
        }
    }
}