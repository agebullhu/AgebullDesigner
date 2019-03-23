namespace Agebull.Common.Organizations
{


    /// <summary>
    /// 行级权限类型
    /// </summary>
    /// <remark>
    /// 行级权限类型
    /// </remark>
    public enum SubjectionType
    {
        /// <summary>
        /// 没有任何权限制
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 仅限本人的数据
        /// </summary>
        Self = 0x1,
        /// <summary>
        /// 本部门的数据
        /// </summary>
        Department = 0x2,
        /// <summary>
        /// 本部门及下级的数据
        /// </summary>
        DepartmentAndLower = 0x3,
        /// <summary>
        /// 本区域的数据
        /// </summary>
        Company = 0x4,
        /// <summary>
        /// 本区域及下级区域与部门的数据
        /// </summary>
        CompanyAndLower = 0x5,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom = 0x6,
    }
}