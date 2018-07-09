using System;
using System.Collections.Generic;
using System.IO;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��Ŀ�����������ĳ�����
    /// </summary>
    public class ProjectCodeCommand : EntityCommandBase
    {
        private readonly Func<ProjectBuilder> _creater;

        private ProjectBuilder _builder;

        public ProjectCodeCommand(Func<ProjectBuilder> creater)
        {
            _creater = creater;
        }
        bool noWriteFile;
        /// <summary>
        /// �ܷ�ִ�еļ��
        /// </summary>
        public override bool CanDo(RuntimeArgument argument)
        {
            if (string.IsNullOrWhiteSpace(SolutionConfig.Current.RootPath) || !Directory.Exists(SolutionConfig.Current.RootPath))
            {
                noWriteFile = true;
                StateMessage = "���������·�����ò���ȷ,�ѽ����ļ����ɣ�";
            }
            foreach (var project in argument.Projects)
            {
                if (string.IsNullOrWhiteSpace(project.ModelPath))
                {
                    noWriteFile = true;
                    StateMessage = $"��Ŀ��{project}����·�����ò���ȷ,�ѽ����ļ����ɣ�"; 
                }
            }
            return true;
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Validate(EntityConfig entity)
        {
            return _builder.Validate(entity.Parent, entity);
        }
        

        /// <summary>
        /// ִ����
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = "��������" + entity.Caption + "...";
            _builder.CreateEntityCode(entity.Parent, entity);
            StateMessage = entity.Caption + "�����";
            return true;
        }

        /// <summary>
        /// ִ����
        /// </summary>
        public override bool Execute(ProjectConfig project)
        {
            StateMessage = "��������" + project.Caption + "...";
            _builder.CreateProjectCode(project);
            StateMessage = project.Caption + "�����";
            return true;
        }

        public override void Prepare(RuntimeArgument argument)
        {
            _builder = _creater();
            _builder.MessageSetter = MessageSetter;
        }
        public Action<Dictionary<string, string>> OnCodeSuccess;
        /// <summary>
        /// ���Ĵ����ɹ���
        /// </summary>
        public override void OnSuccees()
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
        public override bool BeginDo(RuntimeArgument argument)
        {
            codeScope = FileCodeScope.CreateScope(noWriteFile);
            return true;
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public override void EndDo(RuntimeArgument argument)
        {
            _codes = WorkContext.FileCodes;
            codeScope.Dispose();
        }
    }
}