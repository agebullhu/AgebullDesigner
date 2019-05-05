/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.SqlServer;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.EasyUI;



namespace HPC.Projects.DataAccess
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class UserDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_User;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUser";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUser";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"UID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [UID] AS [UID],
    [SiteSID] AS [SiteSID],
    [RefereeUID] AS [RefereeUID],
    [UserID] AS [UserID],
    [wx_appid] AS [wx_appid],
    [wx_openid] AS [wx_openid],
    [wx_unionid] AS [wx_unionid],
    [wx_groupid] AS [wx_groupid],
    [RoleRID] AS [RoleRID],
    [Token] AS [Token],
    [MPSessionKey] AS [MPSessionKey],
    [MPQRCodeImgUrl] AS [MPQRCodeImgUrl],
    [UserPassword] AS [UserPassword],
    [MobilePhone] AS [MobilePhone],
    [MobilePhonePure] AS [MobilePhonePure],
    [MobilePhoneCountryCode] AS [MobilePhoneCountryCode],
    [nickName] AS [nickName],
    [gender] AS [gender],
    [avatarUrl] AS [avatarUrl],
    [language] AS [language],
    [city] AS [city],
    [province] AS [province],
    [country] AS [country],
    [remark] AS [remark],
    [SceneID] AS [SceneID],
    [RegisterFrom] AS [RegisterFrom],
    [RegisterTime] AS [RegisterTime],
    [subscribe] AS [subscribe],
    [subscribe_time] AS [subscribe_time],
    [UserName] AS [UserName],
    [UserFace] AS [UserFace],
    [Age] AS [Age],
    [Birthday] AS [Birthday],
    [HouseInfo] AS [HouseInfo],
    [Email] AS [Email],
    [Hight] AS [Hight],
    [Weight] AS [Weight],
    [QQID] AS [QQID],
    [WXID] AS [WXID],
    [UserState] AS [UserState],
    [LastLoginTime] AS [LastLoginTime],
    [LastLoginIP] AS [LastLoginIP],
    [RecommendCode] AS [RecommendCode],
    [RecommendUseID] AS [RecommendUseID],
    [BonusPoints] AS [BonusPoints],
    [CodeTicket] AS [CodeTicket],
    [MessagePromptType] AS [MessagePromptType],
    [MessagePromptLastTime] AS [MessagePromptLastTime],
    [CustomArea] AS [CustomArea],
    [NativePlaceProvince] AS [NativePlaceProvince],
    [NativePlaceCity] AS [NativePlaceCity],
    [NativePlaceArea] AS [NativePlaceArea],
    [Profession] AS [Profession],
    [TimeVisitOfflineLast] AS [TimeVisitOfflineLast]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [UID],
    [SiteSID],
    [RefereeUID],
    [UserID],
    [wx_appid],
    [wx_openid],
    [wx_unionid],
    [wx_groupid],
    [RoleRID],
    [Token],
    [MPSessionKey],
    [MPQRCodeImgUrl],
    [UserPassword],
    [MobilePhone],
    [MobilePhonePure],
    [MobilePhoneCountryCode],
    [nickName],
    [gender],
    [avatarUrl],
    [language],
    [city],
    [province],
    [country],
    [remark],
    [SceneID],
    [RegisterFrom],
    [RegisterTime],
    [subscribe],
    [subscribe_time],
    [UserName],
    [UserFace],
    [Age],
    [Birthday],
    [HouseInfo],
    [Email],
    [Hight],
    [Weight],
    [QQID],
    [WXID],
    [UserState],
    [LastLoginTime],
    [LastLoginIP],
    [RecommendCode],
    [RecommendUseID],
    [BonusPoints],
    [CodeTicket],
    [MessagePromptType],
    [MessagePromptLastTime],
    [CustomArea],
    [NativePlaceProvince],
    [NativePlaceCity],
    [NativePlaceArea],
    [Profession],
    [TimeVisitOfflineLast]
)
VALUES
(
    @UID,
    @SiteSID,
    @RefereeUID,
    @UserID,
    @wx_appid,
    @wx_openid,
    @wx_unionid,
    @wx_groupid,
    @RoleRID,
    @Token,
    @MPSessionKey,
    @MPQRCodeImgUrl,
    @UserPassword,
    @MobilePhone,
    @MobilePhonePure,
    @MobilePhoneCountryCode,
    @nickName,
    @gender,
    @avatarUrl,
    @language,
    @city,
    @province,
    @country,
    @remark,
    @SceneID,
    @RegisterFrom,
    @RegisterTime,
    @subscribe,
    @subscribe_time,
    @UserName,
    @UserFace,
    @Age,
    @Birthday,
    @HouseInfo,
    @Email,
    @Hight,
    @Weight,
    @QQID,
    @WXID,
    @UserState,
    @LastLoginTime,
    @LastLoginIP,
    @RecommendCode,
    @RecommendUseID,
    @BonusPoints,
    @CodeTicket,
    @MessagePromptType,
    @MessagePromptLastTime,
    @CustomArea,
    @NativePlaceProvince,
    @NativePlaceCity,
    @NativePlaceArea,
    @Profession,
    @TimeVisitOfflineLast
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [UID] = @UID,
       [SiteSID] = @SiteSID,
       [RefereeUID] = @RefereeUID,
       [UserID] = @UserID,
       [wx_appid] = @wx_appid,
       [wx_openid] = @wx_openid,
       [wx_unionid] = @wx_unionid,
       [wx_groupid] = @wx_groupid,
       [RoleRID] = @RoleRID,
       [Token] = @Token,
       [MPSessionKey] = @MPSessionKey,
       [MPQRCodeImgUrl] = @MPQRCodeImgUrl,
       [UserPassword] = @UserPassword,
       [MobilePhone] = @MobilePhone,
       [MobilePhonePure] = @MobilePhonePure,
       [MobilePhoneCountryCode] = @MobilePhoneCountryCode,
       [nickName] = @nickName,
       [gender] = @gender,
       [avatarUrl] = @avatarUrl,
       [language] = @language,
       [city] = @city,
       [province] = @province,
       [country] = @country,
       [remark] = @remark,
       [SceneID] = @SceneID,
       [RegisterFrom] = @RegisterFrom,
       [RegisterTime] = @RegisterTime,
       [subscribe] = @subscribe,
       [subscribe_time] = @subscribe_time,
       [UserName] = @UserName,
       [UserFace] = @UserFace,
       [Age] = @Age,
       [Birthday] = @Birthday,
       [HouseInfo] = @HouseInfo,
       [Email] = @Email,
       [Hight] = @Hight,
       [Weight] = @Weight,
       [QQID] = @QQID,
       [WXID] = @WXID,
       [UserState] = @UserState,
       [LastLoginTime] = @LastLoginTime,
       [LastLoginIP] = @LastLoginIP,
       [RecommendCode] = @RecommendCode,
       [RecommendUseID] = @RecommendUseID,
       [BonusPoints] = @BonusPoints,
       [CodeTicket] = @CodeTicket,
       [MessagePromptType] = @MessagePromptType,
       [MessagePromptLastTime] = @MessagePromptLastTime,
       [CustomArea] = @CustomArea,
       [NativePlaceProvince] = @NativePlaceProvince,
       [NativePlaceCity] = @NativePlaceCity,
       [NativePlaceArea] = @NativePlaceArea,
       [Profession] = @Profession,
       [TimeVisitOfflineLast] = @TimeVisitOfflineLast
 WHERE [UID] = @UID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_UID] > 0)
                sql.AppendLine("       [UID] = @UID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //referee用户标识
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_RefereeUID] > 0)
                sql.AppendLine("       [RefereeUID] = @RefereeUID");
            //用户端
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_UserID] > 0)
                sql.AppendLine("       [UserID] = @UserID");
            //wx应用标识
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_wx_appid] > 0)
                sql.AppendLine("       [wx_appid] = @wx_appid");
            //WXOpenID
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_wx_openid] > 0)
                sql.AppendLine("       [wx_openid] = @wx_openid");
            //WX工会会员
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_wx_unionid] > 0)
                sql.AppendLine("       [wx_unionid] = @wx_unionid");
            //WX群
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_wx_groupid] > 0)
                sql.AppendLine("       [wx_groupid] = @wx_groupid");
            //角色扮演
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_RoleRID] > 0)
                sql.AppendLine("       [RoleRID] = @RoleRID");
            //令牌
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Token] > 0)
                sql.AppendLine("       [Token] = @Token");
            //会话全局标识MP
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_MPSessionKey] > 0)
                sql.AppendLine("       [MPSessionKey] = @MPSessionKey");
            //标签：法典
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_MPQRCodeImgUrl] > 0)
                sql.AppendLine("       [MPQRCodeImgUrl] = @MPQRCodeImgUrl");
            //用户口令
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_UserPassword] > 0)
                sql.AppendLine("       [UserPassword] = @UserPassword");
            //移动电话
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_MobilePhone] > 0)
                sql.AppendLine("       [MobilePhone] = @MobilePhone");
            //纯手机
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_MobilePhonePure] > 0)
                sql.AppendLine("       [MobilePhonePure] = @MobilePhonePure");
            //手机国家代码
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_MobilePhoneCountryCode] > 0)
                sql.AppendLine("       [MobilePhoneCountryCode] = @MobilePhoneCountryCode");
            //昵称
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_nickName] > 0)
                sql.AppendLine("       [nickName] = @nickName");
            //性别
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_gender] > 0)
                sql.AppendLine("       [gender] = @gender");
            //2.Avatar第38142号；
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_avatarUrl] > 0)
                sql.AppendLine("       [avatarUrl] = @avatarUrl");
            //语言
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_language] > 0)
                sql.AppendLine("       [language] = @language");
            //城市
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_city] > 0)
                sql.AppendLine("       [city] = @city");
            //省份
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_province] > 0)
                sql.AppendLine("       [province] = @province");
            //国家
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_country] > 0)
                sql.AppendLine("       [country] = @country");
            //备注
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_remark] > 0)
                sql.AppendLine("       [remark] = @remark");
            //场景叠加
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_SceneID] > 0)
                sql.AppendLine("       [SceneID] = @SceneID");
            //从注册
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_RegisterFrom] > 0)
                sql.AppendLine("       [RegisterFrom] = @RegisterFrom");
            //注册时间
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_RegisterTime] > 0)
                sql.AppendLine("       [RegisterTime] = @RegisterTime");
            //订阅
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_subscribe] > 0)
                sql.AppendLine("       [subscribe] = @subscribe");
            //订阅时间
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_subscribe_time] > 0)
                sql.AppendLine("       [subscribe_time] = @subscribe_time");
            //用户名
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_UserName] > 0)
                sql.AppendLine("       [UserName] = @UserName");
            //用户面
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_UserFace] > 0)
                sql.AppendLine("       [UserFace] = @UserFace");
            //年龄
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Age] > 0)
                sql.AppendLine("       [Age] = @Age");
            //生日
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Birthday] > 0)
                sql.AppendLine("       [Birthday] = @Birthday");
            //住宅信息
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_HouseInfo] > 0)
                sql.AppendLine("       [HouseInfo] = @HouseInfo");
            //电子邮件
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Email] > 0)
                sql.AppendLine("       [Email] = @Email");
            //海特
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Hight] > 0)
                sql.AppendLine("       [Hight] = @Hight");
            //重量
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Weight] > 0)
                sql.AppendLine("       [Weight] = @Weight");
            //QQID
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_QQID] > 0)
                sql.AppendLine("       [QQID] = @QQID");
            //WXID
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_WXID] > 0)
                sql.AppendLine("       [WXID] = @WXID");
            //用户状态
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_UserState] > 0)
                sql.AppendLine("       [UserState] = @UserState");
            //上次登录时间
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_LastLoginTime] > 0)
                sql.AppendLine("       [LastLoginTime] = @LastLoginTime");
            //最后登录IP
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_LastLoginIP] > 0)
                sql.AppendLine("       [LastLoginIP] = @LastLoginIP");
            //推荐代码
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_RecommendCode] > 0)
                sql.AppendLine("       [RecommendCode] = @RecommendCode");
            //推荐使用标识
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_RecommendUseID] > 0)
                sql.AppendLine("       [RecommendUseID] = @RecommendUseID");
            //加分
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_BonusPoints] > 0)
                sql.AppendLine("       [BonusPoints] = @BonusPoints");
            //代码票
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_CodeTicket] > 0)
                sql.AppendLine("       [CodeTicket] = @CodeTicket");
            //快速语音信号
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_MessagePromptType] > 0)
                sql.AppendLine("       [MessagePromptType] = @MessagePromptType");
            //上次消息提示
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_MessagePromptLastTime] > 0)
                sql.AppendLine("       [MessagePromptLastTime] = @MessagePromptLastTime");
            //定制区
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_CustomArea] > 0)
                sql.AppendLine("       [CustomArea] = @CustomArea");
            //本地省
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_NativePlaceProvince] > 0)
                sql.AppendLine("       [NativePlaceProvince] = @NativePlaceProvince");
            //本地城市
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_NativePlaceCity] > 0)
                sql.AppendLine("       [NativePlaceCity] = @NativePlaceCity");
            //本地区
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_NativePlaceArea] > 0)
                sql.AppendLine("       [NativePlaceArea] = @NativePlaceArea");
            //职业
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Profession] > 0)
                sql.AppendLine("       [Profession] = @Profession");
            //上次离线访问时间
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_TimeVisitOfflineLast] > 0)
                sql.AppendLine("       [TimeVisitOfflineLast] = @TimeVisitOfflineLast");
            sql.Append(" WHERE [UID] = @UID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "UID","SiteSID","RefereeUID","UserID","wx_appid","wx_openid","wx_unionid","wx_groupid","RoleRID","Token","MPSessionKey","MPQRCodeImgUrl","UserPassword","MobilePhone","MobilePhonePure","MobilePhoneCountryCode","nickName","gender","avatarUrl","language","city","province","country","remark","SceneID","RegisterFrom","RegisterTime","subscribe","subscribe_time","UserName","UserFace","Age","Birthday","HouseInfo","Email","Hight","Weight","QQID","WXID","UserState","LastLoginTime","LastLoginIP","RecommendCode","RecommendUseID","BonusPoints","CodeTicket","MessagePromptType","MessagePromptLastTime","CustomArea","NativePlaceProvince","NativePlaceCity","NativePlaceArea","Profession","TimeVisitOfflineLast" };

        /// <summary>
        ///  所有字段
        /// </summary>
        public sealed override string[] Fields
        {
            get
            {
                return _fields;
            }
        }

        /// <summary>
        ///  字段字典
        /// </summary>
        public static Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "UID" , "UID" },
            { "SiteSID" , "SiteSID" },
            { "RefereeUID" , "RefereeUID" },
            { "UserID" , "UserID" },
            { "wx_appid" , "wx_appid" },
            { "wx_openid" , "wx_openid" },
            { "wx_unionid" , "wx_unionid" },
            { "wx_groupid" , "wx_groupid" },
            { "RoleRID" , "RoleRID" },
            { "Token" , "Token" },
            { "MPSessionKey" , "MPSessionKey" },
            { "MPQRCodeImgUrl" , "MPQRCodeImgUrl" },
            { "UserPassword" , "UserPassword" },
            { "MobilePhone" , "MobilePhone" },
            { "MobilePhonePure" , "MobilePhonePure" },
            { "MobilePhoneCountryCode" , "MobilePhoneCountryCode" },
            { "nickName" , "nickName" },
            { "gender" , "gender" },
            { "avatarUrl" , "avatarUrl" },
            { "language" , "language" },
            { "city" , "city" },
            { "province" , "province" },
            { "country" , "country" },
            { "remark" , "remark" },
            { "SceneID" , "SceneID" },
            { "RegisterFrom" , "RegisterFrom" },
            { "RegisterTime" , "RegisterTime" },
            { "subscribe" , "subscribe" },
            { "subscribe_time" , "subscribe_time" },
            { "UserName" , "UserName" },
            { "UserFace" , "UserFace" },
            { "Age" , "Age" },
            { "Birthday" , "Birthday" },
            { "HouseInfo" , "HouseInfo" },
            { "Email" , "Email" },
            { "Hight" , "Hight" },
            { "Weight" , "Weight" },
            { "QQID" , "QQID" },
            { "WXID" , "WXID" },
            { "UserState" , "UserState" },
            { "LastLoginTime" , "LastLoginTime" },
            { "LastLoginIP" , "LastLoginIP" },
            { "RecommendCode" , "RecommendCode" },
            { "RecommendUseID" , "RecommendUseID" },
            { "BonusPoints" , "BonusPoints" },
            { "CodeTicket" , "CodeTicket" },
            { "MessagePromptType" , "MessagePromptType" },
            { "MessagePromptLastTime" , "MessagePromptLastTime" },
            { "CustomArea" , "CustomArea" },
            { "NativePlaceProvince" , "NativePlaceProvince" },
            { "NativePlaceCity" , "NativePlaceCity" },
            { "NativePlaceArea" , "NativePlaceArea" },
            { "Profession" , "Profession" },
            { "TimeVisitOfflineLast" , "TimeVisitOfflineLast" },
            { "Id" , "UID" }
        };

        /// <summary>
        ///  字段字典
        /// </summary>
        public sealed override Dictionary<string, string> FieldMap
        {
            get { return fieldMap ; }
        }
        #endregion

        #region 方法实现


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        protected sealed override void LoadEntity(SqlDataReader reader,UserData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._uID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._refereeUID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._userID = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._wx_appid = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._wx_openid = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._wx_unionid = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._wx_groupid = (float)reader.GetDouble(7);
                if (!reader.IsDBNull(8))
                    entity._roleRID = (long)reader.GetInt64(8);
                if (!reader.IsDBNull(9))
                    entity._token = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._mPSessionKey = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._mPQRCodeImgUrl = reader.GetString(11);
                if (!reader.IsDBNull(12))
                    entity._userPassword = reader.GetString(12);
                if (!reader.IsDBNull(13))
                    entity._mobilePhone = reader.GetString(13).ToString();
                if (!reader.IsDBNull(14))
                    entity._mobilePhonePure = reader.GetString(14).ToString();
                if (!reader.IsDBNull(15))
                    entity._mobilePhoneCountryCode = reader.GetString(15).ToString();
                if (!reader.IsDBNull(16))
                    entity._nickName = reader.GetString(16);
                if (!reader.IsDBNull(17))
                    entity._gender = (bool)reader.GetBoolean(17);
                if (!reader.IsDBNull(18))
                    entity._avatarUrl = reader.GetString(18);
                if (!reader.IsDBNull(19))
                    entity._language = reader.GetString(19).ToString();
                if (!reader.IsDBNull(20))
                    entity._city = reader.GetString(20);
                if (!reader.IsDBNull(21))
                    entity._province = reader.GetString(21);
                if (!reader.IsDBNull(22))
                    entity._country = reader.GetString(22);
                if (!reader.IsDBNull(23))
                    entity._remark = reader.GetString(23);
                if (!reader.IsDBNull(24))
                    entity._sceneID = reader.GetInt32(24);
                if (!reader.IsDBNull(25))
                    entity._registerFrom = reader.GetString(25);
                if (!reader.IsDBNull(26))
                    entity._registerTime = reader.GetDateTime(26);
                if (!reader.IsDBNull(27))
                    entity._subscribe = (bool)reader.GetBoolean(27);
                if (!reader.IsDBNull(28))
                    entity._subscribe_time = reader.GetString(28).ToString();
                if (!reader.IsDBNull(29))
                    entity._userName = reader.GetString(29);
                if (!reader.IsDBNull(30))
                    entity._userFace = reader.GetString(30).ToString();
                if (!reader.IsDBNull(31))
                    entity._age = reader.GetInt32(31);
                if (!reader.IsDBNull(32))
                    entity._birthday = reader.GetDateTime(32);
                if (!reader.IsDBNull(33))
                    entity._houseInfo = reader.GetString(33);
                if (!reader.IsDBNull(34))
                    entity._email = reader.GetString(34);
                if (!reader.IsDBNull(35))
                    entity._hight = reader.GetInt32(35);
                if (!reader.IsDBNull(36))
                    entity._weight = (float)reader.GetDouble(36);
                if (!reader.IsDBNull(37))
                    entity._qQID = reader.GetString(37);
                if (!reader.IsDBNull(38))
                    entity._wXID = reader.GetString(38);
                if (!reader.IsDBNull(39))
                    entity._userState = (bool)reader.GetBoolean(39);
                if (!reader.IsDBNull(40))
                    entity._lastLoginTime = reader.GetDateTime(40);
                if (!reader.IsDBNull(41))
                    entity._lastLoginIP = reader.GetString(41).ToString();
                if (!reader.IsDBNull(42))
                    entity._recommendCode = reader.GetString(42).ToString();
                if (!reader.IsDBNull(43))
                    entity._recommendUseID = (long)reader.GetInt64(43);
                if (!reader.IsDBNull(44))
                    entity._bonusPoints = reader.GetInt32(44);
                if (!reader.IsDBNull(45))
                    entity._codeTicket = reader.GetString(45).ToString();
                if (!reader.IsDBNull(46))
                    entity._messagePromptType = reader.GetInt32(46);
                if (!reader.IsDBNull(47))
                    entity._messagePromptLastTime = reader.GetDateTime(47);
                if (!reader.IsDBNull(48))
                    entity._customArea = reader.GetString(48);
                if (!reader.IsDBNull(49))
                    entity._nativePlaceProvince = reader.GetString(49);
                if (!reader.IsDBNull(50))
                    entity._nativePlaceCity = reader.GetString(50);
                if (!reader.IsDBNull(51))
                    entity._nativePlaceArea = reader.GetString(51);
                if (!reader.IsDBNull(52))
                    entity._profession = reader.GetString(52);
                if (!reader.IsDBNull(53))
                    entity._timeVisitOfflineLast = reader.GetDateTime(53);
            }
        }

        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override SqlDbType GetDbType(string field)
        {
            switch (field)
            {
                case "UID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "RefereeUID":
                    return SqlDbType.BigInt;
                case "UserID":
                    return SqlDbType.NVarChar;
                case "wx_appid":
                    return SqlDbType.NVarChar;
                case "wx_openid":
                    return SqlDbType.NVarChar;
                case "wx_unionid":
                    return SqlDbType.NVarChar;
                case "wx_groupid":
                    return SqlDbType.Decimal;
                case "RoleRID":
                    return SqlDbType.BigInt;
                case "Token":
                    return SqlDbType.NVarChar;
                case "MPSessionKey":
                    return SqlDbType.NVarChar;
                case "MPQRCodeImgUrl":
                    return SqlDbType.NVarChar;
                case "UserPassword":
                    return SqlDbType.NVarChar;
                case "MobilePhone":
                    return SqlDbType.NVarChar;
                case "MobilePhonePure":
                    return SqlDbType.NVarChar;
                case "MobilePhoneCountryCode":
                    return SqlDbType.NVarChar;
                case "nickName":
                    return SqlDbType.NVarChar;
                case "gender":
                    return SqlDbType.Bit;
                case "avatarUrl":
                    return SqlDbType.NVarChar;
                case "language":
                    return SqlDbType.NVarChar;
                case "city":
                    return SqlDbType.NVarChar;
                case "province":
                    return SqlDbType.NVarChar;
                case "country":
                    return SqlDbType.NVarChar;
                case "remark":
                    return SqlDbType.NVarChar;
                case "SceneID":
                    return SqlDbType.Int;
                case "RegisterFrom":
                    return SqlDbType.NVarChar;
                case "RegisterTime":
                    return SqlDbType.DateTime;
                case "subscribe":
                    return SqlDbType.Bit;
                case "subscribe_time":
                    return SqlDbType.NVarChar;
                case "UserName":
                    return SqlDbType.NVarChar;
                case "UserFace":
                    return SqlDbType.NVarChar;
                case "Age":
                    return SqlDbType.Int;
                case "Birthday":
                    return SqlDbType.DateTime;
                case "HouseInfo":
                    return SqlDbType.NVarChar;
                case "Email":
                    return SqlDbType.NVarChar;
                case "Hight":
                    return SqlDbType.Int;
                case "Weight":
                    return SqlDbType.Decimal;
                case "QQID":
                    return SqlDbType.NVarChar;
                case "WXID":
                    return SqlDbType.NVarChar;
                case "UserState":
                    return SqlDbType.Bit;
                case "LastLoginTime":
                    return SqlDbType.DateTime;
                case "LastLoginIP":
                    return SqlDbType.NVarChar;
                case "RecommendCode":
                    return SqlDbType.NVarChar;
                case "RecommendUseID":
                    return SqlDbType.BigInt;
                case "BonusPoints":
                    return SqlDbType.Int;
                case "CodeTicket":
                    return SqlDbType.NVarChar;
                case "MessagePromptType":
                    return SqlDbType.Int;
                case "MessagePromptLastTime":
                    return SqlDbType.DateTime;
                case "CustomArea":
                    return SqlDbType.NVarChar;
                case "NativePlaceProvince":
                    return SqlDbType.NVarChar;
                case "NativePlaceCity":
                    return SqlDbType.NVarChar;
                case "NativePlaceArea":
                    return SqlDbType.NVarChar;
                case "Profession":
                    return SqlDbType.NVarChar;
                case "TimeVisitOfflineLast":
                    return SqlDbType.DateTime;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(UserData entity, SqlCommand cmd)
        {
            //02:主键(UID)
            cmd.Parameters.Add(new SqlParameter("UID",SqlDbType.BigInt){ Value = entity.UID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:referee用户标识(RefereeUID)
            cmd.Parameters.Add(new SqlParameter("RefereeUID",SqlDbType.BigInt){ Value = entity.RefereeUID});
            //05:用户端(UserID)
            var isNull = string.IsNullOrWhiteSpace(entity.UserID);
            var parameter = new SqlParameter("UserID",SqlDbType.NVarChar , isNull ? 10 : (entity.UserID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UserID;
            cmd.Parameters.Add(parameter);
            //06:wx应用标识(wx_appid)
            isNull = string.IsNullOrWhiteSpace(entity.wx_appid);
            parameter = new SqlParameter("wx_appid",SqlDbType.NVarChar , isNull ? 10 : (entity.wx_appid).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.wx_appid;
            cmd.Parameters.Add(parameter);
            //07:WXOpenID(wx_openid)
            isNull = string.IsNullOrWhiteSpace(entity.wx_openid);
            parameter = new SqlParameter("wx_openid",SqlDbType.NVarChar , isNull ? 10 : (entity.wx_openid).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.wx_openid;
            cmd.Parameters.Add(parameter);
            //08:WX工会会员(wx_unionid)
            isNull = string.IsNullOrWhiteSpace(entity.wx_unionid);
            parameter = new SqlParameter("wx_unionid",SqlDbType.NVarChar , isNull ? 10 : (entity.wx_unionid).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.wx_unionid;
            cmd.Parameters.Add(parameter);
            //09:WX群(wx_groupid)
            cmd.Parameters.Add(new SqlParameter("wx_groupid",SqlDbType.Decimal){ Value = entity.wx_groupid});
            //10:角色扮演(RoleRID)
            cmd.Parameters.Add(new SqlParameter("RoleRID",SqlDbType.BigInt){ Value = entity.RoleRID});
            //11:令牌(Token)
            isNull = string.IsNullOrWhiteSpace(entity.Token);
            parameter = new SqlParameter("Token",SqlDbType.NVarChar , isNull ? 10 : (entity.Token).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Token;
            cmd.Parameters.Add(parameter);
            //12:会话全局标识MP(MPSessionKey)
            isNull = string.IsNullOrWhiteSpace(entity.MPSessionKey);
            parameter = new SqlParameter("MPSessionKey",SqlDbType.NVarChar , isNull ? 10 : (entity.MPSessionKey).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MPSessionKey;
            cmd.Parameters.Add(parameter);
            //13:标签：法典(MPQRCodeImgUrl)
            isNull = string.IsNullOrWhiteSpace(entity.MPQRCodeImgUrl);
            parameter = new SqlParameter("MPQRCodeImgUrl",SqlDbType.NVarChar , isNull ? 10 : (entity.MPQRCodeImgUrl).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MPQRCodeImgUrl;
            cmd.Parameters.Add(parameter);
            //14:用户口令(UserPassword)
            isNull = string.IsNullOrWhiteSpace(entity.UserPassword);
            parameter = new SqlParameter("UserPassword",SqlDbType.NVarChar , isNull ? 10 : (entity.UserPassword).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UserPassword;
            cmd.Parameters.Add(parameter);
            //15:移动电话(MobilePhone)
            isNull = string.IsNullOrWhiteSpace(entity.MobilePhone);
            parameter = new SqlParameter("MobilePhone",SqlDbType.NVarChar , isNull ? 10 : (entity.MobilePhone).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MobilePhone;
            cmd.Parameters.Add(parameter);
            //16:纯手机(MobilePhonePure)
            isNull = string.IsNullOrWhiteSpace(entity.MobilePhonePure);
            parameter = new SqlParameter("MobilePhonePure",SqlDbType.NVarChar , isNull ? 10 : (entity.MobilePhonePure).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MobilePhonePure;
            cmd.Parameters.Add(parameter);
            //17:手机国家代码(MobilePhoneCountryCode)
            isNull = string.IsNullOrWhiteSpace(entity.MobilePhoneCountryCode);
            parameter = new SqlParameter("MobilePhoneCountryCode",SqlDbType.NVarChar , isNull ? 10 : (entity.MobilePhoneCountryCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MobilePhoneCountryCode;
            cmd.Parameters.Add(parameter);
            //18:昵称(nickName)
            isNull = string.IsNullOrWhiteSpace(entity.nickName);
            parameter = new SqlParameter("nickName",SqlDbType.NVarChar , isNull ? 10 : (entity.nickName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.nickName;
            cmd.Parameters.Add(parameter);
            //19:性别(gender)
            cmd.Parameters.Add(new SqlParameter("gender",SqlDbType.Bit){ Value = entity.gender});
            //20:2.Avatar第38142号；(avatarUrl)
            isNull = string.IsNullOrWhiteSpace(entity.avatarUrl);
            parameter = new SqlParameter("avatarUrl",SqlDbType.NVarChar , isNull ? 10 : (entity.avatarUrl).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.avatarUrl;
            cmd.Parameters.Add(parameter);
            //21:语言(language)
            isNull = string.IsNullOrWhiteSpace(entity.language);
            parameter = new SqlParameter("language",SqlDbType.NVarChar , isNull ? 10 : (entity.language).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.language;
            cmd.Parameters.Add(parameter);
            //22:城市(city)
            isNull = string.IsNullOrWhiteSpace(entity.city);
            parameter = new SqlParameter("city",SqlDbType.NVarChar , isNull ? 10 : (entity.city).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.city;
            cmd.Parameters.Add(parameter);
            //23:省份(province)
            isNull = string.IsNullOrWhiteSpace(entity.province);
            parameter = new SqlParameter("province",SqlDbType.NVarChar , isNull ? 10 : (entity.province).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.province;
            cmd.Parameters.Add(parameter);
            //24:国家(country)
            isNull = string.IsNullOrWhiteSpace(entity.country);
            parameter = new SqlParameter("country",SqlDbType.NVarChar , isNull ? 10 : (entity.country).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.country;
            cmd.Parameters.Add(parameter);
            //25:备注(remark)
            isNull = string.IsNullOrWhiteSpace(entity.remark);
            parameter = new SqlParameter("remark",SqlDbType.NVarChar , isNull ? 10 : (entity.remark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.remark;
            cmd.Parameters.Add(parameter);
            //26:场景叠加(SceneID)
            cmd.Parameters.Add(new SqlParameter("SceneID",SqlDbType.Int){ Value = entity.SceneID});
            //27:从注册(RegisterFrom)
            isNull = string.IsNullOrWhiteSpace(entity.RegisterFrom);
            parameter = new SqlParameter("RegisterFrom",SqlDbType.NVarChar , isNull ? 10 : (entity.RegisterFrom).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RegisterFrom;
            cmd.Parameters.Add(parameter);
            //28:注册时间(RegisterTime)
            isNull = entity.RegisterTime.Year < 1900;
            parameter = new SqlParameter("RegisterTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RegisterTime;
            cmd.Parameters.Add(parameter);
            //29:订阅(subscribe)
            cmd.Parameters.Add(new SqlParameter("subscribe",SqlDbType.Bit){ Value = entity.subscribe});
            //30:订阅时间(subscribe_time)
            isNull = string.IsNullOrWhiteSpace(entity.subscribe_time);
            parameter = new SqlParameter("subscribe_time",SqlDbType.NVarChar , isNull ? 10 : (entity.subscribe_time).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.subscribe_time;
            cmd.Parameters.Add(parameter);
            //31:用户名(UserName)
            isNull = string.IsNullOrWhiteSpace(entity.UserName);
            parameter = new SqlParameter("UserName",SqlDbType.NVarChar , isNull ? 10 : (entity.UserName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UserName;
            cmd.Parameters.Add(parameter);
            //32:用户面(UserFace)
            isNull = string.IsNullOrWhiteSpace(entity.UserFace);
            parameter = new SqlParameter("UserFace",SqlDbType.NVarChar , isNull ? 10 : (entity.UserFace).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UserFace;
            cmd.Parameters.Add(parameter);
            //33:年龄(Age)
            cmd.Parameters.Add(new SqlParameter("Age",SqlDbType.Int){ Value = entity.Age});
            //34:生日(Birthday)
            isNull = entity.Birthday.Year < 1900;
            parameter = new SqlParameter("Birthday",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Birthday;
            cmd.Parameters.Add(parameter);
            //35:住宅信息(HouseInfo)
            isNull = string.IsNullOrWhiteSpace(entity.HouseInfo);
            parameter = new SqlParameter("HouseInfo",SqlDbType.NVarChar , isNull ? 10 : (entity.HouseInfo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.HouseInfo;
            cmd.Parameters.Add(parameter);
            //36:电子邮件(Email)
            isNull = string.IsNullOrWhiteSpace(entity.Email);
            parameter = new SqlParameter("Email",SqlDbType.NVarChar , isNull ? 10 : (entity.Email).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Email;
            cmd.Parameters.Add(parameter);
            //37:海特(Hight)
            cmd.Parameters.Add(new SqlParameter("Hight",SqlDbType.Int){ Value = entity.Hight});
            //38:重量(Weight)
            cmd.Parameters.Add(new SqlParameter("Weight",SqlDbType.Decimal){ Value = entity.Weight});
            //39:QQID(QQID)
            isNull = string.IsNullOrWhiteSpace(entity.QQID);
            parameter = new SqlParameter("QQID",SqlDbType.NVarChar , isNull ? 10 : (entity.QQID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.QQID;
            cmd.Parameters.Add(parameter);
            //40:WXID(WXID)
            isNull = string.IsNullOrWhiteSpace(entity.WXID);
            parameter = new SqlParameter("WXID",SqlDbType.NVarChar , isNull ? 10 : (entity.WXID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.WXID;
            cmd.Parameters.Add(parameter);
            //41:用户状态(UserState)
            cmd.Parameters.Add(new SqlParameter("UserState",SqlDbType.Bit){ Value = entity.UserState});
            //42:上次登录时间(LastLoginTime)
            isNull = entity.LastLoginTime.Year < 1900;
            parameter = new SqlParameter("LastLoginTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastLoginTime;
            cmd.Parameters.Add(parameter);
            //43:最后登录IP(LastLoginIP)
            isNull = string.IsNullOrWhiteSpace(entity.LastLoginIP);
            parameter = new SqlParameter("LastLoginIP",SqlDbType.NVarChar , isNull ? 10 : (entity.LastLoginIP).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastLoginIP;
            cmd.Parameters.Add(parameter);
            //44:推荐代码(RecommendCode)
            isNull = string.IsNullOrWhiteSpace(entity.RecommendCode);
            parameter = new SqlParameter("RecommendCode",SqlDbType.NVarChar , isNull ? 10 : (entity.RecommendCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RecommendCode;
            cmd.Parameters.Add(parameter);
            //45:推荐使用标识(RecommendUseID)
            cmd.Parameters.Add(new SqlParameter("RecommendUseID",SqlDbType.BigInt){ Value = entity.RecommendUseID});
            //46:加分(BonusPoints)
            cmd.Parameters.Add(new SqlParameter("BonusPoints",SqlDbType.Int){ Value = entity.BonusPoints});
            //47:代码票(CodeTicket)
            isNull = string.IsNullOrWhiteSpace(entity.CodeTicket);
            parameter = new SqlParameter("CodeTicket",SqlDbType.NVarChar , isNull ? 10 : (entity.CodeTicket).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.CodeTicket;
            cmd.Parameters.Add(parameter);
            //48:快速语音信号(MessagePromptType)
            cmd.Parameters.Add(new SqlParameter("MessagePromptType",SqlDbType.Int){ Value = entity.MessagePromptType});
            //49:上次消息提示(MessagePromptLastTime)
            isNull = entity.MessagePromptLastTime.Year < 1900;
            parameter = new SqlParameter("MessagePromptLastTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MessagePromptLastTime;
            cmd.Parameters.Add(parameter);
            //50:定制区(CustomArea)
            isNull = string.IsNullOrWhiteSpace(entity.CustomArea);
            parameter = new SqlParameter("CustomArea",SqlDbType.NVarChar , isNull ? 10 : (entity.CustomArea).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.CustomArea;
            cmd.Parameters.Add(parameter);
            //51:本地省(NativePlaceProvince)
            isNull = string.IsNullOrWhiteSpace(entity.NativePlaceProvince);
            parameter = new SqlParameter("NativePlaceProvince",SqlDbType.NVarChar , isNull ? 10 : (entity.NativePlaceProvince).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NativePlaceProvince;
            cmd.Parameters.Add(parameter);
            //52:本地城市(NativePlaceCity)
            isNull = string.IsNullOrWhiteSpace(entity.NativePlaceCity);
            parameter = new SqlParameter("NativePlaceCity",SqlDbType.NVarChar , isNull ? 10 : (entity.NativePlaceCity).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NativePlaceCity;
            cmd.Parameters.Add(parameter);
            //53:本地区(NativePlaceArea)
            isNull = string.IsNullOrWhiteSpace(entity.NativePlaceArea);
            parameter = new SqlParameter("NativePlaceArea",SqlDbType.NVarChar , isNull ? 10 : (entity.NativePlaceArea).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NativePlaceArea;
            cmd.Parameters.Add(parameter);
            //54:职业(Profession)
            isNull = string.IsNullOrWhiteSpace(entity.Profession);
            parameter = new SqlParameter("Profession",SqlDbType.NVarChar , isNull ? 10 : (entity.Profession).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Profession;
            cmd.Parameters.Add(parameter);
            //55:上次离线访问时间(TimeVisitOfflineLast)
            isNull = entity.TimeVisitOfflineLast.Year < 1900;
            parameter = new SqlParameter("TimeVisitOfflineLast",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TimeVisitOfflineLast;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserData entity, SqlCommand cmd)
        {
            cmd.CommandText = UpdateSqlCode;
            CreateFullSqlParameter(entity,cmd);
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand(UserData entity, SqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return false;
        }
        #endregion

    }

    sealed partial class HpcSqlServerDb
    {


        /// <summary>
        /// 用户的结构语句
        /// </summary>
        private TableSql _tbUserSql = new TableSql
        {
            TableName = "tbUser",
            PimaryKey = "UID"
        };


        /// <summary>
        /// 用户数据访问对象
        /// </summary>
        private UserDataAccess _users;

        /// <summary>
        /// 用户数据访问对象
        /// </summary>
        public UserDataAccess Users
        {
            get
            {
                return this._users ?? ( this._users = new UserDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户(tbUser):用户
        /// </summary>
        public const int Table_User = 0x0;
    }
}