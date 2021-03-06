﻿using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<ProjectConfig, EntityListPanel>("实体列表");
            DesignerManager.Registe<ProjectConfig, EnumListPanel>("枚举列表");
            DesignerManager.Registe<ProjectConfig, ApiListPanel>("接口列表");
        }
    }
}