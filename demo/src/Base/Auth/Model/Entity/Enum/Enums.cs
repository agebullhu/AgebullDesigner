using System;

namespace Agebull.Common.Organizations
{


    /// <summary>
    /// 权限范围枚举类型
    /// </summary>
    /// <remark>
    /// 权限范围
    /// </remark>
    public enum DataScopeType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 本人
        /// </summary>
        Person = 0x1,
        /// <summary>
        /// 本级
        /// </summary>
        Home = 0x2,
        /// <summary>
        /// 本人及本级
        /// </summary>
        PersonAndHome = 0x3,
        /// <summary>
        /// 下级
        /// </summary>
        Lower = 0x4,
        /// <summary>
        /// 本级及以下
        /// </summary>
        HomeAndLower = 0x6,
        /// <summary>
        /// 本人本级及下级
        /// </summary>
        Full = 0x7,
        /// <summary>
        /// 无限制
        /// </summary>
        Unlimited = 0x8,
    }


    /// <summary>
    /// 应用归类类型
    /// </summary>
    /// <remark>
    /// 应用归类类型
    /// </remark>
    public enum AppType
    {
        /// <summary>
        /// 无权限
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 管理后台
        /// </summary>
        Background = 0x1,
        /// <summary>
        /// 公众应用
        /// </summary>
        InternetApp = 0x2,
    }


    /// <summary>
    /// 用户类型
    /// </summary>
    /// <remark>
    /// 用户类型
    /// </remark>
    public enum UserType
    {
        /// <summary>
        /// 陌生人
        /// </summary>
        Stranger = 0x0,
        /// <summary>
        /// 员工
        /// </summary>
        Staff = 0x1,
        /// <summary>
        /// 访客
        /// </summary>
        Visitor = 0x2,
        /// <summary>
        /// 访客同伴
        /// </summary>
        Companion = 0x3,
        /// <summary>
        /// VIP客户
        /// </summary>
        Vip = 0x4,
        /// <summary>
        /// 黑名单
        /// </summary>
        Black = 0x5,
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    /// <remark>
    /// 1:大陆；2:外国人永久居住证；3:港澳台
    /// </remark>
    public enum CardType
    {
        /// <summary>
        /// 未确定
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 大陆
        /// </summary>
        Mainland = 0x1,
        /// <summary>
        /// 外国人永久居住证
        /// </summary>
        Foreigners = 0x2,
        /// <summary>
        /// 港澳台
        /// </summary>
        Hmt = 0x3,
    }

    /// <summary>
    /// 用户认证类型
    /// </summary>
    /// <remark>
    /// 0 无,1 手机认证，2 账户认证，4 微信认证，8 微博认证
    /// </remark>
    [Flags]
    public enum AuthorizeType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 手机认证
        /// </summary>
        MobilePhone = 0x1,
        /// <summary>
        /// 账户认证
        /// </summary>
        Account = 0x2,
        /// <summary>
        /// 微信认证
        /// </summary>
        Wechat = 0x4,
        /// <summary>
        /// 微博认证
        /// </summary>
        MicroBlog = 0x8,
        /// <summary>
        /// 身份证
        /// </summary>
        IdCard = 0x10,
        /// <summary>
        /// 活体认证
        /// </summary>
        LivingBody = 0x20,
    }

    /// <summary>
    /// 商户类型
    /// </summary>
    /// <remark>
    /// 1:分期;2:平台商户;3:资金方
    /// </remark>
    public enum MainBusinessType
    {
        /// <summary>
        /// 分期
        /// </summary>
        ByStages = 0x1,
        /// <summary>
        /// 平台商户
        /// </summary>
        PlatformMerchant = 0x2,
        /// <summary>
        /// 资金方
        /// </summary>
        CapitalSide = 0x3,
    }

    /// <summary>
    /// 职位
    /// </summary>
    /// <remark>
    /// 1:总经理，2：项目负责人，3：项目专员
    /// </remark>
    public enum PositionType
    {
        /// <summary>
        /// 总经理
        /// </summary>
        GeneralManager = 0x1,
        /// <summary>
        /// 项目负责人
        /// </summary>
        ProjectLeader = 0x2,
        /// <summary>
        /// 项目专员
        /// </summary>
        ProjectSpecialist = 0x3,
    }

    /// <summary>
    /// 性别
    /// </summary>
    /// <remark>
    /// 性别,0:女;1:男
    /// </remark>
    public enum SexType
    {
        /// <summary>
        /// 未确定
        /// </summary>
        None,
        /// <summary>
        /// 女
        /// </summary>
        Female = 0x1,
        /// <summary>
        /// 男
        /// </summary>
        Male = 0x2,
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    /// <remark>
    /// 用户状态
    /// </remark>
    public enum UserStatusType
    {
        /// <summary>
        /// 访客
        /// </summary>
        Anymouse = 0x0,
        /// <summary>
        /// 注册用户
        /// </summary>
        Regist = 0x1,
        /// <summary>
        /// 黑名单
        /// </summary>
        BlackList = 0x2,
    }

}
