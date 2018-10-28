using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class ParentConfigBase : FileConfigBase
    {
        /// <summary>
        /// 遍历子级
        /// </summary>
        public abstract void ForeachChild(Action<ConfigBase> action);


        /// <summary>
        /// 简称
        /// </summary>
        [DataMember, JsonProperty("_abbreviation", NullValueHandling = NullValueHandling.Ignore)]
        private string _abbreviation;

        /// <summary>
        /// 简称
        /// </summary>
        /// <remark>
        /// 简称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"简称"), Description(@"简称")]
        public string Abbreviation
        {
            get => _abbreviation;
            set
            {
                if (_abbreviation == value)
                    return;
                BeforePropertyChanged(nameof(Abbreviation), _abbreviation, value);
                _abbreviation = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Abbreviation));
            }
        }
        /// <summary>
        /// 全局项目
        /// </summary>
        [DataMember, JsonProperty("IsGlobal", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isGlobal;

        /// <summary>
        /// 全局项目
        /// </summary>
        /// <remark>
        /// 全局项目,是作为设计器支持的基础项目
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"全局项目"), Description("全局项目,是作为设计器支持的基础项目")]
        public bool IsGlobal
        {
            get => _isGlobal;
            set
            {
                if (_isGlobal == value)
                    return;
                BeforePropertyChanged(nameof(IsGlobal), _isGlobal, value);
                _isGlobal = value;
                OnPropertyChanged(nameof(IsGlobal));
            }
        }
    }
}