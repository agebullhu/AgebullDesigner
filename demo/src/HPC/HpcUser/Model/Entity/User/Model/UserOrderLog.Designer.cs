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
    /// 用户日志
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserOrderLogData 
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserOrderLogData()
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
        public void ChangePrimaryKey(int lID)
        {
            _lID = lID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _lID;

        partial void OnLIDGet();

        partial void OnLIDSet(ref int value);

        partial void OnLIDLoad(ref int value);

        partial void OnLIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("LID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public int LID
        {
            get
            {
                OnLIDGet();
                return this._lID;
            }
            set
            {
                if(this._lID == value)
                    return;
                //if(this._lID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnLIDSet(ref value);
                this._lID = value;
                this.OnPropertyChanged(_DataStruct_.Real_LID);
                OnLIDSeted();
            }
        }
        /// <summary>
        /// 订单标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderID;

        partial void OnOrderIDGet();

        partial void OnOrderIDSet(ref string value);

        partial void OnOrderIDSeted();

        
        /// <summary>
        /// 订单标识
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单标识")]
        public  string OrderID
        {
            get
            {
                OnOrderIDGet();
                return this._orderID;
            }
            set
            {
                if(this._orderID == value)
                    return;
                OnOrderIDSet(ref value);
                this._orderID = value;
                OnOrderIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderID);
            }
        }
        /// <summary>
        /// 日志时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _logTime;

        partial void OnLogTimeGet();

        partial void OnLogTimeSet(ref DateTime value);

        partial void OnLogTimeSeted();

        
        /// <summary>
        /// 日志时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LogTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"日志时间")]
        public  DateTime LogTime
        {
            get
            {
                OnLogTimeGet();
                return this._logTime;
            }
            set
            {
                if(this._logTime == value)
                    return;
                OnLogTimeSet(ref value);
                this._logTime = value;
                OnLogTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LogTime);
            }
        }
        /// <summary>
        /// 日志状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _logState;

        partial void OnLogStateGet();

        partial void OnLogStateSet(ref string value);

        partial void OnLogStateSeted();

        
        /// <summary>
        /// 日志状态
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LogState", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"日志状态")]
        public  string LogState
        {
            get
            {
                OnLogStateGet();
                return this._logState;
            }
            set
            {
                if(this._logState == value)
                    return;
                OnLogStateSet(ref value);
                this._logState = value;
                OnLogStateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LogState);
            }
        }
        /// <summary>
        /// 日志相关标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _logRelevantID;

        partial void OnLogRelevantIDGet();

        partial void OnLogRelevantIDSet(ref string value);

        partial void OnLogRelevantIDSeted();

        
        /// <summary>
        /// 日志相关标识
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LogRelevantID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"日志相关标识")]
        public  string LogRelevantID
        {
            get
            {
                OnLogRelevantIDGet();
                return this._logRelevantID;
            }
            set
            {
                if(this._logRelevantID == value)
                    return;
                OnLogRelevantIDSet(ref value);
                this._logRelevantID = value;
                OnLogRelevantIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LogRelevantID);
            }
        }
        /// <summary>
        /// 日志注释
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _logRemarks;

        partial void OnLogRemarksGet();

        partial void OnLogRemarksSet(ref string value);

        partial void OnLogRemarksSeted();

        
        /// <summary>
        /// 日志注释
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LogRemarks", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"日志注释")]
        public  string LogRemarks
        {
            get
            {
                OnLogRemarksGet();
                return this._logRemarks;
            }
            set
            {
                if(this._logRemarks == value)
                    return;
                OnLogRemarksSet(ref value);
                this._logRemarks = value;
                OnLogRemarksSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LogRemarks);
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
            case "lid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.LID = vl;
                        return true;
                    }
                }
                return false;
            case "orderid":
                this.OrderID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "logtime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.LogTime = vl;
                        return true;
                    }
                }
                return false;
            case "logstate":
                this.LogState = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "logrelevantid":
                this.LogRelevantID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "logremarks":
                this.LogRemarks = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "lid":
                this.LID = (int)Convert.ToDecimal(value);
                return;
            case "orderid":
                this.OrderID = value == null ? null : value.ToString();
                return;
            case "logtime":
                this.LogTime = Convert.ToDateTime(value);
                return;
            case "logstate":
                this.LogState = value == null ? null : value.ToString();
                return;
            case "logrelevantid":
                this.LogRelevantID = value == null ? null : value.ToString();
                return;
            case "logremarks":
                this.LogRemarks = value == null ? null : value.ToString();
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
            case _DataStruct_.LID:
                this.LID = Convert.ToInt32(value);
                return;
            case _DataStruct_.OrderID:
                this.OrderID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LogTime:
                this.LogTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.LogState:
                this.LogState = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LogRelevantID:
                this.LogRelevantID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LogRemarks:
                this.LogRemarks = value == null ? null : value.ToString();
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
            case "lid":
                return this.LID;
            case "orderid":
                return this.OrderID;
            case "logtime":
                return this.LogTime;
            case "logstate":
                return this.LogState;
            case "logrelevantid":
                return this.LogRelevantID;
            case "logremarks":
                return this.LogRemarks;
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
                case _DataStruct_.LID:
                    return this.LID;
                case _DataStruct_.OrderID:
                    return this.OrderID;
                case _DataStruct_.LogTime:
                    return this.LogTime;
                case _DataStruct_.LogState:
                    return this.LogState;
                case _DataStruct_.LogRelevantID:
                    return this.LogRelevantID;
                case _DataStruct_.LogRemarks:
                    return this.LogRemarks;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserOrderLogData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserOrderLogData;
            if(sourceEntity == null)
                return;
            this._lID = sourceEntity._lID;
            this._orderID = sourceEntity._orderID;
            this._logTime = sourceEntity._logTime;
            this._logState = sourceEntity._logState;
            this._logRelevantID = sourceEntity._logRelevantID;
            this._logRemarks = sourceEntity._logRemarks;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserOrderLogData source)
        {
                this.LID = source.LID;
                this.OrderID = source.OrderID;
                this.LogTime = source.LogTime;
                this.LogState = source.LogState;
                this.LogRelevantID = source.LogRelevantID;
                this.LogRemarks = source.LogRemarks;
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
                OnLIDModified(subsist,false);
                OnOrderIDModified(subsist,false);
                OnLogTimeModified(subsist,false);
                OnLogStateModified(subsist,false);
                OnLogRelevantIDModified(subsist,false);
                OnLogRemarksModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnLIDModified(subsist,true);
                OnOrderIDModified(subsist,true);
                OnLogTimeModified(subsist,true);
                OnLogStateModified(subsist,true);
                OnLogRelevantIDModified(subsist,true);
                OnLogRemarksModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[6] > 0)
            {
                OnLIDModified(subsist,modifieds[_DataStruct_.Real_LID] == 1);
                OnOrderIDModified(subsist,modifieds[_DataStruct_.Real_OrderID] == 1);
                OnLogTimeModified(subsist,modifieds[_DataStruct_.Real_LogTime] == 1);
                OnLogStateModified(subsist,modifieds[_DataStruct_.Real_LogState] == 1);
                OnLogRelevantIDModified(subsist,modifieds[_DataStruct_.Real_LogRelevantID] == 1);
                OnLogRemarksModified(subsist,modifieds[_DataStruct_.Real_LogRemarks] == 1);
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
        partial void OnLIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 日志时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLogTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 日志状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLogStateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 日志相关标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLogRelevantIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 日志注释修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLogRemarksModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserOrderLog";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户日志";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户日志";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "LID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte LID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_LID = 0;

            /// <summary>
            /// 订单标识的数字标识
            /// </summary>
            public const byte OrderID = 2;
            
            /// <summary>
            /// 订单标识的实时记录顺序
            /// </summary>
            public const int Real_OrderID = 1;

            /// <summary>
            /// 日志时间的数字标识
            /// </summary>
            public const byte LogTime = 3;
            
            /// <summary>
            /// 日志时间的实时记录顺序
            /// </summary>
            public const int Real_LogTime = 2;

            /// <summary>
            /// 日志状态的数字标识
            /// </summary>
            public const byte LogState = 4;
            
            /// <summary>
            /// 日志状态的实时记录顺序
            /// </summary>
            public const int Real_LogState = 3;

            /// <summary>
            /// 日志相关标识的数字标识
            /// </summary>
            public const byte LogRelevantID = 5;
            
            /// <summary>
            /// 日志相关标识的实时记录顺序
            /// </summary>
            public const int Real_LogRelevantID = 4;

            /// <summary>
            /// 日志注释的数字标识
            /// </summary>
            public const byte LogRemarks = 6;
            
            /// <summary>
            /// 日志注释的实时记录顺序
            /// </summary>
            public const int Real_LogRemarks = 5;

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
                        Real_LID,
                        new PropertySturct
                        {
                            Index        = LID,
                            Name         = "LID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "LID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderID,
                        new PropertySturct
                        {
                            Index        = OrderID,
                            Name         = "OrderID",
                            Title        = "订单标识",
                            Caption      = @"订单标识",
                            Description  = @"订单标识",
                            ColumnName   = "OrderID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LogTime,
                        new PropertySturct
                        {
                            Index        = LogTime,
                            Name         = "LogTime",
                            Title        = "日志时间",
                            Caption      = @"日志时间",
                            Description  = @"日志时间",
                            ColumnName   = "LogTime",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LogState,
                        new PropertySturct
                        {
                            Index        = LogState,
                            Name         = "LogState",
                            Title        = "日志状态",
                            Caption      = @"日志状态",
                            Description  = @"日志状态",
                            ColumnName   = "LogState",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LogRelevantID,
                        new PropertySturct
                        {
                            Index        = LogRelevantID,
                            Name         = "LogRelevantID",
                            Title        = "日志相关标识",
                            Caption      = @"日志相关标识",
                            Description  = @"日志相关标识",
                            ColumnName   = "LogRelevantID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LogRemarks,
                        new PropertySturct
                        {
                            Index        = LogRemarks,
                            Name         = "LogRemarks",
                            Title        = "日志注释",
                            Caption      = @"日志注释",
                            Description  = @"日志注释",
                            ColumnName   = "LogRemarks",
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