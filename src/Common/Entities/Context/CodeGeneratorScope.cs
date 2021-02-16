using Agebull.Common.Base;
using System;

namespace Agebull.EntityModel
{
    /// <summary>
    /// �������ɻ���
    /// </summary>
    public class CodeGeneratorScope : ScopeBase
    {
        readonly IDisposable _innerScope;
        CodeGeneratorScope(object config)
        {
            _innerScope = WorkModelScope.CreateScope(WorkModel.Coder);
            if (config == null)
                return;
            GlobalTrigger.OnCodeGeneratorBegin(config);
        }
        /// <summary>
        /// ���ɷ�Χ
        /// </summary>
        /// <returns></returns>
        public static IDisposable CreateScope(object config)
        {
            return new CodeGeneratorScope(config);
        }

        /// <inheritdoc />
        protected override void OnDispose()
        {
            if (_innerScope == null)
                return;
            _innerScope.Dispose();
            GlobalTrigger.OnCodeGeneratorEnd();
        }
    }
}