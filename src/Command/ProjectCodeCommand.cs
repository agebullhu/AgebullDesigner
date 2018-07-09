using System;
using System.Collections.Generic;
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

        private ProjectBuilder _builder;

        public ProjectCodeCommand(Func<ProjectBuilder> creater)
        {
            _creater = creater;
        }
        bool noWriteFile;
        /// <summary>
        /// 能否执行的检查
        /// </summary>
        public override bool CanDo(RuntimeArgument argument)
        {
            if (string.IsNullOrWhiteSpace(SolutionConfig.Current.RootPath) || !Directory.Exists(SolutionConfig.Current.RootPath))
            {
                noWriteFile = true;
                StateMessage = "解决方案根路径设置不正确,已禁用文件生成！";
            }
            foreach (var project in argument.Projects)
            {
                if (string.IsNullOrWhiteSpace(project.ModelPath))
                {
                    noWriteFile = true;
                    StateMessage = $"项目【{project}】的路径设置不正确,已禁用文件生成！"; 
                }
            }
            return true;
        }


        /// <summary>
        /// 单个检查
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        public override void Prepare(RuntimeArgument argument)
        {
            _builder = _creater();
            _builder.MessageSetter = MessageSetter;
        }
        public Action<Dictionary<string, string>> OnCodeSuccess;
        /// <summary>
        /// 最后的处理（成功）
        /// </summary>
        public override void OnSuccees()
        {
            OnCodeSuccess?.Invoke(_codes);
        }
        IDisposable codeScope;
        Dictionary<string, string> _codes;
        /// <summary>
        /// 处理前
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public override bool BeginDo(RuntimeArgument argument)
        {
            codeScope = FileCodeScope.CreateScope(noWriteFile);
            return true;
        }

        /// <summary>
        /// 处理后
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