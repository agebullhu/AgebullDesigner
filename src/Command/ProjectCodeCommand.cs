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
    /// 项目代码命令对象的抽象类
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
        /// 能否执行的检查
        /// </summary>
        public bool CanDo(RuntimeArgument argument)
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
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Validate(EntityConfig entity)
        {
            return _builder.Validate(entity);
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public bool ExecuteEntity(EntityConfig entity)
        {
            return Execute(entity);
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
        public bool Execute(EntityConfig entity)
        {
            StateMessage = "正在生成" + entity.Caption + "...";
            using (CodeGeneratorScope.CreateScope(entity))
            {
                _builder.CreateEntityCode(entity.Parent, entity);
            }
            StateMessage = entity.Caption + "已完成";
            return true;
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public bool Execute(ProjectConfig project)
        {
            StateMessage = "正在生成" + project.Caption + "...";
            using (CodeGeneratorScope.CreateScope(project))
            {
                _builder.CreateProjectCode(project);
            }
            StateMessage = project.Caption + "已完成";
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
        public bool BeginDo(RuntimeArgument argument)
        {
            codeScope = FileCodeScope.CreateScope(noWriteFile);
            return true;
        }

        /// <summary>
        /// 处理后
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public void EndDo(RuntimeArgument argument)
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
        /// 命令使用的
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
                MessageBox.Show("有错误配置,请检查");
            }
            foreach (var entity in argument.Entities)
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
