using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public class CommandItemBuilder : CommandConfig, ICommandItemBuilder
    {
        /// <summary>
        /// ����
        /// </summary>
        public Action<object> Action { get; set; }

        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        public CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator)
        {
            var item = new CommandItem
            {
                Source = arg,
                Action = Action
            };
            item.CopyFrom(this);
            return item;
        }
    }

    /// <summary>
    /// ��ʾһ����������������
    /// </summary>
    public class CommandItemBuilder<TParameter> : CommandConfig, ICommandItemBuilder
        where TParameter : class
    {
        /// <summary>
        /// ����
        /// </summary>
        public CommandItemBuilder()
        {
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }

        /// <summary>
        ///     ��������
        /// </summary>
        public sealed override Type SuppertType => typeof(ConfigBase);

        /// <summary>
        /// ����
        /// </summary>
        public Action<TParameter> Action { get; set; }

        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        public CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator)
        {
            var item = new IteratorCommandItem<TParameter>
            {
                Source = arg,
                Action = Action
            };
            item.CopyFrom(this);
            return item;
        }
    }


    /// <summary>
    /// ��ʾһ����������������
    /// </summary>
    public class CommandItemBuilder<TParameter, TResult> : CommandConfig, ICommandItemBuilder
    {
        /// <summary>
        /// ׼����Ϊ
        /// </summary>
        public Func<TParameter, bool> Prepare;
        /// <summary>
        /// ִ����Ϊ
        /// </summary>
        public Func<TParameter, TResult> Exceute;
        /// <summary>
        /// ������Ϊ
        /// </summary>
        public Action<CommandStatus, Exception, TResult> End;

        /// <summary>
        /// ����
        /// </summary>
        public CommandItemBuilder()
        {
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }
        /// <summary>
        /// ׼����Ϊ
        /// </summary>
        public Func<TParameter> Prepare2;
        /// <summary>
        /// ����
        /// </summary>
        public CommandItemBuilder(Func<TParameter, TResult> exceute)
        {
            Exceute = exceute;
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }
        /// <summary>
        /// ����
        /// </summary>
        public CommandItemBuilder(Func<TParameter, bool> prepare, Func<TParameter, TResult> exceute, Action<CommandStatus, Exception, TResult> end)
        {
            Prepare = prepare;
            Exceute = exceute;
            End = end;
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }
        /// <summary>
        /// ����
        /// </summary>
        public CommandItemBuilder(Func<TParameter> prepare, Func<TParameter, TResult> exceute, Action<CommandStatus, Exception, TResult> end)
        {
            Prepare2 = prepare;
            Exceute = exceute;
            End = end;
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }
        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        public CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator)
        {
            var item = new AsyncCommandItem<TParameter, TResult>(Prepare, Exceute, End)
            {
                Prepare2 = Prepare2
            };
            item.CopyFrom(this);
            return item;
        }
    }


}