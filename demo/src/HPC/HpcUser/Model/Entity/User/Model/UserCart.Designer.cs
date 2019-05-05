/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
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
    /// 用户车
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserCartData 
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserCartData()
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
        public void ChangePrimaryKey(int cID)
        {
            _cID = cID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _cID;

        partial void OnCIDGet();

        partial void OnCIDSet(ref int value);

        partial void OnCIDLoad(ref int value);

        partial void OnCIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("CID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public int CID
        {
            get
            {
                OnCIDGet();
                return this._cID;
            }
            set
            {
                if(this._cID == value)
                    return;
                //if(this._cID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnCIDSet(ref value);
                this._cID = value;
                this.OnPropertyChanged(_DataStruct_.Real_CID);
                OnCIDSeted();
            }
        }
        /// <summary>
        /// 客户满意度
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _customerID;

        partial void OnCustomerIDGet();

        partial void OnCustomerIDSet(ref int value);

        partial void OnCustomerIDSeted();

        
        /// <summary>
        /// 客户满意度
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("CustomerID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"客户满意度")]
        public  int CustomerID
        {
            get
            {
                OnCustomerIDGet();
                return this._customerID;
            }
            set
            {
                if(this._customerID == value)
                    return;
                OnCustomerIDSet(ref value);
                this._customerID = value;
                OnCustomerIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CustomerID);
            }
        }
        /// <summary>
        /// 货物运输
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _goodsID;

        partial void OnGoodsIDGet();

        partial void OnGoodsIDSet(ref int value);

        partial void OnGoodsIDSeted();

        
        /// <summary>
        /// 货物运输
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("GoodsID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"货物运输")]
        public  int GoodsID
        {
            get
            {
                OnGoodsIDGet();
                return this._goodsID;
            }
            set
            {
                if(this._goodsID == value)
                    return;
                OnGoodsIDSet(ref value);
                this._goodsID = value;
                OnGoodsIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_GoodsID);
            }
        }
        /// <summary>
        /// 包装袋
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _packageID;

        partial void OnPackageIDGet();

        partial void OnPackageIDSet(ref int value);

        partial void OnPackageIDSeted();

        
        /// <summary>
        /// 包装袋
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PackageID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"包装袋")]
        public  int PackageID
        {
            get
            {
                OnPackageIDGet();
                return this._packageID;
            }
            set
            {
                if(this._packageID == value)
                    return;
                OnPackageIDSet(ref value);
                this._packageID = value;
                OnPackageIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PackageID);
            }
        }
        /// <summary>
        /// 包装计数
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _packageCount;

        partial void OnPackageCountGet();

        partial void OnPackageCountSet(ref int value);

        partial void OnPackageCountSeted();

        
        /// <summary>
        /// 包装计数
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PackageCount", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"包装计数")]
        public  int PackageCount
        {
            get
            {
                OnPackageCountGet();
                return this._packageCount;
            }
            set
            {
                if(this._packageCount == value)
                    return;
                OnPackageCountSet(ref value);
                this._packageCount = value;
                OnPackageCountSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PackageCount);
            }
        }

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
            case "cid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.CID = vl;
                        return true;
                    }
                }
                return false;
            case "customerid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.CustomerID = vl;
                        return true;
                    }
                }
                return false;
            case "goodsid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.GoodsID = vl;
                        return true;
                    }
                }
                return false;
            case "packageid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.PackageID = vl;
                        return true;
                    }
                }
                return false;
            case "packagecount":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.PackageCount = vl;
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
            case "cid":
                this.CID = (int)Convert.ToDecimal(value);
                return;
            case "customerid":
                this.CustomerID = (int)Convert.ToDecimal(value);
                return;
            case "goodsid":
                this.GoodsID = (int)Convert.ToDecimal(value);
                return;
            case "packageid":
                this.PackageID = (int)Convert.ToDecimal(value);
                return;
            case "packagecount":
                this.PackageCount = (int)Convert.ToDecimal(value);
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
            case _DataStruct_.CID:
                this.CID = Convert.ToInt32(value);
                return;
            case _DataStruct_.CustomerID:
                this.CustomerID = Convert.ToInt32(value);
                return;
            case _DataStruct_.GoodsID:
                this.GoodsID = Convert.ToInt32(value);
                return;
            case _DataStruct_.PackageID:
                this.PackageID = Convert.ToInt32(value);
                return;
            case _DataStruct_.PackageCount:
                this.PackageCount = Convert.ToInt32(value);
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
            case "cid":
                return this.CID;
            case "customerid":
                return this.CustomerID;
            case "goodsid":
                return this.GoodsID;
            case "packageid":
                return this.PackageID;
            case "packagecount":
                return this.PackageCount;
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
                case _DataStruct_.CID:
                    return this.CID;
                case _DataStruct_.CustomerID:
                    return this.CustomerID;
                case _DataStruct_.GoodsID:
                    return this.GoodsID;
                case _DataStruct_.PackageID:
                    return this.PackageID;
                case _DataStruct_.PackageCount:
                    return this.PackageCount;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserCartData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserCartData;
            if(sourceEntity == null)
                return;
            this._cID = sourceEntity._cID;
            this._customerID = sourceEntity._customerID;
            this._goodsID = sourceEntity._goodsID;
            this._packageID = sourceEntity._packageID;
            this._packageCount = sourceEntity._packageCount;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserCartData source)
        {
                this.CID = source.CID;
                this.CustomerID = source.CustomerID;
                this.GoodsID = source.GoodsID;
                this.PackageID = source.PackageID;
                this.PackageCount = source.PackageCount;
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
                OnCIDModified(subsist,false);
                OnCustomerIDModified(subsist,false);
                OnGoodsIDModified(subsist,false);
                OnPackageIDModified(subsist,false);
                OnPackageCountModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnCIDModified(subsist,true);
                OnCustomerIDModified(subsist,true);
                OnGoodsIDModified(subsist,true);
                OnPackageIDModified(subsist,true);
                OnPackageCountModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnCIDModified(subsist,modifieds[_DataStruct_.Real_CID] == 1);
                OnCustomerIDModified(subsist,modifieds[_DataStruct_.Real_CustomerID] == 1);
                OnGoodsIDModified(subsist,modifieds[_DataStruct_.Real_GoodsID] == 1);
                OnPackageIDModified(subsist,modifieds[_DataStruct_.Real_PackageID] == 1);
                OnPackageCountModified(subsist,modifieds[_DataStruct_.Real_PackageCount] == 1);
            }
        }

        /// <summary>
        /// 主键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 客户满意度修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCustomerIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 货物运输修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnGoodsIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 包装袋修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPackageIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 包装计数修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPackageCountModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserCart";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户车";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户车";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "CID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte CID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_CID = 0;

            /// <summary>
            /// 客户满意度的数字标识
            /// </summary>
            public const byte CustomerID = 2;
            
            /// <summary>
            /// 客户满意度的实时记录顺序
            /// </summary>
            public const int Real_CustomerID = 1;

            /// <summary>
            /// 货物运输的数字标识
            /// </summary>
            public const byte GoodsID = 3;
            
            /// <summary>
            /// 货物运输的实时记录顺序
            /// </summary>
            public const int Real_GoodsID = 2;

            /// <summary>
            /// 包装袋的数字标识
            /// </summary>
            public const byte PackageID = 4;
            
            /// <summary>
            /// 包装袋的实时记录顺序
            /// </summary>
            public const int Real_PackageID = 3;

            /// <summary>
            /// 包装计数的数字标识
            /// </summary>
            public const byte PackageCount = 5;
            
            /// <summary>
            /// 包装计数的实时记录顺序
            /// </summary>
            public const int Real_PackageCount = 4;

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
                        Real_CID,
                        new PropertySturct
                        {
                            Index        = CID,
                            Name         = "CID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "CID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CustomerID,
                        new PropertySturct
                        {
                            Index        = CustomerID,
                            Name         = "CustomerID",
                            Title        = "客户满意度",
                            Caption      = @"客户满意度",
                            Description  = @"客户满意度",
                            ColumnName   = "CustomerID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_GoodsID,
                        new PropertySturct
                        {
                            Index        = GoodsID,
                            Name         = "GoodsID",
                            Title        = "货物运输",
                            Caption      = @"货物运输",
                            Description  = @"货物运输",
                            ColumnName   = "GoodsID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PackageID,
                        new PropertySturct
                        {
                            Index        = PackageID,
                            Name         = "PackageID",
                            Title        = "包装袋",
                            Caption      = @"包装袋",
                            Description  = @"包装袋",
                            ColumnName   = "PackageID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PackageCount,
                        new PropertySturct
                        {
                            Index        = PackageCount,
                            Name         = "PackageCount",
                            Title        = "包装计数",
                            Caption      = @"包装计数",
                            Description  = @"包装计数",
                            ColumnName   = "PackageCount",
                            PropertyType = typeof(int),
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