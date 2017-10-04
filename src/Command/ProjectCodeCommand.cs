using System;
using System.IO;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    /// <summary>
    /// ��Ŀ�����������ĳ�����
    /// </summary>
    public class ProjectCodeCommand : EntityCommandBase
    {
        private readonly Func<ProjectBuilder> _creater;
        ProjectBuilder _builder;
        public ProjectCodeCommand(Func<ProjectBuilder> creater)
        {
            _creater = creater;
        }

        public override bool CanDo(RuntimeArgument argument)
        {
            foreach (var project in argument.Projects)
            {
                if (string.IsNullOrWhiteSpace(project.BusinessPath) || !Directory.Exists(project.BusinessPath))
                {
                    StateMessage = $"��Ŀ��{project}����·�����ò���ȷ��"; 
                    return false;
                }
            }
            return true;
        }

        public override void Prepare(RuntimeArgument argument)
        {
            _builder = _creater();
            _builder.MessageSetter = MessageSetter;
        }

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
    }
}