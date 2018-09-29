using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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
        protected ConfigBase()
        {
            GlobalTrigger.OnCtor(this);
            Option.Config = this;
        }


        [IgnoreDataMember, JsonIgnore]
        private ConfigDesignOption _option;

        /// <summary>
        /// ����
        /// </summary>
        [DataMember, JsonProperty("option", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigDesignOption Option
        {
            get => _option ?? (_option = new ConfigDesignOption { Config = this });
            set
            {
                _option = value;
                if (_option != null)
                    _option.Config = this;
                OnPropertyChanged(nameof(Option));
            }
        }

        /// <summary>
        ///     ��ǰʵ��
        /// </summary>
        public SolutionConfig Solution => Option.Solution;

        #endregion

        #region �̳�

        /// <summary>
        ///     ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public sealed override string Name
        {
            get => Option.Reference != null ? Option.Reference.Name : base.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"����")]
        public sealed override string Caption
        {
            get => Option.Reference != null ? Option.Reference.Caption : base.Caption;
            set => base.Caption = value;
        }

        /// <summary>
        ///     ˵��
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"˵��")]
        public sealed override string Description
        {
            get => Option.Reference != null ? Option.Reference.Description : base.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// �μ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("���֧��"), DisplayName(@"�μ�")]
        public sealed override string Remark
        {
            get => Option.Reference != null ? Option.Reference.Remark : base.Remark;
            set => base.Remark = value;
        }
        #endregion

        #region ��չ����

        /// <summary>
        /// ��չ����
        /// </summary>
        [DataMember, JsonProperty("extend", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Dictionary<string, string>> _extend;


        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��")]
        [DisplayName("��չ����")]
        public Dictionary<string, Dictionary<string, string>> Extend
        {
            get
            {
                if (_extend != null)
                    return _extend;
                _extend = new Dictionary<string, Dictionary<string, string>>();
                BeforePropertyChanged(nameof(Extend), null, _extend);
                return _extend;
            }
        }

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemDictionary _extendDictionary;

        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemDictionary ExtendDictionary => _extendDictionary ?? (_extendDictionary = new ConfigItemDictionary(Extend));

        /// <summary>
        /// ��д��չ����
        /// </summary>
        /// <param name="classify">����</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string classify, string name]
        {
            get => ExtendDictionary[classify, name];
            set
            {
                ExtendDictionary[classify, name] = value;
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
            get => ExtendDictionary[name];
            set
            {
                ExtendDictionary[name] = value;
                RaisePropertyChanged(name);
            }
        }

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemListBool _extendBool;

        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemListBool ExtendConfigListBool => _extendBool ?? (_extendBool = new ConfigItemListBool(ExtendDictionary));

        #endregion

        #region ��Ʊ�ʶ

        /// <summary>
        /// ��ʶ
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("��ʶ"), Description("����")]
        public Guid Key => Option.Key;

        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <remark>
        /// Ψһ��ʶ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
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
        [IgnoreDataMember, JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("���"), Description("���")]
        public int Index
        {
            get => Option.Index;
            set => Option.Index = value;
        }
        #endregion

        #region ϵͳ

        /// <summary>
        /// ���ö����
        /// </summary>
        /// <remark>
        /// ���ö����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("���ö����"), Description("���ö������ָ�ڲ����������")]
        public Guid ReferenceKey
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
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("�Ƿ���ն���"), Description("�Ƿ���ն���������Զֻ��")]
        public bool IsReference => Option.IsReference;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("����"), Description("����")]
        public bool IsDiscard => Option.IsDiscard;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ��Ϊ��,�����õĸ��Ľ������ɴ���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("����"), Description("��Ϊ��,�����õĸ��Ľ������ɴ���")]
        public bool IsFreeze => Option.IsFreeze;

        /// <summary>
        /// ���ɾ��
        /// </summary>
        /// <remark>
        /// ��Ϊ��,����ʱɾ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("���ɾ��"), Description("��Ϊ��,����ʱɾ��")]
        public bool IsDelete => Option.IsDelete;
        #endregion ϵͳ 

        #region ��չ

        /// <summary>
        /// ��ǩ����Ӧ��ϵ�ȣ�
        /// </summary>
        /// <remark>
        /// ֵ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*���"), DisplayName("��ǩ����Ӧ��ϵ�ȣ�"), Description("ֵ")]
        public string Tag
        {
            get => this["Tag"];
            set => this["Tag"] = value;
        }

        #endregion
    }
}