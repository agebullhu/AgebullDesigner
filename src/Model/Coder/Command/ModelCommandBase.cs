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
    /// ʵ���������
    /// </summary>
    public abstract class ModelCommandBase<TEntityConfig> : ConfigCommandBase<TEntityConfig>
        where TEntityConfig : ProjectChildConfigBase, IEntityConfig
    {
        protected ModelCommandBase()
        {
            TargetType = typeof(TEntityConfig);
        }

        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
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
        /// ��ǰ������Ϣ
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
        /// ִ����
        /// </summary>
        public abstract bool Execute(TEntityConfig entity);

        /// <summary>
        /// ִ����
        /// </summary>
        public abstract bool Execute(ProjectConfig project);

        /// <summary>
        /// �ܷ�ִ�еļ��
        /// </summary>
        public virtual bool Prepare(ModelArgument<TEntityConfig> argument)
        {
            return true;
        }

        /// <summary>
        /// �ܷ�ִ�еļ��
        /// </summary>
        public virtual bool CanDo(ModelArgument<TEntityConfig> argument)
        {
            return true;
        }

        /// <summary>
        /// ���Ĵ����ɹ���
        /// </summary>
        public virtual void OnSuccees()
        {

        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public virtual bool Validate(ProjectConfig project)
        {
            return true;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Validate(TEntityConfig entity)
        {
            return true;
        }
        /// <summary>
        /// ����ʹ�õ�
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
                MessageBox.Show("�д�������,����");
            }
            foreach (var entity in argument.Models)
            {
                if (Validate(entity))
                    continue;
                success = false;
                MessageBox.Show("�д�������,����");
            }
            if (success)
                setArgs(argument);
            return success;
        }

        /// <summary>
        /// ����ǰ
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public virtual bool BeginDo(ModelArgument<TEntityConfig> argument)
        {
            return true;
        }

        /// <summary>
        /// �����
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
                MessageBox.Show(Caption + "ִ�гɹ���");
                OnSuccees();
            }
            else
            {
                MessageBox.Show(Caption + "����" + ex?.Message);
                Trace.WriteLine(ex?.ToString(), Caption);
            }
        }
    }*/
}