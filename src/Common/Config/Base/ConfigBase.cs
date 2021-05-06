using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{

    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ConfigBase : SimpleConfig
    {
        #region ���
        /// <summary>
        /// ����
        /// </summary>
        public ConfigBase()
        {
            Option.Config = this;
            ValueRecords.Add(nameof(Option), _option);
            GlobalTrigger.OnCtor(this);
        }

        /// <summary>
        /// ���ϱ༭����
        /// </summary>
        [JsonIgnore]
        public virtual ConfigBase Friend => null;

        [JsonIgnore]
        private ConfigDesignOption _option;

        /// <summary>
        /// ����
        /// </summary>
        [DataMember, JsonProperty("option", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public ConfigDesignOption Option
        {
            get
            {
                if (_option != null)
                    return _option;
                _option ??= new ConfigDesignOption { Config = this };
                BeforePropertyChange(nameof(Option), null, _option);
                return _option;
            }
            set
            {
                if (value == null)
                    return;
                BeforePropertyChange(nameof(Option), _option, value);
                _option = value;
                _option.Config = this;
                ValueRecords.Add(nameof(Option), _option);
                OnPropertyChanged(nameof(Option));
            }
        }

        /// <summary>
        ///     ��ǰʵ��
        /// </summary>
        public SolutionConfig Solution => Option.Solution;

        /// <summary>
        /// ����
        /// </summary>
        [DataMember, JsonProperty("Alias", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _alias;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ���Ա���
        /// </remark>
        [JsonIgnore]
        [Category(@"ģ�����"), DisplayName(@"����"), Description("���Ա���")]
        public string Alias
        {
            get => _alias == Name ? null : _alias;
            set
            {
                if (_alias == value)
                    return;
                BeforePropertyChange(nameof(Alias), _alias, value);
                _alias = value.SafeTrim();
                OnPropertyChanged(nameof(Alias));
            }
        }


        /// <summary>
        /// ԭʼ����
        /// </summary>
        [DataMember, JsonProperty("oldName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _oldName;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ���Ա���
        /// </remark>
        [JsonIgnore]
        [Category(@"ģ�����"), DisplayName(@"����"), Description("���Ա���")]
        public string OldName
        {
            get => _oldName;
            set
            {
                if (_oldName == value)
                    return;
                BeforePropertyChange(nameof(OldName), _oldName, value);
                _oldName = value?.Trim();
                OnPropertyChanged(nameof(OldName));
            }
        }
        #endregion

        #region �̳�
        /*
        /// <summary>
        ///     ����
        /// </summary>
        [JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public sealed override string Name
        {
            get => Option.Reference != null ? Option.Reference.Name : base.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     ����
        /// </summary>
        [JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public sealed override string Caption
        {
            get => Option.Reference != null ? Option.Reference.Caption : base.Caption;
            set => base.Caption = value;
        }

        /// <summary>
        ///     ˵��
        /// </summary>
        [JsonIgnore, Category("���֧��"), DisplayName(@"˵��")]
        public sealed override string Description
        {
            get => Option.Reference != null ? Option.Reference.Description : base.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// �μ�
        /// </summary>
        [JsonIgnore, Category("���֧��"), DisplayName(@"�μ�")]
        public sealed override string Remark
        {
            get => Option.Reference != null ? Option.Reference.Remark : base.Remark;
            set => base.Remark = value;
        }*/
        #endregion


        #region ��Ʊ�ʶ

        /// <summary>
        /// ��ʶ
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("��ʶ"), Description("����")]
        public override string Key => Option.Key;

        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <remark>
        /// Ψһ��ʶ
        /// </remark>
        [JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("Ψһ��ʶ"), Description("Ψһ��ʶ")]
        public int Identity
        {
            get => Option.Identity;
            set => Option.Identity = value;
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <remark>
        /// ���
        /// </remark>
        [JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("���"), Description("���")]
        public int Index
        {
            get => Option.Index;
            set => Option.Index = value;
        }
        #endregion

        #region ϵͳ

        /// <summary>
        /// �Ƿ�
        /// </summary>
        [JsonIgnore, Browsable(false), ReadOnly(true)]
        [Category("�����֧��"), DisplayName("�Ƿ�")]
        public bool IsActive => Option.IsNormal;

        /// <summary>
        /// ���ö����
        /// </summary>
        /// <remark>
        /// ���ö����
        /// </remark>
        [JsonIgnore]
        [Category("�����֧��"), DisplayName("���ö����"), Description("���ö������ָ�ڲ����������")]
        public string ReferenceKey
        {
            get => Option.ReferenceKey;
            set => Option.ReferenceKey = value;
        }

        /// <summary>
        /// �Ƿ���ն���
        /// </summary>
        /// <remark>
        /// �Ƿ���ն���������Զֻ��
        /// </remark>
        [JsonIgnore]
        [Category("�����֧��"), DisplayName("�Ƿ���ն���"), Description("�Ƿ���ն���������Զֻ��")]
        public bool IsReference => Option.IsReference;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [JsonIgnore]
        [Category("�����֧��"), DisplayName("����"), Description("����")]
        public bool IsDiscard
        {
            get => Option.IsDiscard;
            set => Option.IsDiscard = value;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ��Ϊ��,�����õĸ��Ľ������ɴ���
        /// </remark>
        [JsonIgnore]
        [Category("�����֧��"), DisplayName("����"), Description("��Ϊ��,�����õĸ��Ľ������ɴ���")]
        public bool IsFreeze
        {
            get => Option.IsFreeze;
            set => Option.IsFreeze = value;
        }

        /// <summary>
        /// ���ɾ��
        /// </summary>
        /// <remark>
        /// ��Ϊ��,����ʱɾ��
        /// </remark>
        [JsonIgnore]
        [Category("�����֧��"), DisplayName("���ɾ��"), Description("��Ϊ��,����ʱɾ��")]
        public bool IsDelete
        {
            get => Option.IsDelete;
            set => Option.IsDelete = value;
        }
        #endregion ϵͳ 

        #region ��չ����

        /// <summary>
        /// ��д��չ����
        /// </summary>
        /// <param name="classify">����</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string classify, string name]
        {
            get => Option[classify, name];
            set
            {
                Option[classify, name] = value;
                RaisePropertyChanged($"{classify}_{name}");
            }
        }
        /// <summary>
        /// ��д��չ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get => Option[name];
            set
            {
                Option[name] = value;
                RaisePropertyChanged(name);
            }
        }

        /// <summary>
        /// ��ǩ����Ӧ��ϵ�ȣ�
        /// </summary>
        /// <remark>
        /// ֵ
        /// </remark>
        [JsonIgnore]
        [Category("*���"), DisplayName("��ǩ����Ӧ��ϵ�ȣ�"), Description("ֵ")]
        public string Tag
        {
            get => this["Tag"];
            set => this["Tag"] = value;
        }

        /// <summary>
        /// ��չ����
        /// </summary>
        [JsonIgnore, Browsable(false)]
        public ConfigItemListBool ExtendConfigListBool => Option.ExtendConfigListBool;

        /// <summary>
        /// ��չ����
        /// </summary>
        [JsonIgnore, Browsable(false)]
        public ConfigItemList ExtendConfigList => Option.ExtendConfigList;

        #endregion
    }
}