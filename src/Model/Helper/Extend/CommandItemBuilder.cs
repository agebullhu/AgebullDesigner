using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 表示一个命令生成器
    /// </summary>
    public class CommandItemBuilder : CommandConfig, ICommandItemBuilder
    {
        /// <summary>
        /// 命令
        /// </summary>
        public Action<object> Action { get; set; }

        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
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
    /// 表示一个迭代命令生成器
    /// </summary>
    public class CommandItemBuilder<TParameter> : CommandConfig, ICommandItemBuilder
        where TParameter : class
    {
        /// <summary>
        /// 构造
        /// </summary>
        public CommandItemBuilder()
        {
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }

        /// <summary>
        ///     代替类型
        /// </summary>
        public sealed override Type SuppertType => typeof(ConfigBase);

        /// <summary>
        /// 命令
        /// </summary>
        public Action<TParameter> Action { get; set; }

        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
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
    /// 表示一个迭代命令生成器
    /// </summary>
    public class CommandItemBuilder<TParameter, TResult> : CommandConfig, ICommandItemBuilder
    {
        /// <summary>
        /// 准备行为
        /// </summary>
        public Func<TParameter, bool> Prepare;
        /// <summary>
        /// 执行行为
        /// </summary>
        public Func<TParameter, TResult> Exceute;
        /// <summary>
        /// 结束行为
        /// </summary>
        public Action<CommandStatus, Exception, TResult> End;

        /// <summary>
        /// 构造
        /// </summary>
        public CommandItemBuilder()
        {
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }
        /// <summary>
        /// 准备行为
        /// </summary>
        public Func<TParameter> Prepare2;
        /// <summary>
        /// 构造
        /// </summary>
        public CommandItemBuilder(Func<TParameter, TResult> exceute)
        {
            Exceute = exceute;
            SignleSoruce = false;
            TargetType = typeof(TParameter);
        }
        /// <summary>
        /// 构造
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
        /// 构造
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
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
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