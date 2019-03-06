/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/6 10:20:20*/
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
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.Extends;
using Agebull.Common.WebApi;


#endregion

namespace Agebull.EntityModel.Demo
{
    /// <summary>
    /// 用于演示实体的作用
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class DemoEntityData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public DemoEntityData()
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
        public void ChangePrimaryKey(long id)
        {
            _id = id;
        }
        /// <summary>
        /// 标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _id;

        partial void OnIdGet();

        partial void OnIdSet(ref long value);

        partial void OnIdLoad(ref long value);

        partial void OnIdSeted();

        
        /// <summary>
        /// 标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
        public long Id
        {
            get
            {
                OnIdGet();
                return this._id;
            }
            set
            {
                if(this._id == value)
                    return;
                //if(this._id > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnIdSet(ref value);
                this._id = value;
                this.OnPropertyChanged(_DataStruct_.Real_Id);
                OnIdSeted();
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _name;

        partial void OnNameGet();

        partial void OnNameSet(ref string value);

        partial void OnNameSeted();

        
        /// <summary>
        /// 名称
        /// </summary>
        /// <remarks>
        /// 对象的名称
        /// </remarks>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("name", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"名称")]
        public  string Name
        {
            get
            {
                OnNameGet();
                return this._name;
            }
            set
            {
                if(this._name == value)
                    return;
                OnNameSet(ref value);
                this._name = value;
                OnNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Name);
            }
        }
        /// <summary>
        /// 价格
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _price;

        partial void OnPriceGet();

        partial void OnPriceSet(ref decimal value);

        partial void OnPriceSeted();

        
        /// <summary>
        /// 价格
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("price", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"价格")]
        public  decimal Price
        {
            get
            {
                OnPriceGet();
                return this._price;
            }
            set
            {
                if(this._price == value)
                    return;
                OnPriceSet(ref value);
                this._price = value;
                OnPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Price);
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _value;

        partial void OnValueGet();

        partial void OnValueSet(ref int value);

        partial void OnValueSeted();

        
        /// <summary>
        /// 数量
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("value", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"数量")]
        public  int Value
        {
            get
            {
                OnValueGet();
                return this._value;
            }
            set
            {
                if(this._value == value)
                    return;
                OnValueSet(ref value);
                this._value = value;
                OnValueSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Value);
            }
        }
        /// <summary>
        /// 注释
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _memo;

        partial void OnMemoGet();

        partial void OnMemoSet(ref string value);

        partial void OnMemoSeted();

        
        /// <summary>
        /// 注释
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("memo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注释")]
        public  string Memo
        {
            get
            {
                OnMemoGet();
                return this._memo;
            }
            set
            {
                if(this._memo == value)
                    return;
                OnMemoSet(ref value);
                this._memo = value;
                OnMemoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Memo);
            }
        }

        #region 接口属性

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
        protected override void SetValueInner(string property, object value)
        {
            if(property == null) return;
            switch(property.Trim().ToLower())
            {
            case "id":
                this.Id = (long)Convert.ToDecimal(value);
                return;
            case "name":
                this.Name = value == null ? null : value.ToString();
                return;
            case "price":
                this.Price = Convert.ToDecimal(value);
                return;
            case "value":
                this.Value = (int)Convert.ToDecimal(value);
                return;
            case "memo":
                this.Memo = value == null ? null : value.ToString();
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
            case _DataStruct_.Id:
                this.Id = Convert.ToInt64(value);
                return;
            case _DataStruct_.Name:
                this.Name = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Price:
                this.Price = Convert.ToDecimal(value);
                return;
            case _DataStruct_.Value:
                this.Value = Convert.ToInt32(value);
                return;
            case _DataStruct_.Memo:
                this.Memo = value == null ? null : value.ToString();
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
            case "id":
                return this.Id;
            case "name":
                return this.Name;
            case "price":
                return this.Price;
            case "value":
                return this.Value;
            case "memo":
                return this.Memo;
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
                case _DataStruct_.Id:
                    return this.Id;
                case _DataStruct_.Name:
                    return this.Name;
                case _DataStruct_.Price:
                    return this.Price;
                case _DataStruct_.Value:
                    return this.Value;
                case _DataStruct_.Memo:
                    return this.Memo;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(DemoEntityData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as DemoEntityData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._name = sourceEntity._name;
            this._price = sourceEntity._price;
            this._value = sourceEntity._value;
            this._memo = sourceEntity._memo;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(DemoEntityData source)
        {
                this.Id = source.Id;
                this.Name = source.Name;
                this.Price = source.Price;
                this.Value = source.Value;
                this.Memo = source.Memo;
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
                OnIdModified(subsist,false);
                OnNameModified(subsist,false);
                OnPriceModified(subsist,false);
                OnValueModified(subsist,false);
                OnMemoModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnNameModified(subsist,true);
                OnPriceModified(subsist,true);
                OnValueModified(subsist,true);
                OnMemoModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnNameModified(subsist,modifieds[_DataStruct_.Real_Name] == 1);
                OnPriceModified(subsist,modifieds[_DataStruct_.Real_Price] == 1);
                OnValueModified(subsist,modifieds[_DataStruct_.Real_Value] == 1);
                OnMemoModified(subsist,modifieds[_DataStruct_.Real_Memo] == 1);
            }
        }

        /// <summary>
        /// 标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 价格修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 数量修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnValueModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注释修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMemoModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"DemoEntity";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"演示实体";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用于演示实体的作用";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "Id";
            
            
            /// <summary>
            /// 标识的数字标识
            /// </summary>
            public const byte Id = 1;
            
            /// <summary>
            /// 标识的实时记录顺序
            /// </summary>
            public const int Real_Id = 0;

            /// <summary>
            /// 名称的数字标识
            /// </summary>
            public const byte Name = 2;
            
            /// <summary>
            /// 名称的实时记录顺序
            /// </summary>
            public const int Real_Name = 1;

            /// <summary>
            /// 价格的数字标识
            /// </summary>
            public const byte Price = 3;
            
            /// <summary>
            /// 价格的实时记录顺序
            /// </summary>
            public const int Real_Price = 2;

            /// <summary>
            /// 数量的数字标识
            /// </summary>
            public const byte Value = 4;
            
            /// <summary>
            /// 数量的实时记录顺序
            /// </summary>
            public const int Real_Value = 3;

            /// <summary>
            /// 注释的数字标识
            /// </summary>
            public const byte Memo = 5;
            
            /// <summary>
            /// 注释的实时记录顺序
            /// </summary>
            public const int Real_Memo = 4;

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
                        Real_Id,
                        new PropertySturct
                        {
                            Index        = Id,
                            Name         = "Id",
                            Title        = "标识",
                            Caption      = @"标识",
                            Description  = @"标识",
                            ColumnName   = "id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Name,
                        new PropertySturct
                        {
                            Index        = Name,
                            Name         = "Name",
                            Title        = "名称",
                            Caption      = @"名称",
                            Description  = @"对象的名称",
                            ColumnName   = "name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Price,
                        new PropertySturct
                        {
                            Index        = Price,
                            Name         = "Price",
                            Title        = "价格",
                            Caption      = @"价格",
                            Description  = @"价格",
                            ColumnName   = "price",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Value,
                        new PropertySturct
                        {
                            Index        = Value,
                            Name         = "Value",
                            Title        = "数量",
                            Caption      = @"数量",
                            Description  = @"数量",
                            ColumnName   = "value",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Memo,
                        new PropertySturct
                        {
                            Index        = Memo,
                            Name         = "Memo",
                            Title        = "注释",
                            Caption      = @"注释",
                            Description  = @"注释",
                            ColumnName   = "memo",
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