using System;
using System.Collections;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// 配置命令基类
    /// </summary>
    public abstract class ConfigCommandBase<TConfig> : CommandConfig, ICommandItemBuilder
        where TConfig : ConfigBase
    {
        protected ConfigCommandBase()
        {
            Signle = false;
            SourceType = typeof(TConfig).FullName;
        }
        /// <summary>
        /// 得到当前的消息跟踪器
        /// </summary>
        public Action<string> MessageSetter { get; set; } = msg => GlobalConfig.Global.StateMessage = msg;

        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
        public abstract CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator = null);
    }
}