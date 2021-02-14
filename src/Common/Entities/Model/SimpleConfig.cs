using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// 标识
        /// </summary>
        string Key
        {
            get;
            set;
        }
    }

    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class SimpleConfig : NotificationObject, IKey
    {
        /// <summary>
        /// 标识
        /// </summary>
        public virtual string Key
        {
            get; set;
        }
        public SimpleConfig()
        {

        }
        public SimpleConfig(bool notCheck)
        {
            notNameCaptionCheck = notCheck;
        }
        /// <summary>
        ///     不检查名称类型
        /// </summary>
        bool notNameCaptionCheck;

        /// <summary>
        ///     名称
        /// </summary>
        [DataMember, JsonProperty("_name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _name;

        /// <summary>
        ///     名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
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
                _name = now;
                RaisePropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        ///     标题
        /// </summary>
        [DataMember, JsonProperty("_caption", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _caption;

        /// <summary>
        ///     标题
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public virtual string Caption
        {
            get => notNameCaptionCheck ? _caption: WorkContext.InCoderGenerating ? _caption ?? _name : _caption;
            set
            {
                var now = value.IsPresent() ? value.Trim() : null;
                if (_caption == now)
                {
                    return;
                }
                if (!notNameCaptionCheck && _name.IsMe(now))
                    now = null;
                BeforePropertyChanged(nameof(Caption), _caption, now);
                _caption = string.IsNullOrWhiteSpace(now) ? null : now.Trim();
                RaisePropertyChanged(nameof(Caption));
            }
        }

        /// <summary>
        ///     说明
        /// </summary>
        [DataMember, JsonProperty("_description", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _description;

        /// <summary>
        ///     说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public virtual string Description
        {
            get => notNameCaptionCheck ? _description : WorkContext.InCoderGenerating ? _description ?? Caption : _description;
            set
            {
                var now = value.IsPresent() ? value.Trim() : null;
                if (_description == now)
                {
                    return;
                }
                if (!notNameCaptionCheck && Caption.IsMe(now))
                    now = null;
                BeforePropertyChanged(nameof(Description), _description, now);
                _description = now;
                RaisePropertyChanged(nameof(Description));
            }
        }

        [DataMember, JsonProperty("_remark", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string _remark;

        /// <summary>
        /// 参见
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
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
        /// 显示文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Caption}({Name})[{GetType().Name}]";
        }

        /// <summary>
        /// 字段复制
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
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        public void Copy(SimpleConfig dest)
        {
            using (WorkModelScope.CreateScope(WorkModel.Loding))
            {
                Name = dest.Name;
                Caption = dest.Caption;
                Description = dest.Description;
                Remark = dest.Remark;
                CopyFrom(dest);
            }
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected virtual void CopyFrom(SimpleConfig dest)
        {
        }

    }
}