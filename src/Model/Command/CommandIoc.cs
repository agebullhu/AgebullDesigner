using System;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 命令注入器
    /// </summary>
    public static class CommandIoc
    {
        /// <summary>
        /// 新增实体的方法(UI后期实现)
        /// </summary>
        public static Func<EntityConfig> NewEntityCommand;
        /// <summary>
        /// 新增多个字段的方法(UI后期实现)
        /// </summary>
        public static Func<EntityConfig> AddFieldsCommand;

        /// <summary>
        /// 枚举编辑的方法(UI后期实现)
        /// </summary>
        /// <returns></returns>
        public static Func<PropertyConfig, EnumConfig> EditPropertyEnumCommand;

        /// <summary>
        /// 枚举编辑的方法(UI后期实现)
        /// </summary>
        /// <returns></returns>
        public static Action<EnumConfig> EditEnumCommand;

        /// <summary>
        /// 生成新配置
        /// </summary>
        /// <returns></returns>
        public static Func<ConfigBase,bool> NewConfigCommand;
        
    }

}
