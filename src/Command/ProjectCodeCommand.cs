using System;
using System.IO;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 项目代码命令对象的抽象类
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
                if (string.IsNullOrWhiteSpace(project.ModelPath) || !Directory.Exists(project.ModelPath))
                {
                    StateMessage = $"项目【{project}】的路径设置不正确！"; 
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
        /// 执行器
        /// </summary>
        public override bool Execute(EntityConfig entity)
        {
            StateMessage = "正在生成" + entity.Caption + "...";
            _builder.CreateEntityCode(entity.Parent, entity);
            StateMessage = entity.Caption + "已完成";
            return true;
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public override bool Execute(ProjectConfig project)
        {
            StateMessage = "正在生成" + project.Caption + "...";
            _builder.CreateProjectCode(project);
            StateMessage = project.Caption + "已完成";
            return true;
        }
    }
}