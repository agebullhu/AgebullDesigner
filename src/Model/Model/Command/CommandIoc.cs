using Agebull.EntityModel.Config;
using System;

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
        public static Func<EntityConfig, bool> EditEntityCommand;
        /// <summary>
        /// 新增多个字段的方法(UI后期实现)
        /// </summary>
        public static Func<EntityConfig, bool> AddFieldsCommand;

        /// <summary>
        /// 生成新配置
        /// </summary>
        /// <returns></returns>
        public static Func<string, ConfigBase, bool> NewConfigCommand;

    }

}
