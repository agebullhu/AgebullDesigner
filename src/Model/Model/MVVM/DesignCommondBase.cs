using Agebull.Common.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Agebull.EntityModel.Designer
{

    public abstract class DesignCommondBase<TConfig> : MvvmBase, IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DoRegist();
            CommandCoefficient.RegisterCommand<TConfig>(CreateCommandsBuilder);
        }


        /// <summary>
        /// ע�����
        /// </summary>
        protected virtual void DoRegist()
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        public virtual string Catalog { get; set; }

        /// <summary>
        ///     Ŀ������
        /// </summary>
        public virtual Type SourceType => typeof(TConfig);

        #region ��������

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ICommandItemBuilder> CreateCommandsBuilder()
        {
            var commands = new List<ICommandItemBuilder>();
            CreateCommands(commands);
            return commands;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="commands"></param>
        protected abstract void CreateCommands(List<ICommandItemBuilder> commands);


        #endregion

        #region ��������

        /// <summary>
        /// ͬ��������
        /// </summary>
        public override ISynchronousContext Synchronous => DataModelDesignModel.Current?.Synchronous;
        /// <inheritdoc />
        /// <summary>
        /// �̵߳�����
        /// </summary>
        public override Dispatcher Dispatcher => DataModelDesignModel.Current?.Dispatcher;

        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel Model => DataModelDesignModel.Current;
        /// <summary>
        /// ������
        /// </summary>
        public DesignContext Context => DataModelDesignModel.Current?.Context;

        #endregion

    }
}