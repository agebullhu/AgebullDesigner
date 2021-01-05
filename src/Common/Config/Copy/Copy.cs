using System.Linq;

namespace Agebull.EntityModel.Config
{

    partial class ConfigBase
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (!(dest is ConfigBase cfg))
                return;
            Option.Copy(cfg.Option, false);//配置
        }
    }

    partial class DataTypeMapConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is DataTypeMapConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(DataTypeMapConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(DataTypeMapConfig dest)
        {
            Datalen = dest.Datalen;
            Scale = dest.Scale;
            CSharp = dest.CSharp;
            Cpp = dest.Cpp;
            Java = dest.Java;
            Golang = dest.Golang;
            MySql = dest.MySql;
            SqlServer = dest.SqlServer;
            Oracle = dest.Oracle;
            Sqlite = dest.Sqlite;
            JavaScript = dest.JavaScript;
        }
    }

    partial class ParentConfigBase
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is ParentConfigBase cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(ParentConfigBase dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(ParentConfigBase dest)
        {
            Abbreviation = dest.Abbreviation;
        }
    }

    partial class ClassifyConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is ClassifyConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(ClassifyConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(ClassifyConfig dest)
        {
            Classify = dest.Classify;
        }
    }

    partial class EntityClassify
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is EntityClassify cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(EntityClassify dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(EntityClassify dest)
        {
            Project = dest.Project;
            Items = dest.Items;
        }
    }

    partial class ApiItem
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is ApiItem cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(ApiItem dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(ApiItem dest)
        {
            Method = dest.Method;
            CallArg = dest.CallArg;
            Code = dest.Code;
            ResultArg = dest.ResultArg;
            RoutePath = dest.RoutePath;
            IsUserCommand = dest.IsUserCommand;
            Argument = dest.Argument;
            Result = dest.Result;
        }
    }

    partial class EntityConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is EntityConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(EntityConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(EntityConfig dest)
        {
            MaxIdentity = dest.MaxIdentity;
            RedisKey = dest.RedisKey;
            IsLinkTable = dest.IsLinkTable;
            IsQuery = dest.IsQuery;
            NoStandardDataType = dest.NoStandardDataType;
            EntityName = dest.EntityName;
            ReferenceType = dest.ReferenceType;
            ModelInclude = dest.ModelInclude;
            ModelBase = dest.ModelBase;
            DataVersion = dest.DataVersion;

            EnableDataBase = dest.EnableDataBase;
            Interfaces = dest.Interfaces;
            ColumnIndexStart = dest.ColumnIndexStart;
            ReadCoreCodes = dest.ReadCoreCodes;
            IsInterface = dest.IsInterface;
            EnableValidate = dest.EnableValidate;
            Properties = dest.Properties;
            ReadTableName = dest.ReadTableName;
            SaveTableName = dest.SaveTableName;
            DbIndex = dest.DbIndex;
            UpdateByModified = dest.UpdateByModified;
            ApiName = dest.ApiName;
            EnableUI = dest.EnableUI;
            IsUiReadOnly = dest.IsUiReadOnly;
            PageFolder = dest.PageFolder;
            TreeUi = dest.TreeUi;
            DetailsPage = dest.DetailsPage;
            FormCloumn = dest.FormCloumn;
            OrderField = dest.OrderField;
            OrderDesc = dest.OrderDesc;
            CppName = dest.CppName;
            LastProperties = dest.LastProperties;
        }
    }

    partial class EnumConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is EnumConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(EnumConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(EnumConfig dest)
        {
            IsFlagEnum = dest.IsFlagEnum;
            Items = dest.Items;
        }
    }

    partial class EnumItem
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is EnumItem cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(EnumItem dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(EnumItem dest)
        {
            Value = dest.Value;
        }
    }

    partial class FieldConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is FieldConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(FieldConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(FieldConfig dest,bool full=true)
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
                StorageProperty = dest.StorageProperty;
            }
            Group = dest.Group;

            DataType = dest.DataType;
            CsType = dest.CsType;
            IsTime = dest.IsTime;
            IsArray = dest.IsArray;
            IsDictionary = dest.IsDictionary;
            IsEnum = dest.IsEnum;
            CustomType = dest.CustomType;
            ReferenceType = dest.ReferenceType;
            Nullable = dest.Nullable;
            NoProperty = dest.NoProperty;
            IsExtendValue = dest.IsExtendValue;
            EnumKey = dest.EnumKey;
            EnumConfig = dest.EnumConfig;
            InnerField = dest.InnerField;
            IsSystemField = dest.IsSystemField;
            IsInterfaceField = dest.IsInterfaceField;
            Initialization = dest.Initialization;
            IsPrivateField = dest.IsPrivateField;
            IsMiddleField = dest.IsMiddleField;
            CppType = dest.CppType;
            CppName = dest.CppName;
            CppLastType = dest.CppLastType;
            CppTypeObject = dest.CppTypeObject;
            CanGet = dest.CanGet;
            CanSet = dest.CanSet;
            IsCompute = dest.IsCompute;
            ComputeGetCode = dest.ComputeGetCode;
            ComputeSetCode = dest.ComputeSetCode;
            IsCustomCompute = dest.IsCustomCompute;
            NoneApiArgument = dest.NoneApiArgument;
            ApiArgumentName = dest.ApiArgumentName;
            NoneJson = dest.NoneJson;
            JsonName = dest.JsonName;
            HelloCode = dest.HelloCode;
            Function = dest.Function;
            Having = dest.Having;
            IsDbIndex = dest.IsDbIndex;
            KeepUpdate = dest.KeepUpdate;
            DbFieldName = dest.DbFieldName;
            DbNullable = dest.DbNullable;
            DbType = dest.DbType;
            Datalen = dest.Datalen;
            ArrayLen = dest.ArrayLen;
            Scale = dest.Scale;
            FixedLength = dest.FixedLength;
            IsMemo = dest.IsMemo;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            NoStorage = dest.NoStorage;
            KeepStorageScreen = dest.KeepStorageScreen;
            CustomWrite = dest.CustomWrite;
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

    }

    partial class ModelConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is ModelConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(ModelConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(ModelConfig dest)
        {
            Entity = dest.Entity;
            Name = dest.Name;
            Caption = dest.Caption;
            Description = dest.Description;
            Remark = dest.Remark;
            
            MaxIdentity = dest.MaxIdentity;
            RedisKey = dest.RedisKey;
            EntityName = dest.EntityName;
            IsQuery = dest.IsQuery;
            ReferenceType = dest.ReferenceType;
            ModelInclude = dest.ModelInclude;
            ModelBase = dest.ModelBase;
            DataVersion = dest.DataVersion;

            EnableDataBase = dest.EnableDataBase;
            IsLinkTable = dest.IsLinkTable;
            Interfaces = dest.Interfaces;
            ColumnIndexStart = dest.ColumnIndexStart;
            ReadCoreCodes = dest.ReadCoreCodes;
            IsInterface = dest.IsInterface;
            EnableValidate = dest.EnableValidate;
            Properties = dest.Properties;
            ReadTableName = dest.ReadTableName;
            SaveTableName = dest.SaveTableName;
            DbIndex = dest.DbIndex;
            UpdateByModified = dest.UpdateByModified;
            ApiName = dest.ApiName;
            EnableUI = dest.EnableUI;
            IsUiReadOnly = dest.IsUiReadOnly;
            PageFolder = dest.PageFolder;
            TreeUi = dest.TreeUi;
            DetailsPage = dest.DetailsPage;
            FormCloumn = dest.FormCloumn;
            OrderField = dest.OrderField;
            OrderDesc = dest.OrderDesc;
            CppName = dest.CppName;
            Releations = dest.Releations;
            Commands = dest.Commands;
            LastProperties = dest.LastProperties;
        }
    }

    partial class ProjectConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is ProjectConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(ProjectConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(ProjectConfig dest)
        {
            Classifies = dest.Classifies;
            Models = dest.Models;
            Entities = dest.Entities;
            ApiItems = dest.ApiItems;
            ApiName = dest.ApiName;
            Enums = dest.Enums;
            ApiFolder = dest.ApiFolder;
            ModelFolder = dest.ModelFolder;
            PageFolder = dest.PageFolder;
            BranchFolder = dest.BranchFolder;
            MobileCsPath = dest.MobileCsPath;
            CppCodePath = dest.CppCodePath;
            BusinessPath = dest.BusinessPath;
            DbType = dest.DbType;
            DbHost = dest.DbHost;
            DbSoruce = dest.DbSoruce;
            DbPassWord = dest.DbPassWord;
            DbPort = dest.DbPort;
            DbUser = dest.DbUser;
            AppId = dest.AppId;
            ProjectType = dest.ProjectType;
            CodeStyle = dest.CodeStyle;
            ReadOnly = dest.ReadOnly;
            UsingNameSpaces = dest.UsingNameSpaces;
            NameSpace = dest.NameSpace;
            DataBaseObjectName = dest.DataBaseObjectName;
            NoClassify = dest.NoClassify;
        }
    }

    partial class PropertyConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is PropertyConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(PropertyConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(PropertyConfig dest)
        {
            Model = dest.Model;
            Field = dest.Field;
            NoProperty = dest.NoProperty;
            Name = dest.Name;
            Caption = dest.Caption;
            Description = dest.Description;
            Remark = dest.Remark;
            DbFieldName = dest.DbFieldName;
            JsonName = dest.JsonName;
            DataType = dest.DataType;
            CsType = dest.CsType;
            DbType = dest.DbType;
            KeepStorageScreen = dest.KeepStorageScreen;
            Initialization = dest.Initialization;
            Function = dest.Function;
            Having = dest.Having;
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
            Group = dest.Group;
            Entity = dest.Entity;

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
            IsCaption = dest.IsCaption;
            IsPrimaryKey = dest.IsPrimaryKey;
            IsExtendKey = dest.IsExtendKey;
            IsIdentity = dest.IsIdentity;
            IsGlobalKey = dest.IsGlobalKey;
            UniqueIndex = dest.UniqueIndex;
            UniqueString = dest.UniqueString;
            KeepUpdate = dest.KeepUpdate;
            DbNullable = dest.DbNullable;
            Datalen = dest.Datalen;
            ArrayLen = dest.ArrayLen;
            Scale = dest.Scale;
            FixedLength = dest.FixedLength;
            IsMemo = dest.IsMemo;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            NoStorage = dest.NoStorage;
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
            IsTime = dest.IsTime;
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
    }

    partial class ReleationConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is ReleationConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(ReleationConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(ReleationConfig dest)
        {
            PrimaryTable = dest.PrimaryTable;
            PrimaryKey = dest.PrimaryKey;
            ForeignTable = dest.ForeignTable;
            ForeignKey = dest.ForeignKey;
            Condition = dest.Condition;
            JoinType = dest.JoinType;
            ModelType = dest.ModelType;
        }
    }

    partial class SolutionConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is SolutionConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(SolutionConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(SolutionConfig dest)
        {
            DetailTrace = dest.DetailTrace;
            GodMode = dest.GodMode;
            EnumList = dest.EnumList;
            EntityList = dest.EntityList;
            ModelList = dest.ModelList;
            ProjectList = dest.ProjectList;
            ApiList = dest.ApiList;
            RootPath = dest.RootPath;
            PagePath = dest.PagePath;
            DocFolder = dest.DocFolder;
            SrcFolder = dest.SrcFolder;
            NameSpace = dest.NameSpace;
            DataTypeMap = dest.DataTypeMap;
            SolutionType = dest.SolutionType;
            IdDataType = dest.IdDataType;
            UserIdDataType = dest.UserIdDataType;
            WorkView = dest.WorkView;
            AdvancedView = dest.AdvancedView;
        }
    }

    partial class UserCommandConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is UserCommandConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(UserCommandConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(UserCommandConfig dest)
        {
            Button = dest.Button;
            Icon = dest.Icon;
            IsLocalAction = dest.IsLocalAction;
            IsSingleObject = dest.IsSingleObject;
            Url = dest.Url;
        }
    }
}
