using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// �����������
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
        /// �õ���ǰ����Ϣ������
        /// </summary>
        public Action<string> MessageSetter => msg => GlobalConfig.Global.StateMessage = msg;

        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
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
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        public abstract CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator = null);
    }
}