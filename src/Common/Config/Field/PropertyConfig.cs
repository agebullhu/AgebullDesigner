﻿/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class PropertyConfig : FieldConfigBase, IPropertyConfig, IChildrenConfig
    {
        #region 视角开关

        ISimpleConfig IChildrenConfig.Parent { get => Model; set => Model = value as ModelConfig; }

        /// <summary>
        /// 启用数据库支持
        /// </summary>
        public bool EnableDataBase => Model.EnableDataBase;

        /// <summary>
        /// 启用数据校验
        /// </summary>
        public bool EnableValidate => Model.EnableValidate;

        /// <summary>
        /// 启用编辑接口
        /// </summary>
        public bool EnableEditApi => Model.EnableEditApi;

        /// <summary>
        /// 启用用户界面
        /// </summary>
        public bool EnableUI => Model.EnableUI;

        #endregion

        #region 模型引用

        /// <summary>
        /// 上级
        /// </summary>
        [JsonIgnore]
        internal ModelConfig _model;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public ModelConfig Model
        {
            get => _model;
            set
            {
                if (_model == value)
                    return;
                BeforePropertyChange(nameof(Model), _model, value);
                _model = value;
                RaisePropertyChanged(nameof(Model));
                RaisePropertyChanged("Parent");
            }
        }
        [DataMember, JsonProperty("fieldKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string fieldKey;

        private FieldConfig field;

        /// <summary>
        /// 字段
        /// </summary>
        [JsonIgnore]
        public FieldConfig Field
        {
            get => field ??= GlobalConfig.GetConfig<FieldConfig>(fieldKey);
            set
            {
                if (field == value)
                    return;
                BeforePropertyChange(nameof(Field), field, value);
                field = value;
                fieldKey = value.Key.ToString();
                OnPropertyChanged(nameof(Field));
                RaisePropertyChanged(nameof(fieldKey));
            }
        }

        /// <summary>
        /// 不生成属性
        /// </summary>
        [DataMember, JsonProperty("noProperty", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noProperty;

        /// <summary>
        /// 不生成属性
        /// </summary>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"不生成属性")]
        public bool NoProperty
        {
            get => _noProperty;
            set
            {
                if (_noProperty == value)
                    return;
                BeforePropertyChange(nameof(NoProperty), _noProperty, value);
                _noProperty = value;
                OnPropertyChanged(nameof(NoProperty));
            }
        }
        #endregion

        #region 扩散信息

        /// <summary>
        ///     名称
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public override string Name
        {
            get => _name ?? Field?.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public override string Caption
        {
            get => _caption ?? Field?.Caption;
            set => base.Caption = value;
        }

        /// <summary>
        ///     说明
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public override string Description
        {
            get => _description ?? Field?.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// 参见
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
        public override string Remark
        {
            get => _remark ?? Field?.Remark;
            set => base.Remark = value;
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
        [JsonIgnore]
        [Category(@"API支持"), DisplayName(@"字段名称(json)")]
        public string JsonName
        {
            get => _jsonName ?? Field?.JsonName;
            set
            {
                if (_jsonName == value)
                    return;
                BeforePropertyChange(nameof(JsonName), _jsonName, value);
                _jsonName = value.SafeTrim();
                OnPropertyChanged(nameof(JsonName));
            }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DataMember, JsonProperty("DataType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataType;

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <remark>
        /// 数据类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"数据类型")]
        public string DataType
        {
            get => _dataType ?? Field.DataType;
            set
            {
                if (_dataType == value)
                    return;
                BeforePropertyChange(nameof(DataType), _dataType, value);
                _dataType = value.SafeTrim();
                OnPropertyChanged(nameof(DataType));
            }
        }

        /// <summary>
        /// 语言类型(C#)
        /// </summary>
        [DataMember, JsonProperty("CsType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _csType;

        /// <summary>
        /// 语言类型(C#)
        /// </summary>
        /// <remark>
        /// C#语言类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"语言类型(C#)"), Description("C#语言类型")]
        public string CsType
        {
            get => _csType ?? Field.CsType;
            set
            {
                if (string.Equals(_csType, value, StringComparison.OrdinalIgnoreCase))
                    return;
                BeforePropertyChange(nameof(CsType), _csType, value);
                _csType = value.SafeTrim();
                OnPropertyChanged(nameof(CsType));
            }
        }

        /// <summary>
        /// 初始值的说明文字
        /// </summary>
        const string Initialization_Description = @"3初始值,原样写入代码,如果是文本,需要加引号";

        /// <summary>
        /// 初始值
        /// </summary>
        [DataMember, JsonProperty("Initialization", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _initialization;

        /// <summary>
        /// 初始值
        /// </summary>
        /// <remark>
        /// 3初始值,原样写入代码,如果是文本,需要加引号
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"初始值"), Description(Initialization_Description)]
        public string Initialization
        {
            get => _initialization ?? Field.Initialization;
            set
            {
                if (_initialization == value)
                    return;
                BeforePropertyChange(nameof(Initialization), _initialization, value);
                _initialization = value.SafeTrim();
                OnPropertyChanged(nameof(Initialization));
            }
        }
        #endregion

        #region CPP

        /// <summary>
        /// 语言类型(C++)
        /// </summary>
        [DataMember, JsonProperty("CppType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppType;

        /// <summary>
        /// 语言类型(C++)
        /// </summary>
        /// <remark>
        /// C++字段类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"语言类型(C++)"), Description("C++字段类型")]
        public string CppType
        {
            get => _cppType ?? Field.CppType;
            set
            {
                if (_cppType == value)
                    return;
                BeforePropertyChange(nameof(CppType), _cppType, value);
                _cppType = value.SafeTrim();
                OnPropertyChanged(nameof(CppType));
            }
        }

        /// <summary>
        /// 字段名称(C++)
        /// </summary>
        [DataMember, JsonProperty("CppName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppName;

        /// <summary>
        /// 字段名称(C++)
        /// </summary>
        /// <remark>
        /// C++字段名称
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"字段名称(C++)"), Description("C++字段名称")]
        public string CppName
        {
            get => _cppName ?? Field.CppName;
            set
            {
                if (_cppName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChange(nameof(CppName), _cppName, value);
                _cppName = value.SafeTrim();
                OnPropertyChanged(nameof(CppName));
            }
        }

        /// <summary>
        /// 结果类型(C++)
        /// </summary>
        [DataMember, JsonProperty("_cppLastType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppLastType;

        /// <summary>
        /// 结果类型(C++)
        /// </summary>
        /// <remark>
        /// 最终生成C++代码时的字段类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"结果类型(C++)"), Description("最终生成C++代码时的字段类型")]
        public string CppLastType
        {
            get => _cppLastType ?? Field.CppLastType;
            set
            {
                if (_cppLastType == value)
                    return;
                BeforePropertyChange(nameof(CppLastType), _cppLastType, value);
                _cppLastType = value.SafeTrim();
                OnPropertyChanged(nameof(CppLastType));
            }
        }

        /// <summary>
        /// C++字段类型
        /// </summary>
        [JsonIgnore]
        internal object _cppTypeObject;

        /// <summary>
        /// C++字段类型
        /// </summary>
        /// <remark>
        /// C++字段类型
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(C++)"), DisplayName(@"C++字段类型"), Description("C++字段类型")]
        public object CppTypeObject
        {
            get => _cppTypeObject ?? Field.CppTypeObject;
            set
            {
                if (_cppTypeObject == value)
                    return;
                BeforePropertyChange(nameof(CppTypeObject), _cppTypeObject, value);
                _cppTypeObject = value;
                OnPropertyChanged(nameof(CppTypeObject));
            }
        }

        #endregion

        #region 计算列

        /// <summary>
        /// 自定义代码(get)
        /// </summary>
        [DataMember, JsonProperty("ComputeGetCode", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _computeGetCode;

        /// <summary>
        /// 自定义代码(get)
        /// </summary>
        /// <remark>
        /// 自定义代码Get部分代码,仅用于C#
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"自定义代码(get)"), Description("自定义代码Get部分代码,仅用于C#")]
        public string ComputeGetCode
        {
            get => _computeGetCode ?? Field.ComputeGetCode;
            set
            {
                if (_computeGetCode == value)
                    return;
                BeforePropertyChange(nameof(ComputeGetCode), _computeGetCode, value);
                _computeGetCode = value.SafeTrim();
                OnPropertyChanged(nameof(ComputeGetCode));
            }
        }

        /// <summary>
        /// 自定义代码(set)
        /// </summary>
        [DataMember, JsonProperty("ComputeSetCode", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _computeSetCode;

        /// <summary>
        /// 自定义代码(set)
        /// </summary>
        /// <remark>
        /// 自定义代码Set部分代码,仅用于C#
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"自定义代码(set)"), Description("自定义代码Set部分代码,仅用于C#")]
        public string ComputeSetCode
        {
            get => _computeSetCode ?? Field.ComputeSetCode;
            set
            {
                if (_computeSetCode == value)
                    return;
                BeforePropertyChange(nameof(ComputeSetCode), _computeSetCode, value);
                _computeSetCode = value.SafeTrim();
                OnPropertyChanged(nameof(ComputeSetCode));
            }
        }

        /// <summary>
        /// 自定义读写代码的说明文字
        /// </summary>
        const string IsCustomCompute_Description = @"自定义读写代码,即不使用代码生成,而使用录入的代码";

        /// <summary>
        /// 自定义读写代码
        /// </summary>
        [DataMember, JsonProperty("IsCustomCompute", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool? _isCustomCompute;

        /// <summary>
        /// 自定义读写代码
        /// </summary>
        /// <remark>
        /// 自定义读写代码,即不使用代码生成,而使用录入的代码
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计(计算列)"), DisplayName(@"自定义读写代码"), Description(IsCustomCompute_Description)]
        public bool IsCustomCompute
        {
            get => _isCustomCompute == null ? Field.IsCustomCompute : _isCustomCompute.Value;
            set
            {
                if (_isCustomCompute == value)
                    return;
                BeforePropertyChange(nameof(IsCustomCompute), _isCustomCompute, value);
                _isCustomCompute = value;
                OnPropertyChanged(nameof(IsCustomCompute));
            }
        }
        #endregion

        #region API支持

        /// <summary>
        /// 不参与ApiArgument序列化
        /// </summary>
        [DataMember, JsonProperty("noneApiArgument", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool? _noneApiArgument;

        /// <summary>
        /// 不参与ApiArgument序列化
        /// </summary>
        /// <remark>
        /// 客户端不显示
        /// </remark>
        [JsonIgnore]
        [Category(@"API支持"), DisplayName(@"不参与ApiArgument序列化")]
        public bool NoneApiArgument
        {
            get => _noneApiArgument == null ? Field.NoneApiArgument : _noneApiArgument.Value;
            set
            {
                if (_noneApiArgument == value)
                    return;
                BeforePropertyChange(nameof(NoneApiArgument), _noneApiArgument, value);
                _noneApiArgument = value;
                OnPropertyChanged(nameof(NoneApiArgument));
            }
        }
        /// <summary>
        /// 字段名称(ApiArgument)
        /// </summary>
        [DataMember, JsonProperty("apiArgumentName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiArgumentName;

        /// <summary>
        /// 字段名称(ApiArgument)
        /// </summary>
        /// <remark>
        /// ApiArgument字段名称
        /// </remark>
        [JsonIgnore]
        [Category(@"API支持"), DisplayName(@"字段名称(ApiArgument)")]
        public string ApiArgumentName
        {
            get => _apiArgumentName ?? Field.ApiArgumentName;
            set
            {
                if (_apiArgumentName == value)
                    return;
                BeforePropertyChange(nameof(ApiArgumentName), _apiArgumentName, value);
                _apiArgumentName = value.SafeTrim();
                OnPropertyChanged(nameof(ApiArgumentName));
            }
        }
        /// <summary>
        /// 不参与Json序列化
        /// </summary>
        [DataMember, JsonProperty("NoneJson", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool? _noneJson;

        /// <summary>
        /// 不参与Json序列化
        /// </summary>
        /// <remark>
        /// 客户端不显示
        /// </remark>
        [JsonIgnore]
        [Category(@"API支持"), DisplayName(@"不参与Json序列化")]
        public bool NoneJson
        {
            get => _noneJson == null ? Field.NoneJson : _noneJson.Value;
            set
            {
                if (_noneJson == value)
                    return;
                BeforePropertyChange(nameof(NoneJson), _noneJson, value);
                _noneJson = value;
                OnPropertyChanged(nameof(NoneJson));
            }
        }

        /// <summary>
        /// 示例内容
        /// </summary>
        [DataMember, JsonProperty("_helloCode", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _helloCode;

        /// <summary>
        /// 示例内容
        /// </summary>
        [JsonIgnore]
        [Category(@"API支持"), DisplayName(@"示例内容")]
        public string HelloCode
        {
            get => _helloCode ?? field.HelloCode;
            set
            {
                if (_helloCode == value)
                    return;
                BeforePropertyChange(nameof(HelloCode), _helloCode, value);
                _helloCode = value.SafeTrim();
                OnPropertyChanged(nameof(HelloCode));
            }
        }
        #endregion

        #region 设计器支持

        /// <summary>
        /// 自已
        /// </summary>
        public sealed override IPropertyConfig Me => this;

        /// <summary>
        /// 分组
        /// </summary>
        [DataMember, JsonProperty("Group", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _group;

        /// <summary>
        /// 分组
        /// </summary>
        /// <remark>
        /// 分组
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"分组"), Description("分组")]
        public string Group
        {
            get => _group ?? Field.Group;
            set
            {
                if (_group == value)
                    return;
                BeforePropertyChange(nameof(Group), _group, value);
                _group = value.SafeTrim();
                OnPropertyChanged(nameof(Group));
            }
        }
        #endregion
    }
}