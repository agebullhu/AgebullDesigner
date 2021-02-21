using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections;

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
        CommandItemBase ICommandItemBuilder.ToCommand(string key, object arg, Func<object, IEnumerator> enumerator)
        {
            Key = key;
            var item = ToCommand(arg, enumerator);
            item.Key = key;
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