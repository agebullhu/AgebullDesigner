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
        /// <summary>
        ///     名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public override string Name
        {
            get => _name ?? Field?.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public override string Caption
        {
            get => _caption ?? Field?.Caption;
            set => base.Caption = value;
        }

        /// <summary>
        ///     说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public override string Description
        {
            get => _description ?? Field?.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// 参见
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
        public override string Remark
        {
            get => _remark ?? Field?.Remark;
            set => base.Remark = value;
        }

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        [DataMember, JsonProperty("_columnName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbFieldName;

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        /// <remark>
        /// 字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库字段名称"), Description("字段名称")]
        public string DbFieldName
        {
            get => _dbFieldName ?? Field?.DbFieldName;
            set
            {
                if (_dbFieldName == value)
                    return;
                BeforePropertyChanged(nameof(DbFieldName), _dbFieldName, value);
                _dbFieldName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DbFieldName));
            }
        }

        /// <summary>
        /// 字段名称(json)
        /// </summary>
        [DataMember, JsonProperty("jsonName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _jsonName;

        /// <summary>
        /// 字段名称(json)
        /// </summary>
        /// <remark>
        /// json字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"API支持"), DisplayName(@"字段名称(json)")]
        public string JsonName
        {
            get => _jsonName ?? Field?.JsonName;
            set
            {
                if (_jsonName == value)
                    return;
                BeforePropertyChanged(nameof(JsonName), _jsonName, value);
                _jsonName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(JsonName));
            }
        }

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