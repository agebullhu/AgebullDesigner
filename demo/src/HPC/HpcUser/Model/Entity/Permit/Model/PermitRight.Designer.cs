/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/12 21:30:55*/
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
    /// 许可权
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PermitRightData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PermitRightData()
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
        public void ChangePrimaryKey(long rID)
        {
            _rID = rID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _rID;

        partial void OnRIDGet();

        partial void OnRIDSet(ref long value);

        partial void OnRIDLoad(ref long value);

        partial void OnRIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("RID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long RID
        {
            get
            {
                OnRIDGet();
                return this._rID;
            }
            set
            {
                if(this._rID == value)
                    return;
                //if(this._rID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnRIDSet(ref value);
                this._rID = value;
                this.OnPropertyChanged(_DataStruct_.Real_RID);
                OnRIDSeted();
            }
        }
        /// <summary>
        /// 导航标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _navigationNID;

        partial void OnNavigationNIDGet();

        partial void OnNavigationNIDSet(ref long value);

        partial void OnNavigationNIDSeted();

        
        /// <summary>
        /// 导航标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("NavigationNID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"导航标识")]
        public  long NavigationNID
        {
            get
            {
                OnNavigationNIDGet();
                return this._navigationNID;
            }
            set
            {
                if(this._navigationNID == value)
                    return;
                OnNavigationNIDSet(ref value);
                this._navigationNID = value;
                OnNavigationNIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NavigationNID);
            }
        }
        /// <summary>
        /// 正确名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _rightName;

        partial void OnRightNameGet();

        partial void OnRightNameSet(ref string value);

        partial void OnRightNameSeted();

        
        /// <summary>
        /// 正确名称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RightName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"正确名称")]
        public  string RightName
        {
            get
            {
                OnRightNameGet();
                return this._rightName;
            }
            set
            {
                if(this._rightName == value)
                    return;
                OnRightNameSet(ref value);
                this._rightName = value;
                OnRightNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RightName);
            }
        }
        /// <summary>
        /// 面间名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _interFaceName;

        partial void OnInterFaceNameGet();

        partial void OnInterFaceNameSet(ref string value);

        partial void OnInterFaceNameSeted();

        
        /// <summary>
        /// 面间名称
        /// </summary>
        /// <remarks>
        /// 接口名称
        /// </remarks>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("InterFaceName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"面间名称")]
        public  string InterFaceName
        {
            get
            {
                OnInterFaceNameGet();
                return this._interFaceName;
            }
            set
            {
                if(this._interFaceName == value)
                    return;
                OnInterFaceNameSet(ref value);
                this._interFaceName = value;
                OnInterFaceNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_InterFaceName);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remark;

        partial void OnRemarkGet();

        partial void OnRemarkSet(ref string value);

        partial void OnRemarkSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>
        /// 可存储400个字符.合理长度应不大于400.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Remark
        {
            get
            {
                OnRemarkGet();
                return this._remark;
            }
            set
            {
                if(this._remark == value)
                    return;
                OnRemarkSet(ref value);
                this._remark = value;
                OnRemarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Remark);
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
                return this.RID;
            }
            set
            {
                this.RID = value;
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
            case "rid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RID = vl;
                        return true;
                    }
                }
                return false;
            case "navigationnid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.NavigationNID = vl;
                        return true;
                    }
                }
                return false;
            case "rightname":
                this.RightName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "interfacename":
                this.InterFaceName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "remark":
                this.Remark = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
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
            case "rid":
                this.RID = (long)Convert.ToDecimal(value);
                return;
            case "navigationnid":
                this.NavigationNID = (long)Convert.ToDecimal(value);
                return;
            case "rightname":
                this.RightName = value == null ? null : value.ToString();
                return;
            case "interfacename":
                this.InterFaceName = value == null ? null : value.ToString();
                return;
            case "remark":
                this.Remark = value == null ? null : value.ToString();
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
            case _DataStruct_.RID:
                this.RID = Convert.ToInt64(value);
                return;
            case _DataStruct_.NavigationNID:
                this.NavigationNID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RightName:
                this.RightName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.InterFaceName:
                this.InterFaceName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Remark:
                this.Remark = value == null ? null : value.ToString();
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
            case "rid":
                return this.RID;
            case "navigationnid":
                return this.NavigationNID;
            case "rightname":
                return this.RightName;
            case "interfacename":
                return this.InterFaceName;
            case "remark":
                return this.Remark;
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
                case _DataStruct_.RID:
                    return this.RID;
                case _DataStruct_.NavigationNID:
                    return this.NavigationNID;
                case _DataStruct_.RightName:
                    return this.RightName;
                case _DataStruct_.InterFaceName:
                    return this.InterFaceName;
                case _DataStruct_.Remark:
                    return this.Remark;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PermitRightData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PermitRightData;
            if(sourceEntity == null)
                return;
            this._rID = sourceEntity._rID;
            this._navigationNID = sourceEntity._navigationNID;
            this._rightName = sourceEntity._rightName;
            this._interFaceName = sourceEntity._interFaceName;
            this._remark = sourceEntity._remark;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PermitRightData source)
        {
                this.RID = source.RID;
                this.NavigationNID = source.NavigationNID;
                this.RightName = source.RightName;
                this.InterFaceName = source.InterFaceName;
                this.Remark = source.Remark;
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
                OnRIDModified(subsist,false);
                OnNavigationNIDModified(subsist,false);
                OnRightNameModified(subsist,false);
                OnInterFaceNameModified(subsist,false);
                OnRemarkModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnRIDModified(subsist,true);
                OnNavigationNIDModified(subsist,true);
                OnRightNameModified(subsist,true);
                OnInterFaceNameModified(subsist,true);
                OnRemarkModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnRIDModified(subsist,modifieds[_DataStruct_.Real_RID] == 1);
                OnNavigationNIDModified(subsist,modifieds[_DataStruct_.Real_NavigationNID] == 1);
                OnRightNameModified(subsist,modifieds[_DataStruct_.Real_RightName] == 1);
                OnInterFaceNameModified(subsist,modifieds[_DataStruct_.Real_InterFaceName] == 1);
                OnRemarkModified(subsist,modifieds[_DataStruct_.Real_Remark] == 1);
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
        partial void OnRIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 导航标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNavigationNIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 正确名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRightNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 面间名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnInterFaceNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRemarkModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PermitRight";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"许可权";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"许可权";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "RID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte RID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_RID = 0;

            /// <summary>
            /// 导航标识的数字标识
            /// </summary>
            public const byte NavigationNID = 2;
            
            /// <summary>
            /// 导航标识的实时记录顺序
            /// </summary>
            public const int Real_NavigationNID = 1;

            /// <summary>
            /// 正确名称的数字标识
            /// </summary>
            public const byte RightName = 3;
            
            /// <summary>
            /// 正确名称的实时记录顺序
            /// </summary>
            public const int Real_RightName = 2;

            /// <summary>
            /// 面间名称的数字标识
            /// </summary>
            public const byte InterFaceName = 4;
            
            /// <summary>
            /// 面间名称的实时记录顺序
            /// </summary>
            public const int Real_InterFaceName = 3;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Remark = 5;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Remark = 4;

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
                        Real_RID,
                        new PropertySturct
                        {
                            Index        = RID,
                            Name         = "RID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "RID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_NavigationNID,
                        new PropertySturct
                        {
                            Index        = NavigationNID,
                            Name         = "NavigationNID",
                            Title        = "导航标识",
                            Caption      = @"导航标识",
                            Description  = @"导航标识",
                            ColumnName   = "NavigationNID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RightName,
                        new PropertySturct
                        {
                            Index        = RightName,
                            Name         = "RightName",
                            Title        = "正确名称",
                            Caption      = @"正确名称",
                            Description  = @"正确名称",
                            ColumnName   = "RightName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_InterFaceName,
                        new PropertySturct
                        {
                            Index        = InterFaceName,
                            Name         = "InterFaceName",
                            Title        = "面间名称",
                            Caption      = @"面间名称",
                            Description  = @"接口名称",
                            ColumnName   = "InterFaceName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Remark,
                        new PropertySturct
                        {
                            Index        = Remark,
                            Name         = "Remark",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "Remark",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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