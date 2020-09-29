/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{


    /// <summary>
    /// 属性配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class PropertyConfig : ConfigBase
    {

        [DataMember, JsonProperty("fieldKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string fieldKey;

        private FieldConfig field;

        /// <summary>
        /// 字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public FieldConfig Field
        {
            get => field ??= GlobalConfig.GetConfig<FieldConfig>(fieldKey);
            set
            {
                if (field == value)
                    return;
                BeforePropertyChanged(nameof(Field), field, value);
                field = value;
                fieldKey = value.Key.ToString();
                OnPropertyChanged(nameof(Field));
            }
        }

        /// <summary>
        /// 上级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal ModelConfig _model;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public ModelConfig Model
        {
            get => _model;
            set
            {
                if (_model == value)
                    return;
                BeforePropertyChanged(nameof(Model), _model, value);
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
    }
}