/*design by:agebull designer date:2017/7/12 23:16:38*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置
    /// </summary>
    public interface IEntityConfig : IConfig
    {
        #region 系统

        /// <summary>
        /// 阻止使用的范围
        /// </summary>
        AccessScopeType DenyScope
        {
            get;
            set;
        }
        /// <summary>
        /// 阻止使用的范围
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
        bool IsUniqueUnion { get; }// Properties.Count > 0 && Properties.Count(p {get;}// p.UniqueIndex > 0) > 1;

        /// <summary>
        /// 主键字段
        /// </summary>
        IFieldConfig PrimaryColumn { get; }// Properties.FirstOrDefault(p {get;}// Entity.PrimaryColumn == p.Field);

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
        /// 是否查询
        /// </summary>
        bool IsQuery
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
        /// 内部数据
        /// </summary>
        /// <remark>
        /// 服务器内部数据,即只在服务器内部使用
        /// </remark>
        bool IsInternal
        {
            get;
            set;
        }
        /// <summary>
        /// 无数据库支持
        /// </summary>
        bool NoDataBase
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
        /// 接口是否为显示实现
        /// </summary>
        bool InterfaceInner
        {
            get;
            set;
        }

        /// <summary>
        /// 生成校验代码
        /// </summary>
        bool HaseValidateCode
        {
            get;
            set;
        }
        #endregion

        #region 子级
        IEnumerable<IFieldConfig> Properties { get; }

        /// <summary>
        /// 最终有效的属性
        /// </summary>
        List<IFieldConfig> LastProperties { get; }

        /// <summary>
        /// 公开的属性
        /// </summary>
        IEnumerable<IFieldConfig> PublishProperty { get; }
        /// <summary>
        /// 用户属性
        /// </summary>
        IEnumerable<IFieldConfig> UserProperty { get; }
        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        IEnumerable<IFieldConfig> ClientProperty { get; }
        /// <summary>
        /// 数据库字段
        /// </summary>
        IEnumerable<IFieldConfig> DbFields { get; }


        #endregion

        #region 数据库

        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>
        /// <remark>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remark>
        string ReadTableName
        {
            get;
        }
        /// <summary>
        /// 存储表名
        /// </summary>
        string SaveTableName
        {
            get;
        }
        /// <summary>
        /// 数据库编号
        /// </summary>
        int DbIndex
        {
            get;
        }
        /// <summary>
        /// 按修改更新
        /// </summary>
        bool UpdateByModified
        {
            get;
            set;
        }
        #endregion

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
        /// 是否有界面
        /// </summary>
        bool HaseEasyUi
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
        /// 页面文件夹名称
        /// </summary>
        string PageFolder
        {
            get;
            set;
        }

        /// <summary>
        /// 上级
        /// </summary>
        ProjectConfig Parent { get; }

        /// <summary>
        /// 简称
        /// </summary>
        string Abbreviation { get; }

        /// <summary>
        /// 分类
        /// </summary>
        string Classify { get; }

        /// <summary>
        /// 页面代码路径
        /// </summary>
        string PagePath(char sp = '\\')
        {
            if (!string.IsNullOrWhiteSpace(PageFolder))
                return PageFolder;
            if (Parent.NoClassify || string.IsNullOrWhiteSpace(Classify) || Classify.Equals("None", StringComparison.InvariantCulture))
                return Abbreviation ?? Name;
            var cls = Parent.Classifies.FirstOrDefault(p => p.Name == Classify);
            return cls == null
? $"{cls.Abbreviation ?? Classify}{sp}{Name}"
: $"{Classify.ToLWord()}{sp}{Name}";
        }

        /// <summary>
        /// 树形界面
        /// </summary>
        bool TreeUi
        {
            get;
            set;
        }

        /// <summary>
        /// 编辑页面最大化
        /// </summary>
        bool MaxForm
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

        /// <summary>
        /// 列表详细页
        /// </summary>
        bool ListDetails
        {
            get;
            set;
        }

        /// <summary>
        /// 主键正序
        /// </summary>
        bool NoSort
        {
            get;
            set;
        }

        /// <summary>
        /// 主页面类型
        /// </summary>
        PanelType PanelType
        {
            get;
            set;
        }

        #endregion 

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
}