using Agebull.Common.Base;
using System;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 工作模式范围
    /// </summary>
    public class WorkModelScope : ScopeBase
    {
        readonly WorkModel oldModel;
        WorkModelScope(WorkModel model)
        {
            oldModel = WorkContext.WorkModel;
            WorkContext._workModel = model;
        }

        /// <summary>
        /// 生成范围
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IDisposable CreateScope(WorkModel model)
        {
            return new WorkModelScope(model);
        }

        /// <inheritdoc />
        protected override void OnDispose()
        {
            WorkContext._workModel = oldModel;
        }

    }

    /// <summary>
    /// 修复范围
    /// </summary>
    public class RepairScope : ScopeBase
    {
        readonly WorkModel oldModel;
        RepairScope()
        {
            oldModel = WorkContext.WorkModel;
            WorkContext._workModel = WorkModel.Repair;
        }

        /// <summary>
        /// 生成范围
        /// </summary>
        /// <returns></returns>
        public static IDisposable CreateScope(object cfg)
        {
            var scope =  new RepairScope();
            GlobalTrigger.Regularize(cfg);
            return scope;
        }

        /// <inheritdoc />
        protected override void OnDispose()
        {
            WorkContext._workModel = oldModel;
        }

    }
}