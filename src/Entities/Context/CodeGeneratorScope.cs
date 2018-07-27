using System;
using Agebull.Common.Base;

namespace Agebull.EntityModel
{
    /// <summary>
    /// �������ɻ���
    /// </summary>
    public class CodeGeneratorScope : ScopeBase
    {
        readonly IDisposable _innerScope;
        CodeGeneratorScope()
        {
            if (WorkContext.WorkModel == WorkModel.Coder)
                return;
            _innerScope = WorkModelScope.CreateScope(WorkModel.Coder);
            GlobalTrigger.OnCodeGeneratorBegin();
        }
        /// <summary>
        /// ���ɷ�Χ
        /// </summary>
        /// <returns></returns>
        public static IDisposable CreateScope()
        {
            return new CodeGeneratorScope();
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