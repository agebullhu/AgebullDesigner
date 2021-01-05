namespace Agebull.EntityModel.Config
{

    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IFieldConfig : IDesignField, IUIFieldConfig, IDataBaseFieldConfig, ICppFieldConfig,
        IRelationFieldConfig, IFieldRuleConfig, ICustomCodeFieldConfig, IKeyFieldConfig, ICollectFieldConfig,
        IApiFieldConfig, ICsModelFieldConfig, IEnumFieldConfig
    {

        #region 视角开关

        /// <summary>
        /// 启用数据库支持
        /// </summary>
        bool EnableDataBase
        {
            get;
        }
        /// <summary>
        /// 启用数据校验
        /// </summary>
        bool EnableValidate
        {
            get;
        }

        /// <summary>
        /// 启用编辑接口
        /// </summary>
        bool EnableEditApi
        {
            get;
        }

        /// <summary>
        /// 启用用户界面
        /// </summary>
        bool EnableUI
        {
            get;
        }

        #endregion
        #region 复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        void Copy(IFieldConfig dest,bool full=true)
        {
            if (full)
            {
                Entity = dest.Entity; 
                IsCaption = dest.IsCaption;
                IsPrimaryKey = dest.IsPrimaryKey;
                IsExtendKey = dest.IsExtendKey;
                IsIdentity = dest.IsIdentity;
                IsGlobalKey = dest.IsGlobalKey;
                UniqueIndex = dest.UniqueIndex;
                UniqueString = dest.UniqueString;
            }
            Group = dest.Group;
            Function = dest.Function;
            Having = dest.Having;
            DbFieldName = dest.DbFieldName;
            JsonName = dest.JsonName;
            DataType = dest.DataType;
            CsType = dest.CsType;
            DbType = dest.DbType;
            Initialization = dest.Initialization;
            CppType = dest.CppType;
            CppName = dest.CppName;
            CppLastType = dest.CppLastType;
            CppTypeObject = dest.CppTypeObject;
            ComputeGetCode = dest.ComputeGetCode;
            ComputeSetCode = dest.ComputeSetCode;
            IsCustomCompute = dest.IsCustomCompute;
            NoneApiArgument = dest.NoneApiArgument;
            ApiArgumentName = dest.ApiArgumentName;
            NoneJson = dest.NoneJson;
            HelloCode = dest.HelloCode;

            IsTime = dest.IsTime;
            IsArray = dest.IsArray;
            IsDictionary = dest.IsDictionary;
            IsEnum = dest.IsEnum;
            CustomType = dest.CustomType;
            ReferenceType = dest.ReferenceType;
            Nullable = dest.Nullable;
            IsExtendValue = dest.IsExtendValue;
            EnumKey = dest.EnumKey;
            EnumConfig = dest.EnumConfig;
            InnerField = dest.InnerField;
            IsSystemField = dest.IsSystemField;
            IsInterfaceField = dest.IsInterfaceField;
            IsPrivateField = dest.IsPrivateField;
            IsMiddleField = dest.IsMiddleField;
            CanGet = dest.CanGet;
            CanSet = dest.CanSet;
            IsCompute = dest.IsCompute;
            
            KeepUpdate = dest.KeepUpdate;
            DbNullable = dest.DbNullable;
            Datalen = dest.Datalen;
            ArrayLen = dest.ArrayLen;
            Scale = dest.Scale;
            FixedLength = dest.FixedLength;
            IsMemo = dest.IsMemo;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            NoProperty = dest.NoProperty;
            NoStorage = dest.NoStorage;
            KeepStorageScreen = dest.KeepStorageScreen;
            CustomWrite = dest.CustomWrite;
            StorageProperty = dest.StorageProperty;
            
            IsUserReadOnly = dest.IsUserReadOnly;
            MulitLine = dest.MulitLine;
            Prefix = dest.Prefix;
            Suffix = dest.Suffix;
            EmptyValue = dest.EmptyValue;
            UiRequired = dest.UiRequired;
            InputType = dest.InputType;
            FormCloumnSapn = dest.FormCloumnSapn;
            FormOption = dest.FormOption;
            UserOrder = dest.UserOrder;
            ComboBoxUrl = dest.ComboBoxUrl;
            IsImage = dest.IsImage;
            IsMoney = dest.IsMoney;
            GridAlign = dest.GridAlign;
            GridWidth = dest.GridWidth;
            DataFormater = dest.DataFormater;
            GridDetails = dest.GridDetails;
            NoneGrid = dest.NoneGrid;
            NoneDetails = dest.NoneDetails;
            GridDetailsCode = dest.GridDetailsCode;
            DataRuleDesc = dest.DataRuleDesc;
            ValidateCode = dest.ValidateCode;
            CanEmpty = dest.CanEmpty;
            IsRequired = dest.IsRequired;
            Max = dest.Max;
            Min = dest.Min;
            IsLinkField = dest.IsLinkField;
            LinkTable = dest.LinkTable;
            IsLinkKey = dest.IsLinkKey;
            IsLinkCaption = dest.IsLinkCaption;
            IsUserId = dest.IsUserId;
            LinkField = dest.LinkField;
        }
        #endregion
    }
}