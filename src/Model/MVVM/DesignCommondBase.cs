using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    public abstract class DesignCommondBase : MvvmBase, IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            var commands = CreateCommands();
            foreach (var command in commands)
            {
                if (command.Catalog == null)
                    command.Catalog = Catalog;
                CommandCoefficient.RegisterCommand(command);
            }
        }

        /// <summary>
        ///     ����
        /// </summary>
        public virtual string Catalog { get; set; }

        #region ��������

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        private List<ICommandItemBuilder> CreateCommands()
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
        public override ISynchronousContext Synchronous => DataModelDesignModel.Current.Synchronous;
        /// <inheritdoc />
        /// <summary>
        /// �̵߳�����
        /// </summary>
        public override Dispatcher Dispatcher => DataModelDesignModel.Current.Dispatcher;

        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel Model => DataModelDesignModel.Current;
        /// <summary>
        /// ������
        /// </summary>
        public DesignContext Context => DataModelDesignModel.Current.Context;

        #endregion

    }


    public abstract class DesignCommondBase<TConfig> : MvvmBase, IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            var commands = CreateCommands();
            foreach (var command in commands)
            {
                if (command.Catalog == null)
                    command.Catalog = Catalog;
                if (command.TargetType == null)
                    command.TargetType = SourceType;
                CommandCoefficient.RegisterCommand(command);
            }
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
        private List<ICommandItemBuilder> CreateCommands()
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
        public override ISynchronousContext Synchronous => DataModelDesignModel.Current.Synchronous;
        /// <inheritdoc />
        /// <summary>
        /// �̵߳�����
        /// </summary>
        public override Dispatcher Dispatcher => DataModelDesignModel.Current.Dispatcher;

        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel Model => DataModelDesignModel.Current;
        /// <summary>
        /// ������
        /// </summary>
        public DesignContext Context => DataModelDesignModel.Current.Context;

        #endregion

    }
}