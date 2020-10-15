using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Agebull.Common.LUA;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// TypedefItem
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class TypedefItem : ProjectChildConfigBase
    {
        #region 子级

        public override void ForeachChild(Action<ConfigBase> action)
        {
            foreach (var item in Items.Values)
                action(item);

        }
        /// <summary>
        /// 子级
        /// </summary>
        [DataMember, JsonProperty("Items",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore), Category("no supper")]
        private Dictionary<string, EnumItem> _items;

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore,Browsable(false)]
        public Dictionary<string, EnumItem> Items
        {
            get
            {
                if (_items != null)
                    return _items;
                _items = new Dictionary<string, EnumItem>();
                BeforePropertyChanged(nameof(Items), null, _items);
                return _items;
            }
            set
            {
                if (_items == value)
                    return;
                BeforePropertyChanged(nameof(Items), _items, value);
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 类型名称对应的语言关键字
        /// </summary>
        [DataMember, JsonProperty("KeyWork",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _keyWork;

        /// <summary>
        /// 类型名称对应的语言关键字
        /// </summary>
        /// <remark>
        /// 类型名称对应的语言关键字
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("类型名称对应的语言关键字"), Description("类型名称对应的语言关键字")]
        public string KeyWork
        {
            get => _keyWork;
            set
            {
                if (_keyWork == value)
                    return;
                BeforePropertyChanged(nameof(KeyWork), _keyWork, value);
                _keyWork = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(KeyWork));
            }
        }

        /// <summary>
        /// 数组名称
        /// </summary>
        [DataMember, JsonProperty("ArrayLen",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _arrayLen;


        /// <summary>
        /// 数组名称
        /// </summary>
        /// <remark>
        /// 数组名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("数组名称"), Description("数组名称")]
        public string ArrayLen
        {
            get => _arrayLen;
            set
            {
                if (_arrayLen == value)
                    return;
                BeforePropertyChanged(nameof(ArrayLen), _arrayLen, value);
                _arrayLen = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ArrayLen));
            }
        }

        #endregion

        /// <summary>
        /// LUA结构支持
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            int idx;
            if (!string.IsNullOrWhiteSpace(KeyWork))
                code.AppendLine($@"['KeyWork'] = '{KeyWork.ToLuaString()}',");
            else
                code.AppendLine($@"['KeyWork'] = nil,");

            if (!string.IsNullOrWhiteSpace(ArrayLen))
                code.AppendLine($@"['ArrayLen'] = '{ArrayLen.ToLuaString()}',");
            else
                code.AppendLine($@"['ArrayLen'] = nil,");

            idx = 0;
            code.AppendLine("['Items'] ={");
            foreach (var val in Items.Values)
            {
                if (idx++ > 0)
                    code.Append(',');
                code.AppendLine($@"{val.GetLuaStruct()}");
            }
            code.AppendLine("},");

        }
    }

}