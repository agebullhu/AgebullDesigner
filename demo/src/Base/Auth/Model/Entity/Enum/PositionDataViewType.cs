namespace Gboxt.Common.DataModel
{
    /// <summary>
    /// 职位数据视角类型
    /// </summary>
    public enum PositionDataScopeType
    {
        /// <summary>
        /// 默认值，没有任何权限制
        /// </summary>
        None,
        /// <summary>
        /// 仅限本人的数据
        /// </summary>
        Self,
        /// <summary>
        /// 本部门的数据
        /// </summary>
        Department,
        /// <summary>
        /// 本部门及下级的数据
        /// </summary>
        DepartmentAndLower,
        /// <summary>
        /// 本公司的数据
        /// </summary>
        Company,
        /// <summary>
        /// 本公司及下级的数据
        /// </summary>
        CompanyAndLower,

        /// <summary>
        /// 自定义
        /// </summary>
        Custom
    }
}