using Agebull.Common.Base;
using Agebull.EntityModel.Config;
using System;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 代码生成基类
    /// </summary>
    public class CodeGeneratorScope : ScopeBase
    {
        readonly IDisposable _innerScope;
        readonly ConfigBase _config;
        readonly bool _includeChild;
        CodeGeneratorScope(ConfigBase config,bool includeChild)
        {
            _includeChild = includeChild;
            _config = config;
            _innerScope = WorkModelScope.CreateScope(WorkModel.Coder);
            if (config == null)
                return;
            if (includeChild)
            {
                config.Postorder<object>(GlobalTrigger.OnCodeGeneratorBegin);
            }
            else
            {
                GlobalTrigger.OnCodeGeneratorBegin(config);
            }
        }

        /// <summary>
        /// 生成范围
        /// </summary>
        /// <returns></returns>
        public static IDisposable CreateScope(object config, bool includeChild)
        {
            return new CodeGeneratorScope((ConfigBase)config, includeChild);
        }

        /// <inheritdoc />
        protected override void OnDispose()
        {
            if (_innerScope == null)
                return;
            _innerScope.Dispose();
            if (_includeChild)
            {
                _config.Postorder<object>(GlobalTrigger.OnCodeGeneratorEnd);
            }
            else
            {
                GlobalTrigger.OnCodeGeneratorEnd(_config);
            }
        }
    }
}