using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 项目代码命令对象的抽象类
    /// </summary>
    public class ProjectCodeCommand : ConfigCommandBase<ProjectChildConfigBase>
    {
        private readonly Func<ProjectBuilder> _creater;

        private ProjectBuilder _builder;

        public ProjectCodeCommand(Func<ProjectBuilder> creater)
        {
            _creater = creater;
            TargetType = typeof(IEntityConfig);
        }

        bool noWriteFile;

        /// <summary>
        /// 能否执行的检查
        /// </summary>
        public bool CanDo(ModelArgument argument)
        {
            noWriteFile = string.IsNullOrWhiteSpace(SolutionConfig.Current.RootPath) ||
                          !Directory.Exists(SolutionConfig.Current.RootPath);
            if (noWriteFile)
            {
                StateMessage = "解决方案根路径设置不正确,已禁用文件生成！";
            }
            foreach (var project in argument.Projects)
            {
                if (!string.IsNullOrWhiteSpace(project.ModelPath))
                    continue;
                StateMessage = $"项目【{project}】的路径设置不正确,已禁用文件生成！";
            }
            return true;
        }


        /// <summary>
        /// 单个检查
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool Validate(ProjectConfig project)
        {
            return _builder.Validate(project);
        }

        /// <summary>
        /// 单个检查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Validate(IEntityConfig model)
        {
            return _builder.Validate(model);
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public bool ExecuteEntity(IEntityConfig model)
        {
            return Execute(model);
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public bool ExecuteProject(ProjectConfig project)
        {
            return Execute(project);
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public bool Execute(IEntityConfig model)
        {
            StateMessage = "正在生成" + model.Caption + "...";
            using (CodeGeneratorScope.CreateScope(model,false))
            {
                _builder.CreateModelCode(model.Project, model);
            }
            StateMessage = model.Caption + "已完成";
            return true;
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public bool Execute(ProjectConfig project)
        {
            StateMessage = "正在生成" + project.Caption + "...";
            using (CodeGeneratorScope.CreateScope(project, false))
            {
                _builder.CreateProjectCode(project);
            }
            StateMessage = project.Caption + "已完成";
            return true;
        }

        public Action<Dictionary<string, string>> OnCodeSuccess;
        /// <summary>
        /// 最后的处理（成功）
        /// </summary>
        public void OnSuccees()
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
        public bool BeginDo(ModelArgument argument)
        {
            codeScope = FileCodeScope.CreateScope(noWriteFile);
            return true;
        }

        /// <summary>
        /// 处理后
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public void EndDo(ModelArgument argument)
        {
            _codes = WorkContext.FileCodes;
            codeScope.Dispose();
        }

        #region 基类


        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
        public override CommandItemBase ToCommand(object arg, Func<object, IEnumerator> enumerator = null)
        {
            var item = new AsyncCommandItem<object, bool>(DoPrepare, Doing, End)
            {
                Source = arg,
                IconName = IconName
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
        /// 命令使用的
        /// </summary>
        /// <param name="args"></param>
        /// <param name="setArgs"></param>
        /// <returns></returns>
        private bool DoPrepare(object args, Action<object> setArgs)
        {
            GlobalTrigger.Regularize(GlobalConfig.CurrentSolution);
            var argument = new ModelArgument
            {
                Argument = args ?? GlobalConfig.CurrentConfig
            };
            if (!CanDo(argument))
                return false;

            _builder = _creater();
            _builder.MessageSetter = MessageSetter;

            bool success = true;
            foreach (var project in argument.Projects)
            {
                if (Validate(project))
                    continue;
                success = false;
                MessageBox.Show("有错误配置,请检查");
            }
            foreach (var model in argument.Models)
            {
                if (Validate(model))
                    continue;
                success = false;
                MessageBox.Show("有错误配置,请检查");
            }
            if (success)
                setArgs(argument);
            return success;
        }

        private bool Doing(object args)
        {
            var argument = (ModelArgument)args;
            if (!BeginDo(argument))
                return false;
            try
            {
                foreach (var model in argument.Models)
                {
                    Execute(model);
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
    }

    #endregion
}
