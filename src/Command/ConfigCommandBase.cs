using System;
using System.Collections;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.Common.SimpleDesign
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