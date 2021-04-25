using Agebull.Common.Base;
using System;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 对象工作范围
    /// </summary>
    public class ObjectWrokModelScope : ScopeBase
    {
        readonly WorkModel oldModel;
        readonly object worker;
        ObjectWrokModelScope(object obj, WorkModel model)
        {
            worker = obj;
            oldModel = WorkContext.WorkModel;
            WorkContext._workModel = model;
            GlobalTrigger.Regularize(obj);
        }

        /// <summary>
        /// 生成范围
        /// </summary>
        /// <returns></returns>
        public static IDisposable CreateScope(object worker, WorkModel model)
        {
            return new ObjectWrokModelScope(worker, model);
        }

        /// <inheritdoc />
        protected override void OnDispose()
        {
            WorkContext._workModel = oldModel;
        }
    }
}