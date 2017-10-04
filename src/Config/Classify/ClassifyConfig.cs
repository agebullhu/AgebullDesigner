using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 分类配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ClassifyConfig : ConfigBase
    {
        /// <summary>
        /// 分类改变事件处理
        /// </summary>
        /// <param name="classify">分类</param>
        protected internal virtual void OnClassifyChanged(ClassifyConfig classify)
        {
            Classify = classify.Name;
        }

        #region *设计

        /// <summary>
        /// 分类
        /// </summary>
        [DataMember, JsonProperty("_Classify", NullValueHandling = NullValueHandling.Ignore)]
        internal string _classify;

        /// <summary>
        /// 分类
        /// </summary>
        /// <remark>
        /// 分类(仅引用可行)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*设计"), DisplayName("分类"), Description("分类(仅引用可行)")]
        public string Classify
        {
            get
            {
                return _classify;
            }
            set
            {
                if (_classify == value)
                    return;
                BeforePropertyChanged(nameof(Classify), _classify, value);
                _classify = value;
                OnPropertyChanged(nameof(Classify));
            }
        }
        #endregion *设计
    }
}