using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��������
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ClassifyConfig : ConfigBase
    {
        /// <summary>
        /// ����ı��¼�����
        /// </summary>
        /// <param name="classify">����</param>
        protected internal virtual void OnClassifyChanged(ClassifyConfig classify)
        {
            Classify = classify.Name;
        }

        #region *���

        /// <summary>
        /// ����
        /// </summary>
        [DataMember, JsonProperty("_Classify", NullValueHandling = NullValueHandling.Ignore)]
        internal string _classify;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����(�����ÿ���)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*���"), DisplayName("����"), Description("����(�����ÿ���)")]
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
        #endregion *���
    }
}