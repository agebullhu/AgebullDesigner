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
    /// 实体命令基类
    /// </summary>
    public abstract class EntityCommandBase : ConfigCommandBase<EntityConfig>
    {
        /// <summary>
        /// 转为命令对象
        /// </summary>
        /// <returns>命令对象</returns>
        public override CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator = null)
        {
            return new CommandItem
            {
                Command = new AsyncCommand<object, bool>(Prepare, Doing, End),
                Parameter = arg,
                Name = Caption,
                NoButton = NoButton,
                IconName = IconName,
                SourceType = SourceType,
                Catalog = Catalog,
                Caption = Caption,
                Description = Description,
                Image = Application.Current.Resources[IconName ?? "imgDefault"] as ImageSource
            };
        }

        public class RuntimeArgument
        {
            /// <summary>
            /// 参数
            /// </summary>
            public object Argument
            {
                get { return _argument; }
                set
                {
                    _argument = value;
                    GetEntities();
                }
            }
            
            private object _argument;

            /// <summary>
            /// 当前实体对象
            /// </summary>
            public IList<EntityConfig> Entities { get; private set; }

            /// <summary>
            /// 当前关联项目
            /// </summary>
            public List<ProjectConfig> Projects { get; private set; }

            /// <summary>
            /// 默认的取当前实体的方法 
            /// </summary>
            /// <returns></returns>
            void GetEntities()
            {
                var list = new List<EntityConfig>();
                var entityConfig = _argument as EntityConfig;
                if (entityConfig != null)
                {
                    list.Add(entityConfig);
                }
                else
                {
                    var propertyConfig = _argument as PropertyConfig;
                    if (propertyConfig != null)
                    {
                        list.Add(propertyConfig.Parent);
                    }
                    else
                    {
                        var projectConfig = _argument as ProjectConfig;
                        list.AddRange(projectConfig != null ? projectConfig.Entities : SolutionConfig.Current.Entities);
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
        public abstract bool Execute(EntityConfig entity);

        /// <summary>
        /// 执行器
        /// </summary>
        public abstract bool Execute(ProjectConfig project);

        /// <summary>
        /// 能否执行的检查
        /// </summary>
        public virtual void Prepare(RuntimeArgument argument)
        {

        }

        /// <summary>
        /// 能否执行的检查
        /// </summary>
        public virtual bool CanDo(RuntimeArgument argument)
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
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Validate(EntityConfig entity)
        {
            return true;
        }
        /// <summary>
        /// 命令使用的
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
                MessageBox.Show("有错误配置,请检查");
            }
            if (success)
                setArgs(argument);
            return success;
        }

        private bool Doing(object args)
        {
            var argument = (RuntimeArgument)args;
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

        protected void End(CommandStatus status, Exception ex, bool result)
        {
            if (status == CommandStatus.Succeed)
            {
                MessageBox.Show(Name + "执行成功！");
                OnSuccees();
            }
            else
            {
                MessageBox.Show(Name + "出错" + ex?.Message);
                Trace.WriteLine(ex?.ToString(), Name);
            }
        }
    }
}