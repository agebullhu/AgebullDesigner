using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /*// <summary>
    /// 实体命令基类
    /// </summary>
    public abstract class ModelCommandBase<TEntityConfig> : ConfigCommandBase<TEntityConfig>
        where TEntityConfig : ProjectChildConfigBase, IEntityConfig
    {
        protected ModelCommandBase()
        {
            TargetType = typeof(TEntityConfig);
        }

        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
        public override CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator = null)
        {
            var item = new AsyncCommandItem<object, bool>(DoPrepare, Doing, End)
            {
                Source = arg,
                Image = Application.Current.Resources[IconName ?? "imgDefault"] as ImageSource
            };
            item.CopyFrom(this);
            return item;
        }


        /// <summary>
        /// 当前跟踪消息
        /// </summary>
        public string StateMessage
        {
            set
            {
                if (MessageSetter == null)
                    Trace.WriteLine(value);
                else
                    MessageSetter.Invoke(value);
            }
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public abstract bool Execute(TEntityConfig entity);

        /// <summary>
        /// 执行器
        /// </summary>
        public abstract bool Execute(ProjectConfig project);

        /// <summary>
        /// 能否执行的检查
        /// </summary>
        public virtual bool Prepare(ModelArgument<TEntityConfig> argument)
        {
            return true;
        }

        /// <summary>
        /// 能否执行的检查
        /// </summary>
        public virtual bool CanDo(ModelArgument<TEntityConfig> argument)
        {
            return true;
        }

        /// <summary>
        /// 最后的处理（成功）
        /// </summary>
        public virtual void OnSuccees()
        {

        }

        /// <summary>
        /// 单个检查
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public virtual bool Validate(ProjectConfig project)
        {
            return true;
        }
        /// <summary>
        /// 单个检查
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Validate(TEntityConfig entity)
        {
            return true;
        }
        /// <summary>
        /// 命令使用的
        /// </summary>
        /// <param name="args"></param>
        /// <param name="setArgs"></param>
        /// <returns></returns>
        private bool DoPrepare(object args, Action<object> setArgs)
        {
            var argument = new ModelArgument<TEntityConfig>
            {
                Argument = args ?? GlobalConfig.CurrentConfig
            };
            if (!CanDo(argument))
                return false;
            Prepare(argument);
            bool success = true;
            foreach (var project in argument.Projects)
            {
                if (Validate(project))
                    continue;
                success = false;
                MessageBox.Show("有错误配置,请检查");
            }
            foreach (var entity in argument.Models)
            {
                if (Validate(entity))
                    continue;
                success = false;
                MessageBox.Show("有错误配置,请检查");
            }
            if (success)
                setArgs(argument);
            return success;
        }

        /// <summary>
        /// 处理前
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public virtual bool BeginDo(ModelArgument<TEntityConfig> argument)
        {
            return true;
        }

        /// <summary>
        /// 处理后
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public virtual void EndDo(ModelArgument<TEntityConfig> argument)
        {
        }
        private bool Doing(object args)
        {
            if (!(args is ModelArgument<TEntityConfig> argument) ||  !BeginDo(argument))
                return false;
            try
            {
                foreach (var entity in argument.Models)
                {
                    Execute(entity);
                }
                foreach (var project in argument.Projects)
                {
                    Execute(project);
                }

                return true;
            }
            finally
            {
                EndDo(argument);
            }

        }

        protected void End(CommandStatus status, Exception ex, bool result)
        {
            if (status == CommandStatus.Succeed)
            {
                MessageBox.Show(Caption + "执行成功！");
                OnSuccees();
            }
            else
            {
                MessageBox.Show(Caption + "出错" + ex?.Message);
                Trace.WriteLine(ex?.ToString(), Caption);
            }
        }
    }*/
}