namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumHelper
    {

        /// <summary>
        ///     权限范围枚举类型名称转换
        /// </summary>
        public static string ToCaption(this DataScopeType value)
        {
            switch (value)
            {
                case DataScopeType.None:
                    return "无";
                case DataScopeType.Person:
                    return "本人";
                case DataScopeType.Home:
                    return "本级";
                case DataScopeType.PersonAndHome:
                    return "本人及本级";
                case DataScopeType.Lower:
                    return "下级";
                case DataScopeType.HomeAndLower:
                    return "本级及以下";
                case DataScopeType.Full:
                    return "本人本级及下级";
                case DataScopeType.Unlimited:
                    return "无限制";
                default:
                    return "权限范围枚举类型(错误)";
            }
        }


        /// <summary>
        ///     应用归类类型名称转换
        /// </summary>
        public static string ToCaption(this AppType value)
        {
            switch (value)
            {
                case AppType.None:
                    return "无权限";
                case AppType.Background:
                    return "管理后台";
                case AppType.InternetApp:
                    return "公众应用";
                default:
                    return "应用归类类型(错误)";
            }
        }


        /// <summary>
        ///     证件类型名称转换
        /// </summary>
        public static string ToCaption(this CardType value)
        {
            switch (value)
            {
                case CardType.None:
                    return "未确定";
                case CardType.Mainland:
                    return "大陆";
                case CardType.Foreigners:
                    return "外国人永久居住证";
                case CardType.Hmt:
                    return "港澳台";
                default:
                    return "证件类型(错误)";
            }
        }

        /// <summary>
        ///     用户类型名称转换
        /// </summary>
        public static string ToCaption(this UserType value)
        {
            switch (value)
            {
                case UserType.Stranger:
                    return "陌生人";
                case UserType.Staff:
                    return "员工";
                case UserType.Visitor:
                    return "访客";
                case UserType.Companion:
                    return "访客同伴";
                case UserType.Vip:
                    return "VIP客户";
                case UserType.Black:
                    return "黑名单";
                default:
                    return "用户类型(错误)";
            }
        }
        /// <summary>
        ///     用户认证类型名称转换
        /// </summary>
        public static string ToCaption(this AuthorizeType value)
        {
            switch (value)
            {
                case AuthorizeType.None:
                    return "无";
                case AuthorizeType.MobilePhone:
                    return "手机认证";
                case AuthorizeType.Account:
                    return "账户认证";
                case AuthorizeType.Wechat:
                    return "微信认证";
                case AuthorizeType.MicroBlog:
                    return "微博认证";
                default:
                    return "用户认证类型(未知)";
            }
        }

        /// <summary>
        ///     商户类型名称转换
        /// </summary>
        public static string ToCaption(this MainBusinessType value)
        {
            switch (value)
            {
                case MainBusinessType.ByStages:
                    return "分期";
                case MainBusinessType.PlatformMerchant:
                    return "平台商户";
                case MainBusinessType.CapitalSide:
                    return "资金方";
                default:
                    return "商户类型(未知)";
            }
        }

        /// <summary>
        ///     职位名称转换
        /// </summary>
        public static string ToCaption(this PositionType value)
        {
            switch (value)
            {
                case PositionType.GeneralManager:
                    return "总经理";
                case PositionType.ProjectLeader:
                    return "项目负责人";
                case PositionType.ProjectSpecialist:
                    return "项目专员";
                default:
                    return "职位(未知)";
            }
        }

        /// <summary>
        ///     性别名称转换
        /// </summary>
        public static string ToCaption(this SexType value)
        {
            switch (value)
            {
                case SexType.Female:
                    return "女";
                case SexType.Male:
                    return "男";
                default:
                    return "性别(未知)";
            }
        }

        /// <summary>
        ///     用户状态名称转换
        /// </summary>
        public static string ToCaption(this UserStatusType value)
        {
            switch (value)
            {
                case UserStatusType.Anymouse:
                    return "访客";
                case UserStatusType.Regist:
                    return "注册用户";
                case UserStatusType.BlackList:
                    return "黑名单";
                default:
                    return "用户状态(未知)";
            }
        }
        /// <summary>
        ///     行级权限类型
        /// </summary>
        public static string ToCaption(this SubjectionType value)
        {
            switch (value)
            {
                case SubjectionType.None:
                    return "没有任何权限制";
                case SubjectionType.Self:
                    return "仅限本人的数据";
                case SubjectionType.Department:
                    return "本部门的数据";
                case SubjectionType.DepartmentAndLower:
                    return "本部门及下级的数据";
                case SubjectionType.Company:
                    return "本区域的数据";
                case SubjectionType.CompanyAndLower:
                    return "本区域及下级区域与部门的数据";
                case SubjectionType.Custom:
                    return "自定义";
                default:
                    return "行级权限类型";
            }
        }

        /// <summary>
        ///     机构类型名称转换
        /// </summary>
        public static string ToCaption(this OrganizationType value)
        {
            switch (value)
            {
                case OrganizationType.None:
                    return "未确定";
                case OrganizationType.Area:
                    return "集团或医联体";
                case OrganizationType.Organization:
                    return "公司或医院";
                case OrganizationType.Department:
                    return "部门或科室";
                default:
                    return "机构类型(未知)";
            }
        }
    }
}