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
            var cfg = dest as ConfigBase;
            if (cfg == null)
                return;

            Option.Copy(cfg.Option);//配置
            if (cfg._extend != null)
                _extend = cfg._extend.ToDictionary(p => p.Key, p => p.Value);//扩展配置
        }
    }

    partial class ApiItem
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as ApiItem;
            if (cfg == null)
                return;

            Method = cfg.Method;//Api调用方式（GET、POST）
            CallArg = cfg.CallArg;//请求参数名称
            ResultArg = cfg.ResultArg;//返回参数名称
            RoutePath = cfg.RoutePath;//路由路径
            IsUserCommand = cfg.IsUserCommand;//是否用户命令
            Argument = cfg.Argument;//参数实体
            Result = cfg.Result;//本地对象名称
        }

    }

    partial class ClassifyConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as ClassifyConfig;
            if (cfg == null)
                return;

            Classify = cfg.Classify;//分类
        }

    }

    partial class DataTypeMapConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as DataTypeMapConfig;
            if (cfg == null)
                return;

            Datalen = cfg.Datalen;//数据长度
            Scale = cfg.Scale;//存储精度
            CSharp = cfg.CSharp;//C#
            Cpp = cfg.Cpp;//C++
            Java = cfg.Java;//Java
            Golang = cfg.Golang;//Golang
            MySql = cfg.MySql;//MySql
            SqlServer = cfg.SqlServer;//SqlServer
            Oracle = cfg.Oracle;//Oracle
            JavaScript = cfg.JavaScript;//JavaScript
        }

    }

    partial class EntityConfig
    {
        /// <summary>
        ///     复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        /// <param name="noChilds">是否复制子级(默认为是)</param>
        public void CopyValue(EntityConfig source, bool noChilds = false)
        {
            Caption = source.Caption + "(复制)";
            Description = source.Description;
            Name = source.Name + "Copy";
            DataVersion = source.DataVersion;
            DbIndex = source.DbIndex;
            IsInternal = source.IsInternal;
            NoDataBase = source.NoDataBase;
            ReadTableName = source.ReadTableName;
            SaveTableName = source.SaveTableName;
            Classify = source.Classify;
            CppName = source.CppName;
            ReferenceKey = source.ReferenceKey;
            TreeUi = source.TreeUi;
            if (noChilds)
                return;
            foreach (var field in source.Properties)
            {
                var nf = new PropertyConfig();
                nf.CopyFromProperty(field,false,true, true);
                if (field.IsPrimaryKey)
                    nf.IsPrimaryKey = true;
                Add(nf);
            }
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public void CopyFrom(EntityConfig cfg)
        {
            base.CopyFrom(cfg);

            foreach (var item in cfg.Properties.Where(p => !p.IsDelete && !p.IsDiscard))//字段列表
            {
                var child = new PropertyConfig();
                child.Copy(item);
                Add(child);
            }
            DenyScope = cfg.DenyScope;//阻止编辑
            MaxIdentity = cfg.MaxIdentity;//最大字段标识号
            RedisKey = cfg.RedisKey;//Redis唯一键模板
            NoStandardDataType = cfg.NoStandardDataType;//非标准数据类型
            EntityName = cfg.EntityName;//实体名称
            ReferenceType = cfg.ReferenceType;//参考类型(C#)
            ModelInclude = cfg.ModelInclude;//模型
            ModelBase = cfg.ModelBase;//基类
            DataVersion = cfg.DataVersion;//数据版本
            IsInternal = cfg.IsInternal;//内部数据
            NoDataBase = cfg.NoDataBase;//无数据库支持
            Interfaces = cfg.Interfaces;//继承的接口集合
            foreach (var item in cfg.Releations)//字段
            {
                var child = new EntityReleationConfig();
                child.Copy(item);
                Add(child);
            }
            ColumnIndexStart = cfg.ColumnIndexStart;//列序号起始值
            ReadCoreCodes = cfg.ReadCoreCodes;//不同版本读数据的代码
            IsInterface = cfg.IsInterface;//接口定义
            foreach (var item in cfg.Commands)//命令集合
            {
                var child = new UserCommandConfig();
                child.Copy(item);
                Add(child);
            }
            ReadTableName = cfg.ReadTableName;//存储表名(设计录入)
            SaveTableName = cfg.SaveTableName;//存储表名
            DbIndex = cfg.DbIndex;//数据库编号
            UpdateByModified = cfg.UpdateByModified;//按修改更新
            PageFolder = cfg.PageFolder;//页面文件夹名称
            TreeUi = cfg.TreeUi;//树形界面
            MaxForm = cfg.MaxForm;//编辑页面最大化
            FormCloumn = cfg.FormCloumn;//编辑页面分几列
            ListDetails = cfg.ListDetails;//列表详细页
            NoSort = cfg.NoSort;//主键正序
            PanelType = cfg.PanelType;//主页面类型
            CppName = cfg.CppName;//C++名称
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            var cfg = dest as EntityConfig;
            if (cfg == null)
                base.CopyFrom(dest);
            else
                CopyFrom(cfg);
        }

    }

    partial class EntityReleationConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as EntityReleationConfig;
            if (cfg == null)
                return;

            Parent = cfg.Parent;//上级
            ForeignKey = cfg.ForeignKey;//关联表的外键名称
            PrimaryKey = cfg.PrimaryKey;//与关联表的外键对应的字段名称
            Friend = cfg.Friend;//关联表
            Releation = cfg.Releation;//关系类型
            Condition = cfg.Condition;//扩展条件
        }

    }

    partial class EnumConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as EnumConfig;
            if (cfg == null)
                return;

            IsFlagEnum = cfg.IsFlagEnum;//是否位域
            foreach (var item in cfg.Items)//枚举节点
            {
                var child = new EnumItem();
                child.Copy(item);
                Items.Add(child);
            }
        }

    }

    partial class EnumItem
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as EnumItem;
            if (cfg == null)
                return;

            Value = cfg.Value;//值
        }

    }

    partial class ProjectConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as ProjectConfig;
            if (cfg == null)
                return;

            //foreach (var item in cfg.Classifies)//实体分组
            //{
            //    var child = new ClassifyItem<EntityConfig>();
            //    child.Copy(item);
            //    Classifies.Add(child);
            //}
            //foreach (var item in cfg.Entities)//实体集合
            //{
            //    var child = new EntityConfig();
            //    child.Copy(item);
            //    Add(child);
            //}
            //foreach (var item in cfg.ApiItems)//API节点集合
            //{
            //    var child = new ApiItem();
            //    child.Copy(item);
            //    Add(child);
            //}
            //ApiName = cfg.ApiName;//接口名称
            //foreach (var item in cfg.Enums)//枚举集合
            //{
            //    var child = new EnumConfig();
            //    child.Copy(item);
            //    Add(child);
            //}
            ApiFolder = cfg.ApiFolder;//接口代码主文件夹
            ModelFolder = cfg.ModelFolder;//模型代码主文件夹
            BranchFolder = cfg.BranchFolder;//子级文件夹
            PageFolder = cfg.PageFolder;//WEB页面(C#)
            MobileCsPath = cfg.MobileCsPath;//移动端(C#)
            CppCodePath = cfg.CppCodePath;//服务端(C++)
            BusinessPath = cfg.BusinessPath;//业务逻辑(C#)
            DbType = cfg.DbType;//数据库类型
            DbHost = cfg.DbHost;//数据库地址
            DbSoruce = cfg.DbSoruce;//数据库名称
            DbPassWord = cfg.DbPassWord;//数据库密码
            DbUser = cfg.DbUser;//数据库用户
            ProjectType = cfg.ProjectType;//项目类型
            ReadOnly = cfg.ReadOnly;//运行时只读
            NameSpace = cfg.NameSpace;//命名空间
            DataBaseObjectName = cfg.DataBaseObjectName;//数据项目名称
        }

    }

    partial class PropertyConfig
    {
        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="cfg">配置对象</param>
        /// <param name="full">全量</param>
        /// <param name="option">系统配置</param>
        /// <param name="primary">主键相关</param>
        public void CopyFromProperty(PropertyConfig cfg, bool primary, bool full,bool option)
        {
            Name = cfg.Name;
            Caption = cfg.Caption;
            Description = cfg.Description;
            Remark = cfg.Remark;

            DataType = cfg.DataType;//数据类型
            CsType = cfg.CsType;//语言类型(C#)
            IsEnum = cfg.IsEnum;//是否枚举类型)
            IsArray = cfg.IsArray;//是否数组
            IsDictionary = cfg.IsDictionary;//是否字典
            CustomType = cfg.CustomType;//非基本类型名称(C#)
            EnumConfig = cfg.EnumConfig;//对应枚举
            ReferenceType = cfg.ReferenceType;//参考类型(C#)
            Nullable = cfg.Nullable;//可空类型(C#)
            IsExtendValue = cfg.IsExtendValue;//是否扩展值
            InnerField = cfg.InnerField;//内部字段
            IsSystemField = cfg.IsSystemField;//系统字段
            IsInterfaceField = cfg.IsInterfaceField;//接口字段
            Initialization = cfg.Initialization;//初始值
            IsMiddleField = cfg.IsMiddleField;//设计时字段
            CppType = cfg.CppType;//语言类型(C++)
            CppName = cfg.CppName;//字段名称(C++)
            CppLastType = cfg.CppLastType;//结果类型(C++)
            CppTypeObject = cfg.CppTypeObject;//C++字段类型
            CanGet = cfg.CanGet;//可读
            CanSet = cfg.CanSet;//可写
            IsCompute = cfg.IsCompute;//计算列
            ComputeGetCode = cfg.ComputeGetCode;//自定义代码(get)
            ComputeSetCode = cfg.ComputeSetCode;//自定义代码(set)
            IsCustomCompute = cfg.IsCustomCompute;//自定义读写代码
            
            HelloCode = cfg.HelloCode;//示例内容
            
            
            KeepUpdate = cfg.KeepUpdate;//不更新
            ColumnName = cfg.ColumnName;//数据库字段名称
            DbNullable = cfg.DbNullable;//能否存储空值
            DbType = cfg.DbType;//存储类型
            Datalen = cfg.Datalen;//数据长度
            ArrayLen = cfg.ArrayLen;//数组长度
            Scale = cfg.Scale;//存储精度
            DbIndex = cfg.DbIndex;//存储列ID
            Unicode = cfg.Unicode;//UNICODE
            FixedLength = cfg.FixedLength;//固定长度
            IsMemo = cfg.IsMemo;//备注字段
            IsBlob = cfg.IsBlob;//大数据
            DbInnerField = cfg.DbInnerField;//内部字段(数据库)
            NoStorage = cfg.NoStorage;//非数据库字段
            KeepStorageScreen = cfg.KeepStorageScreen;//*跳过保存的场景
            CustomWrite = cfg.CustomWrite;//自定义保存
            StorageProperty = cfg.StorageProperty;//存储值读写字段
            CreateDbIndex = cfg.CreateDbIndex;//数据库索引

            IsLinkField = cfg.IsLinkField;//连接字段
            LinkTable = cfg.LinkTable;//关联表名
            LinkField = cfg.LinkField;//关联字段名称
            IsLinkKey = cfg.IsLinkKey;//关联表主键
            IsLinkCaption = cfg.IsLinkCaption;//关联表标题

            IsUserId = cfg.IsUserId;//对应客户ID

            CanEmpty = cfg.CanEmpty;//能否为空
            Max = cfg.Max;//最大值
            Min = cfg.Min;//最大值
            
            ExtendRole = cfg.ExtendRole;//扩展组合规划
            ValueSeparate = cfg.ValueSeparate;//值分隔符
            ArraySeparate = cfg.ArraySeparate;//数组分隔符
            ExtendArray = cfg.ExtendArray;//是否扩展数组
            IsKeyValueArray = cfg.IsKeyValueArray;//是否值对分隔方式
            IsRelation = cfg.IsRelation;//是否为关系表
            ExtendPropertyName = cfg.ExtendPropertyName;//扩展对象属性名称
            ExtendClassName = cfg.ExtendClassName;//扩展对象对象名称
            ExtendClassIsPredestinate = cfg.ExtendClassIsPredestinate;//扩展对象对象已定义

            JsonName = cfg.JsonName;//字段名称(json)

            if (option)
                Option.Copy(cfg.Option);//配置

            if (full)
            {
                Group = cfg.Group;//分组
                Alias = cfg.Alias;//别名

                NoneJson = cfg.NoneJson;//不参与Json序列化
                IsUserReadOnly = cfg.IsUserReadOnly;//不可编辑
                DenyScope = cfg.DenyScope;//阻止编辑

                ValidateCode = cfg.ValidateCode;//校验代码
                IsRequired = cfg.IsRequired;//必填字段

                UiRequired = cfg.UiRequired;//必填字段
                MulitLine = cfg.MulitLine;//多行文本
                Prefix = cfg.Prefix;//前缀
                Suffix = cfg.Suffix;//后缀
                EmptyValue = cfg.EmptyValue;//等同于空值的文本
                InputType = cfg.InputType;//输入类型
                FormCloumnSapn = cfg.FormCloumnSapn;//Form中占几列宽度
                FormOption = cfg.FormOption;//Form中的EasyUi设置
                ComboBoxUrl = cfg.ComboBoxUrl;//下拉列表的地址
                IsMoney = cfg.IsMoney;//货币类型
                GridAlign = cfg.GridAlign;//表格对齐
                GridWidth = cfg.GridWidth;//占表格宽度比例
                DataFormater = cfg.DataFormater;//数据格式器
                GridDetails = cfg.GridDetails;//显示在列表详细页中
                NoneGrid = cfg.NoneGrid;//列表不显示
                NoneDetails = cfg.NoneDetails;//详细不显示
                GridDetailsCode = cfg.GridDetailsCode;//列表详细页代码
                




                if (cfg._extend != null)
                    _extend = cfg._extend.ToDictionary(p => p.Key, p => p.Value);//扩展配置
            }

            if (primary)
            {

                IsCaption = cfg.IsCaption;//标题字段
                IsPrimaryKey = cfg.IsPrimaryKey;//主键字段
                IsExtendKey = cfg.IsExtendKey;//唯一值字段
                IsIdentity = cfg.IsIdentity;//自增字段
                IsGlobalKey = cfg.IsGlobalKey;//全局标识
                UniqueIndex = cfg.UniqueIndex;//唯一属性组合顺序
                UniqueString = cfg.UniqueString;//唯一文本
            }
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as PropertyConfig;
            if (cfg == null)
                return;
            CopyFromProperty(cfg,true,true,true);
        }

    }

    partial class SolutionConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as SolutionConfig;
            if (cfg == null)
                return;

            DocFolder = cfg.DocFolder;//文档文件夹名称
            SrcFolder = cfg.SrcFolder;//代码文件夹名称
            RootPath = cfg.RootPath;//解决方案根路径
            NameSpace = cfg.NameSpace;//解决方案命名空间
            foreach (var item in cfg.DataTypeMap)//数据类型映射
            {
                var child = new DataTypeMapConfig();
                child.Copy(item);
                DataTypeMap.Add(child);
            }
            SolutionType = cfg.SolutionType;//解决方案类型
            IdDataType = cfg.IdDataType;//主键数据类型
            UserIdDataType = cfg.UserIdDataType;//用户标识数据类型
            WorkView = cfg.WorkView;//工作视角
            AdvancedView = cfg.AdvancedView;//高级视角
        }

    }

    partial class UserCommandConfig
    {
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            var cfg = dest as UserCommandConfig;
            if (cfg == null)
                return;

            Button = cfg.Button;//按钮名称
            Icon = cfg.Icon;//按钮图标
            IsLocalAction = cfg.IsLocalAction;//本地操作
            IsSingleObject = cfg.IsSingleObject;//单对象操作
            Url = cfg.Url;//打开链接
        }

    }


}
