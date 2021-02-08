/*design by:agebull designer date:2017/7/12 22:06:40*/
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
    /// 属性配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class FieldConfigBase : ConfigBase
    {
        /// <summary>
        /// 自已
        /// </summary>
        protected abstract IPropertyConfig Me { get; }

        #region 兼容性

        public bool CanImport { get => ExtendConfigListBool["easyui_CanImport"]; set => ExtendConfigListBool["easyui_CanImport"] = value; }
        public bool CanExport { get => ExtendConfigListBool["easyui_CanExport"]; set => ExtendConfigListBool["easyui_CanExport"] = value; }
        public bool UserHint { get => ExtendConfigListBool["user_help"]; set => ExtendConfigListBool["user_help"] = value; }

        /// <summary>
        /// 是否只读
        /// </summary>/// <remarks>
        /// 指数据只可读,无法写入的场景,如此字段为汇总字段
        /// </remarks>
        public bool IsReadonly { get; set; }


        /// <summary>
        /// 值类型
        /// </summary>
        [DataMember, JsonProperty("valueType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _valueType;

        /// <summary>
        /// 值类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"值类型"), Description(@"值类型")]
        public string ValueType
        {
            get => _valueType;
            set
            {
                if (_valueType == value)
                    return;
                BeforePropertyChanged(nameof(ValueType), _valueType, value);
                _valueType = value;
                OnPropertyChanged(nameof(ValueType));
            }
        }
        #endregion
    }
}