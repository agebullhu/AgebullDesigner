using System.Collections.Generic;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 枚举列表集合
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 主页面类型类型的列表
        /// </summary>
        public static List<ComboItem<bool>> BoolList = new List<ComboItem<bool>>
        {
            new ComboItem<bool>
            {
                name = "否",
                value= false
            },
            new ComboItem<bool>
            {
                name = "是",
                value= true
            }
        };

        /// <summary>
        /// 主页面类型类型的列表
        /// </summary>
        public static List<ComboItem<PanelType>> PanelTypeList = new List<ComboItem<PanelType>>
        {
            new ComboItem<PanelType>
            {
                name = "无",
                value= PanelType.None
            },
            new ComboItem<PanelType>
            {
                name = "普通的列表",
                value= PanelType.List
            },
            new ComboItem<PanelType>
            {
                name = "树的列表",
                value= PanelType.TreeList
            },
        };
        /// <summary>
        /// 字段使用范围类型类型的列表
        /// </summary>
        public static List<ComboItem<AccessScopeType>> AccessScopeTypeList = new List<ComboItem<AccessScopeType>>
        {
            new ComboItem<AccessScopeType>
            {
                name = "不阻止",
                value= AccessScopeType.None
            },
            new ComboItem<AccessScopeType>
            {
                name = "客户端",
                value= AccessScopeType.Client
            },
            new ComboItem<AccessScopeType>
            {
                name = "服务端",
                value= AccessScopeType.Server
            },
            new ComboItem<AccessScopeType>
            {
                name = "全阻止",
                value= AccessScopeType.All
            },
        };
        /// <summary>
        /// 存储场景类型类型的列表
        /// </summary>
        public static List<ComboItem<StorageScreenType>> StorageScreenTypeList = new List<ComboItem<StorageScreenType>>
        {
            new ComboItem<StorageScreenType>
            {
                name = "无",
                value= StorageScreenType.None
            },
            new ComboItem<StorageScreenType>
            {
                name = "插入",
                value= StorageScreenType.Insert
            },
            new ComboItem<StorageScreenType>
            {
                name = "更新",
                value= StorageScreenType.Update
            },
            new ComboItem<StorageScreenType>
            {
                name = "全部",
                value= StorageScreenType.All
            },
        };
        /// <summary>
        /// 数据库类型类型的列表
        /// </summary>
        public static List<ComboItem<DataBaseType>> DataBaseTypeList = new List<ComboItem<DataBaseType>>
        {
            new ComboItem<DataBaseType>
            {
                name = "SqlServer",
                value= DataBaseType.SqlServer
            },
            new ComboItem<DataBaseType>
            {
                name = "MySql",
                value= DataBaseType.MySql
            },
            new ComboItem<DataBaseType>
            {
                name = "Sqlite",
                value= DataBaseType.Sqlite
            }
        };
        /// <summary>
        /// 配置状态类型类型的列表
        /// </summary>
        public static List<ComboItem<ConfigStateType>> ConfigStateTypeList = new List<ComboItem<ConfigStateType>>
        {
            new ComboItem<ConfigStateType>
            {
                name = "无",
                value= ConfigStateType.None
            },
            new ComboItem<ConfigStateType>
            {
                name = "引用",
                value= ConfigStateType.Reference
            },
            new ComboItem<ConfigStateType>
            {
                name = "已删除",
                value= ConfigStateType.Delete
            },
            new ComboItem<ConfigStateType>
            {
                name = "已锁定",
                value= ConfigStateType.Freeze
            },
            new ComboItem<ConfigStateType>
            {
                name = "已废弃",
                value= ConfigStateType.Discard
            },
        };
        /// <summary>
        /// 解决方案类型类型的列表
        /// </summary>
        public static List<ComboItem<SolutionType>> SolutionTypeList = new List<ComboItem<SolutionType>>
        {
            new ComboItem<SolutionType>
            {
                name = "普通WEB项目",
                value= SolutionType.Web
            },
            new ComboItem<SolutionType>
            {
                name = "C++项目",
                value= SolutionType.Cpp
            },
        };
    }
}