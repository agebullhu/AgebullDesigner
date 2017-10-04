// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-09-18
// // 修改:2016-09-18
// // *****************************************************/

namespace Agebull.EntityModel.Config
{
    partial class PropertyConfig
    {
        #region 复制
        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(PropertyConfig source)
        {
            //Agebull.EntityModel.Config.SimpleConfig
            Name = source.Name;//_name
            Caption = source.Caption;//_caption
            Description = source.Description;//_description
            //Agebull.EntityModel.Config.ConfigBase
            DenyScope = source.DenyScope;//阻止使用的范围
            IsReference = source.IsReference;//_isReference
            //Agebull.EntityModel.Config.PropertyConfig
            Alias = source.Alias;//Alias
            Group = source.Group;//Group
            CreateIndex = source.CreateIndex;//CreateIndex
            IsUserReadOnly = source.IsUserReadOnly;//IsUserReadOnly
            IsMemo = source.IsMemo;//IsMemo
            NoneJson = source.NoneJson;//NoneJson
            FormCloumnSapn = source.FormCloumnSapn;//FormCloumnSapn
            FormOption = source.FormOption;//FormOption
            IsMoney = source.IsMoney;//IsMoney
            GridAlign = source.GridAlign;//GridAlign
            GridWidth = source.GridWidth;//GridWidth
            DataFormater = source.DataFormater;//DataFormater
            DenyClient = source.DenyClient;//DenyClient
            GridDetails = source.GridDetails;//GridDetails
            NoneGrid = source.NoneGrid;//NoneGrid
            NoneDetails = source.NoneDetails;//NoneDetails
            GridDetailsCode = source.GridDetailsCode;//GridDetailsCode
            CppType = source.CppType;//CppType
            CppName = source.CppName;//CppName
            CsType = source.CsType;//CsType
            Nullable = source.Nullable;//Nullable
            CustomType = source.CustomType;//CustomType
            IsCompute = source.IsCompute;//IsCompute
            ComputeGetCode = source.ComputeGetCode;//ComputeGetCode
            ComputeSetCode = source.ComputeSetCode;//ComputeSetCode
            IsSystemField = source.IsSystemField;//IsSystemField
            IsInterfaceField = source.IsInterfaceField;//IsInterfaceField
            Initialization = source.Initialization;//3初始值,原样写入代码,如果是文本,需要加引号
            EmptyValue = source.EmptyValue;//等同于空值的文本,多个用#号分开
            CanEmpty = source.CanEmpty;//这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
            Max = source.Max;//最大
            Min = source.Min;//最小
            UniqueString = source.UniqueString;//5是否唯一文本
            DbType = source.DbType;//DbType
            ArrayLen = source.ArrayLen;//ArrayLen
            Scale = source.Scale;//Scale
            DbIndex = source.DbIndex;//DbIndex
            Unicode = source.Unicode;//Unicode
            FixedLength = source.FixedLength;//FixedLength
            IsBlob = source.IsBlob;//IsBlob
            DbInnerField = source.DbInnerField;//数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段，不支持外部的读写
            NoStorage = source.NoStorage;//是否不存储,如果为真,数据库的读写均忽略这个字段
            KeepStorageScreen = source.KeepStorageScreen;//跳过保存的场景
            CustomWrite = source.CustomWrite;//自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
            IsLinkField = source.IsLinkField;//IsLinkField
            LinkTable = source.LinkTable;//LinkTable
            IsLinkKey = source.IsLinkKey;//IsLinkKey
            IsLinkCaption = source.IsLinkCaption;//IsLinkCaption
            IsUserId = source.IsUserId;//IsUserId
            LinkField = source.LinkField;//LinkField
            ExtendRole = source.ExtendRole;//扩展组合规划,
            ValueSeparate = source.ValueSeparate;//值分隔符
            ArraySeparate = source.ArraySeparate;//数组分隔符
            ExtendArray = source.ExtendArray;//是否扩展数组,是则解析为二维数组,否解析为一维数组
            IsKeyValueArray = source.IsKeyValueArray;//是否值对分隔方式,
            IsRelation = source.IsRelation;//是否为关系表,是则扩展组合规划 按 表名 解析成表间一对多关系
            ExtendPropertyName = source.ExtendPropertyName;//ExtendPropertyName
            ExtendClassName = source.ExtendClassName;//ExtendClassName
            ExtendClassIsPredestinate = source.ExtendClassIsPredestinate;//ExtendClassIsPredestinate
            _isRequired = source._isRequired;//_isRequired
            _prefix = source._prefix;//_prefix
            _suffix = source._suffix;//_suffix
            _inputType = source._inputType;//_inputType
            _comboBoxUrl = source._comboBoxUrl;//_comboBoxUrl
            _cppLastType = source._cppLastType;//_cppLastType
            IsMiddleField = source.IsMiddleField;//_middleField
            _innerField = source._innerField;//_innerField
            _columnName = source._columnName;//_columnName
            _dbNullable = source._dbNullable;//_dbNullable
            _datalen = source._datalen;//_datalen
        }

