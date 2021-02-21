/*design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 枚举值节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class EnumItem : ConfigBase,IChildrenConfig
    {

        ISimpleConfig IChildrenConfig.Parent { get => _parent; set => Parent = value as EnumConfig; }

        /// <summary>
        /// 上级
        /// </summary>
        [JsonIgnore]
        internal EnumConfig _parent;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public EnumConfig Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;
                BeforePropertyChange(nameof(Parent), _parent, value);
                _parent = value;
                RaisePropertyChanged(nameof(Parent));
            }
        }

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        public EnumItem()
        {
        }

        #endregion


        #region 数据模型

        /// <summary>
        /// 值
        /// </summary>
        [DataMember, JsonProperty("Value", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _value;

        /// <summary>
        /// 值
        /// </summary>
        /// <remark>
        /// 值
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"值"), Description("值")]
        public string Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                BeforePropertyChange(nameof(Value), _value, value);
                _value = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Value));
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        /// <remark>
        /// 值
        /// </remark>
        [JsonIgnore]
        public long Number
        {
            get
            {
                if (_value == null)
                    return -1;
                return long.TryParse(Value, out var num) ? num : Value[0];
            }
        }
        #endregion

    }
}