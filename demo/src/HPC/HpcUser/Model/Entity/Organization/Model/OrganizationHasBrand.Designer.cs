/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:30*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;


using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;


#endregion

namespace HPC.Projects
{
    /// <summary>
    /// 组织是否品牌
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationHasBrandData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationHasBrandData()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
        #endregion

        #region 基本属性


        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey(long oBID)
        {
            _oBID = oBID;
        }
        /// <summary>
        /// 奥比德
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _oBID;

        partial void OnOBIDGet();

        partial void OnOBIDSet(ref long value);

        partial void OnOBIDLoad(ref long value);

        partial void OnOBIDSeted();

        
        /// <summary>
        /// 奥比德
        /// </summary>
        [DataMember , JsonProperty("OBID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"奥比德")]
        public long OBID
        {
            get
            {
                OnOBIDGet();
                return this._oBID;
            }
            set
            {
                if(this._oBID == value)
                    return;
                //if(this._oBID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnOBIDSet(ref value);
                this._oBID = value;
                this.OnPropertyChanged(_DataStruct_.Real_OBID);
                OnOBIDSeted();
            }
        }
        /// <summary>
        /// 组织标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgOID;

        partial void OnOrgOIDGet();

        partial void OnOrgOIDSet(ref long value);

        partial void OnOrgOIDSeted();

        
        /// <summary>
        /// 组织标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识")]
        public  long OrgOID
        {
            get
            {
                OnOrgOIDGet();
                return this._orgOID;
            }
            set
            {
                if(this._orgOID == value)
                    return;
                OnOrgOIDSet(ref value);
                this._orgOID = value;
                OnOrgOIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgOID);
            }
        }
        /// <summary>
        /// 品牌竞标
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _brandBID;

        partial void OnBrandBIDGet();

        partial void OnBrandBIDSet(ref long value);

        partial void OnBrandBIDSeted();

        
        /// <summary>
        /// 品牌竞标
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BrandBID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"品牌竞标")]
        public  long BrandBID
        {
            get
            {
                OnBrandBIDGet();
                return this._brandBID;
            }
            set
            {
                if(this._brandBID == value)
                    return;
                OnBrandBIDSet(ref value);
                this._brandBID = value;
                OnBrandBIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BrandBID);
            }
        }

        #region 接口属性


        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public long Id
        {
            get
            {
                return this.OBID;
            }
            set
            {
                this.OBID = value;
            }
        }
        #endregion
        #region 扩展属性

        #endregion
        #endregion


        #region 名称的属性操作

    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected override bool SetValueInner(string property, string value)
        {
            if(property == null) return false;
            switch(property.Trim().ToLower())
            {
            case "obid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OBID = vl;
                        return true;
                    }
                }
                return false;
            case "orgoid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OrgOID = vl;
                        return true;
                    }
                }
                return false;
            case "brandbid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.BrandBID = vl;
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected override void SetValueInner(string property, object value)
        {
            if(property == null) return;
            switch(property.Trim().ToLower())
            {
            case "obid":
                this.OBID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "brandbid":
                this.BrandBID = (long)Convert.ToDecimal(value);
                return;
            }

            //System.Diagnostics.Trace.WriteLine(property + @"=>" + value);

        }

    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void SetValueInner(int index, object value)
        {
            switch(index)
            {
            case _DataStruct_.OBID:
                this.OBID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.BrandBID:
                this.BrandBID = Convert.ToInt64(value);
                return;
            }
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="property"></param>
        protected override object GetValueInner(string property)
        {
            switch(property)
            {
            case "obid":
                return this.OBID;
            case "orgoid":
                return this.OrgOID;
            case "brandbid":
                return this.BrandBID;
            }

            return null;
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="index"></param>
        protected override object GetValueInner(int index)
        {
            switch(index)
            {
                case _DataStruct_.OBID:
                    return this.OBID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.BrandBID:
                    return this.BrandBID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationHasBrandData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationHasBrandData;
            if(sourceEntity == null)
                return;
            this._oBID = sourceEntity._oBID;
            this._orgOID = sourceEntity._orgOID;
            this._brandBID = sourceEntity._brandBID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationHasBrandData source)
        {
                this.OBID = source.OBID;
                this.OrgOID = source.OrgOID;
                this.BrandBID = source.BrandBID;
        }
        #endregion

        #region 后期处理
        

        /// <summary>
        /// 单个属性修改的后期处理(保存后)
        /// </summary>
        /// <param name="subsist">当前实体生存状态</param>
        /// <param name="modifieds">修改列表</param>
        /// <remarks>
        /// 对当前对象的属性的更改,请自行保存,否则将丢失
        /// </remarks>
        protected override void OnLaterPeriodBySignleModified(EntitySubsist subsist,byte[] modifieds)
        {
            if (subsist == EntitySubsist.Deleting)
            {
                OnOBIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnBrandBIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOBIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnBrandBIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[3] > 0)
            {
                OnOBIDModified(subsist,modifieds[_DataStruct_.Real_OBID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnBrandBIDModified(subsist,modifieds[_DataStruct_.Real_BrandBID] == 1);
            }
        }

        /// <summary>
        /// 奥比德修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOBIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 品牌竞标修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBrandBIDModified(EntitySubsist subsist,bool isModified);
        #endregion

        #region 数据结构

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {
            get
            {
                return _DataStruct_.Struct;
            }
        }
        /// <summary>
        /// 实体结构
        /// </summary>
        public class _DataStruct_
        {
            /// <summary>
            /// 实体名称
            /// </summary>
            public const string EntityName = @"OrganizationHasBrand";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织是否品牌";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织是否品牌";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "OBID";
            
            
            /// <summary>
            /// 奥比德的数字标识
            /// </summary>
            public const byte OBID = 1;
            
            /// <summary>
            /// 奥比德的实时记录顺序
            /// </summary>
            public const int Real_OBID = 0;

            /// <summary>
            /// 组织标识的数字标识
            /// </summary>
            public const byte OrgOID = 2;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_OrgOID = 1;

            /// <summary>
            /// 品牌竞标的数字标识
            /// </summary>
            public const byte BrandBID = 3;
            
            /// <summary>
            /// 品牌竞标的实时记录顺序
            /// </summary>
            public const int Real_BrandBID = 2;

            /// <summary>
            /// 实体结构
            /// </summary>
            public static readonly EntitySturct Struct = new EntitySturct
            {
                EntityName = EntityName,
                Caption    = EntityCaption,
                Description= EntityDescription,
                PrimaryKey = EntityPrimaryKey,
                EntityType = EntityIdentity,
                Properties = new Dictionary<int, PropertySturct>
                {
                    {
                        Real_OBID,
                        new PropertySturct
                        {
                            Index        = OBID,
                            Name         = "OBID",
                            Title        = "奥比德",
                            Caption      = @"奥比德",
                            Description  = @"奥比德",
                            ColumnName   = "OBID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgOID,
                        new PropertySturct
                        {
                            Index        = OrgOID,
                            Name         = "OrgOID",
                            Title        = "组织标识",
                            Caption      = @"组织标识",
                            Description  = @"组织标识",
                            ColumnName   = "OrgOID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BrandBID,
                        new PropertySturct
                        {
                            Index        = BrandBID,
                            Name         = "BrandBID",
                            Title        = "品牌竞标",
                            Caption      = @"品牌竞标",
                            Description  = @"品牌竞标",
                            ColumnName   = "BrandBID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    }
                }
            };
        }
        #endregion

    }
}