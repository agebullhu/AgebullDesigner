using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��Ŀ�����������ĳ�����
    /// </summary>
    public class ProjectCodeCommand : ConfigCommandBase<EntityConfig>
    {
        private readonly Func<ProjectBuilder> _creater;

        private ProjectBuilder _builder;

        public ProjectCodeCommand(Func<ProjectBuilder> creater)
        {
            _creater = creater;
            TargetType = typeof(EntityConfig);
        }
        bool noWriteFile;
        /// <summary>
        /// �ܷ�ִ�еļ��
        /// </summary>
        public bool CanDo(RuntimeArgument argument)
        {
            noWriteFile = string.IsNullOrWhiteSpace(SolutionConfig.Current.RootPath) ||
                          !Directory.Exists(SolutionConfig.Current.RootPath);
            if (noWriteFile)
            {
                StateMessage = "���������·�����ò���ȷ,�ѽ����ļ����ɣ�";
            }
            foreach (var project in argument.Projects)
            {
                if (!string.IsNullOrWhiteSpace(project.ModelPath))
                    continue;
                StateMessage = $"��Ŀ��{project}����·�����ò���ȷ,�ѽ����ļ����ɣ�";
            }
            return true;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool Validate(ProjectConfig project)
        {
            return _builder.Validate(project);
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Validate(EntityConfig entity)
        {
            return _builder.Validate(entity);
        }

        /// <summary>
        /// ִ����
        /// </summary>
        public bool ExecuteEntity(EntityConfig entity)
        {
            return Execute(entity);
        }

        /// <summary>
        /// ִ����
        /// </summary>
        public bool ExecuteProject(ProjectConfig project)
        {
            return Execute(project);
        }

        /// <summary>
        /// ִ����
        /// </summary>
        public bool Execute(EntityConfig entity)
        {
            StateMessage = "��������" + entity.Caption + "...";
            using (CodeGeneratorScope.CreateScope(entity))
            {
                _builder.CreateEntityCode(entity.Parent, entity);
            }
            StateMessage = entity.Caption + "�����";
            return true;
        }

        /// <summary>
        /// ִ����
        /// </summary>
        public bool Execute(ProjectConfig project)
        {
            StateMessage = "��������" + project.Caption + "...";
            using (CodeGeneratorScope.CreateScope(project))
            {
                _builder.CreateProjectCode(project);
            }
            StateMessage = project.Caption + "�����";
            return true;
        }

        public bool Prepare(RuntimeArgument argument)
        {
            _builder = _creater();
            _builder.MessageSetter = MessageSetter;
            return true;
        }
        public Action<Dictionary<string, string>> OnCodeSuccess;
        /// <summary>
        /// ���Ĵ������ɹ���
        /// </summary>
        public void OnSuccees()
        {
            OnCodeSuccess?.Invoke(_codes);
        }
        IDisposable codeScope;
        Dictionary<string, string> _codes;
        /// <summary>
        /// ����ǰ
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public bool BeginDo(RuntimeArgument argument)
        {
            codeScope = FileCodeScope.CreateScope(noWriteFile);
            return true;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public void EndDo(RuntimeArgument argument)
        {
            _codes = WorkContext.FileCodes;
            codeScope.Dispose();
        }

        #region ����


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
        /// ����ʹ�õ�
        /// </summary>
        /// <param name="args"></param>
        /// <param name="setArgs"></param>
        /// <returns></returns>
        private bool DoPrepare(object args, Action<object> setArgs)
        {
            var argument = new RuntimeArgument
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

        private bool Doing(object args)
        {
            using (CodeGeneratorScope.CreateScope(SolutionConfig.Current))
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

    #endregion
}