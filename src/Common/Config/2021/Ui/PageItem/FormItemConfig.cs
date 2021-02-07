using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 表单节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class FormItemConfig : PageItemConfig
    {
        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        [DataMember, JsonProperty("FormCloumn", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _formCloumn;

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        /// <remark>
        /// 编辑页面分几列
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"编辑页面分几列"), Description("编辑页面分几列")]
        public int FormCloumn
        {
            get => _formCloumn;
            set
            {
                if (_formCloumn == value)
                    return;
                BeforePropertyChanged(nameof(FormCloumn), _formCloumn, value);
                _formCloumn = value;
                OnPropertyChanged(nameof(FormCloumn));
            }
        }
    }
}