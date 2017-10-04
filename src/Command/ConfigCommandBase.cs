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
            Signle = false;
            SourceType = typeof(TConfig).FullName;
        }
        /// <summary>
        /// �õ���ǰ����Ϣ������
        /// </summary>
        public Action<string> MessageSetter { get; set; } = msg => GlobalConfig.Global.StateMessage = msg;

        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        public abstract CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator = null);
    }
}