        /// <summary>
        /// 复制设置(不复制特定于实体的内容,如Index,Key等)
        /// </summary>
        /// <param name="source"></param>
        public void CopyConfig(PropertyConfig source,bool byName=true)
        {
            if (byName)
            {
                Name = source.Name;//_name
                Caption = source.Caption;//_caption
                Description = source.Description;//_description
                Alias = source.Alias;//Alias
            }
            //Agebull.EntityModel.Config.ConfigBase
            DenyScope = source.DenyScope;//阻止使用的范围


            IsUserReadOnly = source.IsUserReadOnly;//IsUserReadOnly
            IsMemo = source.IsMemo;//IsMemo
            NoneJson = source.NoneJson;//NoneJson

            IsMoney = source.IsMoney;//IsMoney

            DataFormater = source.DataFormater;//DataFormater
            DenyClient = source.DenyClient;//DenyClient

            CppType = source.CppType;//CppType
            CppName = source.CppName;//CppName
            CsType = source.CsType;//CsType
            Nullable = source.Nullable;//Nullable
            CustomType = source.CustomType;//CustomType
            IsCompute = source.IsCompute;//IsCompute
            ComputeGetCode = source.ComputeGetCode;//ComputeGetCode
            ComputeSetCode = source.ComputeSetCode;//ComputeSetCode
            IsSystemField = source.IsSystemField;//IsSystemField

            Initialization = source.Initialization;//3初始值,原样写入代码,如果是文本,需要加引号
            EmptyValue = source.EmptyValue;//等同于空值的文本,多个用#号分开
            CanEmpty = source.CanEmpty;//这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
            Max = source.Max;//最大
            Min = source.Min;//最小
            UniqueString = source.UniqueString;//5是否唯一文本
            DbType = source.DbType;//DbType
            ArrayLen = source.ArrayLen;//ArrayLen
            Scale = source.Scale;//Scale
            
            Unicode = source.Unicode;//Unicode
            FixedLength = source.FixedLength;//FixedLength
            IsBlob = source.IsBlob;//IsBlob
            DbInnerField = source.DbInnerField;//数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段，不支持外部的读写
            NoStorage = source.NoStorage;//是否不存储,如果为真,数据库的读写均忽略这个字段
            KeepStorageScreen = source.KeepStorageScreen;//跳过保存的场景
            CustomWrite = source.CustomWrite;//自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护

            LinkTable = source.LinkTable;//LinkTable


            IsUserId = source.IsUserId;//IsUserId

            ExtendRole = source.ExtendRole;//扩展组合规划,
            ValueSeparate = source.ValueSeparate;//值分隔符
            ArraySeparate = source.ArraySeparate;//数组分隔符
            ExtendArray = source.ExtendArray;//是否扩展数组,是则解析为二维数组,否解析为一维数组
            IsKeyValueArray = source.IsKeyValueArray;//是否值对分隔方式,
            IsRelation = source.IsRelation;//是否为关系表,是则扩展组合规划 按 表名 解析成表间一对多关系
            ExtendPropertyName = source.ExtendPropertyName;//ExtendPropertyName
            ExtendClassName = source.ExtendClassName;//ExtendClassName
            ExtendClassIsPredestinate = source.ExtendClassIsPredestinate;//ExtendClassIsPredestinate
            _cppLastType = source._cppLastType;//_cppLastType
            IsMiddleField = source.IsMiddleField;//_middleField
            _innerField = source._innerField;//_innerField
            _columnName = source._columnName;//_columnName
            _dbNullable = source._dbNullable;//_dbNullable
            _datalen = source._datalen;//_datalen
        }

        #endregion


        #region 预定义

        #endregion

        #region 扩展

        /*// <summary>
        ///     是否关系字段
        /// </summary>
        [Browsable(false)]
        public bool IsRelationField => IsRelation && !string.IsNullOrWhiteSpace(ExtendRole);

        /// <summary>
        ///     是否关系值
        /// </summary>
        [Browsable(false)]
        public bool IsRelationValue => IsRelation && !ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);

        /// <summary>
        ///     是否关系数组
        /// </summary>
        [Browsable(false)]
        public bool IsRelationArray => IsRelation && ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);
        /// <summary>
        /// 是否扩展数组
        /// </summary>
        [Browsable(false)]
        public bool IsExtendArray => !IsRelation && ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);

        /// <summary>
        /// 是否扩展值
        /// </summary>
        [Browsable(false)]
        public bool IsExtendValue => !IsRelation && !ExtendArray && !string.IsNullOrWhiteSpace(ExtendRole);
        */
        #endregion
    }
}