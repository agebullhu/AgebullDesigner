// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // ����:
// // ����:CodeRefactor
// // ����:2016-06-06
// // �޸�:2016-08-03
// // *****************************************************/

#region ����

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

#endregion

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     ��Ŀ����
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ProjectConfig : ParentConfigBase
    {

        #region �Ӽ�


        /// <summary>
        /// �Ӽ�
        /// </summary>
        public override IEnumerable<ConfigBase> MyChilds => _children;

        /// <summary>
        ///     �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ObservableCollection<EntityConfig> _children;

        /// <summary>
        ///     �Ӽ�
        /// </summary>
        [Browsable(false), IgnoreDataMember, JsonIgnore]
        public ObservableCollection<EntityConfig> Children
        {
            get { return _children ?? (_children = new ObservableCollection<EntityConfig>()); }
            set
            {
                if (_children == value)
                {
                    return;
                }
                _children = value;
                RaisePropertyChanged(() => Children);
            }
        }
        /// <summary>
        ///     �Ӽ�
        /// </summary>
        [Browsable(false), IgnoreDataMember, JsonIgnore]
        public ObservableCollection<EntityConfig> Entities
        {
            get { return Children; }
            set
            {
                Children = value;
                RaisePropertyChanged(() => Entities);
            }
        }

        /// <summary>
        ///     ��Ӧ��API����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ObservableCollection<TypedefItem> _typedefItem;

        /// <summary>
        ///     ��Ӧ��API����
        /// </summary>
        [Browsable(false), IgnoreDataMember, JsonIgnore]
        public ObservableCollection<TypedefItem> TypedefItems
        {
            get { return _typedefItem ?? (_typedefItem = new ObservableCollection<TypedefItem>()); }
            set
            {
                if (_typedefItem == value)
                {
                    return;
                }
                _typedefItem = value;
                RaisePropertyChanged(() => TypedefItems);
            }
        }
        #endregion
        
        #region ����·��

        /// <summary>
        /// C# �����(WEBҳ��)
        /// </summary>
        [DataMember, JsonProperty("_pagePath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _pagePath;

        /// <summary>
        /// C# �����(WEBҳ��)
        /// </summary>
        /// <remark>
        /// ҳ�����·��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����·��"), DisplayName("C# �����(WEBҳ��)"), Description("ҳ�����·��")]
        public string PagePath
        {
            get
            {
                return _pagePath;
            }
            set
            {
                if (_pagePath == value)
                    return;
                BeforePropertyChanged(nameof(PagePath), _pagePath, value);
                _pagePath = value;
                OnPropertyChanged(nameof(PagePath));
            }
        }

        /// <summary>
        /// C# �����(����ģ��)
        /// </summary>
        [DataMember, JsonProperty("_modelPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelPath;

        /// <summary>
        /// C# �����(����ģ��)
        /// </summary>
        /// <remark>
        /// ģ�ʹ���·��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����·��"), DisplayName("C# �����(����ģ��)"), Description("ģ�ʹ���·��")]
        public string ModelPath
        {
            get
            {
                return _modelPath;
            }
            set
            {
                if (_modelPath == value)
                    return;
                BeforePropertyChanged(nameof(ModelPath), _modelPath, value);
                _modelPath = value;
                OnPropertyChanged(nameof(ModelPath));
            }
        }

        /// <summary>
        /// C# �ƶ���
        /// </summary>
        [DataMember, JsonProperty("_mobileCsPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _mobileCsPath;

        /// <summary>
        /// C# �ƶ���
        /// </summary>
        /// <remark>
        /// �ƶ��˴���·��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����·��"), DisplayName("C# �ƶ���"), Description("�ƶ��˴���·��")]
        public string MobileCsPath
        {
            get
            {
                return _mobileCsPath;
            }
            set
            {
                if (_mobileCsPath == value)
                    return;
                BeforePropertyChanged(nameof(MobileCsPath), _mobileCsPath, value);
                _mobileCsPath = value;
                OnPropertyChanged(nameof(MobileCsPath));
            }
        }

        /// <summary>
        /// C++ �����
        /// </summary>
        [DataMember, JsonProperty("_codePath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _codePath;

        /// <summary>
        /// C++ �����
        /// </summary>
        /// <remark>
        /// C++�����ַ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����·��"), DisplayName("C++ �����"), Description("C++�����ַ")]
        public string CodePath
        {
            get
            {
                return _codePath;
            }
            set
            {
                if (_codePath == value)
                    return;
                BeforePropertyChanged(nameof(CodePath), _codePath, value);
                _codePath = value;
                OnPropertyChanged(nameof(CodePath));
            }
        }

        /// <summary>
        /// C# �����(ҵ���߼�)
        /// </summary>
        [DataMember, JsonProperty("_blPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _businessPath;

        /// <summary>
        /// C# �����(ҵ���߼�)
        /// </summary>
        /// <remark>
        /// ҵ���߼�����·��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����·��"), DisplayName("C# �����(ҵ���߼�)"), Description("ҵ���߼�����·��")]
        public string BusinessPath
        {
            get
            {
                return _businessPath;
            }
            set
            {
                if (_businessPath == value)
                    return;
                BeforePropertyChanged(nameof(BusinessPath), _businessPath, value);
                _businessPath = value;
                OnPropertyChanged(nameof(BusinessPath));
            }
        }

        /// <summary>
        /// C# PC��
        /// </summary>
        [DataMember, JsonProperty("_clientPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _clientCsPath;

        /// <summary>
        /// C# PC��
        /// </summary>
        /// <remark>
        /// ģ�ʹ���·��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����·��"), DisplayName("C# PC��"), Description("ģ�ʹ���·��")]
        public string ClientCsPath
        {
            get
            {
                return _clientCsPath;
            }
            set
            {
                if (_clientCsPath == value)
                    return;
                BeforePropertyChanged(nameof(ClientCsPath), _clientCsPath, value);
                _clientCsPath = value;
                OnPropertyChanged(nameof(ClientCsPath));
            }
        }
        #endregion ����·�� 

        #region ���ݿ�

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        [DataMember, JsonProperty("_dbType", NullValueHandling = NullValueHandling.Ignore)]
        internal DataBaseType _dbType;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <remark>
        /// ���ݿ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("���ݿ�����"), Description("���ݿ�����")]
        public DataBaseType DbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                if (_dbType == value)
                    return;
                BeforePropertyChanged(nameof(DbType), _dbType, value);
                _dbType = value;
                OnPropertyChanged(nameof(DbType));
            }
        }

        /// <summary>
        /// ���ݿ��ַ
        /// </summary>
        [DataMember, JsonProperty("_dbHost", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbHost;

        /// <summary>
        /// ���ݿ��ַ
        /// </summary>
        /// <remark>
        /// ���ݿ��ַ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("���ݿ��ַ"), Description("���ݿ��ַ")]
        public string DbHost
        {
            get
            {
                return _dbHost;
            }
            set
            {
                if (_dbHost == value)
                    return;
                BeforePropertyChanged(nameof(DbHost), _dbHost, value);
                _dbHost = value;
                OnPropertyChanged(nameof(DbHost));
            }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        [DataMember, JsonProperty("_dbSoruce", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbSoruce;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <remark>
        /// ���ݿ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("���ݿ�����"), Description("���ݿ�����")]
        public string DbSoruce
        {
            get
            {
                return _dbSoruce;
            }
            set
            {
                if (_dbSoruce == value)
                    return;
                BeforePropertyChanged(nameof(DbSoruce), _dbSoruce, value);
                _dbSoruce = value;
                OnPropertyChanged(nameof(DbSoruce));
            }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        [DataMember, JsonProperty("_dbPassWord", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbPassWord;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <remark>
        /// ���ݿ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("���ݿ�����"), Description("���ݿ�����")]
        public string DbPassWord
        {
            get
            {
                return _dbPassWord;
            }
            set
            {
                if (_dbPassWord == value)
                    return;
                BeforePropertyChanged(nameof(DbPassWord), _dbPassWord, value);
                _dbPassWord = value;
                OnPropertyChanged(nameof(DbPassWord));
            }
        }

        /// <summary>
        /// ���ݿ��û�
        /// </summary>
        [DataMember, JsonProperty("_dbUser", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbUser;

        /// <summary>
        /// ���ݿ��û�
        /// </summary>
        /// <remark>
        /// ���ݿ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("���ݿ��û�"), Description("���ݿ�����")]
        public string DbUser
        {
            get
            {
                return _dbUser;
            }
            set
            {
                if (_dbUser == value)
                    return;
                BeforePropertyChanged(nameof(DbUser), _dbUser, value);
                _dbUser = value;
                OnPropertyChanged(nameof(DbUser));
            }
        }
        #endregion ���ݿ� 

        #region ����ģ��

        /// <summary>
        /// ����ʱֻ��
        /// </summary>
        [DataMember, JsonProperty("_readOnly", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _readOnly;

        /// <summary>
        /// ����ʱֻ��
        /// </summary>
        /// <remark>
        /// ����ʱֻ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����ģ��"), DisplayName("����ʱֻ��"), Description("����ʱֻ��")]
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                if (_readOnly == value)
                    return;
                BeforePropertyChanged(nameof(ReadOnly), _readOnly, value);
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }

        /// <summary>
        /// �����ռ�
        /// </summary>
        [DataMember, JsonProperty("_entityNameSpace", NullValueHandling = NullValueHandling.Ignore)]
        internal string _nameSpace;

        /// <summary>
        /// �����ռ�
        /// </summary>
        /// <remark>
        /// �����ռ�
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����ģ��"), DisplayName("�����ռ�"), Description("�����ռ�")]
        public string NameSpace
        {
            get
            {
                return _nameSpace;
            }
            set
            {
                if (_nameSpace == value)
                    return;
                BeforePropertyChanged(nameof(NameSpace), _nameSpace, value);
                _nameSpace = value;
                OnPropertyChanged(nameof(NameSpace));
            }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        [DataMember, JsonProperty("_dbName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataBaseObjectName;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <remark>
        /// ���ݿ�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����ģ��"), DisplayName("���ݿ�����"), Description("���ݿ�����")]
        public string DataBaseObjectName
        {
            get
            {
                return _dataBaseObjectName;
            }
            set
            {
                if (_dataBaseObjectName == value)
                    return;
                BeforePropertyChanged(nameof(DataBaseObjectName), _dataBaseObjectName, value);
                _dataBaseObjectName = value;
                OnPropertyChanged(nameof(DataBaseObjectName));
            }
        }
        #endregion ����ģ��
    }
}