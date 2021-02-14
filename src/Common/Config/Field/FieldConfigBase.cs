/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Agebull.EntityModel.Config.V2021;
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

        #region 反向链接

        /// <summary>
        /// 内部字段
        /// </summary>
        [DataMember, JsonProperty("_innerField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _innerField;

        /// <summary>
        /// 内部字段
        /// </summary>
        /// <remark>
        /// 是否内部字段,即非用户字段,不呈现给用户
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"内部字段"), Description("是否内部字段,即非用户字段,不呈现给用户")]
        public bool InnerField
        {
            get => !Me.EnableUI || _innerField;
            set
            {
                if (_innerField == value)
                    return;
                BeforePropertyChanged(nameof(InnerField), _innerField, value);
                _innerField = value;
                OnPropertyChanged(nameof(InnerField));
            }
        }

        /// <summary>
        /// 数据字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal DataBaseFieldConfig _dataBaseField;

        /// <summary>
        /// 数据字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"扩展对象"), DisplayName(@"数据字段"), Description("数据字段")]
        public DataBaseFieldConfig DataBaseField
        {
            get => _dataBaseField;
            set
            {
                if (_dataBaseField == value)
                    return;
                BeforePropertyChanged(nameof(DataBaseField), _dataBaseField, value);
                _dataBaseField = value;
                if (_dataBaseField != null)
                    _dataBaseField.Property = Me;
                OnPropertyChanged(nameof(DataBaseField));
                OnPropertyChanged(nameof(IsLinkField));
            }
        }


        /// <summary>
        /// 数据库的读写忽略这个字段
        /// </summary>
        /// <remark>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"非数据库字段"), Description(@"是否非数据库字段,如果为真,数据库的读写均忽略这个字段")]
        public bool IsLinkField
        {
            get => Me.EnableDataBase && _dataBaseField != null && _dataBaseField.IsLinkField;
            set
            {
                if (_dataBaseField == null)
                    return;
                if (_dataBaseField.IsLinkField == value)
                    return;
                _dataBaseField.IsLinkField = value;
                OnPropertyChanged(nameof(IsLinkField));
                OnPropertyChanged(nameof(NoStorage));
            }
        }

        /// <summary>
        /// 数据库的读写忽略这个字段
        /// </summary>
        /// <remark>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"非数据库字段"), Description(@"是否非数据库字段,如果为真,数据库的读写均忽略这个字段")]
        public bool NoStorage
        {
            get => !Me.EnableDataBase || _dataBaseField == null || _dataBaseField.NoStorage;
            set
            {
                if (_dataBaseField == null)
                    return;
                if (_dataBaseField.NoStorage == value)
                    return;
                _dataBaseField.NoStorage = value;
                OnPropertyChanged(nameof(NoStorage));
                OnPropertyChanged(nameof(IsLinkField));
            }
        }

        #endregion
    }
}