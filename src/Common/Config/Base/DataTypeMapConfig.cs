using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ��������ӳ���
    /// </summary>
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class DataTypeMapConfig : ConfigBase
    {
        #region ȱʡ

        /// <summary>
        ///     �������������б�
        /// </summary>
        public static List<DataTypeMapConfig> DataTypeMap = new List<DataTypeMapConfig>
        {
            /* Boolean*/
            new DataTypeMapConfig
            {
                Name = "Boolean",
                Caption = "����",
                CSharp = "bool",
                Java = "bool",
                Cpp = "bool",
                MySql = "BOOL",
                SqlServer = "bit",
                Oracle = "CHAR(1)",
                Sqlite = "Integer",
                JavaScript = "bool"
            },
            /* String*/
            new DataTypeMapConfig
            {
                Datalen=200,
                Name = "String",
                Caption = "�ı�",
                CSharp = "string",
                Java = "string",
                Cpp = "string",
                MySql = "VARCHAR",
                SqlServer = "NVARCHAR",
                Oracle = "VARCHAR",
                Sqlite = "Text",
                JavaScript = "String"
            },
            /* DateTime*/
            new DataTypeMapConfig
            {
                Name = "DateTime",
                Caption = "����ʱ��",
                CSharp = "DateTime",
                Java = "DateTime",
                Cpp = "time_t",
                MySql = "DateTime",
                SqlServer = "DateTime",
                Oracle = "TIMESTAMP",
                Sqlite = "Text",
                JavaScript = "Date"
            },
            /* Guid*/
            new DataTypeMapConfig
            {
                Name = "Guid",
                Caption = "ȫ��Ψһ��ʶ��",
                CSharp = "Guid",
                Java = "uuid",
                Cpp = "uuid",
                MySql = "CHAR(36)",
                SqlServer = "UNIQUEIDENTIFIER",
                Oracle = "CHAR(36)",
                Sqlite = "Text",
                JavaScript = "Date"
            },
            /* Byte*/
            new DataTypeMapConfig
            {
                Name = "Byte",
                Caption = "�з����ֽ�",
                CSharp = "byte",
                Java = "byte",
                Cpp = "unsigned char",
                MySql = "VarBinary",
                SqlServer = "VarBinary",
                Oracle = "NUMBER",
                Sqlite = "Integer",
                JavaScript = "byte"
            },

            /* Byte[]*/
            new DataTypeMapConfig
            {
                Name = "ByteArray",
                Caption = "�з����ֽ�����",
                CSharp = "byte[]",
                Java = "byte[]",
                Cpp = "unsigned char",
                MySql = "BLOB",
                SqlServer = "VarBinary",
                Oracle = "NUMBER(3)",Sqlite = "Integer",
                JavaScript = "byte"
            },
            /* Int16*/
            new DataTypeMapConfig
            {
                Name = "Int16",
                Caption = "�з��Ŷ�����",
                CSharp = "short",
                Java = "short",
                Cpp = "short",
                MySql = "SMALLINT",
                SqlServer = "smallint",
                Oracle = "NUMBER",Sqlite = "Integer",
                JavaScript = "Number"
            },
            /* Int32*/
            new DataTypeMapConfig
            {
                Name = "Int32",
                Caption = "�з���32λ����",
                CSharp = "int",
                Java = "int",
                Cpp = "int",
                MySql = "INT",
                SqlServer = "INT",
                Oracle = "NUMBER",Sqlite = "Integer",
                JavaScript = "Number"
            },
            /* Int64*/
            new DataTypeMapConfig
            {
                Name = "Int64",
                Caption = "�з���64λ����",
                CSharp = "long",
                Java = "long",
                Cpp = "long long",
                MySql = "BIGINT",
                SqlServer = "BIGINT",
                Oracle = "NUMBER",Sqlite = "Integer",
                JavaScript = "Number"
            },
            /* Single */
            new DataTypeMapConfig
            {
                Name = "Single",
                Caption = "������С��",
                CSharp = "float",
                Java = "float",
                Cpp = "float",
                MySql = "FLOAT",
                SqlServer = "real",
                JavaScript = "Number",Sqlite = "Integer",
                Oracle = "BINARY_FLOAT"
            },
            /* Double*/
            new DataTypeMapConfig
            {
                Name = "Double",
                Caption = "˫����С��",
                CSharp = "double",
                Java = "double",
                Cpp = "double",
                MySql = "DOUBLE",
                SqlServer = "float",
                JavaScript = "Number",Sqlite = "Integer",
                Oracle = "BINARY_DOUBLE"
            },
            /* Decimal*/
            new DataTypeMapConfig
            {
                Name = "Decimal",
                Caption = "ʵ��",
                CSharp = "decimal",
                Java = "decimal",
                Cpp = "-",
                MySql = "DECIMAL",
                SqlServer = "DECIMAL",
                JavaScript = "Number",Sqlite = "Integer",
                Oracle = "DECIMAL"
            },
            /* Money*/
            new DataTypeMapConfig
            {
                Name = "Money",
                Caption = "����",
                CSharp = "decimal",
                Java = "decimal",
                Cpp = "-",
                MySql = "Money",
                SqlServer = "Money",
                JavaScript = "Number",Sqlite = "Integer",
                Oracle = "DECIMAL"
            },
            /* Object*/
            new DataTypeMapConfig
            {
                Name = "Object",
                Caption = "����",
                CSharp = "object",
                Java = "object",
                Cpp = "-",
                MySql = "-",
                SqlServer = "-",
                Oracle = "-",
                Sqlite = "-",
                JavaScript = "object",
                NoDbCheck=true
            },
            /* SByte*/
            new DataTypeMapConfig
            {
                Name = "SByte",
                Caption = "�޷����ֽ�",
                CSharp = "sbyte",
                Java = "sbyte",
                Cpp = "char",
                MySql = "VarBinary",
                SqlServer = "VarBinary",
                Oracle = "NUMBER",
                JavaScript = "byte",Sqlite = "Integer",
                NoDbCheck=true
            },
            /* UInt32*/
            new DataTypeMapConfig
            {
                Name = "UInt32",
                Caption = "�޷���32λ����",
                CSharp = "uint",
                Java = "uint",
                Cpp = "unsigned int",
                MySql = "INT UNSIGNED",
                SqlServer = "BIGINT",
                Oracle = "NUMBER",
                JavaScript = "Number",Sqlite = "Integer",
                NoDbCheck=true
            },
            /* UInt16*/
            new DataTypeMapConfig
            {
                Name = "UInt16",
                Caption = "�޷��Ŷ�����",
                CSharp = "ushort",
                Java = "ushort",
                Cpp = "unsigned short",
                MySql = "SMALLINT UNSIGNED",
                SqlServer = "smallint",
                Oracle = "NUMBER",
                JavaScript = "Number",Sqlite = "Integer",
                NoDbCheck=true
            },
            /* Enum*/
            new DataTypeMapConfig
            {
                Name = "Enum",
                Caption = "ö��",
                CSharp = "int",
                Java = "int",
                Cpp = "int",
                MySql = "int",
                SqlServer = "int",
                Oracle = "NUMBER",
                JavaScript = "Number",Sqlite = "Integer",
                NoDbCheck=true
            },
            /* UInt64*/
            new DataTypeMapConfig
            {
                Name = "UInt64",
                Caption = "�޷���64λ����",
                CSharp = "ulong",
                Java = "ulong",
                Cpp = "unsigned long long",
                MySql = "BIGINT UNSIGNED",
                SqlServer = "real",
                Oracle = "NUMBER",
                JavaScript = "Number",Sqlite = "Integer",
                NoDbCheck=true
            }
        };

        #endregion

        #region ����

        /// <summary>
        /// ���������ݿ����ͻ�ԭ
        /// </summary>
        [DataMember, JsonProperty("noDbCheck", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool NoDbCheck;


        /// <summary>
        /// ���ݳ���
        /// </summary>
        [DataMember, JsonProperty("datalen", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _datalen;

        /// <summary>
        /// ���ݳ���
        /// </summary>
        /// <remark>
        /// �ı�������ƴ洢����󳤶�
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"���ݿ�"), DisplayName(@"���ݳ���"), Description("�ı�������ƴ洢����󳤶�")]
        public int Datalen
        {
            get => _datalen;
            set
            {
                if (_datalen == value)
                    return;
                BeforePropertyChanged(nameof(Datalen), _datalen, value);
                _datalen = value;
                OnPropertyChanged(nameof(Datalen));
            }
        }
        /// <summary>
        /// �洢����
        /// </summary>
        [DataMember, JsonProperty("scale", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _scale;

        /// <summary>
        /// �洢����
        /// </summary>
        /// <remark>
        /// �洢����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"���ݿ�"), DisplayName(@"�洢����"), Description("�洢����")]
        public int Scale
        {
            get => _scale;
            set
            {
                if (_scale == value)
                    return;
                BeforePropertyChanged(nameof(Scale), _scale, value);
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }
        /// <summary>
        ///     C#
        /// </summary>
        [DataMember]
        [JsonProperty("csharp", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _csharp;

        /// <summary>
        ///     C#
        /// </summary>
        /// <remark>
        ///     C#
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("C#")]
        public string CSharp
        {
            get => _csharp;
            set
            {
                if (_csharp == value)
                    return;
                BeforePropertyChanged(nameof(CSharp), _csharp, value);
                _csharp = value;
                OnPropertyChanged(nameof(CSharp));
            }
        }

        /// <summary>
        ///     C++
        /// </summary>
        [DataMember]
        [JsonProperty("cpp", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _cpp;

        /// <summary>
        ///     C++
        /// </summary>
        /// <remark>
        ///     C++
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("C++")]
        public string Cpp
        {
            get => _cpp;
            set
            {
                if (_cpp == value)
                    return;
                BeforePropertyChanged(nameof(Cpp), _cpp, value);
                _cpp = value;
                OnPropertyChanged(nameof(Cpp));
            }
        }


        /// <summary>
        ///     Java
        /// </summary>
        [DataMember]
        [JsonProperty("java", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _java;

        /// <summary>
        ///     Java
        /// </summary>
        /// <remark>
        ///     Java
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("Java")]
        public string Java
        {
            get => _java;
            set
            {
                if (_java == value)
                    return;
                BeforePropertyChanged(nameof(Java), _java, value);
                _java = value;
                OnPropertyChanged(nameof(Java));
            }
        }

        /// <summary>
        ///     Golang
        /// </summary>
        [DataMember]
        [JsonProperty("golang", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _golang;

        /// <summary>
        ///     Golang
        /// </summary>
        /// <remark>
        ///     Golang
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("Golang")]
        public string Golang
        {
            get => _golang;
            set
            {
                if (_golang == value)
                    return;
                BeforePropertyChanged(nameof(Golang), _golang, value);
                _golang = value;
                OnPropertyChanged(nameof(Golang));
            }
        }

        /// <summary>
        ///     MySql
        /// </summary>
        [DataMember]
        [JsonProperty("mysql", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _mysql;

        /// <summary>
        ///     MySql
        /// </summary>
        /// <remark>
        ///     MySql
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("MySql")]
        public string MySql
        {
            get => _mysql;
            set
            {
                if (_mysql == value)
                    return;
                BeforePropertyChanged(nameof(MySql), _mysql, value);
                _mysql = value;
                OnPropertyChanged(nameof(MySql));
            }
        }

        /// <summary>
        ///     SqlServer
        /// </summary>
        [DataMember]
        [JsonProperty("sqlServer", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _sqlServer;

        /// <summary>
        ///     SqlServer
        /// </summary>
        /// <remark>
        ///     SqlServer
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("SqlServer")]
        public string SqlServer
        {
            get => _sqlServer;
            set
            {
                if (_sqlServer == value)
                    return;
                BeforePropertyChanged(nameof(SqlServer), _sqlServer, value);
                _sqlServer = value;
                OnPropertyChanged(nameof(SqlServer));
            }
        }

        /// <summary>
        ///     Oracle
        /// </summary>
        [DataMember]
        [JsonProperty("oracle", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _oracle;

        /// <summary>
        ///     Oracle
        /// </summary>
        /// <remark>
        ///     Oracle
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("Oracle")]
        public string Oracle
        {
            get => _oracle;
            set
            {
                if (_oracle == value)
                    return;
                BeforePropertyChanged(nameof(Oracle), _oracle, value);
                _oracle = value;
                OnPropertyChanged(nameof(Oracle));
            }
        }
        /// <summary>
        ///     Oracle
        /// </summary>
        [DataMember]
        [JsonProperty("sqlite", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _sqlite;

        /// <summary>
        ///     Oracle
        /// </summary>
        /// <remark>
        ///     Oracle
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("Oracle")]
        public string Sqlite
        {
            get => _sqlite;
            set
            {
                if (_sqlite == value)
                    return;
                BeforePropertyChanged(nameof(Sqlite), _sqlite, value);
                _sqlite = value;
                OnPropertyChanged(nameof(Sqlite));
            }
        }


        /// <summary>
        ///     JavaScript
        /// </summary>
        [DataMember]
        [JsonProperty("js", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _js;

        /// <summary>
        ///     JavaScript
        /// </summary>
        /// <remark>
        ///     JavaScript
        /// </remark>
        [IgnoreDataMember]
        [JsonIgnore]
        [Category("����")]
        [DisplayName("JavaScript")]
        public string JavaScript
        {
            get => _js;
            set
            {
                if (_js == value)
                    return;
                BeforePropertyChanged(nameof(JavaScript), _js, value);
                _js = value;
                OnPropertyChanged(nameof(JavaScript));
            }
        }

        #endregion
    }
}