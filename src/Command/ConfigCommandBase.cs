using System;
using System.Collections;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 配置命令基类
    /// </summary>
    public abstract class ConfigCommandBase<TConfig> : CommandConfig, ICommandItemBuilder
        where TConfig : ConfigBase
    {
        protected ConfigCommandBase()
        {
            SignleSoruce = false;
            TargetType = typeof(TConfig);
        }

        /// <summary>
        /// 得到当前的消息跟踪器
        /// </summary>
        public Action<string> MessageSetter => msg => GlobalConfig.Global.StateMessage = msg;

        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
        CommandItemBase ICommandItemBuilder.ToCommand(object arg, Func<object, IEnumerator> enumerator)
        {
            var item= ToCommand(arg, enumerator);
            item.Source = arg;
            item.TargetType = TargetType;
            return item;
        }

        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
        public abstract CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator = null);
    }
}