namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    public interface IConfig : IKey
    {

        #region 设计标识

        /// <summary>
        /// 唯一标识
        /// </summary>
        int Identity
        {
            get;
            set;
        }

        /// <summary>
        /// 编号
        /// </summary>
        int Index
        {
            get;
            set;
        }
        #endregion

        #region 系统

        /// <summary>
        /// 引用对象键
        /// </summary>
        string ReferenceKey
        {
            get;
        }

        /// <summary>
        /// 是否参照对象
        /// </summary>
        /// <remark>
        /// 是否参照对象，是则永远只读
        /// </remark>
        bool IsReference
        {
            get;
        }

        /// <summary>
        /// 废弃
        /// </summary>
        bool IsDiscard
        {
            get;
        }

        /// <summary>
        /// 冻结
        /// </summary>
        /// <remark>
        /// 如为真,此配置的更改将不生成代码
        /// </remark>
        bool IsFreeze
        {
            get;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <remark>
        /// 如为真,保存时删除
        /// </remark>
        bool IsDelete
        {
            get;
        }
        #endregion 系统 

        #region 扩散信息

        /// <summary>
        ///     名称
        /// </summary>
        string Name
        {
            get;
            set;
        }
        /// <summary>
        ///     名称
        /// </summary>
        string OldName
        {
            get;
            set;
        }

        /// <summary>
        ///     标题
        /// </summary>
        string Caption
        {
            get;
            set;
        }

        /// <summary>
        ///     说明
        /// </summary>
        string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 参见
        /// </summary>
        string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 别名
        /// </summary>
        string Alias
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展配置
        /// </summary>
        ConfigItemListBool ExtendConfigListBool { get; }

        /// <summary>
        /// 扩展配置
        /// </summary>
        ConfigItemList ExtendConfigList { get; }

        /// <summary>
        /// 配置
        /// </summary>
        ConfigDesignOption Option { get; }

        void RaisePropertyChanged(string property);
        #endregion
    }
}