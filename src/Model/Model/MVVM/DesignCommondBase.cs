using Agebull.Common.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Agebull.EntityModel.Designer
{

    public abstract class DesignCommondBase<TConfig> : MvvmBase, IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DoRegist();
            CommandCoefficient.RegisterCommand<TConfig>(CreateCommandsBuilder);
        }


        /// <summary>
        /// 注册代码
        /// </summary>
        protected virtual void DoRegist()
        {
        }

        /// <summary>
        ///     分类
        /// </summary>
        public virtual string Catalog { get; set; }

        /// <summary>
        ///     目标类型
        /// </summary>
        public virtual Type SourceType => typeof(TConfig);

        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ICommandItemBuilder> CreateCommandsBuilder()
        {
            var commands = new List<ICommandItemBuilder>();
            CreateCommands(commands);
            return commands;
        }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected abstract void CreateCommands(List<ICommandItemBuilder> commands);


        #endregion

        #region 基本设置

        /// <summary>
        /// 同步上下文
        /// </summary>
        public override ISynchronousContext Synchronous => DataModelDesignModel.Current?.Synchronous;
        /// <inheritdoc />
        /// <summary>
        /// 线程调度器
        /// </summary>
        public override Dispatcher Dispatcher => DataModelDesignModel.Current?.Dispatcher;

        /// <summary>
        /// 基本模型
        /// </summary>
        public DataModelDesignModel Model => DataModelDesignModel.Current;
        /// <summary>
        /// 上下文
        /// </summary>
        public DesignContext Context => DataModelDesignModel.Current?.Context;

        #endregion

    }
}