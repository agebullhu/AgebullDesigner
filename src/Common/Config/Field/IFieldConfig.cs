using Agebull.EntityModel.Config.V2021;

namespace Agebull.EntityModel.Config
{

    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IPropertyConfig : IConfig, IDesignField, IUIFieldConfig, ICppFieldConfig,
        IRelationFieldConfig, IFieldRuleConfig, ICustomCodeFieldConfig, IKeyFieldConfig, ICollectFieldConfig,
        IApiFieldConfig, ICsModelFieldConfig, IEnumFieldConfig, IConfigIterator//IDataBaseFieldConfig,
    {

        /// <summary>
        /// 数据字段
        /// </summary>
        DataBaseFieldConfig DataBaseField { get; set; }

        #region 视角开关

        /// <summary>
        /// 自增字段
        /// </summary>
        bool IsIdentity
        {
            get;
        }
        /// <summary>
        /// 活动（未删除未废弃）
        /// </summary>
        bool IsActive
        {
            get;
        }
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
        /// <summary>
        /// 不存储
        /// </summary>
        bool NoStorage { get; }

        #endregion
        #region 复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        void Copy(IPropertyConfig dest, bool full = true);
        #endregion
    }
}