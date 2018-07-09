using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ʵ���������
    /// </summary>
    public abstract class EntityCommandBase : ConfigCommandBase<EntityConfig>
    {
        /// <summary>
        /// תΪ�������
        /// </summary>
        /// <returns>�������</returns>
        public override CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator = null)
        {
            var item = this.CopyCreate<CommandItem>();
            item.Parameter = arg;
            item.Command = new AsyncCommand<object, bool>(Prepare, Doing, End);
            item.Image = Application.Current.Resources[IconName ?? "imgDefault"] as ImageSource;
            return item;
        }

        public class RuntimeArgument
        {
            /// <summary>
            /// ����
            /// </summary>
            public object Argument
            {
                get => _argument;
                set
                {
                    _argument = value;
                    GetEntities();
                }
            }
            
            private object _argument;

            /// <summary>
            /// ��ǰʵ�����
            /// </summary>
            public IList<EntityConfig> Entities { get; private set; }

            /// <summary>
            /// ��ǰ������Ŀ
            /// </summary>
            public List<ProjectConfig> Projects { get; private set; }

            /// <summary>
            /// Ĭ�ϵ�ȡ��ǰʵ��ķ��� 
            /// </summary>
            /// <returns></returns>
            void GetEntities()
            {
                var list = new List<EntityConfig>();
                if (_argument is EntityConfig entityConfig)
                {
                    list.Add(entityConfig);
                }
                else
                {
                    if (_argument is PropertyConfig propertyConfig)
                    {
                        list.Add(propertyConfig.Parent);
                    }
                    else
                    {
                        list.AddRange(_argument is ProjectConfig projectConfig 
                            ? projectConfig.Entities 
                            : SolutionConfig.Current.Entities);
                    }
                }

                Projects = new List<ProjectConfig>();
                foreach (var entity in list)
                {
                    var project = entity.Parent;
                    if (project == null)
                        continue;
                    if (!Projects.Contains(project))
                        Projects.Add(project);
                }
                Entities = list;
            }
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
        public abstract bool Execute(EntityConfig entity);

        /// <summary>
        /// ִ����
        /// </summary>
        public abstract bool Execute(ProjectConfig project);

        /// <summary>
        /// �ܷ�ִ�еļ��
        /// </summary>
        public virtual void Prepare(RuntimeArgument argument)
        {

        }

        /// <summary>
        /// �ܷ�ִ�еļ��
        /// </summary>
        public virtual bool CanDo(RuntimeArgument argument)
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
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Validate(EntityConfig entity)
        {
            return true;
        }
        /// <summary>
        /// ����ʹ�õ�
        /// </summary>
        /// <param name="args"></param>
        /// <param name="setArgs"></param>
        /// <returns></returns>
        private bool Prepare(object args, Action<object> setArgs)
        {
            var argument = new RuntimeArgument
            {
                Argument = args ?? GlobalConfig.CurrentConfig
            };
            if (!CanDo(argument))
                return false;
            Prepare(argument);
            bool success = true;
            foreach (var entity in argument.Entities)
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
        public virtual bool BeginDo(RuntimeArgument argument)
        {
            return true;
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public virtual void EndDo(RuntimeArgument argument)
        {
        }
        private bool Doing(object args)
        {
            var argument = (RuntimeArgument)args;
            if (!BeginDo(argument))
                return false;
            try
            {
                foreach (var entity in argument.Entities)
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
    }
}