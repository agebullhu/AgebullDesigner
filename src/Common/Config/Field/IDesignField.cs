﻿namespace Agebull.EntityModel.Config
{
    public interface IDesignField : IConfig
    {
        #region 设计器支持

        /// <summary>
        /// 上级
        /// </summary>
        IEntityConfig Parent { get; }// Field.Entity;

        /// <summary>
        /// 实体
        /// </summary>
        EntityConfig Entity { get; set; }// Field.Entity;

        /// <summary>
        ///     原始字段
        /// </summary>
        FieldConfig Field
        {
            get;
        }
        /// <summary>
        /// 分组
        /// </summary>
        string Group
        {
            get;
            set;
        }
        /// <summary>
        /// 不生成属性
        /// </summary>
        bool NoProperty { get; set; }

        #endregion

        #region 数据类型

        /// <summary>
        /// 是否用户内容
        /// </summary>
        bool IsUserContent { get; set; }// Field.IsEnum;

        /// <summary>
        /// 枚举类型(C#)
        /// </summary>
        bool IsEnum { get; set; }// Field.IsEnum;

        /// <summary>
        /// 是否扩展数组
        /// </summary>
        /// <remark>
        /// 是否扩展数组
        /// </remark>
        bool IsArray { get; set; }// Field.IsArray;

        /// <summary>
        /// 是否字典
        /// </summary>
        bool IsDictionary { get; set; }// Field.IsDictionary;

        /// <summary>
        /// 数组长度
        /// </summary>
        string ArrayLen { get; set; }// Field.ArrayLen;

        #endregion

        #region 字段特性


        /// <summary>
        /// 是否扩展值
        /// </summary>
        /// <remark>
        /// 是否扩展值
        /// </remark>
        bool IsExtendValue { get; set; }// Field.Nullable;

        /// <summary>
        /// 内部字段,用户不可见
        /// </summary>
        bool InnerField { get; set; }// Field.InnerField;

        /// <summary>
        /// 系统字段
        /// </summary>
        bool IsSystemField { get; set; }// Field.IsSystemField;

        /// <summary>
        /// 接口字段
        /// </summary>
        bool IsInterfaceField { get; set; }// Field.IsInterfaceField;


        /// <summary>
        /// 不参与ApiArgument序列化
        /// </summary>
        bool NoneApiArgument
        {
            get;
            set;
        }

        /// <summary>
        /// 计算列
        /// </summary>
        /// <remark>
        /// 是否计算列，即数据源于其它字段.如关系引用字段
        /// </remark>
        bool IsCompute { get; set; }// Field.IsCompute;

        #endregion

        #region 基本模型

        /// <summary>
        /// 可读
        /// </summary>
        /// <remark>
        /// 可读,即可以生成Get代码
        /// </remark>
        bool CanGet { get; set; }// Field.CanGet;

        /// <summary>
        /// 可写
        /// </summary>
        /// <remark>
        /// 可写,即生成SET代码
        /// </remark>
        bool CanSet { get; set; }// Field.CanSet;
        #endregion

    }
}