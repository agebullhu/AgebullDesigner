using System;
using System.Collections;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

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
        CommandItemBase ICommandItemBuilder.ToCommand(object arg, Func<object, IEnumerator> enumerator)
        {
            var item= ToCommand(arg, enumerator);
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