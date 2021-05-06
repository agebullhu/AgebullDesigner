﻿/*design by:agebull designer date:2017/7/12 23:16:38*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Agebull.EntityModel.Config.V2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{

    /// <summary>
    /// 实体配置
    /// </summary>
    public interface IEntityConfig : IConfig, IEntityUi, IEntityCpp, IConfigIterator//, IDataTable
    {
        #region 视角开关

        bool IsLinkTable
        {
            get;
            set;
        }

        /// <summary>
        /// 启用数据库支持
        /// </summary>
        bool EnableDataBase
        {
            get;
            set;
        }
        
        /// <summary>
        /// 启用数据校验
        /// </summary>
        bool EnableValidate
        {
            get;
            set;
        }

        /// <summary>
        /// 启用编辑接口
        /// </summary>
        bool EnableEditApi
        {
            get;
            set;
        }

        /// <summary>
        /// 启用用户界面
        /// </summary>
        bool EnableUI
        {
            get;
            set;
        }
        /*
        /// <summary>
        /// 页面配置
        /// </summary>
        PageConfig Page
        {
            get;
            set;
        }*/

        /// <summary>
        /// 数据表配置
        /// </summary>
        DataTableConfig DataTable
        {
            get;
            set;
        }

        #endregion

        #region 系统

        /// <summary>
        /// 类型
        /// </summary>
        string Type
        {
            get;
        }

        /// <summary>
        /// 对应实体
        /// </summary>
        EntityConfig Entity
        {
            get;
        }


        /// <summary>
        /// 最大字段标识号
        /// </summary>
        int MaxIdentity
        {
            get;
            set;
        }

        #endregion

        #region 数据标识

        /// <summary>
        /// 是否存在属性组合唯一值
        /// </summary>
        bool IsUniqueUnion { get; }// Properties.Count > 0 && Properties.Count(p {get;}// p.UniqueIndex) > 1;

        /// <summary>
        /// 主键字段
        /// </summary>
        IPropertyConfig PrimaryColumn { get; }// Find(p {get;}// Entity.PrimaryColumn == p.Field);

        /// <summary>
        /// 标题字段
        /// </summary>
        IPropertyConfig CaptionColumn { get; }// Find(p {get;}// Entity.PrimaryColumn == p.Field);

        /// <summary>
        /// 上级字段
        /// </summary>
        IPropertyConfig ParentColumn { get; }

        /// <summary>
        /// 是否有主键
        /// </summary>
        bool HasePrimaryKey { get; }// Entity.HasePrimaryKey;

        /// <summary>
        /// 主键字段
        /// </summary>
        string PrimaryField { get; }// PrimaryColumn?.Name;

        /// <summary>
        /// Redis唯一键模板
        /// </summary>
        string RedisKey
        {
            get;
            set;
        }
        #endregion

        #region 数据模型

        /// <summary>
        /// 实体名称
        /// </summary>
        string EntityName
        {
            get;
            set;
        }


        /// <summary>
        /// 参考类型
        /// </summary>
        string ReferenceType
        {
            get;
            set;
        }

        /// <summary>
        /// 模型
        /// </summary>
        string ModelInclude
        {
            get;
            set;
        }

        /// <summary>
        /// 基类
        /// </summary>
        string ModelBase
        {
            get;
            set;
        }
        /// <summary>
        /// 数据版本
        /// </summary>
        int DataVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 继承的接口集合
        /// </summary>
        string Interfaces
        {
            get;
            set;
        }
        #endregion

        #region 设计器支持

        /// <summary>
        ///     保存地址
        /// </summary>
        string SaveFileName
        {
            get;
        }

        /// <summary>
        /// 列序号起始值
        /// </summary>
        int ColumnIndexStart
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        string DisplayName { get; }// $"{Caption}({EntityName}:{ReadTableName}";

        /// <summary>
        /// 不同版本读数据的代码
        /// </summary>
        Dictionary<int, string> ReadCoreCodes
        {
            get;
            set;
        }
        /// <summary>
        /// 接口定义
        /// </summary>
        /// <remark>
        /// 作为系统的接口的定义
        /// </remark>
        bool IsInterface
        {
            get;
            set;
        }


        /// <summary>
        /// 上级
        /// </summary>
        ProjectConfig Project { get; }

        /// <summary>
        /// 简称
        /// </summary>
        string Abbreviation { get; }

        /// <summary>
        /// 分类
        /// </summary>
        string Classify { get; }

        #endregion

        #region 子级

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPropertyConfig Find(string name)
        {
            return Properties.FirstOrDefault(p => p.Name.IsMe(name) || name.IsMe(p.DataBaseField?.DbFieldName));
        }
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IPropertyConfig Find(Func<IPropertyConfig, bool> filter)
        {
            return Properties.FirstOrDefault(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPropertyConfig Find(params string[] names)
        {
            return Properties.FirstOrDefault(p => names.Exist(p.Name, p.Name, p.DataBaseField?.DbFieldName));
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPropertyConfig[] FindLastAndToArray(Func<IPropertyConfig, bool> filter)
        {
            return LastProperties.Where(filter).ToArray();
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<IPropertyConfig> WhereLast(Func<IPropertyConfig, bool> filter)
        {
            return LastProperties.Where(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPropertyConfig[] FindAndToArray(Func<IPropertyConfig, bool> filter)
        {
            return Properties.Where(filter).ToArray();
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<IPropertyConfig> Where(Func<IPropertyConfig, bool> filter)
        {
            return Properties.Where(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool TryGet(out IPropertyConfig field, params string[] names)
        {
            field = Properties.FirstOrDefault(p => names.Exist(p.Name, p.Name, p.DataBaseField?.DbFieldName));
            return field != null;
        }

        bool Exist(Func<IPropertyConfig, bool> filter)
        {
            return Properties.Any(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Exist(params string[] names)
        {
            return Properties.Any(p => names.Exist(p.Name, p.Name, p.DataBaseField?.DbFieldName));
        }

        IEnumerable<IPropertyConfig> Properties { get; }

        /// <summary>
        /// 最终有效的属性
        /// </summary>
        List<IPropertyConfig> LastProperties { get; set; }

        /// <summary>
        /// 公开的属性
        /// </summary>
        IEnumerable<IPropertyConfig> PublishProperty { get; }
        /// <summary>
        /// 用户属性
        /// </summary>
        IEnumerable<IPropertyConfig> UserProperty { get; }
        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        IEnumerable<IPropertyConfig> ClientProperty { get; }


        /// <summary>
        /// 命令集合
        /// </summary>
        ConfigCollection<UserCommandConfig> Commands { get; }

        #endregion

        #region 方法

        /// <summary>
        /// 页面代码路径
        /// </summary>
        string GetPagePath(char sp = '\\')
        {
            if (!PageFolder.IsMissing())
                return PageFolder;
            if (Project.NoClassify || Classify.IsMissing() || Classify.Equals("None", StringComparison.InvariantCulture))
                return Abbreviation;
            var cls = Project.Classifies.FirstOrDefault(p => p.Name == Classify);
            return $"{cls?.Abbreviation ?? Classify.ToLWord()}{sp}{Abbreviation}";
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        void Copy(IEntityConfig dest)
        {
            MaxIdentity = dest.MaxIdentity;
            RedisKey = dest.RedisKey;
            EntityName = dest.EntityName;
            ReferenceType = dest.ReferenceType;
            ModelInclude = dest.ModelInclude;
            ModelBase = dest.ModelBase;
            DataVersion = dest.DataVersion;
            EnableDataBase = dest.EnableDataBase;
            EnableEditApi = dest.EnableEditApi;
            EnableUI = dest.EnableUI;
            EnableEditApi = dest.EnableEditApi;
            Interfaces = dest.Interfaces;
            ColumnIndexStart = dest.ColumnIndexStart;
            ReadCoreCodes = dest.ReadCoreCodes;
            IsInterface = dest.IsInterface;
            EnableValidate = dest.EnableValidate;
            ApiName = dest.ApiName;

            IsUiReadOnly = dest.IsUiReadOnly;
            PageFolder = dest.PageFolder;
            TreeUi = dest.TreeUi;
            DetailsPage = dest.DetailsPage;
            FormCloumn = dest.FormCloumn;
            CppName = dest.CppName;
            /*
            EnableDataEvent = dest.EnableDataEvent;
            UpdateByModified = dest.UpdateByModified;
            IsQuery = dest.IsQuery;
            EnableDataEvent = dest.EnableDataEvent;*/
        }
        #endregion

    }
    /// <summary>
    /// 数据表配置
    /// </summary>
    public interface IDataTable
    {
        /// <summary>
        /// 存储表名
        /// </summary>/// <remarks>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remarks>
        string ReadTableName { get; }
        /// <summary>
        /// 存储表名
        /// </summary>
        string SaveTableName { get; }
        /// <summary>
        /// 数据库编号
        /// </summary>
        int DbIndex { get; }
        /// <summary>
        /// 按修改更新
        /// </summary>
        bool UpdateByModified { get; }
        /// <summary>
        /// 是否视图
        /// </summary>
        bool IsView { get; }
        /// <summary>
        /// 是否查询
        /// </summary>
        bool IsQuery { get; }

        /// <summary>
        /// 启用数据事件
        /// </summary>
        bool EnableDataEvent { get; }
    }

    public interface IEntityCpp
    {
        #region C++

        /// <summary>
        /// C++名称
        /// </summary>
        string CppName
        {
            get;
            set;
        }
        #endregion
    }

    public interface IEntityUi
    {
        #region 用户界面

        /// <summary>
        /// 接口名称
        /// </summary>
        string ApiName
        {
            get;
            set;
        }

        /// <summary>
        /// 界面只读
        /// </summary>
        bool IsUiReadOnly
        {
            get;
            set;
        }
        /// <summary>
        /// 页面组件名称
        /// </summary>
        string ComponentName
        {
            get;
            set;
        }

        /// <summary>
        /// 图标名称
        /// </summary>
        string Icon
        {
            get;
            set;
        }
        
        /// <summary>
        /// 页面路径
        /// </summary>
        string PagePath
        {
            get;
        }

        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        string PageFolder
        {
            get;
            set;
        }

        /// <summary>
        /// 默认排序字段
        /// </summary>
        string OrderField { get; }

        /// <summary>
        /// 默认反序
        /// </summary>
        bool OrderDesc { get; }

        /// <summary>
        /// 树形界面
        /// </summary>
        bool TreeUi
        {
            get;
            set;
        }

        /// <summary>
        /// 表单查询
        /// </summary>
        bool FormQuery
        {
            get;
            set;
        }

        /// <summary>
        /// 详细编辑页面
        /// </summary>
        bool DetailsPage
        {
            get;
            set;
        }

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        int FormCloumn
        {
            get;
            set;
        }


        #endregion 
    }

}