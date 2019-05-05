/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:30*/
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
    /// 组织机构
    /// </summary>
    public partial class OrganizationDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_Organization;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbOrganization";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbOrganization";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"OID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [OID] AS [OID],
    [PID] AS [PID],
    [SiteSID] AS [SiteSID],
    [OrgKey] AS [OrgKey],
    [OrgName] AS [OrgName],
    [OrgChief] AS [OrgChief],
    [OrgRemark] AS [OrgRemark],
    [OrgLogo] AS [OrgLogo],
    [OrgIntroduce] AS [OrgIntroduce],
    [Sort] AS [Sort],
    [Icon] AS [Icon],
    [StateExternal] AS [StateExternal],
    [StateTrusteeship] AS [StateTrusteeship],
    [BusinessLicenseCode] AS [BusinessLicenseCode],
    [BusinessLicenseImage] AS [BusinessLicenseImage],
    [LegalPersonName] AS [LegalPersonName],
    [LegalPersonCardID] AS [LegalPersonCardID],
    [LegalPersonCardImage01] AS [LegalPersonCardImage01],
    [LegalPersonCardImage02] AS [LegalPersonCardImage02],
    [Address] AS [Address],
    [Location_latitude] AS [Location_latitude],
    [Location_longitude] AS [Location_longitude]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [OID],
    [PID],
    [SiteSID],
    [OrgKey],
    [OrgName],
    [OrgChief],
    [OrgRemark],
    [OrgLogo],
    [OrgIntroduce],
    [Sort],
    [Icon],
    [StateExternal],
    [StateTrusteeship],
    [BusinessLicenseCode],
    [BusinessLicenseImage],
    [LegalPersonName],
    [LegalPersonCardID],
    [LegalPersonCardImage01],
    [LegalPersonCardImage02],
    [Address],
    [Location_latitude],
    [Location_longitude]
)
VALUES
(
    @OID,
    @PID,
    @SiteSID,
    @OrgKey,
    @OrgName,
    @OrgChief,
    @OrgRemark,
    @OrgLogo,
    @OrgIntroduce,
    @Sort,
    @Icon,
    @StateExternal,
    @StateTrusteeship,
    @BusinessLicenseCode,
    @BusinessLicenseImage,
    @LegalPersonName,
    @LegalPersonCardID,
    @LegalPersonCardImage01,
    @LegalPersonCardImage02,
    @Address,
    @Location_latitude,
    @Location_longitude
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [OID] = @OID,
       [PID] = @PID,
       [SiteSID] = @SiteSID,
       [OrgKey] = @OrgKey,
       [OrgName] = @OrgName,
       [OrgChief] = @OrgChief,
       [OrgRemark] = @OrgRemark,
       [OrgLogo] = @OrgLogo,
       [OrgIntroduce] = @OrgIntroduce,
       [Sort] = @Sort,
       [Icon] = @Icon,
       [StateExternal] = @StateExternal,
       [StateTrusteeship] = @StateTrusteeship,
       [BusinessLicenseCode] = @BusinessLicenseCode,
       [BusinessLicenseImage] = @BusinessLicenseImage,
       [LegalPersonName] = @LegalPersonName,
       [LegalPersonCardID] = @LegalPersonCardID,
       [LegalPersonCardImage01] = @LegalPersonCardImage01,
       [LegalPersonCardImage02] = @LegalPersonCardImage02,
       [Address] = @Address,
       [Location_latitude] = @Location_latitude,
       [Location_longitude] = @Location_longitude
 WHERE [OID] = @OID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizationData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //组织编号
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OID] > 0)
                sql.AppendLine("       [OID] = @OID");
            //上级组织编号
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_PID] > 0)
                sql.AppendLine("       [PID] = @PID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //全局标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgKey] > 0)
                sql.AppendLine("       [OrgKey] = @OrgKey");
            //组织名称
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgName] > 0)
                sql.AppendLine("       [OrgName] = @OrgName");
            //首席执行官
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgChief] > 0)
                sql.AppendLine("       [OrgChief] = @OrgChief");
            //组织备注
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgRemark] > 0)
                sql.AppendLine("       [OrgRemark] = @OrgRemark");
            //Logo
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgLogo] > 0)
                sql.AppendLine("       [OrgLogo] = @OrgLogo");
            //组织简介
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgIntroduce] > 0)
                sql.AppendLine("       [OrgIntroduce] = @OrgIntroduce");
            //序号
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Sort] > 0)
                sql.AppendLine("       [Sort] = @Sort");
            //小图标
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Icon] > 0)
                sql.AppendLine("       [Icon] = @Icon");
            //外部状态
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_StateExternal] > 0)
                sql.AppendLine("       [StateExternal] = @StateExternal");
            //托管状态
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_StateTrusteeship] > 0)
                sql.AppendLine("       [StateTrusteeship] = @StateTrusteeship");
            //营业执照代码
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_BusinessLicenseCode] > 0)
                sql.AppendLine("       [BusinessLicenseCode] = @BusinessLicenseCode");
            //营业执照图片
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_BusinessLicenseImage] > 0)
                sql.AppendLine("       [BusinessLicenseImage] = @BusinessLicenseImage");
            //法人姓名
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LegalPersonName] > 0)
                sql.AppendLine("       [LegalPersonName] = @LegalPersonName");
            //法人身份证号
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LegalPersonCardID] > 0)
                sql.AppendLine("       [LegalPersonCardID] = @LegalPersonCardID");
            //法人证件图像01
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LegalPersonCardImage01] > 0)
                sql.AppendLine("       [LegalPersonCardImage01] = @LegalPersonCardImage01");
            //法人证件图像02
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LegalPersonCardImage02] > 0)
                sql.AppendLine("       [LegalPersonCardImage02] = @LegalPersonCardImage02");
            //地理地址
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Address] > 0)
                sql.AppendLine("       [Address] = @Address");
            //地理纬度
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Location_latitude] > 0)
                sql.AppendLine("       [Location_latitude] = @Location_latitude");
            //地理经度
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Location_longitude] > 0)
                sql.AppendLine("       [Location_longitude] = @Location_longitude");
            sql.Append(" WHERE [OID] = @OID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "OID","PID","SiteSID","OrgKey","OrgName","OrgChief","OrgRemark","OrgLogo","OrgIntroduce","Sort","Icon","StateExternal","StateTrusteeship","BusinessLicenseCode","BusinessLicenseImage","LegalPersonName","LegalPersonCardID","LegalPersonCardImage01","LegalPersonCardImage02","Address","Location_latitude","Location_longitude" };

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
            { "OID" , "OID" },
            { "PID" , "PID" },
            { "SiteSID" , "SiteSID" },
            { "OrgKey" , "OrgKey" },
            { "OrgName" , "OrgName" },
            { "OrgChief" , "OrgChief" },
            { "OrgRemark" , "OrgRemark" },
            { "OrgLogo" , "OrgLogo" },
            { "OrgIntroduce" , "OrgIntroduce" },
            { "Sort" , "Sort" },
            { "Icon" , "Icon" },
            { "StateExternal" , "StateExternal" },
            { "StateTrusteeship" , "StateTrusteeship" },
            { "BusinessLicenseCode" , "BusinessLicenseCode" },
            { "BusinessLicenseImage" , "BusinessLicenseImage" },
            { "LegalPersonName" , "LegalPersonName" },
            { "LegalPersonCardID" , "LegalPersonCardID" },
            { "LegalPersonCardImage01" , "LegalPersonCardImage01" },
            { "LegalPersonCardImage02" , "LegalPersonCardImage02" },
            { "Address" , "Address" },
            { "Location_latitude" , "Location_latitude" },
            { "Location_longitude" , "Location_longitude" },
            { "Id" , "OID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,OrganizationData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._oID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._pID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._siteSID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._orgKey = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._orgName = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._orgChief = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._orgRemark = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._orgLogo = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._orgIntroduce = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._sort = reader.GetInt32(9);
                if (!reader.IsDBNull(10))
                    entity._icon = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._stateExternal = (bool)reader.GetBoolean(11);
                if (!reader.IsDBNull(12))
                    entity._stateTrusteeship = (bool)reader.GetBoolean(12);
                if (!reader.IsDBNull(13))
                    entity._businessLicenseCode = reader.GetString(13);
                if (!reader.IsDBNull(14))
                    entity._businessLicenseImage = reader.GetString(14);
                if (!reader.IsDBNull(15))
                    entity._legalPersonName = reader.GetString(15);
                if (!reader.IsDBNull(16))
                    entity._legalPersonCardID = reader.GetString(16);
                if (!reader.IsDBNull(17))
                    entity._legalPersonCardImage01 = reader.GetString(17);
                if (!reader.IsDBNull(18))
                    entity._legalPersonCardImage02 = reader.GetString(18);
                if (!reader.IsDBNull(19))
                    entity._address = reader.GetString(19);
                if (!reader.IsDBNull(20))
                    entity._location_latitude = (float)reader.GetDouble(20);
                if (!reader.IsDBNull(21))
                    entity._location_longitude = (float)reader.GetDouble(21);
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
                case "OID":
                    return SqlDbType.BigInt;
                case "PID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgKey":
                    return SqlDbType.NVarChar;
                case "OrgName":
                    return SqlDbType.NVarChar;
                case "OrgChief":
                    return SqlDbType.NVarChar;
                case "OrgRemark":
                    return SqlDbType.NVarChar;
                case "OrgLogo":
                    return SqlDbType.NVarChar;
                case "OrgIntroduce":
                    return SqlDbType.NVarChar;
                case "Sort":
                    return SqlDbType.Int;
                case "Icon":
                    return SqlDbType.NVarChar;
                case "StateExternal":
                    return SqlDbType.Bit;
                case "StateTrusteeship":
                    return SqlDbType.Bit;
                case "BusinessLicenseCode":
                    return SqlDbType.NVarChar;
                case "BusinessLicenseImage":
                    return SqlDbType.NVarChar;
                case "LegalPersonName":
                    return SqlDbType.NVarChar;
                case "LegalPersonCardID":
                    return SqlDbType.NVarChar;
                case "LegalPersonCardImage01":
                    return SqlDbType.NVarChar;
                case "LegalPersonCardImage02":
                    return SqlDbType.NVarChar;
                case "Address":
                    return SqlDbType.NVarChar;
                case "Location_latitude":
                    return SqlDbType.Decimal;
                case "Location_longitude":
                    return SqlDbType.Decimal;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(OrganizationData entity, SqlCommand cmd)
        {
            //02:组织编号(OID)
            cmd.Parameters.Add(new SqlParameter("OID",SqlDbType.BigInt){ Value = entity.OID});
            //03:上级组织编号(PID)
            cmd.Parameters.Add(new SqlParameter("PID",SqlDbType.BigInt){ Value = entity.PID});
            //04:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //05:全局标识(OrgKey)
            var isNull = string.IsNullOrWhiteSpace(entity.OrgKey);
            var parameter = new SqlParameter("OrgKey",SqlDbType.NVarChar , isNull ? 10 : (entity.OrgKey).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgKey;
            cmd.Parameters.Add(parameter);
            //06:组织名称(OrgName)
            isNull = string.IsNullOrWhiteSpace(entity.OrgName);
            parameter = new SqlParameter("OrgName",SqlDbType.NVarChar , isNull ? 10 : (entity.OrgName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgName;
            cmd.Parameters.Add(parameter);
            //07:首席执行官(OrgChief)
            isNull = string.IsNullOrWhiteSpace(entity.OrgChief);
            parameter = new SqlParameter("OrgChief",SqlDbType.NVarChar , isNull ? 10 : (entity.OrgChief).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgChief;
            cmd.Parameters.Add(parameter);
            //08:组织备注(OrgRemark)
            isNull = string.IsNullOrWhiteSpace(entity.OrgRemark);
            parameter = new SqlParameter("OrgRemark",SqlDbType.NVarChar , isNull ? 10 : (entity.OrgRemark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgRemark;
            cmd.Parameters.Add(parameter);
            //09:Logo(OrgLogo)
            isNull = string.IsNullOrWhiteSpace(entity.OrgLogo);
            parameter = new SqlParameter("OrgLogo",SqlDbType.NVarChar , isNull ? 10 : (entity.OrgLogo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgLogo;
            cmd.Parameters.Add(parameter);
            //10:组织简介(OrgIntroduce)
            isNull = string.IsNullOrWhiteSpace(entity.OrgIntroduce);
            parameter = new SqlParameter("OrgIntroduce",SqlDbType.NVarChar , isNull ? 10 : (entity.OrgIntroduce).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgIntroduce;
            cmd.Parameters.Add(parameter);
            //11:序号(Sort)
            cmd.Parameters.Add(new SqlParameter("Sort",SqlDbType.Int){ Value = entity.Sort});
            //12:小图标(Icon)
            isNull = string.IsNullOrWhiteSpace(entity.Icon);
            parameter = new SqlParameter("Icon",SqlDbType.NVarChar , isNull ? 10 : (entity.Icon).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Icon;
            cmd.Parameters.Add(parameter);
            //13:外部状态(StateExternal)
            cmd.Parameters.Add(new SqlParameter("StateExternal",SqlDbType.Bit){ Value = entity.StateExternal});
            //14:托管状态(StateTrusteeship)
            cmd.Parameters.Add(new SqlParameter("StateTrusteeship",SqlDbType.Bit){ Value = entity.StateTrusteeship});
            //15:营业执照代码(BusinessLicenseCode)
            isNull = string.IsNullOrWhiteSpace(entity.BusinessLicenseCode);
            parameter = new SqlParameter("BusinessLicenseCode",SqlDbType.NVarChar , isNull ? 10 : (entity.BusinessLicenseCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.BusinessLicenseCode;
            cmd.Parameters.Add(parameter);
            //16:营业执照图片(BusinessLicenseImage)
            isNull = string.IsNullOrWhiteSpace(entity.BusinessLicenseImage);
            parameter = new SqlParameter("BusinessLicenseImage",SqlDbType.NVarChar , isNull ? 10 : (entity.BusinessLicenseImage).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.BusinessLicenseImage;
            cmd.Parameters.Add(parameter);
            //17:法人姓名(LegalPersonName)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonName);
            parameter = new SqlParameter("LegalPersonName",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonName;
            cmd.Parameters.Add(parameter);
            //18:法人身份证号(LegalPersonCardID)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonCardID);
            parameter = new SqlParameter("LegalPersonCardID",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonCardID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonCardID;
            cmd.Parameters.Add(parameter);
            //19:法人证件图像01(LegalPersonCardImage01)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonCardImage01);
            parameter = new SqlParameter("LegalPersonCardImage01",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonCardImage01).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonCardImage01;
            cmd.Parameters.Add(parameter);
            //20:法人证件图像02(LegalPersonCardImage02)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonCardImage02);
            parameter = new SqlParameter("LegalPersonCardImage02",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonCardImage02).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonCardImage02;
            cmd.Parameters.Add(parameter);
            //21:地理地址(Address)
            isNull = string.IsNullOrWhiteSpace(entity.Address);
            parameter = new SqlParameter("Address",SqlDbType.NVarChar , isNull ? 10 : (entity.Address).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Address;
            cmd.Parameters.Add(parameter);
            //22:地理纬度(Location_latitude)
            cmd.Parameters.Add(new SqlParameter("Location_latitude",SqlDbType.Decimal){ Value = entity.Location_latitude});
            //23:地理经度(Location_longitude)
            cmd.Parameters.Add(new SqlParameter("Location_longitude",SqlDbType.Decimal){ Value = entity.Location_longitude});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(OrganizationData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizationData entity, SqlCommand cmd)
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
        /// 组织机构的结构语句
        /// </summary>
        private TableSql _tbOrganizationSql = new TableSql
        {
            TableName = "tbOrganization",
            PimaryKey = "OID"
        };


        /// <summary>
        /// 组织机构数据访问对象
        /// </summary>
        private OrganizationDataAccess _organizations;

        /// <summary>
        /// 组织机构数据访问对象
        /// </summary>
        public OrganizationDataAccess Organizations
        {
            get
            {
                return this._organizations ?? ( this._organizations = new OrganizationDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 组织机构(tbOrganization):组织机构
        /// </summary>
        public const int Table_Organization = 0x0;
    }
}